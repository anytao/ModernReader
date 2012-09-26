namespace At.Okr.OKrBook.Packer
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Create = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_PreCount = new System.Windows.Forms.TextBox();
            this.fb_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txt_Prefix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Location = new System.Windows.Forms.TextBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.fb_File = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(88, 164);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(120, 35);
            this.btn_Create.TabIndex = 31;
            this.btn_Create.Text = "生 成";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "位置：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "前缀：";
            // 
            // txt_PreCount
            // 
            this.txt_PreCount.Location = new System.Drawing.Point(88, 114);
            this.txt_PreCount.Name = "txt_PreCount";
            this.txt_PreCount.Size = new System.Drawing.Size(355, 20);
            this.txt_PreCount.TabIndex = 28;
            this.txt_PreCount.Text = "0";
            // 
            // fb_Dialog
            // 
            this.fb_Dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fb_Dialog.ShowNewFolderButton = false;
            // 
            // txt_Prefix
            // 
            this.txt_Prefix.Location = new System.Drawing.Point(88, 68);
            this.txt_Prefix.Name = "txt_Prefix";
            this.txt_Prefix.Size = new System.Drawing.Size(355, 20);
            this.txt_Prefix.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "位置：";
            // 
            // txt_Location
            // 
            this.txt_Location.Location = new System.Drawing.Point(88, 22);
            this.txt_Location.Name = "txt_Location";
            this.txt_Location.Size = new System.Drawing.Size(355, 20);
            this.txt_Location.TabIndex = 25;
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(470, 22);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(75, 23);
            this.btn_Browse.TabIndex = 32;
            this.btn_Browse.Text = "浏 览";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // fb_File
            // 
            this.fb_File.FileName = "openFileDialog1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 230);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.btn_Create);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_PreCount);
            this.Controls.Add(this.txt_Prefix);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Location);
            this.Name = "Main";
            this.Text = "Modern Reader Builder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_PreCount;
        private System.Windows.Forms.FolderBrowserDialog fb_Dialog;
        private System.Windows.Forms.TextBox txt_Prefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Location;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.OpenFileDialog fb_File;
    }
}