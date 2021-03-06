﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix.AppearanceModule.Views
{
	using System;
	using System.Windows.Controls;
	using System.Windows.Media;
	using ConceptMatrix.AppearanceModule.Utilities;
	using ConceptMatrix.Services;

	/// <summary>
	/// Interaction logic for FxivColorSelectorDrawer.xaml.
	/// </summary>
	public partial class FxivColorSelectorDrawer : UserControl, IDrawer
	{
		private bool locked = false;

		public FxivColorSelectorDrawer(ColorData.Entry[] colors, int selectedIndex)
		{
			this.InitializeComponent();

			this.locked = true;
			this.List.ItemsSource = colors;
			this.List.SelectedIndex = selectedIndex;
			this.locked = false;
		}

		public event DrawerEvent Close;

		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.locked)
				return;

			this.Close?.Invoke();
		}
	}
}
