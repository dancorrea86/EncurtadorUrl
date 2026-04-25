using System;
using System.Collections.Generic;
using System.Text;

namespace EncurtadorUrl.Data.Model
{
    public class Url
    {
        public int id { get; set; }
        public string urlOriginal { get; set; }
        public string urlEncurtada { get; set; }
        public DateTime dataExpiracao { get; set; }
    }
}
