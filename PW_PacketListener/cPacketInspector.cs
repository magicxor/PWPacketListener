using System;
using System.Collections.Generic;
using System.IO;

namespace PW_PacketListener
{
	internal class cPacketInspector
	{
		private List<cKnownPacket> KnownPackets = new List<cKnownPacket>();

		public cPacketInspector()
		{
			StreamReader streamReader = File.OpenText("packets.txt");
			List<string> strs = new List<string>();
			do
			{
				string str = streamReader.ReadLine();
				if (str != "")
				{
					strs.Add(str);
				}
				else
				{
					if (strs.Count <= 0)
					{
						continue;
					}
					this.KnownPackets.Add(new cKnownPacket(strs.ToArray()));
					strs.Clear();
				}
			}
			while (!streamReader.EndOfStream);
			if (strs.Count > 0)
			{
				this.KnownPackets.Add(new cKnownPacket(strs.ToArray()));
			}
			streamReader.Close();
		}

		public cKnownPacket[] GetPackets()
		{
			return this.KnownPackets.ToArray();
		}

		public cPacket ParseByteArray(byte[] bytes)
		{
			return new cPacket(bytes);
		}
	}
}