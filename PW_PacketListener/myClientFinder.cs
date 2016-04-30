using PWFrameWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PW_PacketListener
{
	internal class myClientFinder : ClientFinder
	{
		private int BaseAddress
		{
			get;
			set;
		}

		private int GameRun
		{
			get;
			set;
		}

		private int HostPlayerName
		{
			get;
			set;
		}

		private int HostPlayerStruct
		{
			get;
			set;
		}

		public myClientFinder() : base(cOptions.BaseAddress, cOptions.GameRun, cOptions.Name)
		{
			this.BaseAddress = cOptions.BaseAddress;
			this.GameRun = cOptions.GameRun;
			this.HostPlayerStruct = cOptions.HostPlayerStruct;
			this.HostPlayerName = cOptions.Name;
		}

		public new ClientWindow[] GetWindows()
		{
			List<ClientWindow> clientWindows = new List<ClientWindow>();
			Process[] processes = Process.GetProcesses();
			for (int i = 0; i < (int)processes.Length; i++)
			{
				Process process = processes[i];
				try
				{
					if (WinApi.GetWindowClass(process.MainWindowHandle).Equals(cOptions.WindowClassName))
					{
						MemoryManager.OpenProcess(process.Id);
						int gameRun = this.GameRun;
						int[] hostPlayerStruct = new int[] { this.HostPlayerStruct, this.HostPlayerName, 0 };
						string str = MemoryManager.ChainReadString(gameRun, 32, hostPlayerStruct);
						clientWindows.Add(new ClientWindow((string.IsNullOrEmpty(str) ? process.MainWindowTitle : str), process.MainWindowHandle, process.Id));
						MemoryManager.CloseProcess();
					}
				}
				catch (Exception exception)
				{
				}
			}
			return clientWindows.ToArray();
		}
	}
}