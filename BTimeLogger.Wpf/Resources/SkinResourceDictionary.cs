using BTimeLogger.Wpf.Model;
using System;
using System.Windows;

namespace BTimeLogger.Wpf.Resources
{
	/// <summary>
	/// https://michaelscodingspot.com/wpf-complete-guide-themes-skins/
	/// </summary>
	public class SkinResourceDictionary : ResourceDictionary
	{
		private Uri _lightSource;
		public Uri LightSource
		{
			get { return _lightSource; }
			set { _lightSource = value; UpdateSource(); }
		}

		private Uri _darkSource;
		public Uri DarkSource
		{
			get { return _darkSource; }
			set { _darkSource = value; UpdateSource(); }
		}

		public void UpdateSource()
		{
			Skin currentSkin = (App.Current as App).SkinManager.AppSkin;
			Uri skinSource = currentSkin == Skin.Dark
				? DarkSource
				: LightSource;
			if (skinSource != null && Source != skinSource)
				Source = skinSource; // Update the ResourceDictionaries source, therefore updating the theme that is applied.
		}
	}
}
