﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix.Injection.Memory
{
	using System;

	public class ActorTypesMemory : MemoryBase<ActorTypes>
	{
		public ActorTypesMemory(ProcessInjection process, UIntPtr address)
			: base(process, address, 1)
		{
		}

		protected override ActorTypes Read(ref byte[] data)
		{
			return (ActorTypes)data[0];
		}

		protected override void Write(ActorTypes value, ref byte[] data)
		{
			data[0] = (byte)value;
		}
	}
}
