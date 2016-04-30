using PWFrameWork;
using System;

namespace PW_PacketListener
{
	internal class cPacketInjection
	{
		private readonly byte[] _sendPacketOpcode = new byte[] { 96, 184, 0, 0, 0, 0, 139, 13, 0, 0, 0, 0, 139, 73, 32, 191, 0, 0, 0, 0, 104, 0, 0, 0, 0, 87, 255, 208, 97, 195 };

		private int _packetAddressLocation;

		private int _packetSizeAddress;

		private int _sendPacketOpcodeAddress;

		public cPacketInjection()
		{
		}

		private void LoadSendPacketOpcode(IntPtr processHandle)
		{
			this._sendPacketOpcodeAddress = InjectHelper.AllocateMemory(processHandle, (int)this._sendPacketOpcode.Length);
			MemoryManager.WriteBytes(this._sendPacketOpcodeAddress, this._sendPacketOpcode);
			byte[] bytes = BitConverter.GetBytes(cOptions.PacketSendFunction);
			byte[] numArray = BitConverter.GetBytes(cOptions.BaseAddress);
			MemoryManager.WriteBytes(this._sendPacketOpcodeAddress + 2, bytes);
			MemoryManager.WriteBytes(this._sendPacketOpcodeAddress + 8, numArray);
			this._packetAddressLocation = this._sendPacketOpcodeAddress + 16;
			this._packetSizeAddress = this._sendPacketOpcodeAddress + 21;
		}

		public void SendPacket(byte[] packetData)
		{
			IntPtr openProcessHandle = MemoryManager.OpenProcessHandle;
			int num = InjectHelper.AllocateMemory(openProcessHandle, (int)packetData.Length);
			MemoryManager.WriteBytes(num, packetData);
			byte[] bytes = BitConverter.GetBytes(num);
			if (this._sendPacketOpcodeAddress == 0)
			{
				this.LoadSendPacketOpcode(openProcessHandle);
			}
			MemoryManager.WriteBytes(this._packetAddressLocation, bytes);
			MemoryManager.WriteBytes(this._packetSizeAddress, BitConverter.GetBytes((int)packetData.Length));
			IntPtr intPtr = InjectHelper.CreateRemoteThread(openProcessHandle, this._sendPacketOpcodeAddress);
			WinApi.WaitForSingleObject(intPtr, 100);
			WinApi.CloseHandle(intPtr);
			InjectHelper.FreeMemory(openProcessHandle, num, (int)packetData.Length);
			InjectHelper.FreeMemory(openProcessHandle, this._sendPacketOpcodeAddress, (int)this._sendPacketOpcode.Length);
		}
	}
}