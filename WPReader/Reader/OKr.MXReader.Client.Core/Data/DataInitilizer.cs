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
using System.IO.IsolatedStorage;
using System.IO;
using At.Phone.Common.Utils;

namespace OKr.MXReader.Client.Core.Data
{
    public class DataInitilizer
    {
        public void Init()
        {
            if (!OkrRepository.ExistDB())
            {
                string db = OkrConstant.DB;
                Stream stream = Application.GetResourceStream(new Uri(OkrConstant.DBPATH, UriKind.Relative)).Stream;

                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (!storage.FileExists(db))
                    {
                        IsolatedStorageFileStream outFile = storage.CreateFile(db);
                        outFile.Write(AtUtils.ReadToEnd(stream), 0, (int)stream.Length);
                        stream.Close();
                        outFile.Close();
                    }
                }
            }
        }
    }
}
