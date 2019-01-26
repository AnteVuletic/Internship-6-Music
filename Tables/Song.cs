using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Tables
{
    public class Song
    {
        public int ID_Song { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
