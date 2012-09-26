using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using Microsoft.Build.BuildEngine;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;

namespace At.Okr.OKrBook.Auter
{
    public partial class Main : Form
    {
        private String rootPath;
        private String description;
        private String productID;
        private int count;

        public Main()
        {
            InitializeComponent();
        }


        private void btn_Browse_Click(object sender, EventArgs e)
        {
            if (this.fb_Dialog.ShowDialog() == DialogResult.OK)
            {
                this.rootPath = this.fb_Dialog.SelectedPath;
                LoadData();
            }
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            // 1. 修改WMAppManifest.xml中的ProductID和Title
            OnResetManifest();

            // 2. 拷贝文件到Data目录
            OnCopyData();

            // 3. 修改Csproj文件
            OnResetCsproj();

            // todo: {WT}, 需要重构OnResetConfig()，实现修改data/app.config的目的。
            // 4. 重置config文件
            OnResetConfig();

            // 5. 编译打包
            OnReBuild();

            // 6. 准备发布素材
            OnPrePublish();

            // 7. 清除
            OnCleanUp();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadData()
        {
            String appName = LoadAppName();

            this.txt_Location.Text = this.rootPath;
            this.txt_AppName.Text = appName;
            this.txt_Description.Text = LoadDescription();

            int index = appName.IndexOf("：");
            if (index != -1)
            {
                this.txt_Title.Text = appName.Substring(index + 1, appName.Length - index - 1);
            }
            else
            {
                this.txt_Title.Text = appName;
            }
        }

        private String LoadAppName()
        {
            String result = String.Empty;

            int index = this.rootPath.LastIndexOf("\\");
            result = this.rootPath.Substring(index + 1, this.rootPath.Length - index - 1);

            return result;
        }

        private String LoadDescription()
        {
            //String introPath = this.rootPath + "\\" + "intro.txt";

            //if (!File.Exists(introPath))
            //{
            //    MessageBox.Show("不是有效的目录");
            //    return String.Empty;
            //}
            //this.description = File.ReadAllText(introPath, Encoding.UTF8);

            //return this.description;

            String introPath = this.rootPath + "\\" + "app.config";

            if (!File.Exists(introPath))
            {
                MessageBox.Show("不是有效的目录");
                return String.Empty;
            }

            Uri uriResource = new Uri(introPath, UriKind.RelativeOrAbsolute);
            XElement element = XElement.Load(introPath);

            // Load the app from config
            XElement appElement = element.Element("okrbook");
            this.description = appElement.Element("intro").Value;

            return this.description;
        }

        private void OnResetManifest()
        {
            String sourceRoot = ConfigurationManager.AppSettings["source_root"];
            String manifestFile = sourceRoot + "Properties\\WMAppManifest.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(manifestFile);

            XmlNode nodeApp = doc.SelectSingleNode("//App");
            this.productID = Guid.NewGuid().ToString("B");

            nodeApp.Attributes["ProductID"].Value = this.productID;
            nodeApp.Attributes["Title"].Value = this.txt_AppName.Text;

            XmlNode nodeTitle = doc.SelectSingleNode("//Title");
            nodeTitle.InnerText = this.txt_AppName.Text;

            doc.Save(manifestFile);
        }

        private void OnCopyData()
        {
            String sourceRoot = ConfigurationManager.AppSettings["source_root"];
            String subDataDir = sourceRoot + "_static\\data\\";

            DirectoryInfo di = new DirectoryInfo(subDataDir);

            ClearDirectory(subDataDir);
            Directory.CreateDirectory(subDataDir);

            String[] files = Directory.GetFileSystemEntries(rootPath);

            foreach (var file in files)
            {
                // 子目录
                if (Directory.Exists(file))
                {
                    String[] subFiles = Directory.GetFileSystemEntries(file);
                    foreach (var subFile in subFiles)
                    {
                        File.Copy(subFile, subDataDir + Path.GetFileName(file) + "\\" + Path.GetFileName(subFile));
                    }

                    this.count = subFiles.Count();
                }
                else
                {
                    File.Copy(file, subDataDir + Path.GetFileName(file), true);
                }
            }
        }

        private void OnResetCsproj()
        {
            String sourceRoot = ConfigurationManager.AppSettings["source_root"];
            String csprojName = ConfigurationManager.AppSettings["csproj_name"];

            String csprojFile = sourceRoot + csprojName;

            Project project = new Project();
            project.Load(csprojFile);

            BuildItemGroup itemGroup = project.AddNewItemGroup();

            itemGroup.AddNewItem("Content", "_static\\data\\app.config");
            itemGroup.AddNewItem("Content", "_static\\data\\cover.png");
            itemGroup.AddNewItem("Content", "_static\\data\\okr-bg.png");
            itemGroup.AddNewItem("Content", "_static\\data\\okr-icon.png");
            itemGroup.AddNewItem("Content", "_static\\data\\okr-splash.png");
            itemGroup.AddNewItem("Content", "_static\\data\\okrapp.config");

            String[] appFiles = Directory.GetFileSystemEntries(sourceRoot + "_static\\data\\app\\");

            foreach (var file in appFiles)
            {
                itemGroup.AddNewItem("Content", "_static\\data\\app\\" + Path.GetFileName(file));
            }

            String[] bookFiles = Directory.GetFileSystemEntries(sourceRoot + "_static\\data\\book\\");

            foreach (var file in bookFiles)
            {
                itemGroup.AddNewItem("Content", "_static\\data\\book\\" + Path.GetFileName(file));
            }

            project.Save(csprojFile);
        }

        private void OnResetConfig()
        {
            String xml = this.rootPath + "\\" + "app.config";

            Uri uriResource = new Uri(xml, UriKind.RelativeOrAbsolute);
            XElement doc = XElement.Load(xml);

            // Refresh the new value
            doc.Element("app").SetElementValue("name", this.txt_Title.Text);
            doc.Element("okrbook").SetElementValue("intro", this.description);
            doc.Element("okrbook").SetElementValue("name", this.txt_Title.Text);

            doc.Save(xml);
        }

        private void OnReBuild()
        {
            String sourceRoot = ConfigurationManager.AppSettings["source_root"];
            String csprojName = ConfigurationManager.AppSettings["csproj_name"];

            String csprojFile = sourceRoot + csprojName;

            Process p = new Process();
            p.StartInfo.FileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe";
            p.StartInfo.Arguments = csprojFile + @" /t:Rebuild /p:Configuration=Release";
            p.StartInfo.UseShellExecute = true;
            p.Start();

            p.WaitForExit();
        }

        private void OnPrePublish()
        {
            String targetPath = this.rootPath + "\\_publish";
            DirectoryInfo targetDir = new DirectoryInfo(targetPath);
            if (targetDir.Exists)
            {
                ClearDirectory(targetPath);
            }

            targetDir.Create();

            // 拷贝相关材料文件
            String[] files = Directory.GetFileSystemEntries(rootPath);
            foreach (var file in files)
            {
                if (!Directory.Exists(file) && Path.GetFileName(file) != "cover.jpg")
                {
                    File.Copy(file, targetPath + "\\" + Path.GetFileName(file), true);
                }
            }

            // 拷贝xap包文件
            String sourceRoot = ConfigurationManager.AppSettings["source_root"];
            String xapFile = sourceRoot + "\\Bin\\Release\\okrbook.xap";
            File.Copy(xapFile, targetDir.FullName + "\\okrbook.xap", true);

            // 写入intro
            //FileInfo fi = new FileInfo(targetPath + "\\intro.txt");
            //using (FileStream fs = fi.OpenWrite())
            //{
            //    String text = this.txt_Description.Text;
            //    byte[] buffer = Encoding.UTF8.GetBytes(text);
            //    fs.Write(buffer, 0, buffer.Length);
            //    fs.Flush();
            //}
        }

        private void OnCleanUp()
        {
            String sourceRoot = ConfigurationManager.AppSettings["source_root"];
            String csprojName = ConfigurationManager.AppSettings["csproj_name"];

            String csprojFile = sourceRoot + csprojName;

            Project project = new Project();
            project.Load(csprojFile);

            project.RemoveItemsByName("Content");

            // 补上img文件夹
            BuildItemGroup itemGroup = project.AddNewItemGroup();
            String[] bookFiles = Directory.GetFileSystemEntries(sourceRoot + "_static\\img\\");

            foreach (var file in bookFiles)
            {
                itemGroup.AddNewItem("Content", "_static\\img\\" + Path.GetFileName(file));
            }

            project.Save(csprojFile);
        }

        // 清空一个目录
        private void ClearDirectory(String path)
        {
            if (Directory.Exists(path))
            {
                String[] files = Directory.GetFileSystemEntries(path);

                foreach (var file in files)
                {
                    if (Directory.Exists(file))
                    {
                        ClearDirectory(file);
                    }
                    else
                    {
                        File.Delete(file);
                    }
                }
            }
        }
    }
}
