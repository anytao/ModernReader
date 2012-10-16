using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Data;

namespace OKr.Win8Book.Client.Core.Data
{
    [DataContract]
    public class Setting : EntityBase
    {
        [DataMember(Name = "font")]
        public int Font { get; set; }
    }
}
