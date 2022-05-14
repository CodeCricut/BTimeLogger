using System;
using System.Windows.Media;

namespace BTimeLogger.Wpf.Util;

public static class ColorUtil
{
	public static SolidColorBrush ToSolidColorBrush(this Color color)
	{
		return new SolidColorBrush(color);
	}

	public static Color ToColor(this SolidColorBrush brush)
	{
		return brush.Color;
	}

	private static readonly Random _random = new();


	public static Color RandColor()
	{
		return Color.FromRgb(RandColorByte(), RandColorByte(), RandColorByte());
	}

	public static Color GetCloseColor(Color color, byte maxDiff = 50)
	{
		byte r = GetCloseByte(color.R, maxDiff);
		byte g = GetCloseByte(color.G, maxDiff);
		byte b = GetCloseByte(color.B, maxDiff);

		return Color.FromRgb(r, g, b);
	}

	private static byte GetCloseByte(byte b, byte maxDiff)
	{
		int newB = b + _random.Next(0, maxDiff);
		return (byte)Math.Clamp(newB, 0, byte.MaxValue);
	}

	private static byte RandColorByte()
	{
		return (byte)_random.Next(byte.MinValue, byte.MaxValue);
	}
}
