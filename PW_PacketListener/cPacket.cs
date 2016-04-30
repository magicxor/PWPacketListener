using System;
using System.Collections.Generic;
using System.Globalization;

namespace PW_PacketListener
{
	internal class cPacket
	{
		private byte[] aBytes;

		private string sPacket;

		public cKnownPacket KnownPacket;

		public cPacket(string str)
		{
			this.ConvertFromString(str);
		}

		public cPacket(byte[] bytes)
		{
			this.aBytes = bytes.Clone() as byte[];
			this.sPacket = this.BytesToString(this.aBytes);
		}

		public cPacket()
		{
			this.sPacket = "Пакет пустой.";
		}

		private string BytesToString(byte[] a)
		{
			string str = "";
			for (int i = 0; i < (int)a.Length; i++)
			{
				str = string.Concat(str, a[i].ToString("X2"), " ");
			}
			return str;
		}

		public void ConvertFromString(string str)
		{
			string str1 = "";
			str = str.ToLower();
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] >= '0' && str[i] <= '9' || str[i] >= 'a' && str[i] <= 'f')
				{
					str1 = string.Concat(str1, str[i]);
				}
			}
			this.sPacket = str1;
			if (this.sPacket.Length % 2 != 0)
			{
				this.sPacket = string.Concat("0", this.sPacket);
			}
			this.aBytes = cPacket.ParseStringToBytesArray(this.sPacket);
		}

		private byte[] GetBytesFromArea(int index)
		{
			cPacketArea item = this.KnownPacket.Areas[index];
			byte[] numArray = new byte[item.iEnd - item.iStart + 1];
			int num = 0;
			for (int i = item.iStart; i <= item.iEnd; i++)
			{
				int num1 = num;
				num = num1 + 1;
				numArray[num1] = this.aBytes[i];
			}
			return numArray;
		}

		public byte[] GetPacket()
		{
			return this.aBytes;
		}

		public string[] GetParams()
		{
			if (this.KnownPacket == null)
			{
				return null;
			}
			string[] strArrays = new string[this.KnownPacket.Areas.Count];
			for (int i = 0; i < this.KnownPacket.Areas.Count; i++)
			{
				strArrays[i] = string.Concat(this.KnownPacket.Areas[i].sText, " : ", this.BytesToString(this.GetBytesFromArea(i)));
			}
			return strArrays;
		}

		public bool isEmpty()
		{
			return this.aBytes == null;
		}

		public static byte[] ParseStringToBytesArray(string str)
		{
			int length = str.Length / 2;
			byte[] numArray = new byte[length];
			for (int i = 0; i < length; i++)
			{
				numArray[i] = byte.Parse(str.Substring(i * 2, 2), NumberStyles.HexNumber);
			}
			return numArray;
		}

		public override string ToString()
		{
			return this.sPacket;
		}
	}
}