using System;
using System.Collections.Generic;
using System.Text;

namespace EncurtadorUrl.Data.Model
{
    public class Url
    {
        public int ID { get; set; }
        public string UrlOriginal { get; set; }
        public string UrlEncurtada { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}
