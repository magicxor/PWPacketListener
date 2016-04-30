using System;
using System.Globalization;
using System.Windows.Forms;

namespace PW_PacketListener
{
	internal class cOptions
	{
		public static int BaseAddress;

		public static int GameRun;

		public static int PacketSendFunction;

		public static int NumBytesToCopy;

		public static int HostPlayerStruct;

		public static int Name;

		public static string WindowClassName;

		static cOptions()
		{
			cOptions.WindowClassName = "ElementClient Window";
		}

		public cOptions()
		{
		}

		public static void ReadConfigFile()
		{
			string str = string.Concat(Application.StartupPath, "\\config.ini");
			string str1 = "main";
			string nIValue = cINIReader.GetINIValue(str, str1, "BaseAddress");
			string nIValue1 = cINIReader.GetINIValue(str, str1, "GameRun");
			string nIValue2 = cINIReader.GetINIValue(str, str1, "PacketSendFunction");
			string str2 = cINIReader.GetINIValue(str, str1, "HostPlayerStruct");
			string nIValue3 = cINIReader.GetINIValue(str, str1, "Name");
			int nIInt = cINIReader.GetINIInt(str, str1, "MagicNumber");
			string str3 = cINIReader.GetINIValue(str, str1, "WindowClassName");
			if (str3 != null)
			{
				cOptions.WindowClassName = str3;
			}
			try
			{
				cOptions.BaseAddress = int.Parse(nIValue, NumberStyles.AllowHexSpecifier);
				cOptions.GameRun = int.Parse(nIValue1, NumberStyles.AllowHexSpecifier);
				cOptions.PacketSendFunction = int.Parse(nIValue2, NumberStyles.AllowHexSpecifier);
				cOptions.NumBytesToCopy = nIInt;
				cOptions.HostPlayerStruct = int.Parse(str2, NumberStyles.AllowHexSpecifier);
				cOptions.Name = int.Parse(nIValue3, NumberStyles.AllowHexSpecifier);
				if (cOptions.NumBytesToCopy < 7)
				{
					MessageBox.Show("Магическое число не может быть меньше 7", "error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Application.Exit();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show("Произошла ошибка при чтении конфиг файла.", "error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Application.Exit();
			}
		}
	}
}