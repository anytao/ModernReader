using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OKr.Common.Context;
using OKr.Common.Storage;
using OKr.Win8Book.Client.Core.Builder;
using OKr.Win8Book.Client.Core.Data;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace OKr.Win8Book.Client.Core.Context
{
    public class BookContext : OKrStateContextBase<Book>
    {
        public BookContext()
            : base(new OKrStorage<Book>(OKrBookConstant.BOOK), true)
        {
 
        }

        protected async override Task<Book> DoLoad()
        {
            var book = await TextParser.GetBook("book\\category.txt");            

            Package package = Package.Current;
            StorageFolder installedLocation = package.InstalledLocation;

            var file = await StorageFile.GetFileFromPathAsync(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, @"Assets\Data\me.okr"));

            XmlDocument doc = await XmlDocument.LoadFromFileAsync(file);
            XDocument xdoc = XDocument.Parse(doc.GetXml());

            var ele = xdoc.Element("config").Element("okrbook");
            book.Name = ele.Element("name").Value;
            book.Author = ele.Element("author").Value;
            book.Desc = ele.Element("intro").Value; 

            return book;
        }

        protected override void DoSave(Book data)
        {
            throw new NotImplementedException();
        }
    }
}
