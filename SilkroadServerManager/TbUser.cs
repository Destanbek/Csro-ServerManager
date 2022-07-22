using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SilkroadServerManager
{
	public class TbUser : Form
	{
		private SqlConnection sqlcon = new SqlConnection(fonksiyonlar.connect_string("Cert\\ini\\srGlobalService.ini"));

		private string jid;

		private string islem = "newuser";

		private IContainer components = null;

		private TextBox username;

		private TextBox password;

		private TextBox primar;

		private Label label6;

		private Label label5;

		private Label label4;

		private Label label3;

		private TextBox content;

		private TextBox user_bul;

		private Button bul;

		private Button kaydet;

		private GroupBox groupBox1;

		private TextBox silk_own;

		private Label label1;

		private Button iptal;

		private Button Cancel;

		private RichTextBox sonuc;

		public TbUser()
		{
			InitializeComponent();
		}

		private void TbUser_Load(object sender, EventArgs e)
		{
			varsayilan();
			if (Language.Default.language == "English")
			{
				this.Text = "Add TB_User Edit";
				groupBox1.Text = "Edit or Add New User";
				bul.Text = "Find";
				label3.Text = "User name";
				label4.Text = "Password";
				label5.Text = "Primary Group";
				label6.Text = "Content Group";
				password.Text = "MD5 Encrypted";
				Cancel.Text = "Exit";
				iptal.Text = "Cancel";
				kaydet.Text = "Save";
			}
		}

		private string getMD5(string str)
		{
			MD5 mD = MD5.Create();
			byte[] array = mD.ComputeHash(Encoding.Default.GetBytes(str));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		private string sqlislem(string sqlkomut, string vericek)
		{
			try
			{
				sqlcon.Open();
				SqlCommand sqlCommand = new SqlCommand(sqlkomut, sqlcon);
				sqlCommand.ExecuteNonQuery();
				if (vericek != "")
				{
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					vericek = ((!sqlDataReader.Read()) ? "" : sqlDataReader[vericek].ToString());
				}
				else
				{
					if (Language.Default.language == "English")
					{
						vericek = "Successful !";
					}
					else
					{
						vericek = "Başarılı !";
					}
				}
				sqlcon.Close();
			}
			catch (Exception ex)
			{
				sonuc.Select(sonuc.Text.Length, 0);
				sonuc.SelectionColor = Color.Red;
				sonuc.DeselectAll();
				sonuc.AppendText(ex.Message + "\n");
				if (Language.Default.language == "English")
				{
					vericek = "ERROR";
				}
				else
				{
					vericek = "HATA";
				}
				sqlcon.Close();
			}
			return vericek;
		}

		private void bul_Click(object sender, EventArgs e)
		{
			if (Language.Default.language == "English")
			{
				sonuc.Clear();
				if (user_bul.Text != "")
				{
					user_bul.BackColor = SystemColors.Window;
					jid = sqlislem("Select * from TB_User where StrUserID='" + user_bul.Text + "'", "JID");
					if (!(jid == "ERROR"))
					{
						if (jid == "")
						{
							sonuc.Select(sonuc.Text.Length, 0);
							sonuc.SelectionColor = Color.Red;
							sonuc.DeselectAll();
							sonuc.AppendText("User not found\nUsername may be incorrect\n");
							varsayilan();
						}
						else
						{
							username.Text = sqlislem("Select * from TB_User where JID='" + jid + "'", "StrUserID");
							primar.Text = sqlislem("Select * from TB_User where JID='" + jid + "'", "sec_primary");
							content.Text = sqlislem("Select * from TB_User where JID='" + jid + "'", "sec_content");
							silk_own.Text = sqlislem("Select * from SK_Silk where JID='" + jid + "'", "silk_own");
							kaydet.Text = "Fix it";
							islem = "edituser";
						}
					}
				}
				else
				{
					user_bul.BackColor = Color.Coral;
					user_bul.Focus();
				}
			}
			else
			{
				sonuc.Clear();
				if (user_bul.Text != "")
				{
					user_bul.BackColor = SystemColors.Window;
					jid = sqlislem("Select * from TB_User where StrUserID='" + user_bul.Text + "'", "JID");
					if (!(jid == "HATA"))
					{
						if (jid == "")
						{
							sonuc.Select(sonuc.Text.Length, 0);
							sonuc.SelectionColor = Color.Red;
							sonuc.DeselectAll();
							sonuc.AppendText("Kullanıcı bulunamadı\nKullanıcı adı hatalı olabilir\n");
							varsayilan();
						}
						else
						{
							username.Text = sqlislem("Select * from TB_User where JID='" + jid + "'", "StrUserID");
							primar.Text = sqlislem("Select * from TB_User where JID='" + jid + "'", "sec_primary");
							content.Text = sqlislem("Select * from TB_User where JID='" + jid + "'", "sec_content");
							silk_own.Text = sqlislem("Select * from SK_Silk where JID='" + jid + "'", "silk_own");
							kaydet.Text = "Düzelt";
							islem = "edituser";
						}
					}
				}
				else
				{
					user_bul.BackColor = Color.Coral;
					user_bul.Focus();
				}
			}
		}

		private void kaydet_Click(object sender, EventArgs e)
		{
			if (Language.Default.language == "English")
			{
				sonuc.Clear();
				username.BackColor = SystemColors.Window;
				primar.BackColor = SystemColors.Window;
				content.BackColor = SystemColors.Window;
				password.BackColor = SystemColors.Window;
				if (username.Text == "")
				{
					username.BackColor = Color.Coral;
					username.Focus();
					return;
				}
				if (primar.Text == "")
				{
					primar.BackColor = Color.Coral;
					primar.Focus();
					return;
				}
				if (content.Text == "")
				{
					content.BackColor = Color.Coral;
					content.Focus();
					return;
				}
				if (silk_own.Text == "")
				{
					silk_own.Text = "0";
				}
				if (islem == "edituser" && jid != null && jid != "ERROR")
				{
					string a = sqlislem("Select * from SK_Silk where JID='" + jid + "'", "silk_own");
					if (a != "")
					{
						sonuc.AppendText("Silk update --->" + sqlislem("update SK_Silk set silk_own='" + silk_own.Text + "'  where JID='" + jid + "'", "") + "\n");
					}
					else
					{
						sonuc.AppendText("Silk insert --->" + sqlislem("insert into SK_Silk (JID, silk_own) values (" + jid + "," + silk_own.Text + ")", "") + "\n");
					}
					if (password.Text != "MD5 Encrypted" && password.Text != "")
					{
						sonuc.AppendText("TB_User update --->" + sqlislem("update TB_User set StrUserID='" + username.Text + "', password='" + getMD5(password.Text) + "', sec_primary='" + primar.Text + "', sec_content='" + content.Text + "' where JID='" + jid + "'", "") + "\n");
						jid = null;
					}
					else
					{
						sonuc.AppendText("TB_User update --->" + sqlislem("update TB_User set StrUserID='" + username.Text + "', sec_primary='" + primar.Text + "', sec_content='" + content.Text + "' where JID='" + jid + "'", "") + "\n");
						jid = null;
					}
				}
				if (islem == "newuser")
				{
					if (password.Text == "MD5 Encrypted" || password.Text == "")
					{
						password.BackColor = Color.Coral;
						password.Focus();
						return;
					}
					string sqlkomut = "insert into TB_User (StrUserID, password, certificate_num, sec_primary, sec_content) values ('" + username.Text + "', '" + getMD5(password.Text) + "', 203103198405099999, " + primar.Text + ", " + content.Text + ")";
					string a2 = sqlislem("select * from TB_User where StrUserID='" + username.Text + "'", "StrUserID");
					if (a2 == "")
					{
						sonuc.AppendText("New TB_User insert --->" + sqlislem(sqlkomut, "") + "\n");
						jid = sqlislem("select * from TB_User where StrUserID='" + username.Text + "'", "JID");
						string a = sqlislem("Select * from SK_Silk where JID='" + jid + "'", "silk_own");
						if (a != "")
						{
							sonuc.AppendText("Silk update --->" + sqlislem("update SK_Silk set silk_own='" + silk_own.Text + "'  where JID='" + jid + "'", "") + "\n");
						}
						else
						{
							sonuc.AppendText("Silk insert --->" + sqlislem("insert into SK_Silk (JID, silk_own) values (" + jid + "," + silk_own.Text + ")", "") + "\n");
						}
					}
					else if (a2 == "ERROR")
					{
						sonuc.Select(sonuc.Text.Length, 0);
						sonuc.SelectionColor = Color.Red;
						sonuc.DeselectAll();
						sonuc.AppendText("SQL Connection Failed !\n");
					}
					else
					{
						sonuc.Select(sonuc.Text.Length, 0);
						sonuc.SelectionColor = Color.Red;
						sonuc.DeselectAll();
						sonuc.AppendText(username.Text + " ---> Not available !\n");
					}
				}

			}
			else
			{
				sonuc.Clear();
				username.BackColor = SystemColors.Window;
				primar.BackColor = SystemColors.Window;
				content.BackColor = SystemColors.Window;
				password.BackColor = SystemColors.Window;
				if (username.Text == "")
				{
					username.BackColor = Color.Coral;
					username.Focus();
					return;
				}
				if (primar.Text == "")
				{
					primar.BackColor = Color.Coral;
					primar.Focus();
					return;
				}
				if (content.Text == "")
				{
					content.BackColor = Color.Coral;
					content.Focus();
					return;
				}
				if (silk_own.Text == "")
				{
					silk_own.Text = "0";
				}
				if (islem == "edituser" && jid != null && jid != "HATA")
				{
					string a = sqlislem("Select * from SK_Silk where JID='" + jid + "'", "silk_own");
					if (a != "")
					{
						sonuc.AppendText("Silk update --->" + sqlislem("update SK_Silk set silk_own='" + silk_own.Text + "'  where JID='" + jid + "'", "") + "\n");
					}
					else
					{
						sonuc.AppendText("Silk insert --->" + sqlislem("insert into SK_Silk (JID, silk_own) values (" + jid + "," + silk_own.Text + ")", "") + "\n");
					}
					if (password.Text != "MD5 Şifreli" && password.Text != "")
					{
						sonuc.AppendText("TB_User update --->" + sqlislem("update TB_User set StrUserID='" + username.Text + "', password='" + getMD5(password.Text) + "', sec_primary='" + primar.Text + "', sec_content='" + content.Text + "' where JID='" + jid + "'", "") + "\n");
						jid = null;
					}
					else
					{
						sonuc.AppendText("TB_User update --->" + sqlislem("update TB_User set StrUserID='" + username.Text + "', sec_primary='" + primar.Text + "', sec_content='" + content.Text + "' where JID='" + jid + "'", "") + "\n");
						jid = null;
					}
				}
				if (islem == "newuser")
				{
					if (password.Text == "MD5 Şifreli" || password.Text == "")
					{
						password.BackColor = Color.Coral;
						password.Focus();
						return;
					}
					string sqlkomut = "insert into TB_User (StrUserID, password, certificate_num, sec_primary, sec_content) values ('" + username.Text + "', '" + getMD5(password.Text) + "', 203103198405099999, " + primar.Text + ", " + content.Text + ")";
					string a2 = sqlislem("select * from TB_User where StrUserID='" + username.Text + "'", "StrUserID");
					if (a2 == "")
					{
						sonuc.AppendText("New TB_User insert --->" + sqlislem(sqlkomut, "") + "\n");
						jid = sqlislem("select * from TB_User where StrUserID='" + username.Text + "'", "JID");
						string a = sqlislem("Select * from SK_Silk where JID='" + jid + "'", "silk_own");
						if (a != "")
						{
							sonuc.AppendText("Silk update --->" + sqlislem("update SK_Silk set silk_own='" + silk_own.Text + "'  where JID='" + jid + "'", "") + "\n");
						}
						else
						{
							sonuc.AppendText("Silk insert --->" + sqlislem("insert into SK_Silk (JID, silk_own) values (" + jid + "," + silk_own.Text + ")", "") + "\n");
						}
					}
					else if (a2 == "HATA")
					{
						sonuc.Select(sonuc.Text.Length, 0);
						sonuc.SelectionColor = Color.Red;
						sonuc.DeselectAll();
						sonuc.AppendText("SQL Bağlantısı Başarısız!\n");
					}
					else
					{
						sonuc.Select(sonuc.Text.Length, 0);
						sonuc.SelectionColor = Color.Red;
						sonuc.DeselectAll();
						sonuc.AppendText(username.Text + " ---> Müsait değil !\n");
					}
				}

			}
			varsayilan();
		}

		private void iptal_Click(object sender, EventArgs e)
		{
			varsayilan();
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void password_MouseClick(object sender, MouseEventArgs e)
		{
			password.Text = "";
		}

		private void password_Leave(object sender, EventArgs e)
		{
			if (password.Text == "")
			{
				if (Language.Default.language == "English")
				{
					password.Text = "MD5 Encrypted";
				}
				else
				{
					password.Text = "MD5 Şifreli";
				}
				username.Focus();
			}
		}

		private void varsayilan()
		{
			if (Language.Default.language == "English")
			{
				user_bul.Text = "";
				username.Text = "";
				password.Text = "MD5 Encrypted";
				primar.Text = "3";
				content.Text = "3";
				silk_own.Text = "0";
				kaydet.Text = "Save";
				islem = "newuser";
				jid = null;
			}
			else
			{
				user_bul.Text = "";
				username.Text = "";
				password.Text = "MD5 Şifreli";
				primar.Text = "3";
				content.Text = "3";
				silk_own.Text = "0";
				kaydet.Text = "Kaydet";
				islem = "newuser";
				jid = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TbUser));
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.primar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.content = new System.Windows.Forms.TextBox();
            this.user_bul = new System.Windows.Forms.TextBox();
            this.bul = new System.Windows.Forms.Button();
            this.kaydet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.silk_own = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.iptal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sonuc = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(138, 57);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(331, 26);
            this.username.TabIndex = 3;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(138, 89);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(331, 26);
            this.password.TabIndex = 4;
            this.password.Text = "MD5 Şifreli";
            this.password.MouseClick += new System.Windows.Forms.MouseEventHandler(this.password_MouseClick);
            this.password.Leave += new System.EventHandler(this.password_Leave);
            // 
            // primar
            // 
            this.primar.Location = new System.Drawing.Point(138, 121);
            this.primar.Name = "primar";
            this.primar.Size = new System.Drawing.Size(45, 26);
            this.primar.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 19);
            this.label6.TabIndex = 15;
            this.label6.Text = "Content Grubu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "Primary Grubu";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Şifre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Kullanıcı adı";
            // 
            // content
            // 
            this.content.Location = new System.Drawing.Point(138, 153);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(45, 26);
            this.content.TabIndex = 6;
            // 
            // user_bul
            // 
            this.user_bul.Location = new System.Drawing.Point(6, 25);
            this.user_bul.Name = "user_bul";
            this.user_bul.Size = new System.Drawing.Size(375, 26);
            this.user_bul.TabIndex = 1;
            // 
            // bul
            // 
            this.bul.Location = new System.Drawing.Point(387, 25);
            this.bul.Name = "bul";
            this.bul.Size = new System.Drawing.Size(82, 26);
            this.bul.TabIndex = 2;
            this.bul.Text = "Bul";
            this.bul.UseVisualStyleBackColor = true;
            this.bul.Click += new System.EventHandler(this.bul_Click);
            // 
            // kaydet
            // 
            this.kaydet.Location = new System.Drawing.Point(394, 121);
            this.kaydet.Name = "kaydet";
            this.kaydet.Size = new System.Drawing.Size(75, 58);
            this.kaydet.TabIndex = 8;
            this.kaydet.Text = "Kaydet";
            this.kaydet.UseVisualStyleBackColor = true;
            this.kaydet.Click += new System.EventHandler(this.kaydet_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.silk_own);
            this.groupBox1.Controls.Add(this.Cancel);
            this.groupBox1.Controls.Add(this.iptal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.kaydet);
            this.groupBox1.Controls.Add(this.username);
            this.groupBox1.Controls.Add(this.bul);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.user_bul);
            this.groupBox1.Controls.Add(this.primar);
            this.groupBox1.Controls.Add(this.content);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 225);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Düzelt veya Yeni kullanıcı Ekle";
            // 
            // silk_own
            // 
            this.silk_own.Location = new System.Drawing.Point(138, 185);
            this.silk_own.Name = "silk_own";
            this.silk_own.Size = new System.Drawing.Size(331, 26);
            this.silk_own.TabIndex = 7;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(189, 121);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 58);
            this.Cancel.TabIndex = 22;
            this.Cancel.Text = "Çıkış";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // iptal
            // 
            this.iptal.Location = new System.Drawing.Point(292, 121);
            this.iptal.Name = "iptal";
            this.iptal.Size = new System.Drawing.Size(75, 58);
            this.iptal.TabIndex = 6;
            this.iptal.Text = "İptal";
            this.iptal.UseVisualStyleBackColor = true;
            this.iptal.Click += new System.EventHandler(this.iptal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 19);
            this.label1.TabIndex = 20;
            this.label1.Text = "Silk  ------------>";
            // 
            // sonuc
            // 
            this.sonuc.Location = new System.Drawing.Point(12, 249);
            this.sonuc.Name = "sonuc";
            this.sonuc.ReadOnly = true;
            this.sonuc.Size = new System.Drawing.Size(475, 200);
            this.sonuc.TabIndex = 23;
            this.sonuc.Text = "";
            // 
            // TbUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(499, 461);
            this.Controls.Add(this.sonuc);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(515, 500);
            this.MinimumSize = new System.Drawing.Size(515, 500);
            this.Name = "TbUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TB_Kullanıcı Ekle Düzenle";
            this.Load += new System.EventHandler(this.TbUser_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
	}
}
