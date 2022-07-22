using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SilkroadServerManager
{
	public class itemmall : Form
	{
		private SqlConnection sqlconnect = new SqlConnection(fonksiyonlar.connect_string("Cert\\ini\\srShard.ini"));

		private string itemcodename = "";

		private IContainer components = null;

		private TreeView treeView1;

		private FlowLayoutPanel flowLayoutPanel1;

		private Label itemname;

		private Label label1;

		private Label label2;

		private Label label3;

		private TextBox gift;

		private TextBox point;

		private GroupBox groupBox1;

		private Button kaydet;

		private Button iptal;

		private Button sil;

		private GroupBox groupBox2;

		private Button ara;

		private TextBox itemara;

		private ListBox itemlist;

		private Label tabname;

		private Label label4;

		private Label label5;

		private Label label6;

		private TextBox miktar;

		private Button cancel;

		private TextBox silk;

		public itemmall()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			TreeNode treeNode = new TreeNode("Expendables");
			TreeNode treeNode2 = new TreeNode("Avatar");
			TreeNode treeNode3 = new TreeNode("Pet");
			TreeNode treeNode4 = new TreeNode("Premium");
			TreeNode treeNode5 = new TreeNode("Archemy");
			TreeNode treeNode6 = new TreeNode("Fellows");
			TreeNode treeNode7 = new TreeNode("Other");
			SqlCommand sqlCommand = new SqlCommand("SELECT CodeName128 FROM _RefShopTab where CodeName128 like 'MALL[_]%' and [Service] = 1", sqlconnect);
			sqlconnect.Open();
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				string text = sqlDataReader["CodeName128"].ToString();
				if (text.Contains("_CONSUME_"))
				{
					treeNode.Nodes.Add(text, text);
				}
				else if (text.Contains("_AVATAR_"))
				{
					treeNode2.Nodes.Add(text, text);
				}
				else if (text.Contains("_PET_"))
				{
					treeNode3.Nodes.Add(text, text);
				}
				else if (text.Contains("_PREMIUM_"))
				{
					treeNode4.Nodes.Add(text, text);
				}
				else if (text.Contains("_ARCHEMY_"))
				{
					treeNode5.Nodes.Add(text, text);
				}
				else if (text.Contains("_FELLOWS_"))
				{
					treeNode6.Nodes.Add(text, text);
				}
				else
				{
					treeNode7.Nodes.Add(text, text);
				}
			}
			sqlconnect.Close();
			treeView1.Nodes.Add(treeNode);
			treeView1.Nodes.Add(treeNode2);
			treeView1.Nodes.Add(treeNode3);
			treeView1.Nodes.Add(treeNode4);
			treeView1.Nodes.Add(treeNode5);
			treeView1.Nodes.Add(treeNode6);
			treeView1.Nodes.Add(treeNode7);
			if (Language.Default.language == "English")
			{
				this.Text = "Item Mall Editing";
				groupBox1.Text = "Item Mall Edit";
				tabname.Text = "Tab Name";
				itemname.Text = "Item Name";
				label6.Text = "Quantity :";
				label6.Location = new Point(373, 78);
				groupBox2.Text = "Add New Item";
				ara.Text = "Search";
				cancel.Text = "Close";
				iptal.Text = "Cancel";
				sil.Text = "Delete";
				kaydet.Text = "Save";
			}
		}

		private string sqlislem(string sqlkomut, string vericek)
		{
			try
			{
				sqlconnect.Open();
				SqlCommand sqlCommand = new SqlCommand(sqlkomut, sqlconnect);
				if (vericek != "")
				{
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					if (Language.Default.language == "English")
					{
						vericek = ((!sqlDataReader.Read()) ? "NONE" : sqlDataReader[vericek].ToString());
					}
					else
					{
						vericek = ((!sqlDataReader.Read()) ? "YOK" : sqlDataReader[vericek].ToString());
					}
				}
				else
				{
					sqlCommand.ExecuteNonQuery();
					if (Language.Default.language == "English")
					{
						vericek = "Successful !";
					}
					else
					{
						vericek = "Başarılı !";
					}
				}
				sqlconnect.Close();
			}
			catch (Exception ex)
			{
				vericek = "HATA";
				MessageBox.Show(ex.ToString());
				sqlconnect.Close();
			}
			return vericek;
		}

		private void flowpaneldoldur()
		{
			flowLayoutPanel1.Controls.Clear();
			silk.Clear();
			gift.Clear();
			point.Clear();
			miktar.Text = "0";
			miktar.Enabled = false;
			if (Language.Default.language == "English")
			{
				kaydet.Name = "Save";
				kaydet.Text = "Save";
			}
			else
			{
				kaydet.Name = "kaydet";
				kaydet.Text = "Kaydet";
			}
			itemcodename = "";
			tabname.Text = treeView1.SelectedNode.Name;
			SqlCommand sqlCommand = new SqlCommand("select a.RefPackageItemCodeName,b.NameStrID,REPLACE(b.AssocFileIcon,'.ddj','.png') as AssocFileIcon from _RefShopGoods a,_RefPackageItem b WHERE a.RefPackageItemCodeName=b.CodeName128 and a.[Service]=1 and a.RefTabCodeName='" + treeView1.SelectedNode.Name + "'", sqlconnect);
			sqlconnect.Open();
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				Button button = new Button();
				button.Size = new Size(40, 40);
				button.Name = sqlDataReader["NameStrID"].ToString();
				button.Tag = sqlDataReader["RefPackageItemCodeName"].ToString();
				button.Click += btn_Click;
				if (File.Exists("icon\\" + sqlDataReader["AssocFileIcon"]))
				{
					button.Image = Image.FromFile("icon\\" + sqlDataReader["AssocFileIcon"]);
				}
				else
				{
					button.Image = Image.FromFile("icon\\icon_default.png");
				}
				flowLayoutPanel1.Controls.Add(button);
			}
			sqlconnect.Close();
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeView1.SelectedNode.Name != "")
			{
				flowpaneldoldur();
			}
		}

		private void btn_Click(object sender, EventArgs e)
		{
			flowLayoutPanel1.Controls.Clear();
			PictureBox pictureBox = new PictureBox();
			pictureBox.Size = new Size(64, 64);
			pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox.Image = ((Button)sender).Image;
			flowLayoutPanel1.Controls.Add(pictureBox);
			flowLayoutPanel1.Cursor = Cursors.UpArrow;
			itemcodename = ((Button)sender).Tag.ToString();
			itemname.Text = itemcodename;
			silk.Text = sqlislem("SELECT Cost FROM _RefPricePolicyOfItem WHERE PaymentDevice = 2 and RefPackageItemCodeName='" + itemcodename + "'", "Cost");
			string text = sqlislem("SELECT Cost FROM _RefPricePolicyOfItem WHERE PaymentDevice = 4 and RefPackageItemCodeName='" + itemcodename + "'", "Cost");
			string text2 = sqlislem("SELECT Cost FROM _RefPricePolicyOfItem WHERE PaymentDevice = 16 and RefPackageItemCodeName='" + itemcodename + "'", "Cost");
			if (Language.Default.language == "English")
            {
				if (text != "NONE")
				{
					gift.Text = text;
				}
				if (text2 != "NONE")
				{
					point.Text = text2;
				}
				if (sqlislem("select TypeID2 from _RefObjCommon where NameStrID128='" + ((Button)sender).Name + "'", "TypeID2") == "3")
				{
					string text3 = sqlislem("SELECT Data FROM _RefScrapOfPackageItem WHERE RefPackageItemCodeName='" + itemcodename + "'", "Data");
					miktar.Text = text3;
					miktar.Enabled = true;
				}
				kaydet.Name = "Save";
				kaydet.Text = "Save";
				sil.Text = "Delete";
				sil.Enabled = true;
			}
			else
            {
				if (text != "YOK")
				{
					gift.Text = text;
				}
				if (text2 != "YOK")
				{
					point.Text = text2;
				}
				if (sqlislem("select TypeID2 from _RefObjCommon where NameStrID128='" + ((Button)sender).Name + "'", "TypeID2") == "3")
				{
					string text3 = sqlislem("SELECT Data FROM _RefScrapOfPackageItem WHERE RefPackageItemCodeName='" + itemcodename + "'", "Data");
					miktar.Text = text3;
					miktar.Enabled = true;
				}
				kaydet.Name = "kaydet";
				kaydet.Text = "Kaydet";
				sil.Text = "Sil";
				sil.Enabled = true;
			}

		}

		private void sil_Click(object sender, EventArgs e)
		{
			if (itemcodename != "")
			{
				sqlislem("DELETE FROM _RefShopGoods WHERE RefPackageItemCodeName='" + itemcodename + "'", "");
				sqlislem("DELETE FROM _RefPricePolicyOfItem WHERE RefPackageItemCodeName='" + itemcodename + "'", "");
				sqlislem("DELETE FROM _RefScrapOfPackageItem WHERE RefPackageItemCodeName='" + itemcodename + "'", "");
				sqlislem("DELETE FROM _RefPackageItem WHERE CodeName128='" + itemcodename + "'", "");
				if (Language.Default.language == "English")
				{
					itemname.Text = "Successful !";
				}
				else
				{
					itemname.Text = "Başarılı !";
				}
				flowpaneldoldur();
			}
			else
			{
				if (Language.Default.language == "English")
				{
					itemname.Text = "Please select item !";
				}
				else
				{
					itemname.Text = "Lütfen item seçiniz !";
				}
			}
		}

		private void iptal_Click(object sender, EventArgs e)
		{
			flowpaneldoldur();
			if (Language.Default.language == "English")
			{
				itemname.Text = "item name";
			}
			else
			{
				itemname.Text = "item ismi";
			}
		}

		private void kaydet_Click(object sender, EventArgs e)
		{
			if (itemcodename == "")
			{
				if (Language.Default.language == "English")
				{
					itemname.Text = "Please select item !";
				}
				else
				{
					itemname.Text = "Lütfen item seçiniz !";
				}

				return;
			}
			if (treeView1.SelectedNode.Name == "")
			{
				if (Language.Default.language == "English")
				{
					tabname.Text = "Select Tab from Tab List";
				}
				else
				{
					tabname.Text = "Tab List 'den Tab Seçiniz";
				}
				return;
			}
			if (silk.Text == "")
			{
				if (Language.Default.language == "English")
				{
					itemname.Text = "Please Write Silk Fee";
				}
				else
				{
					itemname.Text = "Lütfen Silk Ücretini Yazınız";
				}
				silk.Focus();
				return;
			}
			if (gift.Text == "" && point.Text == "")
			{
				gift.Text = "0";
				point.Text = "0";
			}
			if (miktar.Text == "")
			{
				miktar.Text = "0";
			}
			if (Language.Default.language == "English")
			{
				if (((Button)sender).Name == "Save")
				{
					if (silk.Text != "")
					{
						sqlislem("UPDATE _RefPricePolicyOfItem SET Cost = " + silk.Text + " WHERE PaymentDevice = 2 and RefPackageItemCodeName='" + itemcodename + "'", "");
					}
					if (gift.Text != "")
					{
						sqlislem("UPDATE _RefPricePolicyOfItem SET Cost = " + gift.Text + " WHERE PaymentDevice = 4 and RefPackageItemCodeName='" + itemcodename + "'", "");
					}
					if (point.Text != "")
					{
						sqlislem("UPDATE _RefPricePolicyOfItem SET Cost = " + point.Text + " WHERE PaymentDevice = 16 and RefPackageItemCodeName='" + itemcodename + "'", "");
					}
					sqlislem("UPDATE _RefScrapOfPackageItem SET Data = " + miktar.Text + " WHERE RefPackageItemCodeName='" + itemcodename + "'", "");
					itemname.Text = "Successful !";
					flowpaneldoldur();
					return;
				}
			}
			else
			{
				if (((Button)sender).Name == "kaydet")
				{
					if (silk.Text != "")
					{
						sqlislem("UPDATE _RefPricePolicyOfItem SET Cost = " + silk.Text + " WHERE PaymentDevice = 2 and RefPackageItemCodeName='" + itemcodename + "'", "");
					}
					if (gift.Text != "")
					{
						sqlislem("UPDATE _RefPricePolicyOfItem SET Cost = " + gift.Text + " WHERE PaymentDevice = 4 and RefPackageItemCodeName='" + itemcodename + "'", "");
					}
					if (point.Text != "")
					{
						sqlislem("UPDATE _RefPricePolicyOfItem SET Cost = " + point.Text + " WHERE PaymentDevice = 16 and RefPackageItemCodeName='" + itemcodename + "'", "");
					}
					sqlislem("UPDATE _RefScrapOfPackageItem SET Data = " + miktar.Text + " WHERE RefPackageItemCodeName='" + itemcodename + "'", "");
					itemname.Text = "Başarılı !";
					flowpaneldoldur();
					return;
				}
			}
			string text = sqlislem("select ID from _RefShopObject", "ID");
			string text2 = sqlislem("select MAX(SlotIndex)+1 as slotindex from _RefShopGoods where RefTabCodeName='" + treeView1.SelectedNode.Name + "'", "slotindex");
			if (text2 == "")
			{
				text2 = "0";
			}
			if (Language.Default.language == "English")
			{
				if (sqlislem("select CodeName128 from _RefPackageItem where CodeName128 ='PACKAGE_" + itemcodename + "'", "CodeName128") == "NONE")
				{
					sqlislem("insert into _RefPackageItem SELECT 1, " + text + ", 'PACKAGE_' + CodeName128, 0, 'EXPAND_TERM_ALL', NameStrID128, DescStrID128, AssocFileIcon128, -1, 'xxx', -1, 'xxx', -1, 'xxx', -1, 'xxx' FROM [dbo].[_RefObjCommon] where CodeName128 = '" + itemcodename + "'", "");
					if (silk.Text != "")
					{
						sqlislem("insert into _RefPricePolicyOfItem values(1," + text + ",'PACKAGE_" + itemcodename + "',2,0," + silk.Text + ",-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					}
					if (gift.Text != "")
					{
						sqlislem("insert into _RefPricePolicyOfItem values(1," + text + ",'PACKAGE_" + itemcodename + "',4,0," + gift.Text + ",-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					}
					if (point.Text != "")
					{
						sqlislem("insert into _RefPricePolicyOfItem values(1," + text + ",'PACKAGE_" + itemcodename + "',16,0," + point.Text + ",-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					}
					sqlislem("insert into _RefScrapOfPackageItem values(1," + text + ",'PACKAGE_" + itemcodename + "','" + itemcodename + "',0,0," + miktar.Text + ",0,0,0,0,0,0,0,0,0,0,0,0,0,-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					sqlislem("insert into _RefShopGoods values (1," + text + ",'" + treeView1.SelectedNode.Name + "','PACKAGE_" + itemcodename + "'," + text2 + ",-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					itemname.Text = "Item Added";
					flowpaneldoldur();
				}
				else
				{
					itemname.Text = "The Item is already Added First, please click the Clear Button to delete the item.";
					sil.Text = "Clean";
					sil.Enabled = true;
					itemcodename = "PACKAGE_" + itemcodename;
				}
			}
			else
			{
				if (sqlislem("select CodeName128 from _RefPackageItem where CodeName128 ='PACKAGE_" + itemcodename + "'", "CodeName128") == "YOK")
				{
					sqlislem("insert into _RefPackageItem SELECT 1, " + text + ", 'PACKAGE_' + CodeName128, 0, 'EXPAND_TERM_ALL', NameStrID128, DescStrID128, AssocFileIcon128, -1, 'xxx', -1, 'xxx', -1, 'xxx', -1, 'xxx' FROM [dbo].[_RefObjCommon] where CodeName128 = '" + itemcodename + "'", "");
					if (silk.Text != "")
					{
						sqlislem("insert into _RefPricePolicyOfItem values(1," + text + ",'PACKAGE_" + itemcodename + "',2,0," + silk.Text + ",-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					}
					if (gift.Text != "")
					{
						sqlislem("insert into _RefPricePolicyOfItem values(1," + text + ",'PACKAGE_" + itemcodename + "',4,0," + gift.Text + ",-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					}
					if (point.Text != "")
					{
						sqlislem("insert into _RefPricePolicyOfItem values(1," + text + ",'PACKAGE_" + itemcodename + "',16,0," + point.Text + ",-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					}
					sqlislem("insert into _RefScrapOfPackageItem values(1," + text + ",'PACKAGE_" + itemcodename + "','" + itemcodename + "',0,0," + miktar.Text + ",0,0,0,0,0,0,0,0,0,0,0,0,0,-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					sqlislem("insert into _RefShopGoods values (1," + text + ",'" + treeView1.SelectedNode.Name + "','PACKAGE_" + itemcodename + "'," + text2 + ",-1,'xxx',-1,'xxx',-1,'xxx',-1,'xxx')", "");
					itemname.Text = "Item Eklendi ";
					flowpaneldoldur();
				}
				else
				{
					itemname.Text = "Item zaten Ekli Önce Temizle Butonuna basarak İtemi siliniz";
					sil.Text = "Temizle";
					sil.Enabled = true;
					itemcodename = "PACKAGE_" + itemcodename;
				}
			}
		}

		private void ara_Click(object sender, EventArgs e)
		{
			itemlist.Items.Clear();
			itemcodename = "";
			SqlCommand sqlCommand = new SqlCommand("SELECT CodeName128 FROM [dbo].[_RefObjCommon] where [Service] = 1 and TypeID1=3 and CodeName128 like '%" + itemara.Text + "%'", sqlconnect);
			sqlconnect.Open();
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				itemlist.Items.Add(sqlDataReader["CodeName128"]);
			}
			sqlconnect.Close();
		}

		private void itemlist_SelectedIndexChanged(object sender, EventArgs e)
		{
			silk.Clear();
			gift.Clear();
			point.Clear();
			itemcodename = itemlist.SelectedItem.ToString();
			string text = "icon\\" + sqlislem("SELECT REPLACE(AssocFileIcon128,'.ddj','.png') as AssocFileIcon128 FROM [dbo].[_RefObjCommon] where CodeName128='" + itemcodename + "'", "AssocFileIcon128");
			if (sqlislem("select TypeID2 from _RefObjCommon where TypeID1=3 and CodeName128='" + itemcodename + "'", "TypeID2") == "3")
			{
				miktar.Enabled = true;
				miktar.Text = "1";
			}
			else
			{
				miktar.Enabled = false;
				miktar.Text = "0";
			}
			flowLayoutPanel1.Controls.Clear();
			PictureBox pictureBox = new PictureBox();
			pictureBox.Size = new Size(64, 64);
			pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			if (File.Exists(text))
			{
				pictureBox.Image = Image.FromFile(text);
			}
			else
			{
				pictureBox.Image = Image.FromFile("icon\\icon_default.png");
			}
			flowLayoutPanel1.Controls.Add(pictureBox);
			itemname.Text = itemcodename;
			if (Language.Default.language == "English")
			{
				kaydet.Name = "add";
				kaydet.Text = "Add";
			}
			else
			{
				kaydet.Name = "ekle";
				kaydet.Text = "Ekle";
			}
			sil.Enabled = false;
		}

		private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
		{
			flowpaneldoldur();
			if (Language.Default.language == "English")
			{
				itemname.Text = "item name";
			}
			else
			{
				itemname.Text = "item ismi";
			}
			flowLayoutPanel1.Cursor = Cursors.Default;
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(itemmall));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.itemname = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gift = new System.Windows.Forms.TextBox();
            this.point = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.silk = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabname = new System.Windows.Forms.Label();
            this.miktar = new System.Windows.Forms.TextBox();
            this.kaydet = new System.Windows.Forms.Button();
            this.iptal = new System.Windows.Forms.Button();
            this.sil = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.itemlist = new System.Windows.Forms.ListBox();
            this.ara = new System.Windows.Forms.Button();
            this.itemara = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 34);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(226, 422);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.DarkGray;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(244, 34);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(115, 422);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseClick);
            // 
            // itemname
            // 
            this.itemname.AutoSize = true;
            this.itemname.BackColor = System.Drawing.SystemColors.Control;
            this.itemname.Location = new System.Drawing.Point(6, 49);
            this.itemname.MinimumSize = new System.Drawing.Size(480, 17);
            this.itemname.Name = "itemname";
            this.itemname.Size = new System.Drawing.Size(480, 17);
            this.itemname.TabIndex = 2;
            this.itemname.Text = "item ismi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Silk :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Gift Silk :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Point :";
            // 
            // gift
            // 
            this.gift.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gift.Location = new System.Drawing.Point(183, 75);
            this.gift.Name = "gift";
            this.gift.Size = new System.Drawing.Size(50, 22);
            this.gift.TabIndex = 7;
            // 
            // point
            // 
            this.point.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.point.Location = new System.Drawing.Point(306, 75);
            this.point.Name = "point";
            this.point.Size = new System.Drawing.Size(50, 22);
            this.point.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.silk);
            this.groupBox1.Controls.Add(this.gift);
            this.groupBox1.Controls.Add(this.tabname);
            this.groupBox1.Controls.Add(this.itemname);
            this.groupBox1.Controls.Add(this.miktar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.point);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(365, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(502, 106);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item Mall Düzenle";
            // 
            // silk
            // 
            this.silk.Location = new System.Drawing.Point(42, 75);
            this.silk.Name = "silk";
            this.silk.Size = new System.Drawing.Size(50, 22);
            this.silk.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Miktar :";
            // 
            // tabname
            // 
            this.tabname.AutoSize = true;
            this.tabname.BackColor = System.Drawing.SystemColors.Control;
            this.tabname.Location = new System.Drawing.Point(6, 22);
            this.tabname.MinimumSize = new System.Drawing.Size(480, 18);
            this.tabname.Name = "tabname";
            this.tabname.Size = new System.Drawing.Size(480, 18);
            this.tabname.TabIndex = 12;
            this.tabname.Text = "Tab ismi";
            // 
            // miktar
            // 
            this.miktar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.miktar.Location = new System.Drawing.Point(436, 75);
            this.miktar.Name = "miktar";
            this.miktar.Size = new System.Drawing.Size(50, 22);
            this.miktar.TabIndex = 2;
            // 
            // kaydet
            // 
            this.kaydet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kaydet.Location = new System.Drawing.Point(777, 124);
            this.kaydet.Name = "kaydet";
            this.kaydet.Size = new System.Drawing.Size(92, 35);
            this.kaydet.TabIndex = 3;
            this.kaydet.Text = "Kaydet";
            this.kaydet.UseVisualStyleBackColor = true;
            this.kaydet.Click += new System.EventHandler(this.kaydet_Click);
            // 
            // iptal
            // 
            this.iptal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.iptal.Location = new System.Drawing.Point(483, 124);
            this.iptal.Name = "iptal";
            this.iptal.Size = new System.Drawing.Size(92, 35);
            this.iptal.TabIndex = 10;
            this.iptal.Text = "İptal";
            this.iptal.UseVisualStyleBackColor = true;
            this.iptal.Click += new System.EventHandler(this.iptal_Click);
            // 
            // sil
            // 
            this.sil.Enabled = false;
            this.sil.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sil.Location = new System.Drawing.Point(659, 124);
            this.sil.Name = "sil";
            this.sil.Size = new System.Drawing.Size(92, 35);
            this.sil.TabIndex = 9;
            this.sil.Text = "Sil";
            this.sil.UseVisualStyleBackColor = true;
            this.sil.Click += new System.EventHandler(this.sil_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.itemlist);
            this.groupBox2.Controls.Add(this.ara);
            this.groupBox2.Controls.Add(this.itemara);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.Location = new System.Drawing.Point(365, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(502, 294);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Yeni Item Ekle";
            // 
            // itemlist
            // 
            this.itemlist.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.itemlist.FormattingEnabled = true;
            this.itemlist.ItemHeight = 16;
            this.itemlist.Location = new System.Drawing.Point(9, 61);
            this.itemlist.Name = "itemlist";
            this.itemlist.Size = new System.Drawing.Size(485, 228);
            this.itemlist.TabIndex = 3;
            this.itemlist.SelectedIndexChanged += new System.EventHandler(this.itemlist_SelectedIndexChanged);
            // 
            // ara
            // 
            this.ara.Location = new System.Drawing.Point(424, 29);
            this.ara.Name = "ara";
            this.ara.Size = new System.Drawing.Size(70, 26);
            this.ara.TabIndex = 1;
            this.ara.Text = "Ara";
            this.ara.UseVisualStyleBackColor = true;
            this.ara.Click += new System.EventHandler(this.ara_Click);
            // 
            // itemara
            // 
            this.itemara.Location = new System.Drawing.Point(9, 29);
            this.itemara.Name = "itemara";
            this.itemara.Size = new System.Drawing.Size(409, 26);
            this.itemara.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(45, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Item Mall Tab List";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(267, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Item List";
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cancel.Location = new System.Drawing.Point(365, 124);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(92, 35);
            this.cancel.TabIndex = 13;
            this.cancel.Text = "Kapat";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // itemmall
            // 
            this.AcceptButton = this.kaydet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(879, 461);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.kaydet);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.iptal);
            this.Controls.Add(this.sil);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(895, 500);
            this.MinimumSize = new System.Drawing.Size(895, 500);
            this.Name = "itemmall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Mall Düzenleme";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
