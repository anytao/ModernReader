using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OKr.Common.Storage;
using OKr.Win8Book.Client.Core.Data;

namespace OKr.Win8Book.Client.Core.Builder
{
    public class TextParser
    {
        public async static Task<Book> GetBook(string url)
        {
            Book result = new Book();
            OKrStorage storage = new OKrStorage();
            string content = await storage.ReadString(url);
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            StreamReader reader = null;            

            try
            {
                reader = new StreamReader(stream);
                string str2 = null;
                int index = 0;
                while ((str2 = reader.ReadLine()) != null)
                {
                    string[] strArray = str2.Replace("&&", "&").Split(new char[] { '&' });
                    if (strArray.Length >= 3)
                    {
                        Chapter item = new Chapter();
                        item.Title = strArray[0];
                        item.FileName = strArray[1];
                        item.Size = int.Parse(strArray[2]);
                        item.ChapterNo = index;
                        result.Chapters.Add(item);
                    }

                    index++;
                }
                reader.Dispose();
            }
            catch (NullReferenceException ex)
            {

            }
            return result;
        }
        
        public static Chapter GetChapter(string context, int[] count)
        {
            Chapter chapter = new Chapter();
            List<string> list = new List<string>();
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(context);
            int start = 0;
            int num2 = 0;
            int location = 1;

            while (start < bytes.Length)
            {
                Page item = GetOnePage(context, start, count, ref location);
                start += item.CharNum;
                list.Add(item.Result);
                chapter.Pages.Add(item);
                if (start >= context.Length)
                {
                    num2++;
                    break;
                }
                num2++;
            }
            chapter.PageList = list;
            chapter.PageNum = num2;
            return chapter;
        }

        public static Page GetOnePage(string str, int start, int[] count, ref int location)
        {
            Page result = new Page();
            int num = 1;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int length = 0;
            int startIndex = start;

            for (int i = start; i < str.Length; i++)
            {
                if (Regex.IsMatch(str.Substring(i, 1), @"[\r\n]"))
                {
                    num2 = 0;
                    length++;
                    num3++;
                    num4++;
                    if (num4 == 1)
                    {
                        num++;
                        result.Row.Add(str.Substring(startIndex, length));
                    }
                    length = 0;
                    startIndex = i + 1;
                    if (num != count[0])
                    {
                        continue;
                    }
                    break;
                }
                num4 = 0;
                string s = str.Substring(i, 1);
                byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(s);
                if (bytes.Length == 3)
                {
                    if (((count[1] * 2) - num2) >= 3)
                    {
                        num2 += 3;
                    }
                    else
                    {
                        num++;
                        result.Row.Add(str.Substring(startIndex, length));
                        length = 0;
                        startIndex = i;
                        num2 = 3;
                        if (num == count[0])
                        {
                            break;
                        }
                    }
                }
                else if (bytes.Length != 2)
                {
                    if (((count[1] * 2) - num2) >= 1)
                    {
                        num2++;
                    }
                    else
                    {
                        num++;
                        result.Row.Add(str.Substring(startIndex, length));
                        length = 0;
                        startIndex = i;
                        num2 = 1;
                        if (num == count[0])
                        {
                            break;
                        }
                    }
                }

                if (s.Contains("。") || s.Contains("."))
                {
                    result.Locations.Add(location);
                    location++;
                }

                num3++;
                length++;
            }
            result.CharNum = num3;
            return result;
        }
    }
}
