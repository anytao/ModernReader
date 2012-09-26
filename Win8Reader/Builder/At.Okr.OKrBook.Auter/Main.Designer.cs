namespace At.Okr.OKrBook.Auter
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
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_Create = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Series = new System.Windows.Forms.TextBox();
            this.txt_Title = new System.Windows.Forms.TextBox();
            this.txt_AppName = new System.Windows.Forms.TextBox();
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.txt_Location = new System.Windows.Forms.TextBox();
            this.fb_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(416, 449);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(120, 35);
            this.btn_Exit.TabIndex = 25;
            this.btn_Exit.Text = "退 出";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(76, 449);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(120, 35);
            this.btn_Create.TabIndex = 24;
            this.btn_Create.Text = "生 成";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "描述：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "系列：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "标题：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "应用名：";
            // 
            // txt_Series
            // 
            this.txt_Series.Location = new System.Drawing.Point(76, 158);
            this.txt_Series.Name = "txt_Series";
            this.txt_Series.Size = new System.Drawing.Size(355, 20);
            this.txt_Series.TabIndex = 19;
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(76, 111);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(355, 20);
            this.txt_Title.TabIndex = 18;
            // 
            // txt_AppName
            // 
            this.txt_AppName.Location = new System.Drawing.Point(76, 65);
            this.txt_AppName.Name = "txt_AppName";
            this.txt_AppName.Size = new System.Drawing.Size(355, 20);
            this.txt_AppName.TabIndex = 17;
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(76, 208);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(460, 220);
            this.txt_Description.TabIndex = 16;
            this.txt_Description.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "位置：";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(461, 15);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(75, 23);
            this.btn_Browse.TabIndex = 14;
            this.btn_Browse.Text = "浏 览";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // txt_Location
            // 
            this.txt_Location.Location = new System.Drawing.Point(76, 19);
            this.txt_Location.Name = "txt_Location";
            this.txt_Location.Size = new System.Drawing.Size(355, 20);
            this.txt_Location.TabIndex = 13;
            // 
            // fb_Dialog
            // 
            this.fb_Dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fb_Dialog.ShowNewFolderButton = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 499);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_Create);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Series);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.txt_AppName);
            this.Controls.Add(this.txt_Description);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.txt_Location);
            this.Name = "Main";
            this.Text = "OKrBoo Auto Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Series;
        private System.Windows.Forms.TextBox txt_Title;
        private System.Windows.Forms.TextBox txt_AppName;
        private System.Windows.Forms.RichTextBox txt_Description;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.TextBox txt_Location;
        private System.Windows.Forms.FolderBrowserDialog fb_Dialog;
    }
}