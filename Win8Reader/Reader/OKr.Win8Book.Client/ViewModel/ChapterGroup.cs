using OKr.Win8Book.Client.Core.Data;
using System.Collections.Generic;

namespace OKr.Win8Book.Client.ViewModel
{
    public class ChapterGroup
    {
        private string key = "";
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        private List<Chapter> chapters = new List<Chapter>();
        public List<Chapter> Chapters
        {
            get
            {
                return chapters;
            }
        }
    }
}
