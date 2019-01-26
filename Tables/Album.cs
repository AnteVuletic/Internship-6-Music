using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Tables
{
    public class Album
    {
        public int ID_Album { get; set; }
        public string Name { get; set; }
        public DateTime YearOfRelease { get; set; }
        public int FK_Artist { get; set; }
    }
}
