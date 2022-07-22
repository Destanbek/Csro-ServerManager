using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SilkroadServerManager
{
	public class aconf : Form
	{
		private IContainer components = null;

		private Button cancel;

		private Button adsave;

		private RichTextBox richTextBox1;

		private ListBox advlist;

		private Button clear;

		private Button convert;

		public aconf()
		{
			InitializeComponent();
		}

		private void aconf_Load(object sender, EventArgs e)
		{
            if (Language.Default.language == "English")
            {
                this.Text = "Advanced Configuration";
                clear.Text = "Clean";
                cancel.Text = "Close";
                adsave.Text = "Save";
                convert.Text = "Create Cert";
            }
		}

		private void Advfind(string index, string path)
		{
			if (Directory.Exists(path))
			{
				string[] files = Directory.GetFiles(path, index);
				string[] array = files;
				foreach (string item in array)
				{
					advlist.Items.Add(item);
				}
			}
			else
			{
                if (Language.Default.language == "English")
                {
                    richTextBox1.AppendText("Folder --- " + path + "----->Not found !\n");
                }
                else
                {
                    richTextBox1.AppendText("Dosya --- " + path + "----->Bulunamadı !\n");
                }
			}
		}

		private void advlist_SelectedIndexChanged(object sender, EventArgs e)
		{
			object selectedItem = advlist.SelectedItem;
			if (selectedItem != null)
			{
				richTextBox1.Clear();
				richTextBox1.AppendText(File.ReadAllText(selectedItem.ToString()));
			}
		}

		private void adsave_Click(object sender, EventArgs e)
		{
			object selectedItem = advlist.SelectedItem;
			if (selectedItem != null)
			{
				richTextBox1.SaveFile(selectedItem.ToString(), RichTextBoxStreamType.PlainText);
				richTextBox1.Clear();
                if (Language.Default.language == "English")
                {
                    richTextBox1.Text = "Successful";
                }
                else
                {
                    richTextBox1.Text = "Başarılı";
                }
				advlist.ClearSelected();
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void clear_Click(object sender, EventArgs e)
		{
			richTextBox1.Clear();
			advlist.ClearSelected();
		}

		private void convert_Click(object sender, EventArgs e)
		{
			if (File.Exists("Cert\\Convert.exe"))
			{
				Process.Start("Cert\\Convert.exe", " ini Cert\\ini dat Cert\\packt.dat");
				Process process = Process.GetProcessesByName("Convert").FirstOrDefault();
				if (process != null)
				{
					process.WaitForExit(3000);
					process.Kill();
				}
			}
			else
			{
				richTextBox1.Select(richTextBox1.Text.Length, 0);
				richTextBox1.SelectionColor = Color.Red;
				richTextBox1.DeselectAll();

                if (Language.Default.language == "English")
                {
                    richTextBox1.AppendText("Cert\\Convert.exe ------> Not found !\\n");
                }
                else
                {
                    richTextBox1.AppendText("Cert\\Convert.exe ------> Bulunamadı !\\n");
                }
            }
		}

		private void aconf_Shown(object sender, EventArgs e)
		{
			Advfind("*.ini", "Cert\\ini");
			Advfind("*.cfg", "SMC");
			Advfind("*.cfg", ".");
            Advfind("*.ini", "sroprot");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(aconf));
            this.cancel = new System.Windows.Forms.Button();
            this.adsave = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.advlist = new System.Windows.Forms.ListBox();
            this.clear = new System.Windows.Forms.Button();
            this.convert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cancel.Location = new System.Drawing.Point(12, 431);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(80, 51);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "Kapat";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // adsave
            // 
            this.adsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.adsave.Location = new System.Drawing.Point(101, 373);
            this.adsave.Name = "adsave";
            this.adsave.Size = new System.Drawing.Size(80, 109);
            this.adsave.TabIndex = 4;
            this.adsave.Text = "Kaydet";
            this.adsave.UseVisualStyleBackColor = true;
            this.adsave.Click += new System.EventHandler(this.adsave_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.PeachPuff;
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richTextBox1.Location = new System.Drawing.Point(187, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(635, 537);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // advlist
            // 
            this.advlist.BackColor = System.Drawing.Color.PeachPuff;
            this.advlist.FormattingEnabled = true;
            this.advlist.HorizontalScrollbar = true;
            this.advlist.Location = new System.Drawing.Point(12, 12);
            this.advlist.Name = "advlist";
            this.advlist.Size = new System.Drawing.Size(169, 355);
            this.advlist.TabIndex = 1;
            this.advlist.SelectedIndexChanged += new System.EventHandler(this.advlist_SelectedIndexChanged);
            // 
            // clear
            // 
            this.clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.clear.Location = new System.Drawing.Point(12, 373);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(80, 52);
            this.clear.TabIndex = 5;
            this.clear.Text = "Temizle";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // convert
            // 
            this.convert.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.convert.Location = new System.Drawing.Point(12, 488);
            this.convert.Name = "convert";
            this.convert.Size = new System.Drawing.Size(169, 61);
            this.convert.TabIndex = 6;
            this.convert.Text = "Cert Oluştur";
            this.convert.UseVisualStyleBackColor = true;
            this.convert.Click += new System.EventHandler(this.convert_Click);
            // 
            // aconf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(834, 561);
            this.Controls.Add(this.convert);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.adsave);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.advlist);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(850, 600);
            this.MinimumSize = new System.Drawing.Size(850, 600);
            this.Name = "aconf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gelişmiş Yapılandırma";
            this.Load += new System.EventHandler(this.aconf_Load);
            this.Shown += new System.EventHandler(this.aconf_Shown);
            this.ResumeLayout(false);

		}
	}
}
