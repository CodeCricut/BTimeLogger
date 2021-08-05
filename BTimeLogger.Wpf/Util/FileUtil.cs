using System.IO;

namespace BTimeLogger.Wpf.Util
{
	public static class FileUtil
	{
		public static void AppendLine(string fileLocation, string line)
		{
			File.AppendAllLines(fileLocation, new string[] { line });
		}
	}
}
