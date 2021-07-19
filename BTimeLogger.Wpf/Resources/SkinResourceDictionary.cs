using System;
using System.Windows;

namespace ATimeLogger.WPF.Resources
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
			base.Source = DarkSource ?? LightSource; // TODO: add theme changer 
		}
	}
}
