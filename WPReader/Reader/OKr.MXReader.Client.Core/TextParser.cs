using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using OKr.MXReader.Client.Core.Data;
using OKr.MXReader.Client.Core.File;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace OKr.MXReader.Client.Core
{
    public class TextParser
    {
        public static Book GetBook(string url, UriKind urikind)
        {
            Book result = new Book();
            string content = AtFile.GetContent(url, 4);
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(stream);
                string str2 = null;
                while ((str2 = reader.ReadLine()) != null)
                {
                    string[] strArray = str2.Replace("&&", "&").Split(new char[] { '&' });
                    if (strArray.Length >= 3)
                    {
                        Chapter item = new Chapter();
                        item.Title = strArray[0];
                        item.FileName = strArray[1];
                        item.Size = int.Parse(strArray[2]);
                        result.Chapters.Add(item);
                    }
                }
                reader.Close();
            }
            catch (NullReferenceException ex)
            {
                
            }
            return result;
        }


        public static OKr.MXReader.Client.Core.Data.Page GetOnePage(string str, int start, int[] count)
        {
            OKr.MXReader.Client.Core.Data.Page result = new OKr.MXReader.Client.Core.Data.Page();
            int num = 1;
            int num2 = 0;
            int num3 = 0;
            int length = 0;
            int startIndex = start;
            int num6 = 0;
            for (int i = start; i < str.Length; i++)
            {
                if (Regex.IsMatch(str.Substring(i, 1), @"[\r\n]"))
                {
                    num2 = 0;
                    length++;
                    num3++;
                    num6++;
                    if (num6 == 1)
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
                num6 = 0;
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
                num3++;
                length++;
            }
            result.CharNum = num3;
            return result;
        }


        public static Chapter GetChapter(string context, int[] count)
        {
            Chapter bean = new Chapter();
            List<string> list = new List<string>();
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(context);
            int start = 0;
            int num2 = 0;
            while (start < bytes.Length)
            {
                OKr.MXReader.Client.Core.Data.Page item = GetOnePage(context, start, count);
                start += item.CharNum;
                list.Add(item.Result);
                bean.Pages.Add(item);
                if (start >= context.Length)
                {
                    num2++;
                    break;
                }
                num2++;
            }
            bean.PageList = list;
            bean.PageNum = num2;
            return bean;
        }

 

 

 

 

    }
}
