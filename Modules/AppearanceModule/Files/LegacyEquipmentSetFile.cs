﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix.AppearanceModule.Files
{
	using System;
	using System.Globalization;
	using ConceptMatrix.Services;

	[Serializable]
	public class LegacyEquipmentSetFile : FileBase
	{
		public static readonly FileType FileType = new FileType("json", "CM2 Equipment Set", typeof(LegacyEquipmentSetFile));

		public Item MainHand { get; set; }
		public Item OffHand { get; set; }

		public string EquipmentBytes { get; set; }

		public override FileType GetFileType()
		{
			return FileType;
		}

		/// <summary>
		/// Upgrades a CM2 equipment set to a CM3 equipment set file.
		/// </summary>
		public EquipmentSetFile Upgrade()
		{
			EquipmentSetFile file = new EquipmentSetFile();
			file.MainHand = this.MainHand.Upgrade();
			file.OffHand = this.OffHand.Upgrade();

			string[] parts = this.EquipmentBytes.Split(' ');
			byte[] data = new byte[parts.Length];
			for (int i = 0; i < parts.Length; i++)
			{
				data[i] = byte.Parse(parts[i], NumberStyles.HexNumber);
			}

			// From CM2: CharacterDetailsView2.xaml.cs line 801
			file.Head.ModelBase = (ushort)(data[0] + (data[1] * 256));
			file.Head.ModelVariant = data[2];
			file.Head.DyeId = data[3];
			file.Body.ModelBase = (ushort)(data[4] + (data[5] * 256));
			file.Body.ModelVariant = data[6];
			file.Body.DyeId = data[7];
			file.Hands.ModelBase = (ushort)(data[8] + (data[9] * 256));
			file.Hands.ModelVariant = data[10];
			file.Hands.DyeId = data[11];
			file.Legs.ModelBase = (ushort)(data[12] + (data[13] * 256));
			file.Legs.ModelVariant = data[14];
			file.Legs.DyeId = data[15];
			file.Feet.ModelBase = (ushort)(data[16] + (data[17] * 256));
			file.Feet.ModelVariant = data[18];
			file.Feet.DyeId = data[19];
			file.Ears.ModelBase = (ushort)(data[20] + (data[21] * 256));
			file.Ears.ModelVariant = data[22];
			file.Neck.ModelBase = (ushort)(data[24] + (data[25] * 256));
			file.Neck.ModelVariant = data[26];
			file.Wrists.ModelBase = (ushort)(data[28] + (data[29] * 256));
			file.Wrists.ModelVariant = data[30];
			file.RightRing.ModelBase = (ushort)(data[32] + (data[33] * 256));
			file.RightRing.ModelVariant = data[34];
			file.LeftRing.ModelBase = (ushort)(data[36] + (data[37] * 256));
			file.LeftRing.ModelVariant = data[38];

			return file;
		}

		[Serializable]
		public class Item
		{
			// Set.
			public ushort Item1 { get; set; }

			// Base.
			public ushort Item2 { get; set; }

			// Variant.
			public ushort Item3 { get; set; }

			// Dye.
			public ushort Item4 { get; set; }

			public EquipmentSetFile.Weapon Upgrade()
			{
				EquipmentSetFile.Weapon item = new EquipmentSetFile.Weapon();
				item.ModelSet = this.Item1;
				item.ModelBase = this.Item2;
				item.ModelVariant = this.Item3;
				item.DyeId = (byte)this.Item4;
				item.Scale = Vector.One;
				item.Color = Color.White;
				return item;
			}
		}
	}
}
