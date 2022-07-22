using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SilkroadServerManager
{
	public class main : Form
	{
		private int zaman = 1;

		private int allstart = 2;

		private int all_show = 1;

		private IContainer components = null;

		private CheckBox serverclear;

		private Button all_Show;

		private Button all_stop;

		private Button all_start;

		private Button smc_Show;

		private Button game_Show;

		private Button shard_Show;

		private Button agent_Show;

		private Button farm_Show;

		private Button gateway_Show;

		private Button download_Show;

		private Button machine_Show;

		private Button global_Show;

		private Button certification_Show;

		private Button smc_start;

		private Button game_start;

		private Button shard_start;

		private Button agent_start;

		private Button farm_start;

		private Button gateway_start;

		private Button download_start;

		private Button machine_start;

		private Button global_start;

        private Button SroProt_start;

        private Button SroProt_Show;

        private Timer start_timer;

        private Button sertification_start;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem serverConfigToolStripMenuItem;

		private ToolStripMenuItem quickToolStripMenuItem;

		private ToolStripMenuItem advencedToolStripMenuItem;

		private ToolStripMenuItem toolsToolStripMenuItem;

		private ToolStripMenuItem tBUserToolStripMenuItem;

		private ToolStripMenuItem languageToolStripMenuItem;

		private ToolStripMenuItem dBtoMediaToolStripMenuItem;

		private ToolStripMenuItem itemmallToolStripMenuItem;

		private ToolStripMenuItem npcmallToolStripMenuItem;

        private ToolStripMenuItem OtherstoolStripMenuItem;

        private ToolStripMenuItem ayarlarToolStripMenuItem;

        private ToolStripMenuItem hakkındaToolStripMenuItem;

        private Timer refresh;




		public main()
		{
			InitializeComponent();
		}

		private void main_Load(object sender, EventArgs e)
		{
            if (Language.Default.language == "English")
            {
                serverConfigToolStripMenuItem.Text = "Server Config";
                quickToolStripMenuItem.Text = "Quick Configuration";
                advencedToolStripMenuItem.Text = "Manual Configuration";
                toolsToolStripMenuItem.Text = "Vehicles";
                tBUserToolStripMenuItem.Text = "Create User";
                languageToolStripMenuItem.Text = "Language Edit";
                dBtoMediaToolStripMenuItem.Text = "Create DB_Media";
                itemmallToolStripMenuItem.Text = "Item Mall Edit";
                npcmallToolStripMenuItem.Text = "Npc Mall Edit";
                OtherstoolStripMenuItem.Text = "other";
                ayarlarToolStripMenuItem.Text = "Settings";
                hakkındaToolStripMenuItem.Text = "About";
                certification_Show.Text = "Show";
                global_Show.Text = "Show";
                machine_Show.Text = "Show";
                download_Show.Text = "Show";
                game_Show.Text = "Show";
                farm_Show.Text = "Show";
                agent_Show.Text = "Show";
                shard_Show.Text = "Show";
                game_Show.Text = "Show";
                SroProt_Show.Text = "Show";
                smc_Show.Text = "Show";
                all_start.Text = "START";
                all_stop.Text = "STOP";
                all_Show.Text = "SHOW";
                serverclear.Text = "Server Folder Cleanup";
            }
        }

		[DllImport("User32")]
		public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

		[DllImport("User32")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr parentWindow, IntPtr previousChildWindow, string windowClass, string windowTitle);

		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowThreadProcessId(IntPtr window, out int process);

		private IntPtr[] GetProcessWindows(int process)
		{
			IntPtr[] array = new IntPtr[256];
			int num = 0;
			IntPtr ıntPtr = IntPtr.Zero;
			do
			{
				ıntPtr = FindWindowEx(IntPtr.Zero, ıntPtr, null, null);
				GetWindowThreadProcessId(ıntPtr, out int process2);
				if (process2 == process)
				{
					array[num++] = ıntPtr;
				}
			}
			while (ıntPtr != IntPtr.Zero);
			Array.Resize(ref array, num);
			return array;
		}

		private void sertification_start_Click(object sender, EventArgs e)
		{
			start("Cert\\CustomCertificationServer.exe", "CustomCertificationServer", "Cert\\packt.dat", 0);
		}

		private void certification_Show_Click(object sender, EventArgs e)
		{
			show("CustomCertificationServer", 0);
		}

		private void global_start_Click(object sender, EventArgs e)
		{
			start("GlobalManager.exe", "GlobalManager", "", 0);
		}

		private void global_Show_Click(object sender, EventArgs e)
		{
			show("GlobalManager", 0);
		}

		private void machine_start_Click(object sender, EventArgs e)
		{
			start("MachineManager.exe", "MachineManager", "", 0);
		}

		private void machine_Show_Click(object sender, EventArgs e)
		{
			show("MachineManager", 0);
		}

		private void download_start_Click(object sender, EventArgs e)
		{
			start("DownloadServer.exe", "DownloadServer", "", 0);
		}

		private void download_Show_Click(object sender, EventArgs e)
		{
			show("DownloadServer", 0);
		}

		private void gateway_start_Click(object sender, EventArgs e)
		{
			start("GatewayServer.exe", "GatewayServer", "", 0);
		}

		private void gateway_Show_Click(object sender, EventArgs e)
		{
			show("GatewayServer", 0);
		}

		private void farm_start_Click(object sender, EventArgs e)
		{
			start("FarmManager.exe", "FarmManager", "", 0);
		}

		private void farm_Show_Click(object sender, EventArgs e)
		{
			show("FarmManager", 0);
		}

		private void agent_start_Click(object sender, EventArgs e)
		{
			start("AgentServer.exe", "AgentServer", "", 0);
		}

		private void agent_Show_Click(object sender, EventArgs e)
		{
			show("AgentServer", 0);
		}

		private void shard_start_Click(object sender, EventArgs e)
		{
			start("SR_ShardManager.exe", "SR_ShardManager", "", 0);
		}

		private void shard_Show_Click(object sender, EventArgs e)
		{
			show("SR_ShardManager", 0);
		}

		private void game_start_Click(object sender, EventArgs e)
		{
			start("SR_GameServer.exe", "SR_GameServer", "", 0);
		}

		private void game_Show_Click(object sender, EventArgs e)
		{
			show("SR_GameServer", 0);
		}
 
        private void SroProt_start_Click(object sender, EventArgs e)
        {
            start("sroprot\\sroprot.exe", "sroprot", "", 0);
        }
        private void SroProt_Show_Click(object sender, EventArgs e)
        {
            show("sroprot", 0);
        }
        private void smc_start_Click(object sender, EventArgs e)
		{
			start("SMC\\smc.exe", "smc", "", 0);
		}

		private void smc_Show_Click(object sender, EventArgs e)
		{
			show("smc", 0);
		}

		private void all_start_Click(object sender, EventArgs e)
		{
			start_timer.Enabled = false;
			zaman = 1;
			allstart = 2;
			start_timer.Enabled = true;
			start_timer.Interval = 1000;
			all_start.BackColor = Color.LawnGreen;
			all_stop.BackColor = Color.Transparent;
		}

		private void all_stop_Click(object sender, EventArgs e)
		{
			start_timer.Enabled = false;
			zaman = 1;
			allstart = 1;
			start_timer.Interval = 10;
			start_timer.Enabled = true;
			all_start.BackColor = Color.Transparent;
			all_stop.BackColor = Color.LawnGreen;
		}

		private void all_Show_Click(object sender, EventArgs e)
		{
			if (all_show == 1)
			{
				all_Show.BackColor = Color.LawnGreen;
                if (Language.Default.language == "English")
                {
                    all_Show.Text = "HIDE";
                }
                else
                {
                    all_Show.Text = "GİZLE";
                }

				all_show = 2;
			}
			else
			{
				all_Show.BackColor = Color.Transparent;
                if (Language.Default.language == "English")
                {
                    all_Show.Text = "SHOW";
                }
                else
                {
                    all_Show.Text = "GÖSTER";
                }
				all_show = 1;
			}
			show("CustomCertificationServer", all_show);
			show("GlobalManager", all_show);
			show("MachineManager", all_show);
			show("DownloadServer", all_show);
			show("GatewayServer", all_show);
			show("FarmManager", all_show);
			show("AgentServer", all_show);
			show("SR_ShardManager", all_show);
			show("SR_GameServer", all_show);
            show("SroProt", all_show);
            show("smc", all_show);
		}

		private void start_durum(string prname, object start, object show)
		{
			Process process = Process.GetProcessesByName(prname).FirstOrDefault();
			if (process != null)
			{
				(start as Button).BackColor = Color.LawnGreen;
				if (process.MainWindowHandle.ToInt32() == 0)
				{
					(show as Button).BackColor = Color.Transparent;
                    if (Language.Default.language == "English")
                    {
                        (show as Button).Text = "Show";
                    }
                    else
                    {
                        (show as Button).Text = "Göster";
                    }
				}
				else
				{
					(show as Button).BackColor = Color.LawnGreen;
                    if (Language.Default.language == "English")
                    {
                        (show as Button).Text = "Hide";
                    }
                    else
                    {
                        (show as Button).Text = "Gizle";
                    }
				}
			}
			else
			{
				(start as Button).BackColor = Color.Transparent;
				(show as Button).BackColor = Color.Transparent;
			}
		}

		private void start(string path, string prname, string arg, int all)
		{
			Process process = Process.GetProcessesByName(prname).FirstOrDefault();
			if (process != null)
			{
				if (all != 2)
				{
					process.Kill();
				}
				else
				{
					start_timer.Interval = 10;
				}
			}
			else
			{
				if (all == 1)
				{
					return;
				}
				start_timer.Interval = 1000;
				if (File.Exists(path))
				{
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					if (path == "SMC\\smc.exe")
					{
						processStartInfo.UseShellExecute = false;
						processStartInfo.WorkingDirectory = "smc";
					}
					processStartInfo.FileName = path;
					processStartInfo.Arguments = arg;
					if (all == 0)
					{
						processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
					}
					else
					{
						processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					}
					Process.Start(processStartInfo);
				}
				else
				{
					start_timer.Enabled = false;
                    if (Language.Default.language == "English")
                    {
                        MessageBox.Show(path + "----->Not found !");
                    }
                    else
                    {
                        MessageBox.Show(path + "----->Bulunamadı !");
                    }
				}
			}
		}

		private void show(string prname, int all)
		{
			Process process = Process.GetProcessesByName(prname).FirstOrDefault();
			if (process == null)
			{
				return;
			}
			IntPtr ıntPtr = FindWindow(null, prname);
			if (ıntPtr == (IntPtr)0)
			{
				ıntPtr = GetProcessWindows(process.Id).FirstOrDefault();
			}
			if ((int)process.MainWindowHandle == 0)
			{
				if (all != 1)
				{
					ShowWindow(ıntPtr, 5);
				}
			}
			else if (all != 2)
			{
				ShowWindow(process.MainWindowHandle, 0);
			}
		}

		private void start_timer_Tick(object sender, EventArgs e)
		{
			switch (zaman)
			{
			case 1:
				start("Cert\\CustomCertificationServer.exe", "CustomCertificationServer", "Cert\\packt.dat", allstart);
				break;
			case 4:
				start("GlobalManager.exe", "GlobalManager", "", allstart);
				break;
			case 8:
				start("MachineManager.exe", "MachineManager", "", allstart);
				break;
			case 12:
				start("DownloadServer.exe", "DownloadServer", "", allstart);
				break;
			case 16:
				start("GatewayServer.exe", "GatewayServer", "", allstart);
				break;
			case 20:
				start("FarmManager.exe", "FarmManager", "", allstart);
				break;
			case 22:
				start("AgentServer.exe", "AgentServer", "", allstart);
				break;
			case 23:
				start("SR_ShardManager.exe", "SR_ShardManager", "", allstart);
				break;
			case 24:
				start("SR_GameServer.exe", "SR_GameServer", "", allstart);
				break;
			case 25:
				start("SroProt\\SroProt.exe", "SroProt", "", allstart);
				break;
			case 26:
				start_timer.Enabled = false;
				break;
			}
			zaman++;
		}

		private void refresh_Tick(object sender, EventArgs e)
		{
			start_durum("CustomCertificationServer", sertification_start, certification_Show);
			start_durum("GlobalManager", global_start, global_Show);
			start_durum("MachineManager", machine_start, machine_Show);
			start_durum("DownloadServer", download_start, download_Show);
			start_durum("GatewayServer", gateway_start, gateway_Show);
			start_durum("FarmManager", farm_start, farm_Show);
			start_durum("AgentServer", agent_start, agent_Show);
			start_durum("SR_ShardManager", shard_start, shard_Show);
			start_durum("SR_GameServer", game_start, game_Show);
            start_durum("SroProt", SroProt_start, SroProt_Show);
            start_durum("smc", smc_start, smc_Show);
			if (!serverclear.Checked)
			{
				return;
			}
			string[] files = Directory.GetFiles(Environment.CurrentDirectory);
			string[] array = files;
			foreach (string text in array)
			{
				if (text.Contains(".dmp") || text.Contains("FatalLog.txt") || text.Contains("ConcurrentUser*.txt") || text.Contains("ReportLog"))
				{
					try
					{
						File.Delete(text);
					}
					catch
					{
					}
				}
			}
		}

		private void quickToolStripMenuItem_Click(object sender, EventArgs e)
		{
			qconf qconf = new qconf();
			qconf.ShowDialog();
		}

		private void advencedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			aconf aconf = new aconf();
			aconf.ShowDialog();
		}

		private void tBUserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TbUser tbUser = new TbUser();
			tbUser.ShowDialog();
		}

		private void languageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lang lang = new lang();
			lang.ShowDialog();
		}

		private void dBtoMediaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DbToMd dbToMd = new DbToMd();
			dbToMd.ShowDialog();
		}

		private void itemmallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			itemmall itemmall = new itemmall();
			itemmall.ShowDialog();
		}

		private void npcmallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			npcmall npcmall = new npcmall();
			npcmall.ShowDialog();
		}
        private void ayarlarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Settings login = new Settings();
            login.ShowDialog();
        }

        private void hakkındaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.serverclear = new System.Windows.Forms.CheckBox();
            this.all_Show = new System.Windows.Forms.Button();
            this.all_stop = new System.Windows.Forms.Button();
            this.all_start = new System.Windows.Forms.Button();
            this.smc_Show = new System.Windows.Forms.Button();
            this.game_Show = new System.Windows.Forms.Button();
            this.shard_Show = new System.Windows.Forms.Button();
            this.agent_Show = new System.Windows.Forms.Button();
            this.farm_Show = new System.Windows.Forms.Button();
            this.gateway_Show = new System.Windows.Forms.Button();
            this.download_Show = new System.Windows.Forms.Button();
            this.machine_Show = new System.Windows.Forms.Button();
            this.global_Show = new System.Windows.Forms.Button();
            this.certification_Show = new System.Windows.Forms.Button();
            this.smc_start = new System.Windows.Forms.Button();
            this.game_start = new System.Windows.Forms.Button();
            this.shard_start = new System.Windows.Forms.Button();
            this.agent_start = new System.Windows.Forms.Button();
            this.farm_start = new System.Windows.Forms.Button();
            this.gateway_start = new System.Windows.Forms.Button();
            this.download_start = new System.Windows.Forms.Button();
            this.machine_start = new System.Windows.Forms.Button();
            this.global_start = new System.Windows.Forms.Button();
            this.sertification_start = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advencedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tBUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dBtoMediaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemmallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.npcmallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OtherstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hakkındaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refresh = new System.Windows.Forms.Timer(this.components);
            this.start_timer = new System.Windows.Forms.Timer(this.components);
            this.SroProt_start = new System.Windows.Forms.Button();
            this.SroProt_Show = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverclear
            // 
            this.serverclear.AutoSize = true;
            this.serverclear.Checked = true;
            this.serverclear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.serverclear.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.serverclear.ForeColor = System.Drawing.SystemColors.Window;
            this.serverclear.Location = new System.Drawing.Point(10, 426);
            this.serverclear.Name = "serverclear";
            this.serverclear.Size = new System.Drawing.Size(223, 23);
            this.serverclear.TabIndex = 52;
            this.serverclear.Text = "Sunucu Klasörü Temizliği";
            this.serverclear.UseVisualStyleBackColor = true;
            // 
            // all_Show
            // 
            this.all_Show.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold);
            this.all_Show.Location = new System.Drawing.Point(185, 370);
            this.all_Show.Name = "all_Show";
            this.all_Show.Size = new System.Drawing.Size(87, 43);
            this.all_Show.TabIndex = 51;
            this.all_Show.Text = "GÖSTER";
            this.all_Show.UseVisualStyleBackColor = true;
            this.all_Show.Click += new System.EventHandler(this.all_Show_Click);
            // 
            // all_stop
            // 
            this.all_stop.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.all_stop.Location = new System.Drawing.Point(95, 370);
            this.all_stop.Name = "all_stop";
            this.all_stop.Size = new System.Drawing.Size(84, 43);
            this.all_stop.TabIndex = 50;
            this.all_stop.Text = "DURDUR";
            this.all_stop.UseVisualStyleBackColor = true;
            this.all_stop.Click += new System.EventHandler(this.all_stop_Click);
            // 
            // all_start
            // 
            this.all_start.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold);
            this.all_start.Location = new System.Drawing.Point(5, 370);
            this.all_start.Name = "all_start";
            this.all_start.Size = new System.Drawing.Size(84, 43);
            this.all_start.TabIndex = 49;
            this.all_start.Text = "BAŞLAT";
            this.all_start.UseVisualStyleBackColor = true;
            this.all_start.Click += new System.EventHandler(this.all_start_Click);
            // 
            // smc_Show
            // 
            this.smc_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.smc_Show.Location = new System.Drawing.Point(185, 336);
            this.smc_Show.Name = "smc_Show";
            this.smc_Show.Size = new System.Drawing.Size(87, 29);
            this.smc_Show.TabIndex = 48;
            this.smc_Show.Text = "Göster";
            this.smc_Show.UseVisualStyleBackColor = true;
            this.smc_Show.Click += new System.EventHandler(this.smc_Show_Click);
            // 
            // game_Show
            // 
            this.game_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.game_Show.Location = new System.Drawing.Point(185, 276);
            this.game_Show.Name = "game_Show";
            this.game_Show.Size = new System.Drawing.Size(87, 25);
            this.game_Show.TabIndex = 46;
            this.game_Show.Text = "Göster";
            this.game_Show.UseVisualStyleBackColor = true;
            this.game_Show.Click += new System.EventHandler(this.game_Show_Click);
            // 
            // shard_Show
            // 
            this.shard_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.shard_Show.Location = new System.Drawing.Point(185, 246);
            this.shard_Show.Name = "shard_Show";
            this.shard_Show.Size = new System.Drawing.Size(87, 25);
            this.shard_Show.TabIndex = 45;
            this.shard_Show.Text = "Göster";
            this.shard_Show.UseVisualStyleBackColor = true;
            this.shard_Show.Click += new System.EventHandler(this.shard_Show_Click);
            // 
            // agent_Show
            // 
            this.agent_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.agent_Show.Location = new System.Drawing.Point(185, 216);
            this.agent_Show.Name = "agent_Show";
            this.agent_Show.Size = new System.Drawing.Size(87, 25);
            this.agent_Show.TabIndex = 44;
            this.agent_Show.Text = "Göster";
            this.agent_Show.UseVisualStyleBackColor = true;
            this.agent_Show.Click += new System.EventHandler(this.agent_Show_Click);
            // 
            // farm_Show
            // 
            this.farm_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.farm_Show.Location = new System.Drawing.Point(185, 186);
            this.farm_Show.Name = "farm_Show";
            this.farm_Show.Size = new System.Drawing.Size(87, 25);
            this.farm_Show.TabIndex = 43;
            this.farm_Show.Text = "Göster";
            this.farm_Show.UseVisualStyleBackColor = true;
            this.farm_Show.Click += new System.EventHandler(this.farm_Show_Click);
            // 
            // gateway_Show
            // 
            this.gateway_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gateway_Show.Location = new System.Drawing.Point(185, 156);
            this.gateway_Show.Name = "gateway_Show";
            this.gateway_Show.Size = new System.Drawing.Size(87, 25);
            this.gateway_Show.TabIndex = 42;
            this.gateway_Show.Text = "Göster";
            this.gateway_Show.UseVisualStyleBackColor = true;
            this.gateway_Show.Click += new System.EventHandler(this.gateway_Show_Click);
            // 
            // download_Show
            // 
            this.download_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.download_Show.Location = new System.Drawing.Point(185, 126);
            this.download_Show.Name = "download_Show";
            this.download_Show.Size = new System.Drawing.Size(87, 25);
            this.download_Show.TabIndex = 41;
            this.download_Show.Text = "Göster";
            this.download_Show.UseVisualStyleBackColor = true;
            this.download_Show.Click += new System.EventHandler(this.download_Show_Click);
            // 
            // machine_Show
            // 
            this.machine_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.machine_Show.Location = new System.Drawing.Point(185, 96);
            this.machine_Show.Name = "machine_Show";
            this.machine_Show.Size = new System.Drawing.Size(87, 25);
            this.machine_Show.TabIndex = 40;
            this.machine_Show.Text = "Göster";
            this.machine_Show.UseVisualStyleBackColor = true;
            this.machine_Show.Click += new System.EventHandler(this.machine_Show_Click);
            // 
            // global_Show
            // 
            this.global_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.global_Show.Location = new System.Drawing.Point(185, 66);
            this.global_Show.Name = "global_Show";
            this.global_Show.Size = new System.Drawing.Size(87, 25);
            this.global_Show.TabIndex = 39;
            this.global_Show.Text = "Göster";
            this.global_Show.UseVisualStyleBackColor = true;
            this.global_Show.Click += new System.EventHandler(this.global_Show_Click);
            // 
            // certification_Show
            // 
            this.certification_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.certification_Show.Location = new System.Drawing.Point(185, 36);
            this.certification_Show.Name = "certification_Show";
            this.certification_Show.Size = new System.Drawing.Size(87, 25);
            this.certification_Show.TabIndex = 38;
            this.certification_Show.Text = "Göster";
            this.certification_Show.UseVisualStyleBackColor = true;
            this.certification_Show.Click += new System.EventHandler(this.certification_Show_Click);
            // 
            // smc_start
            // 
            this.smc_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.smc_start.Location = new System.Drawing.Point(5, 336);
            this.smc_start.Name = "smc_start";
            this.smc_start.Size = new System.Drawing.Size(172, 29);
            this.smc_start.TabIndex = 37;
            this.smc_start.Text = "SMC";
            this.smc_start.UseVisualStyleBackColor = true;
            this.smc_start.Click += new System.EventHandler(this.smc_start_Click);
            // 
            // game_start
            // 
            this.game_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.game_start.Location = new System.Drawing.Point(5, 276);
            this.game_start.Name = "game_start";
            this.game_start.Size = new System.Drawing.Size(172, 25);
            this.game_start.TabIndex = 35;
            this.game_start.Text = "Game Server";
            this.game_start.UseVisualStyleBackColor = true;
            this.game_start.Click += new System.EventHandler(this.game_start_Click);
            // 
            // shard_start
            // 
            this.shard_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.shard_start.Location = new System.Drawing.Point(5, 246);
            this.shard_start.Name = "shard_start";
            this.shard_start.Size = new System.Drawing.Size(172, 25);
            this.shard_start.TabIndex = 34;
            this.shard_start.Text = "Shard Manager";
            this.shard_start.UseVisualStyleBackColor = true;
            this.shard_start.Click += new System.EventHandler(this.shard_start_Click);
            // 
            // agent_start
            // 
            this.agent_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.agent_start.Location = new System.Drawing.Point(5, 216);
            this.agent_start.Name = "agent_start";
            this.agent_start.Size = new System.Drawing.Size(172, 25);
            this.agent_start.TabIndex = 33;
            this.agent_start.Text = "Agent Server";
            this.agent_start.UseVisualStyleBackColor = true;
            this.agent_start.Click += new System.EventHandler(this.agent_start_Click);
            // 
            // farm_start
            // 
            this.farm_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.farm_start.Location = new System.Drawing.Point(5, 186);
            this.farm_start.Name = "farm_start";
            this.farm_start.Size = new System.Drawing.Size(172, 25);
            this.farm_start.TabIndex = 32;
            this.farm_start.Text = "Farm Manager";
            this.farm_start.UseVisualStyleBackColor = true;
            this.farm_start.Click += new System.EventHandler(this.farm_start_Click);
            // 
            // gateway_start
            // 
            this.gateway_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gateway_start.Location = new System.Drawing.Point(5, 156);
            this.gateway_start.Name = "gateway_start";
            this.gateway_start.Size = new System.Drawing.Size(172, 25);
            this.gateway_start.TabIndex = 31;
            this.gateway_start.Text = "Gateway Server";
            this.gateway_start.UseVisualStyleBackColor = true;
            this.gateway_start.Click += new System.EventHandler(this.gateway_start_Click);
            // 
            // download_start
            // 
            this.download_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.download_start.Location = new System.Drawing.Point(5, 126);
            this.download_start.Name = "download_start";
            this.download_start.Size = new System.Drawing.Size(172, 25);
            this.download_start.TabIndex = 30;
            this.download_start.Text = "Download Server";
            this.download_start.UseVisualStyleBackColor = true;
            this.download_start.Click += new System.EventHandler(this.download_start_Click);
            // 
            // machine_start
            // 
            this.machine_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.machine_start.Location = new System.Drawing.Point(5, 96);
            this.machine_start.Name = "machine_start";
            this.machine_start.Size = new System.Drawing.Size(172, 25);
            this.machine_start.TabIndex = 29;
            this.machine_start.Text = "Machine Manager";
            this.machine_start.UseVisualStyleBackColor = true;
            this.machine_start.Click += new System.EventHandler(this.machine_start_Click);
            // 
            // global_start
            // 
            this.global_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.global_start.Location = new System.Drawing.Point(5, 66);
            this.global_start.Name = "global_start";
            this.global_start.Size = new System.Drawing.Size(172, 25);
            this.global_start.TabIndex = 28;
            this.global_start.Text = "Global Manager";
            this.global_start.UseVisualStyleBackColor = true;
            this.global_start.Click += new System.EventHandler(this.global_start_Click);
            // 
            // sertification_start
            // 
            this.sertification_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sertification_start.Location = new System.Drawing.Point(5, 36);
            this.sertification_start.Name = "sertification_start";
            this.sertification_start.Size = new System.Drawing.Size(172, 25);
            this.sertification_start.TabIndex = 27;
            this.sertification_start.Text = "Sertification Server";
            this.sertification_start.UseVisualStyleBackColor = true;
            this.sertification_start.Click += new System.EventHandler(this.sertification_start_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverConfigToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.OtherstoolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 53;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serverConfigToolStripMenuItem
            // 
            this.serverConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickToolStripMenuItem,
            this.advencedToolStripMenuItem});
            this.serverConfigToolStripMenuItem.Name = "serverConfigToolStripMenuItem";
            this.serverConfigToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.serverConfigToolStripMenuItem.Text = "Sunucu Yapılandır";
            // 
            // quickToolStripMenuItem
            // 
            this.quickToolStripMenuItem.Name = "quickToolStripMenuItem";
            this.quickToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.quickToolStripMenuItem.Text = "Hızlı Yapılandırma";
            this.quickToolStripMenuItem.Click += new System.EventHandler(this.quickToolStripMenuItem_Click);
            // 
            // advencedToolStripMenuItem
            // 
            this.advencedToolStripMenuItem.Name = "advencedToolStripMenuItem";
            this.advencedToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.advencedToolStripMenuItem.Text = "El ile Yapılandırma";
            this.advencedToolStripMenuItem.Click += new System.EventHandler(this.advencedToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tBUserToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.dBtoMediaToolStripMenuItem,
            this.itemmallToolStripMenuItem,
            this.npcmallToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.toolsToolStripMenuItem.Text = "Araçlar";
            // 
            // tBUserToolStripMenuItem
            // 
            this.tBUserToolStripMenuItem.Name = "tBUserToolStripMenuItem";
            this.tBUserToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.tBUserToolStripMenuItem.Text = "Kullanıcı Oluştur";
            this.tBUserToolStripMenuItem.Click += new System.EventHandler(this.tBUserToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.languageToolStripMenuItem.Text = "Dil Düzenle";
            this.languageToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // dBtoMediaToolStripMenuItem
            // 
            this.dBtoMediaToolStripMenuItem.Name = "dBtoMediaToolStripMenuItem";
            this.dBtoMediaToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.dBtoMediaToolStripMenuItem.Text = "DB_Media Oluştur";
            this.dBtoMediaToolStripMenuItem.Click += new System.EventHandler(this.dBtoMediaToolStripMenuItem_Click);
            // 
            // itemmallToolStripMenuItem
            // 
            this.itemmallToolStripMenuItem.Name = "itemmallToolStripMenuItem";
            this.itemmallToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.itemmallToolStripMenuItem.Text = "Item Mall Düzenle";
            this.itemmallToolStripMenuItem.Click += new System.EventHandler(this.itemmallToolStripMenuItem_Click);
            // 
            // npcmallToolStripMenuItem
            // 
            this.npcmallToolStripMenuItem.Name = "npcmallToolStripMenuItem";
            this.npcmallToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.npcmallToolStripMenuItem.Text = "Npc Mall Düzenle";
            this.npcmallToolStripMenuItem.Click += new System.EventHandler(this.npcmallToolStripMenuItem_Click);
            // 
            // OtherstoolStripMenuItem
            // 
            this.OtherstoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ayarlarToolStripMenuItem,
            this.hakkındaToolStripMenuItem});
            this.OtherstoolStripMenuItem.Name = "OtherstoolStripMenuItem";
            this.OtherstoolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.OtherstoolStripMenuItem.Text = "Diğer";
            // 
            // ayarlarToolStripMenuItem
            // 
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            this.ayarlarToolStripMenuItem.Click += new System.EventHandler(this.ayarlarToolStripMenuItem_Click_1);
            // 
            // hakkındaToolStripMenuItem
            // 
            this.hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            this.hakkındaToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.hakkındaToolStripMenuItem.Text = "Hakkında";
            this.hakkındaToolStripMenuItem.Click += new System.EventHandler(this.hakkındaToolStripMenuItem_Click_1);
            // 
            // refresh
            // 
            this.refresh.Enabled = true;
            this.refresh.Interval = 500;
            this.refresh.Tick += new System.EventHandler(this.refresh_Tick);
            // 
            // start_timer
            // 
            this.start_timer.Tick += new System.EventHandler(this.start_timer_Tick);
            // 
            // SroProt_start
            // 
            this.SroProt_start.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.SroProt_start.Location = new System.Drawing.Point(5, 306);
            this.SroProt_start.Name = "SroProt_start";
            this.SroProt_start.Size = new System.Drawing.Size(172, 25);
            this.SroProt_start.TabIndex = 54;
            this.SroProt_start.Text = "SroProt";
            this.SroProt_start.UseVisualStyleBackColor = true;
            this.SroProt_start.Click += new System.EventHandler(this.SroProt_start_Click);
            // 
            // SroProt_Show
            // 
            this.SroProt_Show.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.SroProt_Show.Location = new System.Drawing.Point(185, 306);
            this.SroProt_Show.Name = "SroProt_Show";
            this.SroProt_Show.Size = new System.Drawing.Size(87, 25);
            this.SroProt_Show.TabIndex = 55;
            this.SroProt_Show.Text = "Göster";
            this.SroProt_Show.UseVisualStyleBackColor = true;
            this.SroProt_Show.Click += new System.EventHandler(this.SroProt_Show_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(284, 466);
            this.Controls.Add(this.SroProt_Show);
            this.Controls.Add(this.SroProt_start);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.serverclear);
            this.Controls.Add(this.all_Show);
            this.Controls.Add(this.all_stop);
            this.Controls.Add(this.all_start);
            this.Controls.Add(this.smc_Show);
            this.Controls.Add(this.game_Show);
            this.Controls.Add(this.shard_Show);
            this.Controls.Add(this.agent_Show);
            this.Controls.Add(this.farm_Show);
            this.Controls.Add(this.gateway_Show);
            this.Controls.Add(this.download_Show);
            this.Controls.Add(this.machine_Show);
            this.Controls.Add(this.global_Show);
            this.Controls.Add(this.certification_Show);
            this.Controls.Add(this.smc_start);
            this.Controls.Add(this.game_start);
            this.Controls.Add(this.shard_start);
            this.Controls.Add(this.agent_start);
            this.Controls.Add(this.farm_start);
            this.Controls.Add(this.gateway_start);
            this.Controls.Add(this.download_start);
            this.Controls.Add(this.machine_start);
            this.Controls.Add(this.global_start);
            this.Controls.Add(this.sertification_start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 505);
            this.MinimumSize = new System.Drawing.Size(300, 505);
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServerManager";
            this.Load += new System.EventHandler(this.main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
    }
}
