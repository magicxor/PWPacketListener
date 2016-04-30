using System;

namespace PW_PacketListener
{
	internal class cPacketArea
	{
		public int iStart;

		public int iEnd;

		public string sText;

		public int iType;

		public cPacketArea()
		{
		}

		public override string ToString()
		{
			return this.sText;
		}
	}
}