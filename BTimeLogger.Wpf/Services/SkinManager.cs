using BTimeLogger.Wpf.Model;
using BTimeLogger.Wpf.Resources;
using System.Windows;

namespace BTimeLogger.Wpf.Services
{
	public interface ISkinManager
	{
		Skin AppSkin { get; set; }
		void ToggleSkin();
	}

	class SkinManager : ISkinManager
	{
		private Skin _appSkin = Skin.Dark;
		public Skin AppSkin
		{
			get => _appSkin;
			set
			{
				_appSkin = value;
				UpdateSkinResourceDictionaries();
			}
		}

		public void ToggleSkin()
		{
			AppSkin = AppSkin == Skin.Dark
				? Skin.Light
				: Skin.Dark;
		}

		private void UpdateSkinResourceDictionaries()
		{
			foreach (ResourceDictionary dictionary in App.Current.Resources.MergedDictionaries)
			{
				if (dictionary is SkinResourceDictionary skinDict)
				{
					skinDict.UpdateSource();
				}
				else
				{
					// I think this refreshes dictionaries, not too sure
					dictionary.Source = dictionary.Source;
				}
			}
		}
	}
}
