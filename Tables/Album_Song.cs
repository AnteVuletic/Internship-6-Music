using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Tables
{
    public class Album_Song
    {
        public int FK_Album{ get; set; }
        public int FK_Song { get; set; }
    }
}
