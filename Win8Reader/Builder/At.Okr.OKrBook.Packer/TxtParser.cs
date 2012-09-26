using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace At.Okr.OKrBook.Packer
{
    public class TxtParser
    {
        public TxtParser(string src, string target, string prefix, int preCount)
        {
            this.src = src;
            this.target = target;
            this.prefix = prefix;
            this.preCount = preCount;
        }

        public void Parse()
        {
            Segment data = null;
            IList<Segment> datas = new List<Segment>();
            int index = 1;

            try
            {
                FileStream fs = new FileStream(this.src, FileMode.Open, FileAccess.Read);

                using (StreamReader sr = new StreamReader(fs))
                {
                    string line = sr.ReadLine().Trim();

                    while (line != "*END*")
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            if (line.StartsWith(this.prefix))
                            {
                                if (data != null)
                                {
                                    datas.Add(data);

                                    SaveToFile(data);

                                    //Console.WriteLine("生成[" + data.ID + "]完成。");

                                    index++;
                                }

                                data = new Segment();

                                //int preLength = this.prefix.Length;
                                string title = line.Substring(this.preCount).Trim();
                                data.Title = title;
                                data.ID = index.ToString("D3"); //输出001
                            }
                            else
                            {
                                string content = line.Trim();

                                if (!string.IsNullOrEmpty(content))
                                {
                                    data.Content += content + "\n";
                                }   
                            }
                        }

                        line = sr.ReadLine().Trim();
                    }

                    // Save the last
                    SaveToFile(data);
 
                    // Save the category
                    datas.Add(data);
                    SaveToCategory(datas);
                    //Console.WriteLine("生成[" + "目录" + "]完成。");
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        private void SaveToCategory(IList<Segment> datas)
        {
            string saveTo = this.target + "category.txt";


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveTo))
            {
                foreach (var item in datas)
                {
                    string line = item.Title + "&&" + item.ID + "&&1024";

                    file.WriteLine(line);
                }
            }
        }

        private void SaveToFile(Segment data)
        {
            string saveTo = this.target + data.ID + ".txt";


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveTo))
            {
                file.WriteLine(data.Title);
                file.WriteLine(data.Content);
                file.WriteLine("[好阅, http://okr.me]");
            }
        }

        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }

        private string src;
        private string target;

        private string prefix; // TW>, 正文, 
        private int preCount;
    }
}
