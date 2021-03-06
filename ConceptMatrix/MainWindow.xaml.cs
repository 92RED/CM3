﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix.GUI
{
	using System;
	using System.Reflection;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using ConceptMatrix;
	using ConceptMatrix.GUI.Services;
	using ConceptMatrix.GUI.Views;
	using ConceptMatrix.Services;

	/// <summary>
	/// Interaction logic for MainWindow.xaml.
	/// </summary>
	public partial class MainWindow : Window
	{
		private UserControl currentView;
		private ViewService viewService;

		public MainWindow()
		{
			this.InitializeComponent();

			this.viewService = App.Services.Get<ViewService>();
			this.viewService.ShowingDrawer += this.OnShowDrawer;
			this.viewService.ShowingPage += this.OnShowPage;

			this.AlwaysOnTopToggle.IsChecked = App.Settings.AlwaysOnTop;

			this.OnShowPage("Home", typeof(HomeView));
		}

		private void OnShowPage(string path, Type pageType)
		{
			try
			{
				this.currentView = (UserControl)Activator.CreateInstance(pageType);
				this.ViewArea.Content = this.currentView;
			}
			catch (TargetInvocationException ex)
			{
				Log.Write(new Exception($"Failed to create view: {pageType}", ex.InnerException));
			}
			catch (Exception ex)
			{
				Log.Write(new Exception($"Failed to create view: {pageType}", ex));
			}
		}

		private async Task OnShowDrawer(string title, UserControl view, DrawerDirection direction)
		{
			// Close all current drawers.
			this.DrawerHost.IsLeftDrawerOpen = false;
			this.DrawerHost.IsTopDrawerOpen = false;
			this.DrawerHost.IsRightDrawerOpen = false;
			this.DrawerHost.IsBottomDrawerOpen = false;

			// If this is a drawer view, bind to its events.
			if (view is IDrawer drawer)
			{
				drawer.Close += () => this.DrawerHost.IsLeftDrawerOpen = false;
				drawer.Close += () => this.DrawerHost.IsTopDrawerOpen = false;
				drawer.Close += () => this.DrawerHost.IsRightDrawerOpen = false;
				drawer.Close += () => this.DrawerHost.IsBottomDrawerOpen = false;
			}

			switch (direction)
			{
				case DrawerDirection.Left:
				{
					this.DrawerLeft.Content = view;
					this.DrawerHost.IsLeftDrawerOpen = true;
					this.LeftTitle.Content = title;
					break;
				}

				case DrawerDirection.Top:
				{
					this.DrawerTop.Content = view;
					this.DrawerHost.IsTopDrawerOpen = true;
					break;
				}

				case DrawerDirection.Right:
				{
					this.DrawerRight.Content = view;
					this.DrawerHost.IsRightDrawerOpen = true;
					this.RightTitle.Content = title;
					break;
				}

				case DrawerDirection.Bottom:
				{
					this.DrawerBottom.Content = view;
					this.DrawerHost.IsBottomDrawerOpen = true;
					break;
				}
			}

			// Wait while any of the drawer areas remain open
			while (this.DrawerHost.IsLeftDrawerOpen
				|| this.DrawerHost.IsRightDrawerOpen
				|| this.DrawerHost.IsBottomDrawerOpen
				|| this.DrawerHost.IsTopDrawerOpen)
			{
				await Task.Delay(250);
			}
		}

		private void OnTitleBarMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}

		private void Window_Activated(object sender, EventArgs e)
		{
			this.ActiveBorder.Visibility = Visibility.Visible;
			this.InActiveBorder.Visibility = Visibility.Collapsed;
		}

		private void Window_Deactivated(object sender, EventArgs e)
		{
			this.ActiveBorder.Visibility = Visibility.Collapsed;
			this.InActiveBorder.Visibility = Visibility.Visible;
		}

		private void OnCloseClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void OnMinimiseClick(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void OnThemeClick(object sender, RoutedEventArgs e)
		{
			App.Services.Get<IViewService>().ShowDrawer<ThemeSettingsView>("Theme");
		}

		private void OnAlwaysOnTopChecked(object sender, RoutedEventArgs e)
		{
			this.Topmost = true;
			App.Settings.AlwaysOnTop = true;
			App.Settings.Save();
		}

		private void OnAlwaysOnTopUnchecked(object sender, RoutedEventArgs e)
		{
			this.Topmost = false;
			App.Settings.AlwaysOnTop = false;
			App.Settings.Save();
		}
	}
}
