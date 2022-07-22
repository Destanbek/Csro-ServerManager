using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SilkroadServerManager
{
	public class lang : Form
	{
		private string folder;

		private string savefolder = "media\\server_dep\\silkroad\\textdata\\";

		private IContainer components = null;

		private Label label1;

		private Button cancel;

		private Button start;

		private ProgressBar progressBar1;

		private RadioButton arp;

		private RadioButton esp;

		private RadioButton deu;

		private RadioButton eng;

		private RadioButton tur;

		private Button openfile;

		private BackgroundWorker backgroundWorker1;

		private FolderBrowserDialog folderBrowserDialog1;

		public lang()
		{
			Control.CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
		}

		private void lang_Load(object sender, EventArgs e)
		{
            if (Language.Default.language == "English")
            {
                this.Text = "Silkroad Language Changer";
                label1.Text = "Please select the original textdata folder";
                openfile.Text = "Select folder";
                cancel.Text = "Exit";
                start.Text = "Start";
            }
        }

		private void openfile_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				label1.Text = folderBrowserDialog1.SelectedPath;
				folder = folderBrowserDialog1.SelectedPath + "\\";
			}
		}

		private void start_Click(object sender, EventArgs e)
		{
			if (folder != null)
			{
				backgroundWorker1.RunWorkerAsync();
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			birlestir("textdata_equip&skill.txt");
			birlestir("textdata_object.txt");
			birlestir("textevent.txt");
			birlestir("texthelp.txt");
			birlestir("textquest_otherstring.txt");
			birlestir("textquest_queststring.txt");
			birlestir("textquest_speech&name.txt");
			birlestir("textuisystem.txt");
			birlestir("textzonename.txt");
		}

		private void birlestir(string path)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 9;
			if (tur.Checked)
			{
				num = 13;
			}
			if (eng.Checked)
			{
				num = 9;
			}
			if (deu.Checked)
			{
				num = 16;
			}
			if (esp.Checked)
			{
				num = 14;
			}
			if (arp.Checked)
			{
				num = 15;
			}
			string[] array = File.ReadAllText(folder + path).Replace("\n", "").Split('\r');
			for (int i = 0; i < array.Length - 1; i++)
			{
				progressBar1.Maximum = array.Length - 2;
				string[] array2 = File.ReadAllText(folder + array[i]).Split('\r');
				for (int j = 0; j < array2.Length - 1; j++)
				{
					string[] array3 = array2[j].Split('\t');
					stringBuilder.AppendLine(array3[0] + "\t" + array3[2] + "\t" + array3[3] + "\t" + array3[4] + "\t" + array3[5] + "\t" + array3[6] + "\t" + array3[7] + "\t" + array3[8] + "\t" + array3[num] + "\t" + array3[10] + "\t" + array3[11] + "\t" + array3[12] + "\t" + array3[12] + "\t" + array3[12] + "\t" + array3[12] + "\t" + array3[12] + "\t");
				}
                if (Language.Default.language == "English")
                {
                    label1.Text = path + " ---> Successful !";
                }
                else
                {
                    label1.Text = path + " ---> Başarılı !";
                }
                backgroundWorker1.ReportProgress(i);
			}
			if (!Directory.Exists(savefolder))
			{
				Directory.CreateDirectory(savefolder);
			}
			File.WriteAllText(savefolder + path, stringBuilder.ToString(), Encoding.Unicode);
			stringBuilder.Clear();
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
            if (Language.Default.language == "English")
            {
                label1.Text = "Completed";
            }
            else
            {
                label1.Text = "Tamamlandı";
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lang));
            this.label1 = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.arp = new System.Windows.Forms.RadioButton();
            this.esp = new System.Windows.Forms.RadioButton();
            this.deu = new System.Windows.Forms.RadioButton();
            this.eng = new System.Windows.Forms.RadioButton();
            this.tur = new System.Windows.Forms.RadioButton();
            this.openfile = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.MaximumSize = new System.Drawing.Size(300, 18);
            this.label1.MinimumSize = new System.Drawing.Size(490, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(490, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "Lütfen orijinal textdata klasörünü seçin";
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cancel.Location = new System.Drawing.Point(240, 103);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 40);
            this.cancel.TabIndex = 17;
            this.cancel.Text = "Çıkış";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.start.Location = new System.Drawing.Point(425, 103);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 40);
            this.start.TabIndex = 16;
            this.start.Text = "Başla";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 46);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(490, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // arp
            // 
            this.arp.AutoSize = true;
            this.arp.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.arp.Location = new System.Drawing.Point(430, 12);
            this.arp.Name = "arp";
            this.arp.Size = new System.Drawing.Size(72, 28);
            this.arp.TabIndex = 14;
            this.arp.TabStop = true;
            this.arp.Text = "العربية";
            this.arp.UseVisualStyleBackColor = true;
            // 
            // esp
            // 
            this.esp.AutoSize = true;
            this.esp.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.esp.Location = new System.Drawing.Point(321, 12);
            this.esp.Name = "esp";
            this.esp.Size = new System.Drawing.Size(103, 28);
            this.esp.TabIndex = 13;
            this.esp.TabStop = true;
            this.esp.Text = "Español";
            this.esp.UseVisualStyleBackColor = true;
            // 
            // deu
            // 
            this.deu.AutoSize = true;
            this.deu.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.deu.Location = new System.Drawing.Point(210, 12);
            this.deu.Name = "deu";
            this.deu.Size = new System.Drawing.Size(105, 28);
            this.deu.TabIndex = 12;
            this.deu.TabStop = true;
            this.deu.Text = "Deutsch";
            this.deu.UseVisualStyleBackColor = true;
            // 
            // eng
            // 
            this.eng.AutoSize = true;
            this.eng.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.eng.Location = new System.Drawing.Point(109, 12);
            this.eng.Name = "eng";
            this.eng.Size = new System.Drawing.Size(95, 28);
            this.eng.TabIndex = 11;
            this.eng.TabStop = true;
            this.eng.Text = "English";
            this.eng.UseVisualStyleBackColor = true;
            // 
            // tur
            // 
            this.tur.AutoSize = true;
            this.tur.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tur.Location = new System.Drawing.Point(12, 12);
            this.tur.Name = "tur";
            this.tur.Size = new System.Drawing.Size(91, 28);
            this.tur.TabIndex = 10;
            this.tur.TabStop = true;
            this.tur.Text = "Türkçe";
            this.tur.UseVisualStyleBackColor = true;
            // 
            // openfile
            // 
            this.openfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.openfile.Location = new System.Drawing.Point(12, 103);
            this.openfile.Name = "openfile";
            this.openfile.Size = new System.Drawing.Size(109, 40);
            this.openfile.TabIndex = 19;
            this.openfile.Text = "Klasör seç";
            this.openfile.UseVisualStyleBackColor = true;
            this.openfile.Click += new System.EventHandler(this.openfile_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "server_dep\\silkroad\\textdata";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // lang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(514, 151);
            this.Controls.Add(this.openfile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.start);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.arp);
            this.Controls.Add(this.esp);
            this.Controls.Add(this.deu);
            this.Controls.Add(this.eng);
            this.Controls.Add(this.tur);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(530, 190);
            this.MinimumSize = new System.Drawing.Size(530, 190);
            this.Name = "lang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Silkroad Dil Değiştirici";
            this.Load += new System.EventHandler(this.lang_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
