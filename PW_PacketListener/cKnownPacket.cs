using System;
using System.Collections.Generic;

namespace PW_PacketListener
{
	internal class cKnownPacket
	{
		private string[] sArray;

		public string sTitle;

		public byte[] aBytes;

		public string sDescription;

		public List<cPacketArea> Areas = new List<cPacketArea>();

		public cKnownPacket(string[] item)
		{
			this.sArray = item;
			this.sTitle = this.sArray[0];
			this.ParseFromItem();
			this.MakeDescription();
		}

		public bool CheckBytes(byte[] bytes)
		{
			if ((int)bytes.Length != (int)this.aBytes.Length)
			{
				return false;
			}
			for (int i = 0; i < (int)bytes.Length; i++)
			{
				if (this.IsByteInConstArea(i) && bytes[i] != this.aBytes[i])
				{
					return false;
				}
			}
			return true;
		}

		public string GetDescription()
		{
			return this.sDescription;
		}

		private bool IsByteInArea(int ByteIndex)
		{
			for (int i = 0; i < this.Areas.Count; i++)
			{
				if (ByteIndex >= this.Areas[i].iStart && ByteIndex <= this.Areas[i].iEnd)
				{
					return true;
				}
			}
			return false;
		}

		private bool IsByteInConstArea(int ByteIndex)
		{
			return !this.IsByteInArea(ByteIndex);
		}

		private void MakeDescription()
		{
			List<string> strs = new List<string>();
			for (int i = 0; i < (int)this.aBytes.Length; i++)
			{
				strs.Add(this.aBytes[i].ToString("X2"));
			}
			string str = "";
			for (int j = 0; j < this.Areas.Count; j++)
			{
				char chr = '{';
				char chr1 = '}';
				object obj = str;
				object[] item = new object[] { obj, "<br><span style=\"background-color:green\">", chr, "&nbsp;", chr1, "</span>: ", this.Areas[j].sText };
				str = string.Concat(item);
				strs[this.Areas[j].iStart] = string.Concat("<span style=\"background-color:green\">", strs[this.Areas[j].iStart]);
				List<string> strs1 = strs;
				List<string> strs2 = strs1;
				int num = this.Areas[j].iEnd;
				int num1 = num;
				strs1[num] = string.Concat(strs2[num1], "</span>");
			}
			string str1 = "<b>Общий вид пакета</b>:<br><pre>";
			for (int k = 0; k < strs.Count; k++)
			{
				str1 = string.Concat(str1, strs[k], " ");
			}
			str1 = string.Concat(str1, "</pre>");
			str1 = string.Concat(str1, str);
			this.sDescription = str1;
		}

		private void ParseFromItem()
		{
			string str = "";
			string lower = this.sArray[1].ToLower();
			int length = lower.Length;
			cPacketArea _cPacketArea = new cPacketArea();
			for (int i = 0; i < length; i++)
			{
				char chr = lower[i];
				if (chr >= '0' && chr <= '9' || chr >= 'a' && chr <= 'f')
				{
					str = string.Concat(str, chr.ToString());
				}
				if (chr == '{')
				{
					_cPacketArea.iType = 2;
					_cPacketArea.iStart = str.Length / 2;
				}
				if (chr == '}')
				{
					_cPacketArea.iEnd = str.Length / 2 - 1;
					this.Areas.Add(_cPacketArea);
					_cPacketArea = new cPacketArea();
				}
			}
			this.aBytes = cPacket.ParseStringToBytesArray(str);
			for (int j = 2; j < (int)this.sArray.Length; j++)
			{
				string str1 = this.sArray[j];
				char chr1 = '}';
				if (str1[0] == '<')
				{
					chr1 = '>';
				}
				if (str1[0] == '[')
				{
					chr1 = ']';
				}
				int num = str1.IndexOf(chr1);
				int num1 = int.Parse(str1.Substring(1, num - 1)) - 1;
				this.Areas[num1].sText = str1.Substring(str1.IndexOf('-') + 1).Trim();
			}
		}

		public override string ToString()
		{
			return this.sTitle;
		}
	}
}