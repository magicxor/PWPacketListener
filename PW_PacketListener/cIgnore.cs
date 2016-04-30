using System;
using System.Collections.Generic;
using System.IO;

namespace PW_PacketListener
{
	internal static class cIgnore
	{
		private readonly static List<List<byte>> IgnorePackets;

		private readonly static bool bInit;

		static cIgnore()
		{
			cIgnore.IgnorePackets = new List<List<byte>>();
			if (!File.Exists("ignore.txt"))
			{
				return;
			}
			StreamReader streamReader = File.OpenText("ignore.txt");
			while (true)
			{
				string lower = streamReader.ReadLine();
				if (lower == null)
				{
					break;
				}
				lower = lower.Trim();
				if (lower != "")
				{
					string str = "";
					lower = lower.ToLower();
					for (int i = 0; i < lower.Length; i++)
					{
						if (lower[i] >= '0' && lower[i] <= '9' || lower[i] >= 'a' && lower[i] <= 'f')
						{
							str = string.Concat(str, lower[i]);
						}
					}
					if (str.Length % 2 != 0)
					{
						str = string.Concat("0", lower);
					}
					byte[] bytesArray = cPacket.ParseStringToBytesArray(str);
					if ((int)bytesArray.Length > 0)
					{
						cIgnore.IgnorePackets.Add(new List<byte>(bytesArray));
					}
				}
			}
			streamReader.Close();
			if (cIgnore.IgnorePackets.Count > 0)
			{
				cIgnore.bInit = true;
			}
		}

		public static bool IsPacketIgnored(byte[] p)
		{
			bool flag;
			if (!cIgnore.bInit)
			{
				return false;
			}
			if (p == null)
			{
				return false;
			}
			List<List<byte>>.Enumerator enumerator = cIgnore.IgnorePackets.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					List<byte> current = enumerator.Current;
					bool flag1 = true;
					for (int i = 0; i < current.Count; i++)
					{
						flag1 = (!flag1 ? false : p[i] == current[i]);
					}
					if (!flag1)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}
	}
}