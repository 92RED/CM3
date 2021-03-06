﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix.Injection.Memory
{
	using System;
	using System.ComponentModel;
	using System.Runtime.InteropServices;
	using ConceptMatrix;
	using ConceptMatrix.Exceptions;

	public abstract class MemoryBase<T> : MemoryBase, IMemory<T>
	{
		private T value;
		private bool freeze;

		private ulong length;
		private byte[] oldData;
		private byte[] newData;

		private Exception lastException;

		public MemoryBase(ProcessInjection process, UIntPtr address, ulong length)
			: base(process, address)
		{
			this.length = length;
			this.oldData = new byte[this.length];
			this.newData = new byte[this.length];

			this.value = this.Read(ref this.oldData);

			// Tick once to ensure current value is valid
			this.Tick();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public event ValueChangedEventHandler ValueChanged;

		public T Value
		{
			get
			{
				if (this.lastException != null)
					throw this.lastException;

				return this.value;
			}

			set
			{
				this.value = value;
				if (this.DoWrite(value))
				{
					this.ValueChanged?.Invoke(this, value);
				}

				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
			}
		}

		public bool Freeze
		{
			get
			{
				return this.freeze;
			}

			set
			{
				this.freeze = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
			}
		}

		protected static bool Equals(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
		{
			return a1.SequenceEqual(a2);
		}

		protected abstract T Read(ref byte[] data);
		protected abstract void Write(T value, ref byte[] data);

		protected override void Tick()
		{
			try
			{
				this.lastException = null;

				if (this.Freeze)
				{
					// Frozen values get written constantly
					this.DoWrite(this.value);
				}
				else
				{
					// unfrozen values get read constantly
					this.DoRead();

					if (!Equals(this.newData, this.oldData))
					{
						this.value = this.Read(ref this.newData);
						Array.Copy(this.newData, this.oldData, (int)this.length);

						this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
						this.ValueChanged?.Invoke(this, this.value);
					}
				}
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				this.Dispose();
				Log.Write("Disposing of memory due to exception: " + ex.Message);
			}
		}

		// writes value to oldData and to memory
		private bool DoWrite(T val)
		{
			this.Write(val, ref this.newData);

			if (!Equals(this.newData, this.oldData))
			{
				Array.Copy(this.newData, this.oldData, (int)this.length);
				InjectionService.WriteProcessMemory(this.process.Handle, this.address, this.oldData, (UIntPtr)this.length, out IntPtr bytesRead);
				return true;
			}

			return false;
		}

		// Reads memory into newData array
		private void DoRead()
		{
			if (!InjectionService.ReadProcessMemory(this.process.Handle, this.address, this.newData, (UIntPtr)this.length, IntPtr.Zero))
			{
				int code = Marshal.GetLastWin32Error();

				// code 0 means success. ooh boy Win32 legacy API's.
				if (code == 0)
					return;

				throw new MemoryException("Failed to read process memory", new Win32Exception(code));
			}
		}
	}
}
