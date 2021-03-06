﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix.AppearanceModule.Pages
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;
	using ConceptMatrix.AppearanceModule.Utilities;
	using ConceptMatrix.AppearanceModule.Views;
	using ConceptMatrix.Services;
	using PropertyChanged;

	/// <summary>
	/// Interaction logic for AppearancePage.xaml.
	/// </summary>
	[AddINotifyPropertyChangedInterface]
	public partial class AppearancePage : UserControl
	{
		private IRace race;

		public AppearancePage()
		{
			this.InitializeComponent();
			this.ContentArea.DataContext = this;

			IGameDataService gameDataService = Module.Services.Get<IGameDataService>();

			this.GenderComboBox.ItemsSource = new[] { Appearance.Genders.Masculine, Appearance.Genders.Feminine };
			this.RaceComboBox.ItemsSource = gameDataService.Races.All;
			this.TribeComboBox.ItemsSource = gameDataService.Tribes.All;

			ColorData.GetSkin(Appearance.Tribes.Highlander, Appearance.Genders.Feminine);

			this.Gender = Appearance.Genders.Feminine;
		}

		public bool HasFur
		{
			get
			{
				return this.Race.Race == Appearance.Races.Hrothgar;
			}
		}

		public bool HasLimbal
		{
			get
			{
				return this.Race.Race == Appearance.Races.AuRa;
			}
		}

		public Appearance.Genders Gender
		{
			get;
			set;
		}

		public IRace Race
		{
			get
			{
				return this.race;
			}

			set
			{
				this.race = value;
				this.TribeComboBox.ItemsSource = this.race.Tribes;
				this.Tribe = this.race.Tribes.First();
			}
		}

		public ITribe Tribe
		{
			get;
			set;
		}

		public byte Skintone { get; set; }
		public byte EyeColorL { get; set; }
		public byte EyeColorR { get; set; }
		public byte FacePaintColor { get; set; }
		public byte LimbalTattooColor { get; set; }
		public byte HairColor { get; set; }
		public byte HairHighlights { get; set; }
		public byte LipsColor { get; set; }
	}
}
