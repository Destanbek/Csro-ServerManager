using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SilkroadServerManager
{
	internal static class Program
	{
		[DllImport("User32")]
		public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

		[STAThread]
		private static void Main()
		{
			Process[] processesByName = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
			if (processesByName.Length > 1)
			{
				ShowWindow(processesByName.FirstOrDefault().MainWindowHandle, 5);
				ShowWindow(processesByName.LastOrDefault().MainWindowHandle, 5);
				Application.Exit();
			}
			else
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(defaultValue: false);
				Application.Run(new main());
			}
		}
	}
}
