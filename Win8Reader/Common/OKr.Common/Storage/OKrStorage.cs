// 2012 OKr Works, http://okr.me

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OKr.Common.Utils;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace OKr.Common.Storage
{
    public class OKrStorage<T> : OKrStorage
    {
        public OKrStorage()
            : this("")
        {
        }

        public OKrStorage(string fileName)
            : this(fileName, StorageType.Local)
        {
        }

        public OKrStorage(string fileName, StorageType StorageType)
        {
            serializer = new XmlSerializer(typeof(T));
            storageType = StorageType;
            this.fileName = fileName;
        }

        public async Task DeleteAsync()
        {
            try
            {
                StorageFolder folder = GetFolder(storageType);

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = FileName(Activator.CreateInstance<T>());
                }

                var file = await GetFileIfExistsAsync(folder, fileName);
                if (file != null)
                {
                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task DeleteAllAsync(string sub)
        {
            try
            {
                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(appData.LocalFolder.Path + "\\" + sub);

                await folder.DeleteAsync();
            }
            catch (System.Exception)
            {
            }
        }

        public async Task SaveAsync(T Obj)
        {
            try
            {
                if (Obj != null)
                {
                    StorageFile file = null;
                    StorageFolder folder = GetFolder(storageType);

                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = FileName(Obj);
                    }

                    file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                    IRandomAccessStream writeStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    Stream outStream = Task.Run(() => writeStream.AsStreamForWrite()).Result;
                    serializer.Serialize(outStream, Obj);

                    outStream.Dispose();
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        public async Task<T> LoadAsync()
        {
            try
            {
                StorageFile file = null;
                StorageFolder folder = GetFolder(storageType);

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = FileName(Activator.CreateInstance<T>());
                }

                file = await folder.GetFileAsync(fileName);
                IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read);
                Stream inStream = Task.Run(() => readStream.AsStreamForRead()).Result;
                return (T)serializer.Deserialize(inStream);
            }
            catch (FileNotFoundException)
            {
                //file not existing is perfectly valid so simply return the default 
                return default(T);
                //Interesting thread here: How to detect if a file exists (http://social.msdn.microsoft.com/Forums/en-US/winappswithcsharp/thread/1eb71a80-c59c-4146-aeb6-fefd69f4b4bb)
                //throw;
            }
            catch (System.Exception ex)
            {
                //Unable to load contents of file
                return default(T);
            }
        }

        private StorageFolder GetFolder(StorageType storageType)
        {
            StorageFolder folder;
            switch (storageType)
            {
                case StorageType.Roaming:
                    folder = appData.RoamingFolder;
                    break;
                case StorageType.Local:
                    folder = appData.LocalFolder;
                    break;
                case StorageType.Temporary:
                    folder = appData.TemporaryFolder;
                    break;
                default:
                    throw new System.Exception(String.Format("Unknown StorageType: {0}", storageType));
            }
            return folder;
        }

        private string FileName(T Obj)
        {
            return String.Format("{0}.xml", Obj.GetType().FullName);
        }

        private string fileName;
        private ApplicationData appData = Windows.Storage.ApplicationData.Current;
        private XmlSerializer serializer;
        private StorageType storageType;
    }

    public class OKrStorage
    {
        public async Task SaveFile(string url, string fileName)
        {
            try
            {
                var response = await System.Net.HttpWebRequest.Create(url).GetResponseAsync();
                List<Byte> allBytes = new List<byte>();
                using (Stream imageStream = response.GetResponseStream())
                {
                    byte[] buffer = new byte[4000];
                    int bytesRead = 0;
                    while ((bytesRead = await imageStream.ReadAsync(buffer, 0, 4000)) > 0)
                    {
                        allBytes.AddRange(buffer.Take(bytesRead));
                    }
                }

                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(file, allBytes.ToArray());
            }
            catch (System.Exception ex)
            {
            }
        }

        public async Task<String> ReadString(String fileName)
        {
            StorageFile file = null;
            StorageFolder folder = ApplicationData.Current.LocalFolder;

            file = await folder.GetFileAsync(fileName);
            IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read);
            Stream inStream = Task.Run(() => readStream.AsStreamForRead()).Result;

            if (inStream.CanRead)
            {
                using (var reader = new StreamReader(inStream))
                {
                    return reader.ReadToEnd();
                }
            }

            return string.Empty;
        }

        public async Task SaveToPictureLib(string url)
        {
            var client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var folder = await GetFolderIfExistsAsync(KnownFolders.PicturesLibrary, "SinaView");
            if (folder == null)
            {
                folder = await KnownFolders.PicturesLibrary.CreateFolderAsync("SinaView");
            }

            var ext = url.Substring(url.LastIndexOf('.') + 1);
            var fileName = MD5.Hash(url) + "." + ext;
            StorageFile sfile = await GetFileIfExistsAsync(folder, fileName);

            if (sfile == null)
            {
                var img = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                var fs = await img.OpenAsync(FileAccessMode.ReadWrite);
                var writer = new DataWriter(fs.GetOutputStreamAt(0));
                writer.WriteBytes(await response.Content.ReadAsByteArrayAsync());
                await writer.StoreAsync();
                writer.DetachStream();
                await fs.FlushAsync();

                var dialog = new MessageDialog("保存成功。", "保存图片");
                await dialog.ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog("文件存在。", "保存图片");
                await dialog.ShowAsync();
            }
        }

        public async Task<StorageFile> GetFileIfExistsAsync(StorageFolder folder, string fileName)
        {
            try
            {
                return await folder.GetFileAsync(fileName);
            }
            catch
            {
                return null;
            }
        }

        public async Task<StorageFolder> GetFolderIfExistsAsync(StorageFolder folder, string subName)
        {
            try
            {
                return await folder.GetFolderAsync(subName);
            }
            catch
            {
                return null;
            }
        }
    }

    public enum StorageType
    {
        Roaming, Local, Temporary
    }
}
