using PWFrameWork;
using System;
using System.Threading;

namespace PW_PacketListener
{
	internal class cHookModule
	{
		private readonly byte[] _ListenFunction = new byte[] { 139, 68, 36, 4, 163, 0, 0, 0, 0, 139, 68, 36, 8, 163, 0, 0, 0, 0, 199, 5, 0, 0, 0, 0, 1, 0, 0, 0, 161, 0, 0, 0, 0, 131, 248, 1, 116, 246 };

		private byte[] OriginalBytes;

		private IntPtr processHandle;

		private int offset_MyFunc;

		private int offset_data_addr;

		private int offset_data_size;

		private int offset_flag;

		public cHookModule()
		{
		}

		private void RestoreOriginalFunction()
		{
			byte[] numArray = new byte[] { 144, 144 };
			MemoryManager.WriteBytes(this.offset_MyFunc + 36, numArray);
			MemoryManager.WriteBytes(cOptions.PacketSendFunction, this.OriginalBytes);
			Thread.Sleep(500);
			InjectHelper.FreeMemory(this.processHandle, this.offset_MyFunc, (int)this._ListenFunction.Length);
			InjectHelper.FreeMemory(this.processHandle, this.offset_data_addr, 4);
			InjectHelper.FreeMemory(this.processHandle, this.offset_data_size, 4);
			InjectHelper.FreeMemory(this.processHandle, this.offset_flag, 4);
		}

		public void StartHook()
		{
			this.processHandle = MemoryManager.OpenProcessHandle;
			this.OriginalBytes = new byte[cOptions.NumBytesToCopy];
			for (int i = 0; i < cOptions.NumBytesToCopy; i++)
			{
				this.OriginalBytes[i] = MemoryManager.ReadByte(cOptions.PacketSendFunction + i);
			}
			byte[] numArray = (byte[])this._ListenFunction.Clone();
			this.offset_MyFunc = InjectHelper.AllocateMemory(this.processHandle, (int)numArray.Length + cOptions.NumBytesToCopy + 5 + 2);
			this.offset_data_addr = InjectHelper.AllocateMemory(this.processHandle, 4);
			this.offset_data_size = InjectHelper.AllocateMemory(this.processHandle, 4);
			this.offset_flag = InjectHelper.AllocateMemory(this.processHandle, 4);
			MemoryManager.WriteBytes(this.offset_data_addr, new byte[4]);
			MemoryManager.WriteBytes(this.offset_data_size, new byte[4]);
			MemoryManager.WriteBytes(this.offset_flag, new byte[4]);
			byte[] bytes = BitConverter.GetBytes(this.offset_data_addr);
			byte[] bytes1 = BitConverter.GetBytes(this.offset_data_size);
			byte[] numArray1 = BitConverter.GetBytes(this.offset_flag);
			numArray[5] = bytes[0];
			numArray[6] = bytes[1];
			numArray[7] = bytes[2];
			numArray[8] = bytes[3];
			numArray[14] = bytes1[0];
			numArray[15] = bytes1[1];
			numArray[16] = bytes1[2];
			numArray[17] = bytes1[3];
			numArray[20] = numArray1[0];
			numArray[21] = numArray1[1];
			numArray[22] = numArray1[2];
			numArray[23] = numArray1[3];
			numArray[29] = numArray1[0];
			numArray[30] = numArray1[1];
			numArray[31] = numArray1[2];
			numArray[32] = numArray1[3];
			MemoryManager.WriteBytes(this.offset_MyFunc, numArray);
			MemoryManager.WriteBytes(this.offset_MyFunc + (int)numArray.Length, this.OriginalBytes);
			byte[] bytes2 = BitConverter.GetBytes(cOptions.PacketSendFunction + cOptions.NumBytesToCopy);
			byte[] numArray2 = new byte[] { 184, bytes2[0], bytes2[1], bytes2[2], bytes2[3], 255, 224 };
			MemoryManager.WriteBytes(this.offset_MyFunc + (int)numArray.Length + (int)this.OriginalBytes.Length, numArray2);
			byte[] bytes3 = BitConverter.GetBytes(this.offset_MyFunc);
			byte[] numArray3 = new byte[cOptions.NumBytesToCopy];
			numArray3[0] = 184;
			numArray3[1] = bytes3[0];
			numArray3[2] = bytes3[1];
			numArray3[3] = bytes3[2];
			numArray3[4] = bytes3[3];
			numArray3[5] = 255;
			numArray3[6] = 224;
			for (int j = 7; j < cOptions.NumBytesToCopy; j++)
			{
				numArray3[j] = 144;
			}
			MemoryManager.WriteBytes(cOptions.PacketSendFunction, numArray3);
		}

		public void StopHook()
		{
			this.RestoreOriginalFunction();
		}

		public byte[] TimerTick()
		{
			int num;
			if (MemoryManager.ReadInt32(this.offset_flag) != 1)
			{
				return null;
			}
			int num1 = MemoryManager.ReadInt32(this.offset_data_size);
			int num2 = MemoryManager.ReadInt32(this.offset_data_addr);
			byte[] numArray = new byte[num1];
			WinApi.ReadProcessMemory(this.processHandle, num2, numArray, (int)numArray.Length, out num);
			MemoryManager.WriteInt32(this.offset_flag, 0);
			return numArray;
		}
	}
}