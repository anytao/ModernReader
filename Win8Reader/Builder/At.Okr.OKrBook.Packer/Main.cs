using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace At.Okr.OKrBook.Packer
{
    public partial class Main : Form
    {
        private string src;

        public Main()
        {
            InitializeComponent();
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            string path = null;

            int index = this.src.LastIndexOf("\\");
            path = this.src.Substring(0, index);

            string target = path + @"\book\";

            if (System.IO.Directory.Exists(target))
            {
                Directory.Delete(target, true);
            }
            Directory.CreateDirectory(target);

            string prefix = this.txt_Prefix.Text;
            int preCount = int.Parse(this.txt_PreCount.Text);

            TxtParser parser = new TxtParser(src, target, prefix, preCount);
            parser.Parse();

            MessageBox.Show("生成成功。");
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            //if (this.fb_Dialog.ShowDialog() == DialogResult.OK)
            //{
            //    this.rootPath = this.fb_Dialog.SelectedPath;
            //    LoadData();
            //}

            if (this.fb_File.ShowDialog() == DialogResult.OK)
            {
                this.src = this.fb_File.FileName;

                LoadData();                
            }
        }

        private void LoadData()
        {
            //String appName = LoadAppName();

            this.txt_Location.Text = this.src;
            //this.txt_AppName.Text = appName;
            //this.txt_Description.Text = LoadDescription();

            //int index = appName.IndexOf("：");
            //if (index != -1)
            //{
            //    this.txt_Title.Text = appName.Substring(index + 1, appName.Length - index - 1);
            //}
            //else
            //{
            //    this.txt_Title.Text = appName;
            //}
        }
    }
}
