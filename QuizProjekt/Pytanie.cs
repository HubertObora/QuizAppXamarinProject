using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProjekt
{
    class Pytanie
    {
        public string Kategoria { get; set; }
        public string TrescPytania { get; set; }
        public string[] Odpowiedzi { get; set; }
        public int PoprawnaOdpowiedz { get; set; }
    }
}
