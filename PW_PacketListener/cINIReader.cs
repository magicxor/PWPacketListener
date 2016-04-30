using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PW_PacketListener
{
	internal class cINIReader
	{
		public cINIReader()
		{
		}

		public static int GetINIInt(string filename, string section, string key)
		{
			return cINIReader.GetPrivateProfileInt(section, key, -1, filename);
		}

		public static string GetINIValue(string filename, string section, string key)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (cINIReader.GetPrivateProfileString(section, key, "", stringBuilder, stringBuilder.Capacity, filename) == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		[DllImport("KERNEL32.DLL", CharSet=CharSet.None, ExactSpelling=false)]
		protected internal static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int iDefault, string lpFileName);

		[DllImport("KERNEL32.DLL", CharSet=CharSet.None, ExactSpelling=false)]
		protected internal static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
	}
}