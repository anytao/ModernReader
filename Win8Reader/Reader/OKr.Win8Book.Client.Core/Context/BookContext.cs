using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Context;
using OKr.Common.Storage;
using OKr.Win8Book.Client.Core.Builder;
using OKr.Win8Book.Client.Core.Data;

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
            book.Name = "三国演义";

            return book;
        }

        protected override void DoSave(Book data)
        {
            throw new NotImplementedException();
        }
    }
}
