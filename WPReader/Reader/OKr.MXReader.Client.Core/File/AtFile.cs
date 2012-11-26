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
using System.Windows.Resources;
using System.IO;
using System.Text;

namespace OKr.MXReader.Client.Core.File
{
    public class AtFile
    {

        public static string GetContent(string url, int index)
        {
            byte[] array = GetBytes(url, UriKind.Relative);
            
            //array = Subarray(array, index, array.Length);
            
            //array = Decompress(array);
            string str = Encoding.UTF8.GetString(array, 0, array.Length);

            return str.Replace("\0", "");
        }

        public static byte[] Subarray(byte[] array, int startIndexInclusive, int endIndexExclusive)
        {
            if (array == null)
            {
                return null;
            }
            if (startIndexInclusive < 0)
            {
                startIndexInclusive = 0;
            }
            if (endIndexExclusive > array.Length)
            {
                endIndexExclusive = array.Length;
            }
            int length = endIndexExclusive - startIndexInclusive;
            if (length <= 0)
            {
                return new byte[0];
            }
            byte[] destinationArray = new byte[length];
            Array.Copy(array, startIndexInclusive, destinationArray, 0, length);
            return destinationArray;
        }


        public static byte[] Decompress(byte[] b)
        {
            MemoryStream stream = new MemoryStream(b);
            //GZipInputStream stream2 = new GZipInputStream(new MemoryStream(b));
            try
            {
                int count = 0x800;
                byte[] buffer = new byte[count];
                while (count > 0)
                {
                    //count = stream2.Read(buffer, 0, count);
                    stream.Write(buffer, 0, count);
                }
            }
            catch (Exception exception)
            {
                
            }
            //stream2.Close();
            return stream.GetBuffer();
        }

 


 




        public static byte[] GetBytes(string url, UriKind urikind)
        {
            Uri uriResource = new Uri(url, urikind);
            StreamResourceInfo resourceStream = Application.GetResourceStream(uriResource);
            MemoryStream stream = new MemoryStream();
            int count = 0x800;
            byte[] buffer = new byte[count];
            while (count > 0)
            {
                count = resourceStream.Stream.Read(buffer, 0, count);
                stream.Write(buffer, 0, count);
            }
            return stream.GetBuffer();
        }
    }
}
