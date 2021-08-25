using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusic.Models
{
    public class Musica
    {

        public int id { get; set; }
        public int id_album { get; set; }
        public string name { get; set; }
        public string artist { get; set; }
        public string cover { get; set; }
        public string color { get; set; }
        public string audio { get; set; }
        public bool isFavorite { get; set; }
        public string HeartImage { get; set; }



    }

    public class MusicaList
    {
        public string audio { get; set; }

    }
}
