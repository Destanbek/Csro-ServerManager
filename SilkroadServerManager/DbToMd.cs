using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SilkroadServerManager
{
	public class DbToMd : Form
	{
		private string path = "media\\server_dep\\silkroad\\textdata\\";

		private SqlConnection connect = new SqlConnection(fonksiyonlar.connect_string("Cert\\ini\\srShard.ini"));

		private IContainer components = null;

		private BackgroundWorker backgroundWorker1;

		private Button start;

		private Button cancel;

		private CheckBox chardata;

		private CheckBox itemdata;

		private CheckBox marketsys;

		private ProgressBar progressBar1;

		private Label label1;

		private RichTextBox sonuc;

		private GroupBox groupBox1;

		private CheckBox teleport;

		private CheckBox skildata;

		private CheckBox npcpos;

		public DbToMd()
		{
			Control.CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
		}

		private void DbToM_Load(object sender, EventArgs e)
		{
            if (Language.Default.language == "English")
            {
                groupBox1.Text = "Select";
                start.Text = "Start";
                cancel.Text = "Close";
            }
        }

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			if (marketsys.Checked)
			{
				textdata("select * from _refpackageitem where service = 1", "refpackageitem.txt");
				textdata("select * from _refpricepolicyofitem where service = 1", "refpricepolicyofitem.txt");
				textdata("select * from _refscrapofpackageitem where service = 1", "refscrapofpackageitem.txt");
				textdata("select * from _refshop where service = 1", "refshop.txt");
				textdata("select * from _refshopgoods where service = 1", "refshopgoods.txt");
				textdata("select * from _refshopgroup where service = 1", "refshopgroup.txt");
				textdata("select * from _refshoptab where service = 1", "refshoptab.txt");
				textdata("select * from _refshoptabgroup where service = 1", "refshoptabgroup.txt");
				textdata("select * from _refmappingshopgroup where service = 1", "refmappingshopgroup.txt");
				textdata("select * from _refmappingshopwithtab where service = 1", "refmappingshopwithtab.txt");
			}
			if (chardata.Checked)
			{
				CharData();
			}
			if (itemdata.Checked)
			{
				ItemData();
			}
			if (skildata.Checked)
			{
				SkillData();
			}
			if (teleport.Checked)
			{
				textdata("select Service,ID,CodeName128,ObjName128,OrgObjCodeName128,NameStrID128,DescStrID128,CashItem,Bionic,TypeID1,TypeID2,TypeID3,TypeID4,DecayTime,Country,Rarity,CanTrade,CanSell,CanBuy,CanBorrow,CanDrop,CanPick,CanRepair,CanRevive,CanUse,CanThrow,Price,CostRepair,CostRevive,CostBorrow,KeepingFee,SellPrice,ReqLevelType1,ReqLevel1,ReqLevelType2,ReqLevel2,ReqLevelType3,ReqLevel3,ReqLevelType4,ReqLevel4,MaxContain,RegionID,Dir,OffsetX,OffsetY,OffsetZ,Speed1,Speed2,Scale,BCHeight,BCRadius,EventID,AssocFileObj128,AssocFileDrop128,AssocFileIcon128,AssocFile1_128,AssocFile2_128,0 from _RefObjCommon where TypeID1=4 and Service = 1", "teleportbuilding.txt");
				textdata("select Service,ID,rtrim(CodeName128),AssocRefObjID,ZoneName128,GenRegionID,GenPos_X,GenPos_Y,GenPos_Z,GenAreaRadius,CanBeResurrectPos,CanGotoResurrectPos,GenWorldID from _RefTeleport where Service = 1", "teleportdata.txt");
				textdata("select Service,OwnerTeleport,TargetTeleport,Fee,0,0,Restrict1,Data1_1,Data1_2,Restrict2,Data2_1,Data2_2,Restrict3,Data3_1,Data3_2,Restrict4,Data4_1,Data4_2,Restrict5,Data5_1,Data5_2 from _RefTeleLink where Service = 1", "teleportlink.txt");
				textdata("select * from _RefOptionalTeleport where service=1", "refoptionalteleport.txt");
			}
			if (npcpos.Checked)
			{
				textdata("select dwObjID, nRegionDbID,replace(convert(decimal(18,7), fLocalPosX),'0000000','0'),replace(convert(decimal(18,7), fLocalPosY),'0000000','0'),replace(convert(decimal(18,7), fLocalPosZ),'0000000','0') from Tab_RefNest inner join tab_reftactics on tab_refnest.dwtacticsid = tab_reftactics.dwtacticsid", "npcpos.txt");
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
            if (Language.Default.language == "English")
            {
                label1.Text = "Successfull";
            }
            else
            {
                label1.Text = "Başarılı";
            }
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void start_Click(object sender, EventArgs e)
		{
			backgroundWorker1.RunWorkerAsync();
		}

		private void CharData()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (int i = 5000; i <= 105000; i += 5000)
			{
				stringBuilder.AppendLine("characterdata_" + i + ".txt");
				textdata("select a.Service,a.ID,a.CodeName128,a.ObjName128,a.OrgObjCodeName128,a.NameStrID128,a.DescStrID128,a.CashItem,a.Bionic,a.TypeID1,a.TypeID2,a.TypeID3,a.TypeID4,a.DecayTime,a.Country,a.Rarity,a.CanTrade,a.CanSell,a.CanBuy,a.CanBorrow,a.CanDrop,a.CanPick,a.CanRepair,a.CanRevive,a.CanUse,a.CanThrow,a.Price,a.CostRepair,a.CostRevive,a.CostBorrow,a.KeepingFee,a.SellPrice,a.ReqLevelType1,a.ReqLevel1,a.ReqLevelType2,a.ReqLevel2,a.ReqLevelType3,a.ReqLevel3,a.ReqLevelType4,a.ReqLevel4,a.MaxContain,a.RegionID,a.Dir,a.OffsetX,a.OffsetY,a.OffsetZ,a.Speed1,a.Speed2,a.Scale,a.BCHeight,a.BCRadius,a.EventID,a.AssocFileObj128,a.AssocFileDrop128,a.AssocFileIcon128,a.AssocFile1_128,a.AssocFile2_128,b.Lvl,b.CharGender,b.MaxHP,b.MaxMP,b.InventorySize,b.CanStore_TID1,b.CanStore_TID2,b.CanStore_TID3,b.CanStore_TID4,b.CanBeVehicle,b.CanControl,b.DamagePortion,b.MaxPassenger,b.AssocTactics,b.PD,b.MD,b.PAR,b.MAR,b.ER,b.BR,b.HR,b.CHR,b.ExpToGive,b.CreepType,b.Knockdown,b.KO_RecoverTime,b.DefaultSkill_1,b.DefaultSkill_2,b.DefaultSkill_3,b.DefaultSkill_4,b.DefaultSkill_5,b.DefaultSkill_6,b.DefaultSkill_7,b.DefaultSkill_8,b.DefaultSkill_9,b.DefaultSkill_10,b.TextureType,b.Except_1,b.Except_2,b.Except_3,b.Except_4,b.Except_5,b.Except_6,b.Except_7,b.Except_8,b.Except_9,b.Except_10 from _RefObjCommon a, _RefObjChar b where a.Link = b.ID and a.Bionic = 1 and a.Service = 1 and a.ID > " + num + " and a.ID < " + i + "  order by a.ID", "characterdata_" + i + ".txt");
				num = i - 1;
			}
			File.WriteAllText(path + "characterdata.txt", stringBuilder.ToString(), Encoding.Unicode);
			stringBuilder.Clear();
		}

		private void ItemData()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (int i = 5000; i <= 105000; i += 5000)
			{
				stringBuilder.AppendLine("itemdata_" + i + ".txt");
				textdata("SELECT a.Service,a.ID,a.CodeName128,a.ObjName128,a.OrgObjCodeName128,a.NameStrID128,a.DescStrID128,a.CashItem,a.Bionic,a.TypeID1,a.TypeID2,a.TypeID3,a.TypeID4,a.DecayTime,a.Country,a.Rarity,a.CanTrade,a.CanSell,a.CanBuy,a.CanBorrow,a.CanDrop,a.CanPick,a.CanRepair,a.CanRevive,a.CanUse,a.CanThrow,a.Price,a.CostRepair,a.CostRevive,a.CostBorrow,a.KeepingFee,a.SellPrice,a.ReqLevelType1,a.ReqLevel1,a.ReqLevelType2,a.ReqLevel2,a.ReqLevelType3,a.ReqLevel3,a.ReqLevelType4,a.ReqLevel4,a.MaxContain,a.RegionID,a.Dir     ,a.OffsetX,a.OffsetY,a.OffsetZ,a.Speed1,a.Speed2,a.Scale     ,a.BCHeight,a.BCRadius,a.EventID,a.AssocFileObj128,a.AssocFileDrop128,a.AssocFileIcon128     ,a.AssocFile1_128,a.AssocFile2_128,b.MaxStack     ,b.ReqGender,b.ReqStr,b.ReqInt,b.ItemClass,b.SetID,replace(convert(decimal(18,7), b.Dur_L),'0000000','0'),replace(convert(decimal(18,7), b.Dur_U),'0000000','0'),replace(convert(decimal(18,7), b.PD_L),'0000000','0'),replace(convert(decimal(18,7), b.PD_U),'0000000','0'),replace(convert(decimal(18,7), b.PDInc),'0000000','0')     ,replace(convert(decimal(18,7), b.ER_L),'0000000','0')     ,replace(convert(decimal(18,7), b.ER_U),'0000000','0'),replace(convert(decimal(18,7), b.ERInc),'0000000','0'),replace(convert(decimal(18,7), b.PAR_L),'0000000','0'),replace(convert(decimal(18,7), b.PAR_U),'0000000','0'),replace(convert(decimal(18,7), b.PARInc),'0000000','0'),replace(convert(decimal(18,7), b.BR_L),'0000000','0'),replace(convert(decimal(18,7), b.BR_U),'0000000','0'),replace(convert(decimal(18,7), b.MD_L),'0000000','0')     ,replace(convert(decimal(18,7), b.MD_U),'0000000','0'),replace(convert(decimal(18,7), b.MDInc),'0000000','0'),replace(convert(decimal(18,7), b.MAR_L),'0000000','0'),replace(convert(decimal(18,7), b.MAR_U),'0000000','0'),replace(convert(decimal(18,7), b.MARInc),'0000000','0')     ,replace(convert(decimal(18,7), b.PDStr_L),'0000000','0'),replace(convert(decimal(18,7), b.PDStr_U),'0000000','0')     ,replace(convert(decimal(18,7), b.MDInt_L),'0000000','0')     ,replace(convert(decimal(18,7), b.MDInt_U),'0000000','0'),b.Quivered,b.Ammo1_TID4,b.Ammo2_TID4     ,b.Ammo3_TID4,b.Ammo4_TID4,b.Ammo5_TID4,b.SpeedClass,b.TwoHanded,b.Range,replace(convert(decimal(18,7), b.PAttackMin_L),'0000000','0'),replace(convert(decimal(18,7), b.PAttackMin_U),'0000000','0'),replace(convert(decimal(18,7), b.PAttackMax_L),'0000000','0')     ,replace(convert(decimal(18,7), b.PAttackMax_U),'0000000','0'),replace(convert(decimal(18,7), b.PAttackInc),'0000000','0'),replace(convert(decimal(18,7), b.MAttackMin_L),'0000000','0'),replace(convert(decimal(18,7), b.MAttackMin_U),'0000000','0'),replace(convert(decimal(18,7), b.MAttackMax_L),'0000000','0'),replace(convert(decimal(18,7), b.MAttackMax_U),'0000000','0'),replace(convert(decimal(18,7), b.MAttackInc),'0000000','0'),replace(convert(decimal(18,7), b.PAStrMin_L),'0000000','0'),replace(convert(decimal(18,7), b.PAStrMin_U),'0000000','0'),replace(convert(decimal(18,7), b.PAStrMax_L),'0000000','0'),replace(convert(decimal(18,7), b.PAStrMax_U),'0000000','0'),replace(convert(decimal(18,7), b.MAInt_Min_L),'0000000','0'),replace(convert(decimal(18,7), b.MAInt_Min_U),'0000000','0'),replace(convert(decimal(18,7), b.MAInt_Max_L),'0000000','0')     ,replace(convert(decimal(18,7), b.MAInt_Max_U),'0000000','0')     ,replace(convert(decimal(18,7), b.HR_L),'0000000','0')     ,replace(convert(decimal(18,7), b.HR_U),'0000000','0')  ,replace(convert(decimal(18,7), b.HRInc),'0000000','0'),replace(convert(decimal(18,7), b.CHR_L),'0000000','0'),replace(convert(decimal(18,7), b.CHR_U),'0000000','0'),b.Param1,b.Desc1_128,b.Param2,RTRIM(b.Desc2_128),b.Param3,b.Desc3_128,b.Param4,b.Desc4_128,b.Param5,b.Desc5_128,b.Param6,b.Desc6_128,b.Param7,b.Desc7_128,b.Param8,b.Desc8_128     ,b.Param9     ,b.Desc9_128,b.Param10,b.Desc10_128,b.Param11,b.Desc11_128,b.Param12,b.Desc12_128,b.Param13,b.Desc13_128,b.Param14,b.Desc14_128,b.Param15,b.Desc15_128,b.Param16,b.Desc16_128,b.Param17,b.Desc17_128,b.Param18,b.Desc18_128,b.Param19,b.Desc19_128,b.Param20     ,b.Desc20_128,b.MaxMagicOptCount,b.ChildItemCount  FROM _RefObjCommon a , _RefObjItem b where a.Link = b.ID and a.TypeID1 = 3 and a.[Service] = 1 and a.ID > " + num + " and a.ID < " + i + "order by a.ID", "itemdata_" + i + ".txt");
				num = i - 1;
			}
			File.WriteAllText(path + "itemdata.txt", stringBuilder.ToString(), Encoding.Unicode);
			stringBuilder.Clear();
		}

		private void SkillData()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (int i = 5000; i <= 105000; i += 5000)
			{
				stringBuilder.AppendLine("skilldata_" + i + ".txt");
				textdata("SELECT * FROM _RefSkill where [Service] = 1 and ID > " + num + " and ID < " + i + "order by ID", "skilldata_" + i + ".txt");
				num = i - 1;
			}
			File.WriteAllText(path + "skilldata.txt", stringBuilder.ToString(), Encoding.Unicode);
			stringBuilder.Clear();
		}

		private void textdata(string sqlkomut, string txtname)
		{
			int num = 0;
			label1.Text = txtname;
			try
			{
				connect.Open();
				SqlCommand selectCommand = new SqlCommand(sqlkomut, connect);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				progressBar1.Maximum = dataTable.Rows.Count;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (DataRow row in dataTable.Rows)
				{
					stringBuilder.AppendLine(string.Join("\t", row.ItemArray));
					num++;
					backgroundWorker1.ReportProgress(num);
				}
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				File.WriteAllText(path + txtname, stringBuilder.ToString(), Encoding.Unicode);
				stringBuilder.Clear();
				connect.Close();
			}
			catch (Exception ex)
			{
				sonuc.Select(sonuc.Text.Length, 0);
				sonuc.SelectionColor = Color.Red;
				sonuc.DeselectAll();
				sonuc.AppendText(ex.Message + "\n");
				connect.Close();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbToMd));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.start = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.chardata = new System.Windows.Forms.CheckBox();
            this.itemdata = new System.Windows.Forms.CheckBox();
            this.marketsys = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.sonuc = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.npcpos = new System.Windows.Forms.CheckBox();
            this.skildata = new System.Windows.Forms.CheckBox();
            this.teleport = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.start.Location = new System.Drawing.Point(125, 12);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 53);
            this.start.TabIndex = 0;
            this.start.Text = "Başla";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cancel.Location = new System.Drawing.Point(397, 12);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 53);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Kapat";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // chardata
            // 
            this.chardata.AutoSize = true;
            this.chardata.Location = new System.Drawing.Point(6, 19);
            this.chardata.Name = "chardata";
            this.chardata.Size = new System.Drawing.Size(95, 17);
            this.chardata.TabIndex = 2;
            this.chardata.Text = "CharacterData";
            this.chardata.UseVisualStyleBackColor = true;
            // 
            // itemdata
            // 
            this.itemdata.AutoSize = true;
            this.itemdata.Location = new System.Drawing.Point(6, 42);
            this.itemdata.Name = "itemdata";
            this.itemdata.Size = new System.Drawing.Size(69, 17);
            this.itemdata.TabIndex = 3;
            this.itemdata.Text = "ItemData";
            this.itemdata.UseVisualStyleBackColor = true;
            // 
            // marketsys
            // 
            this.marketsys.AutoSize = true;
            this.marketsys.Location = new System.Drawing.Point(6, 88);
            this.marketsys.Name = "marketsys";
            this.marketsys.Size = new System.Drawing.Size(96, 17);
            this.marketsys.TabIndex = 4;
            this.marketsys.Text = "Market System";
            this.marketsys.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(125, 71);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(347, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // sonuc
            // 
            this.sonuc.Location = new System.Drawing.Point(125, 100);
            this.sonuc.Name = "sonuc";
            this.sonuc.ReadOnly = true;
            this.sonuc.Size = new System.Drawing.Size(347, 249);
            this.sonuc.TabIndex = 8;
            this.sonuc.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.npcpos);
            this.groupBox1.Controls.Add(this.skildata);
            this.groupBox1.Controls.Add(this.teleport);
            this.groupBox1.Controls.Add(this.marketsys);
            this.groupBox1.Controls.Add(this.itemdata);
            this.groupBox1.Controls.Add(this.chardata);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 337);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seç";
            // 
            // npcpos
            // 
            this.npcpos.AutoSize = true;
            this.npcpos.Location = new System.Drawing.Point(6, 134);
            this.npcpos.Name = "npcpos";
            this.npcpos.Size = new System.Drawing.Size(64, 17);
            this.npcpos.TabIndex = 12;
            this.npcpos.Text = "NpcPos";
            this.npcpos.UseVisualStyleBackColor = true;
            // 
            // skildata
            // 
            this.skildata.AutoSize = true;
            this.skildata.Location = new System.Drawing.Point(6, 65);
            this.skildata.Name = "skildata";
            this.skildata.Size = new System.Drawing.Size(71, 17);
            this.skildata.TabIndex = 11;
            this.skildata.Text = "Skill Data";
            this.skildata.UseVisualStyleBackColor = true;
            // 
            // teleport
            // 
            this.teleport.AutoSize = true;
            this.teleport.Location = new System.Drawing.Point(6, 111);
            this.teleport.Name = "teleport";
            this.teleport.Size = new System.Drawing.Size(65, 17);
            this.teleport.TabIndex = 10;
            this.teleport.Text = "Teleport";
            this.teleport.UseVisualStyleBackColor = true;
            // 
            // DbToMd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sonuc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 400);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "DbToMd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database To Media.pk2";
            this.Load += new System.EventHandler(this.DbToM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
