using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SilkroadServerManager
{
	public class qconf : Form
	{
		private string opttype;

		private string dgfm_common_port;

		private string ass_common_port;

		private string cert_port;

		private IContainer components = null;

		private RichTextBox sonuc;

		private GroupBox srSpoof;

		private CheckBox ShardManager;

		private CheckBox GameServer;

		private CheckBox MachineManager;

		private CheckBox GlobalManager;

		private CheckBox GatewayServer;

		private CheckBox FarmManager;

		private CheckBox DownloadServer;

		private CheckBox AgentServer;

		private Button cancel;

		private TextBox ftpurl;

		private Label label16;

		private TextBox dwport;

		private Label label13;

		private TextBox capacity;

		private Label label9;

		private Label label12;

		private TextBox sqlserver;

		private TextBox username;

		private Button quicksetup;

		private TextBox password;

		private Label label10;

		private TextBox shardname;

		private TextBox shardlog;

		private Label label8;

		private TextBox account;

		private Label label7;

		private TextBox serverip;

		private Label label6;

		private TextBox gtwport;

		private Label label5;

		private TextBox agnport;

		private Label label4;

		private TextBox servername;

		private Label label3;

		private Label label1;

		private Label label2;

		private Button refresh;

		public qconf()
		{
			InitializeComponent();
		}

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		private void qconf_Load(object sender, EventArgs e)
		{
			mevcut();
            if (Language.Default.language == "English")
            {
                this.Text = "Quick Configuration";
                label2.Text = "SQL Username";
                label3.Text = "SQL Password";
                label7.Text = "Server IP";
                label10.Text = "Server name";
                label12.Text = "Capacity";
                quicksetup.Text = "Setup Server";
                refresh.Text = "Refresh";
                cancel.Text = "Exit";
            }
        }

		private void refresh_Click(object sender, EventArgs e)
		{
			mevcut();
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void quicksetup_Click(object sender, EventArgs e)
		{
			sonuc.Clear();
			iniWriter("Cert\\ini\\srGlobalOperation.ini", "entry0", "operation_type", opttype);
			iniWriter("Cert\\ini\\srGlobalService.ini", "entry0", "name", servername.Text);
			iniWriter("Cert\\ini\\srGlobalService.ini", "entry0", "query", "DRIVER={SQL Server};SERVER=" + sqlserver.Text + ";DSN=acc;UID=" + username.Text + ";PWD=" + password.Text + ";DATABASE=" + account.Text);
			for (int i = 0; i < 12; i++)
			{
				if (i != 1)
				{
					iniWriter("Cert\\ini\\srNodeData.ini", "entry" + i, "operation_type", opttype);
				}
			}
			iniWriter("Cert\\ini\\srNodeData.ini", "entry2", "port", gtwport.Text);
			iniWriter("Cert\\ini\\srNodeData.ini", "entry4", "port", dwport.Text);
			iniWriter("Cert\\ini\\srNodeData.ini", "entry7", "port", agnport.Text);
			iniWriter("Cert\\ini\\srNodeType.ini", "entry0", "operation_type", opttype);
			iniWriter("Cert\\ini\\srNodeType.ini", "entry0", "name", servername.Text);
			iniWriter("Cert\\ini\\srNodeType.ini", "entry0", "wip", serverip.Text);
			iniWriter("Cert\\ini\\srNodeType.ini", "entry0", "nip", serverip.Text);
			iniWriter("Cert\\ini\\srShard.ini", "entry0", "operation_type", opttype);
			iniWriter("Cert\\ini\\srShard.ini", "entry0", "name", servername.Text);
			iniWriter("Cert\\ini\\srShard.ini", "entry0", "query", "DRIVER={SQL Server};SERVER=" + sqlserver.Text + ";DSN=shard;UID=" + username.Text + ";PWD=" + password.Text + ";DATABASE=" + shardname.Text);
			iniWriter("Cert\\ini\\srShard.ini", "entry0", "query_log", "DRIVER={SQL Server};SERVER=" + sqlserver.Text + ";DSN=shard;UID=" + username.Text + ";PWD=" + password.Text + ";DATABASE=" + shardlog.Text);
			iniWriter("Cert\\ini\\srShard.ini", "entry0", "capacity", capacity.Text);
			iniWriter("Cert\\ini\\srUnknown.ini", "entry0", "operation_type", opttype);
			cfgWriter("server.cfg", "GlobalManager", "Certification", "DownloadServer", "\tCertification\t\t\t\"" + serverip.Text + "\", " + cert_port);
			cfgWriter("server.cfg", "DownloadServer", "Certification", "GatewayServer", "\tCertification\t\t\t\"" + serverip.Text + "\", " + dgfm_common_port);
			cfgWriter("server.cfg", "GatewayServer", "Certification", "FarmManager", "\tCertification\t\t\t\"" + serverip.Text + "\", " + dgfm_common_port);
			cfgWriter("server.cfg", "FarmManager", "Certification", "MachineManager", "\tCertification\t\t\t\"" + serverip.Text + "\", " + dgfm_common_port);
			cfgWriter("server.cfg", "MachineManager", "Certification", "AgentServer", "\tCertification\t\t\t\"" + serverip.Text + "\", " + dgfm_common_port);
			cfgWriter("server.cfg", "AgentServer", "Certification", "SR_GameServer", "\tCertification\t\t\t\"" + serverip.Text + "\", " + ass_common_port);
			cfgWriter("server.cfg", "SR_GameServer", "Certification", "SR_ShardManager", "\tCertification\t\t\t\"" + serverip.Text + "\", " + ass_common_port);
			cfgWriter("server.cfg", "SR_ShardManager", "Certification", "ArenaMatchPoint", "\tCertification\t\t\t\"" + serverip.Text + "\", " + ass_common_port);
			cfgWriter("server.cfg", "SR_ShardManager", "CREST_FTP_URL", "ArenaMatchPoint", "\tCREST_FTP_URL\t\t\t\"" + ftpurl.Text + "\"");
			cfgWriter("SMC\\ServiceManager.cfg", "SMC", "DivisionManager", "ModulePatch", "\tDivisionManager \"" + serverip.Text + "\", " + dgfm_common_port);
			if (kontrol("Cert\\Convert.exe"))
			{
				Process.Start("Cert\\Convert.exe", " ini Cert\\ini dat Cert\\packt.dat");
				Process process = Process.GetProcessesByName("Convert").FirstOrDefault();
				if (process != null)
				{
					process.WaitForExit(3000);
					process.Kill();
				}
			}
			if (FarmManager.Checked)
			{
				srspoof("FarmManager.exe", serverip.Text);
			}
			if (AgentServer.Checked)
			{
				srspoof("AgentServer.exe", serverip.Text);
			}
			if (ShardManager.Checked)
			{
				srspoof("SR_ShardManager.exe", serverip.Text);
			}
			if (GameServer.Checked)
			{
				srspoof("SR_GameServer.exe", serverip.Text);
			}
			if (GlobalManager.Checked)
			{
				srspoof("GlobalManager.exe", serverip.Text);
			}
			if (MachineManager.Checked)
			{
				srspoof("MachineManager.exe", serverip.Text);
			}
			if (GatewayServer.Checked)
			{
				srspoof("GatewayServer.exe", serverip.Text);
			}
			if (DownloadServer.Checked)
			{
				srspoof("DownloadServer.exe", serverip.Text);
			}
		}

		private void mevcut()
		{
			servername.Text = iniReader("Cert\\ini\\srGlobalService.ini", "entry0", "name", 0);
			opttype = iniReader("Cert\\ini\\srGlobalService.ini", "entry0", "operation_type", 0);
			sqlserver.Text = iniReader("Cert\\ini\\srGlobalService.ini", "entry0", "query", 2);
			username.Text = iniReader("Cert\\ini\\srGlobalService.ini", "entry0", "query", 4);
			password.Text = iniReader("Cert\\ini\\srGlobalService.ini", "entry0", "query", 5);
			account.Text = iniReader("Cert\\ini\\srGlobalService.ini", "entry0", "query", 6);
			shardname.Text = iniReader("Cert\\ini\\srShard.ini", "entry0", "query", 6);
			shardlog.Text = iniReader("Cert\\ini\\srShard.ini", "entry0", "query_log", 6);
			capacity.Text = iniReader("Cert\\ini\\srShard.ini", "entry0", "capacity", 0);
			serverip.Text = iniReader("Cert\\ini\\srNodeType.ini", "entry0", "wip", 0);
			cert_port = iniReader("Cert\\ini\\srNodeData.ini", "entry1", "port", 0);
			dgfm_common_port = iniReader("Cert\\ini\\srNodeData.ini", "entry0", "port", 0);
			ass_common_port = iniReader("Cert\\ini\\srNodeData.ini", "entry5", "port", 0);
			gtwport.Text = iniReader("Cert\\ini\\srNodeData.ini", "entry2", "port", 0);
			dwport.Text = iniReader("Cert\\ini\\srNodeData.ini", "entry4", "port", 0);
			agnport.Text = iniReader("Cert\\ini\\srNodeData.ini", "entry7", "port", 0);
			ftpurl.Text = cfgReader("server.cfg", "CREST_FTP_URL");
		}

		private bool kontrol(string path)
		{
			if (File.Exists(path))
			{
				return true;
			}
			sonuc.Select(sonuc.Text.Length, 0);
			sonuc.SelectionColor = Color.Red;
			sonuc.DeselectAll();
            if (Language.Default.language == "English")
            {
                sonuc.AppendText(path + "---->Not found !\n");
            }
            else
            {
                sonuc.AppendText(path + "---->Bulunamadı !\n");
            }

			return false;
		}

		private void iniWriter(string filePath, string section, string key, string val)
		{
			if (kontrol(filePath))
			{
				WritePrivateProfileString(section, key, val, filePath);
                if (Language.Default.language == "English")
                {
                    sonuc.AppendText(filePath + " ---> " + key + "=" + val + " Successful\n");
                }
                else
                {
                    sonuc.AppendText(filePath + " ---> " + key + "=" + val + " Başarılı\n");
                }
			}
		}

		private void cfgWriter(string path, string index, string option, string durdur, string replace)
		{
			int num = 0;
			if (!kontrol(path))
			{
				return;
			}
			string[] array = File.ReadAllText(path).Split('\n');
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains(index))
				{
					num = 1;
				}
				if (num == 1 && array[i].Contains(option))
				{
					array[i] = array[i].Replace(array[i], replace + "\r");
                    if (Language.Default.language == "English")
                    {
                        sonuc.AppendText(path + " ---> " + replace + " Successful\n");
                    }
                    else
                    {
                        sonuc.AppendText(path + " ---> " + replace + " Başarılı\n");
                    }
					break;
				}
				if (array[i].Contains(durdur))
				{
					sonuc.Select(sonuc.Text.Length, 0);
					sonuc.SelectionColor = Color.Red;
					sonuc.DeselectAll();
                    if (Language.Default.language == "English")
                    {
                        sonuc.AppendText(path + " ---> " + index + "---" + option + " Not found !\n");
                    }
                    else
                    {
                        sonuc.AppendText(path + " ---> " + index + "---" + option + " Bulunamadı !\n");
                    }
					break;
				}
			}
			File.WriteAllText(path, string.Join("\n", array));
		}

		private string iniReader(string path, string sec, string key, int sira)
		{
			if (kontrol(path))
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				GetPrivateProfileString(sec, key, "", stringBuilder, 255, path);
				key = stringBuilder.ToString();
				if (sira > 0)
				{
					key = key.Replace(";DSN", "");
					key = key.Replace(";PWD", "");
					key = key.Replace(";DATABASE", "");
					string[] array = key.Split('=');
					key = array[sira];
				}
			}
			return key;
		}

		private string cfgReader(string path, string index)
		{
			if (kontrol(path))
			{
				string[] array = File.ReadAllText(path).Split('\n');
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Contains(index))
					{
						index = array[i].Replace(index, "");
						index = index.Replace("\r", "");
						index = index.Replace("\"", "");
						index = index.Replace(" ", "");
						index = index.Replace("\t", "");
						break;
					}
				}
			}
			return index;
		}

		private void srspoof(string path, string ip)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			int num = 4194304;
			int num2 = 100;
			try
			{
				FileStream fileStream = File.Open(path, FileMode.Open);
				BinaryReader binaryReader = new BinaryReader(fileStream);
				long length = binaryReader.BaseStream.Length;
				long num3 = 0L;
				binaryReader.BaseStream.Seek(0L, SeekOrigin.Begin);
				int num4 = 0;
				int num5 = 0;
				for (int i = 0; i < length - 1000; i++)
				{
					try
					{
						if (binaryReader.ReadByte() == 82 && binaryReader.ReadByte() == 80 && binaryReader.ReadByte() == 15 && binaryReader.ReadByte() == 182 && binaryReader.ReadByte() == 201 && binaryReader.ReadByte() == 81)
						{
							if (num4 == 0)
							{
								num4 = (int)binaryReader.BaseStream.Position + num;
							}
							else
							{
								num5 = (int)binaryReader.BaseStream.Position + num;
							}
						}
					}
					catch
					{
						break;
					}
				}
				if (num4 == 0 || num5 == 0)
				{
					sonuc.Select(sonuc.Text.Length, 0);
					sonuc.SelectionColor = Color.Red;
					sonuc.DeselectAll();
                    if (Language.Default.language == "English")
                    {
                        sonuc.AppendText("Something went wrong (one or both of addresses of push instructions are 0 !) - signature detection failed ?\n");
                    }
                    else
                    {
                        sonuc.AppendText("Bir şeyler ters gitti (push komutlarının adreslerinden biri veya ikisi 0!) - imza algılama başarısız mı?\n");
                    }
                }
				else
				{
					binaryReader.BaseStream.Seek(0L, SeekOrigin.Begin);
					long num6 = 0L;
					for (int j = 0; j < length - num2; j++)
					{
						num6 = ((binaryReader.ReadByte() != 0) ? 0 : (num6 + 1));
						if (num6 >= num2)
						{
							num3 = binaryReader.BaseStream.Position + num - num2;
							break;
						}
					}
					binaryReader.Close();
					fileStream.Close();
					FileStream fileStream2 = new FileStream(path, FileMode.Open);
					BinaryWriter binaryWriter = new BinaryWriter(fileStream2);
					fileStream2.Seek(num3 - num, SeekOrigin.Begin);
					byte[] bytes = Encoding.ASCII.GetBytes(ip);
					foreach (byte value in bytes)
					{
						fileStream2.WriteByte(value);
					}
					fileStream2.Seek(num4 - num + 1, SeekOrigin.Begin);
					byte[] bytes2 = BitConverter.GetBytes(num3);
					for (int l = 0; l < 4; l++)
					{
						fileStream2.WriteByte(bytes2[l]);
					}
					fileStream2.Seek(num5 - num + 1, SeekOrigin.Begin);
					byte[] bytes3 = BitConverter.GetBytes(num3);
					for (int m = 0; m < 4; m++)
					{
						fileStream2.WriteByte(bytes3[m]);
					}
					binaryWriter.Close();
					fileStream2.Close();
                    if (Language.Default.language == "English")
                    {
                        sonuc.AppendText(path + "-----> Patching finished !\n");
                    }
                    else
                    {
                        sonuc.AppendText(path + "-----> Yama tamamlandı!\n");
                    }
					if (num3 == 0)
					{
                        if (Language.Default.language == "English")
                        {
                            sonuc.AppendText("Something went wrong (no free space at PE ?!\n");
                        }
                        else
                        {
                            sonuc.AppendText("Bir şeyler ters gitti (PE'de boş alan yok mu?!\n");
                        }
					}
				}
			}
			catch (Exception ex)
			{
				sonuc.Select(sonuc.Text.Length, 0);
				sonuc.SelectionColor = Color.Red;
				sonuc.DeselectAll();
                if (Language.Default.language == "English")
                {
                    sonuc.AppendText("Something went terribly wrong:\n" + ex.Message + "\n");
                }
                else
                {
                    sonuc.AppendText("Bir şeyler çok yanlış gitti:\n" + ex.Message + "\n");
                }
			}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(qconf));
            this.sonuc = new System.Windows.Forms.RichTextBox();
            this.srSpoof = new System.Windows.Forms.GroupBox();
            this.ShardManager = new System.Windows.Forms.CheckBox();
            this.GameServer = new System.Windows.Forms.CheckBox();
            this.MachineManager = new System.Windows.Forms.CheckBox();
            this.GlobalManager = new System.Windows.Forms.CheckBox();
            this.GatewayServer = new System.Windows.Forms.CheckBox();
            this.FarmManager = new System.Windows.Forms.CheckBox();
            this.DownloadServer = new System.Windows.Forms.CheckBox();
            this.AgentServer = new System.Windows.Forms.CheckBox();
            this.cancel = new System.Windows.Forms.Button();
            this.ftpurl = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dwport = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.capacity = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.sqlserver = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.quicksetup = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.shardname = new System.Windows.Forms.TextBox();
            this.shardlog = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.account = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.serverip = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gtwport = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.agnport = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.servername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Button();
            this.srSpoof.SuspendLayout();
            this.SuspendLayout();
            // 
            // sonuc
            // 
            this.sonuc.Location = new System.Drawing.Point(12, 328);
            this.sonuc.Name = "sonuc";
            this.sonuc.ReadOnly = true;
            this.sonuc.Size = new System.Drawing.Size(560, 221);
            this.sonuc.TabIndex = 85;
            this.sonuc.Text = "";
            // 
            // srSpoof
            // 
            this.srSpoof.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.srSpoof.Controls.Add(this.ShardManager);
            this.srSpoof.Controls.Add(this.GameServer);
            this.srSpoof.Controls.Add(this.MachineManager);
            this.srSpoof.Controls.Add(this.GlobalManager);
            this.srSpoof.Controls.Add(this.GatewayServer);
            this.srSpoof.Controls.Add(this.FarmManager);
            this.srSpoof.Controls.Add(this.DownloadServer);
            this.srSpoof.Controls.Add(this.AgentServer);
            this.srSpoof.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.srSpoof.Location = new System.Drawing.Point(339, 113);
            this.srSpoof.Name = "srSpoof";
            this.srSpoof.Size = new System.Drawing.Size(233, 114);
            this.srSpoof.TabIndex = 84;
            this.srSpoof.TabStop = false;
            this.srSpoof.Text = "___srSpoof_ip_patcher";
            // 
            // ShardManager
            // 
            this.ShardManager.AutoSize = true;
            this.ShardManager.Location = new System.Drawing.Point(11, 65);
            this.ShardManager.Name = "ShardManager";
            this.ShardManager.Size = new System.Drawing.Size(96, 17);
            this.ShardManager.TabIndex = 18;
            this.ShardManager.Text = "ShardManager";
            this.ShardManager.UseVisualStyleBackColor = true;
            // 
            // GameServer
            // 
            this.GameServer.AutoSize = true;
            this.GameServer.Location = new System.Drawing.Point(11, 88);
            this.GameServer.Name = "GameServer";
            this.GameServer.Size = new System.Drawing.Size(85, 17);
            this.GameServer.TabIndex = 19;
            this.GameServer.Text = "GameServer";
            this.GameServer.UseVisualStyleBackColor = true;
            // 
            // MachineManager
            // 
            this.MachineManager.AutoSize = true;
            this.MachineManager.Location = new System.Drawing.Point(113, 42);
            this.MachineManager.Name = "MachineManager";
            this.MachineManager.Size = new System.Drawing.Size(109, 17);
            this.MachineManager.TabIndex = 21;
            this.MachineManager.Text = "MachineManager";
            this.MachineManager.UseVisualStyleBackColor = true;
            // 
            // GlobalManager
            // 
            this.GlobalManager.AutoSize = true;
            this.GlobalManager.Location = new System.Drawing.Point(113, 19);
            this.GlobalManager.Name = "GlobalManager";
            this.GlobalManager.Size = new System.Drawing.Size(98, 17);
            this.GlobalManager.TabIndex = 20;
            this.GlobalManager.Text = "GlobalManager";
            this.GlobalManager.UseVisualStyleBackColor = true;
            // 
            // GatewayServer
            // 
            this.GatewayServer.AutoSize = true;
            this.GatewayServer.Location = new System.Drawing.Point(113, 65);
            this.GatewayServer.Name = "GatewayServer";
            this.GatewayServer.Size = new System.Drawing.Size(99, 17);
            this.GatewayServer.TabIndex = 22;
            this.GatewayServer.Text = "GatewayServer";
            this.GatewayServer.UseVisualStyleBackColor = true;
            // 
            // FarmManager
            // 
            this.FarmManager.AutoSize = true;
            this.FarmManager.Location = new System.Drawing.Point(11, 19);
            this.FarmManager.Name = "FarmManager";
            this.FarmManager.Size = new System.Drawing.Size(91, 17);
            this.FarmManager.TabIndex = 16;
            this.FarmManager.Text = "FarmManager";
            this.FarmManager.UseVisualStyleBackColor = true;
            // 
            // DownloadServer
            // 
            this.DownloadServer.AutoSize = true;
            this.DownloadServer.Location = new System.Drawing.Point(113, 88);
            this.DownloadServer.Name = "DownloadServer";
            this.DownloadServer.Size = new System.Drawing.Size(105, 17);
            this.DownloadServer.TabIndex = 23;
            this.DownloadServer.Text = "DownloadServer";
            this.DownloadServer.UseVisualStyleBackColor = true;
            // 
            // AgentServer
            // 
            this.AgentServer.AutoSize = true;
            this.AgentServer.Location = new System.Drawing.Point(11, 42);
            this.AgentServer.Name = "AgentServer";
            this.AgentServer.Size = new System.Drawing.Size(85, 17);
            this.AgentServer.TabIndex = 17;
            this.AgentServer.Text = "AgentServer";
            this.AgentServer.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cancel.Location = new System.Drawing.Point(12, 269);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(90, 53);
            this.cancel.TabIndex = 14;
            this.cancel.Text = "Çıkış";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // ftpurl
            // 
            this.ftpurl.Location = new System.Drawing.Point(158, 243);
            this.ftpurl.Name = "ftpurl";
            this.ftpurl.Size = new System.Drawing.Size(414, 20);
            this.ftpurl.TabIndex = 9;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label16.Location = new System.Drawing.Point(12, 243);
            this.label16.MaximumSize = new System.Drawing.Size(2, 20);
            this.label16.MinimumSize = new System.Drawing.Size(139, 2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(139, 20);
            this.label16.TabIndex = 75;
            this.label16.Text = "CREST FTP URL";
            // 
            // dwport
            // 
            this.dwport.Location = new System.Drawing.Point(507, 62);
            this.dwport.Name = "dwport";
            this.dwport.Size = new System.Drawing.Size(65, 20);
            this.dwport.TabIndex = 12;
            this.dwport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label13.Location = new System.Drawing.Point(339, 62);
            this.label13.MinimumSize = new System.Drawing.Size(162, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(162, 21);
            this.label13.TabIndex = 76;
            this.label13.Text = "Download Port";
            // 
            // capacity
            // 
            this.capacity.Location = new System.Drawing.Point(158, 217);
            this.capacity.MinimumSize = new System.Drawing.Size(4, 4);
            this.capacity.Name = "capacity";
            this.capacity.Size = new System.Drawing.Size(83, 20);
            this.capacity.TabIndex = 8;
            this.capacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(339, 35);
            this.label9.MaximumSize = new System.Drawing.Size(2, 20);
            this.label9.MinimumSize = new System.Drawing.Size(162, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 20);
            this.label9.TabIndex = 80;
            this.label9.Text = "Agent Port";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label12.Location = new System.Drawing.Point(12, 217);
            this.label12.MaximumSize = new System.Drawing.Size(2, 20);
            this.label12.MinimumSize = new System.Drawing.Size(139, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 20);
            this.label12.TabIndex = 81;
            this.label12.Text = "Kapasite";
            // 
            // sqlserver
            // 
            this.sqlserver.Location = new System.Drawing.Point(158, 9);
            this.sqlserver.MinimumSize = new System.Drawing.Size(160, 4);
            this.sqlserver.Name = "sqlserver";
            this.sqlserver.Size = new System.Drawing.Size(160, 20);
            this.sqlserver.TabIndex = 0;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(158, 35);
            this.username.MinimumSize = new System.Drawing.Size(160, 4);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(160, 20);
            this.username.TabIndex = 1;
            // 
            // quicksetup
            // 
            this.quicksetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.quicksetup.Location = new System.Drawing.Point(465, 269);
            this.quicksetup.Name = "quicksetup";
            this.quicksetup.Size = new System.Drawing.Size(107, 53);
            this.quicksetup.TabIndex = 13;
            this.quicksetup.Text = "Server Kur";
            this.quicksetup.UseVisualStyleBackColor = true;
            this.quicksetup.Click += new System.EventHandler(this.quicksetup_Click);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(158, 61);
            this.password.MinimumSize = new System.Drawing.Size(160, 4);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(160, 20);
            this.password.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(12, 191);
            this.label10.MaximumSize = new System.Drawing.Size(2, 20);
            this.label10.MinimumSize = new System.Drawing.Size(139, 2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 20);
            this.label10.TabIndex = 74;
            this.label10.Text = "Sunucu adı";
            // 
            // shardname
            // 
            this.shardname.Location = new System.Drawing.Point(158, 113);
            this.shardname.MinimumSize = new System.Drawing.Size(160, 4);
            this.shardname.Name = "shardname";
            this.shardname.Size = new System.Drawing.Size(160, 20);
            this.shardname.TabIndex = 4;
            // 
            // shardlog
            // 
            this.shardlog.Location = new System.Drawing.Point(158, 139);
            this.shardlog.MinimumSize = new System.Drawing.Size(160, 4);
            this.shardlog.Name = "shardlog";
            this.shardlog.Size = new System.Drawing.Size(160, 20);
            this.shardlog.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(339, 10);
            this.label8.MaximumSize = new System.Drawing.Size(2, 20);
            this.label8.MinimumSize = new System.Drawing.Size(162, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 20);
            this.label8.TabIndex = 79;
            this.label8.Text = "Gateway Port";
            // 
            // account
            // 
            this.account.Location = new System.Drawing.Point(158, 87);
            this.account.MinimumSize = new System.Drawing.Size(160, 4);
            this.account.Name = "account";
            this.account.Size = new System.Drawing.Size(160, 20);
            this.account.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(12, 165);
            this.label7.MaximumSize = new System.Drawing.Size(2, 20);
            this.label7.MinimumSize = new System.Drawing.Size(139, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 20);
            this.label7.TabIndex = 73;
            this.label7.Text = "Sunucu IP";
            // 
            // serverip
            // 
            this.serverip.Location = new System.Drawing.Point(158, 165);
            this.serverip.MinimumSize = new System.Drawing.Size(160, 4);
            this.serverip.Name = "serverip";
            this.serverip.Size = new System.Drawing.Size(160, 20);
            this.serverip.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(12, 87);
            this.label6.MaximumSize = new System.Drawing.Size(2, 20);
            this.label6.MinimumSize = new System.Drawing.Size(139, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 20);
            this.label6.TabIndex = 72;
            this.label6.Text = "SQL Account DB";
            // 
            // gtwport
            // 
            this.gtwport.Location = new System.Drawing.Point(507, 10);
            this.gtwport.Name = "gtwport";
            this.gtwport.Size = new System.Drawing.Size(65, 20);
            this.gtwport.TabIndex = 10;
            this.gtwport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(12, 139);
            this.label5.MaximumSize = new System.Drawing.Size(2, 20);
            this.label5.MinimumSize = new System.Drawing.Size(139, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 20);
            this.label5.TabIndex = 71;
            this.label5.Text = "SQL Shard Log";
            // 
            // agnport
            // 
            this.agnport.Location = new System.Drawing.Point(507, 35);
            this.agnport.Name = "agnport";
            this.agnport.Size = new System.Drawing.Size(65, 20);
            this.agnport.TabIndex = 11;
            this.agnport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(12, 113);
            this.label4.MaximumSize = new System.Drawing.Size(2, 20);
            this.label4.MinimumSize = new System.Drawing.Size(139, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 20);
            this.label4.TabIndex = 70;
            this.label4.Text = "SQL Shard DB";
            // 
            // servername
            // 
            this.servername.Location = new System.Drawing.Point(158, 191);
            this.servername.MinimumSize = new System.Drawing.Size(160, 4);
            this.servername.Name = "servername";
            this.servername.Size = new System.Drawing.Size(160, 20);
            this.servername.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.MaximumSize = new System.Drawing.Size(2, 20);
            this.label3.MinimumSize = new System.Drawing.Size(139, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 69;
            this.label3.Text = "SQL Şifresi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.MaximumSize = new System.Drawing.Size(2, 20);
            this.label1.MinimumSize = new System.Drawing.Size(139, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 67;
            this.label1.Text = "SQL Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.MaximumSize = new System.Drawing.Size(2, 20);
            this.label2.MinimumSize = new System.Drawing.Size(139, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 68;
            this.label2.Text = "SQL Kullanıcı adı";
            // 
            // refresh
            // 
            this.refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.refresh.Location = new System.Drawing.Point(247, 269);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(90, 53);
            this.refresh.TabIndex = 15;
            this.refresh.Text = "Yenile";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // qconf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.sonuc);
            this.Controls.Add(this.srSpoof);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ftpurl);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dwport);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.capacity);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.sqlserver);
            this.Controls.Add(this.username);
            this.Controls.Add(this.quicksetup);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.shardname);
            this.Controls.Add(this.shardlog);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.account);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.serverip);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gtwport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.agnport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.servername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "qconf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hızlı Yapılandırma";
            this.Load += new System.EventHandler(this.qconf_Load);
            this.srSpoof.ResumeLayout(false);
            this.srSpoof.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
