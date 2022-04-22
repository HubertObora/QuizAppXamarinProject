using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProjekt
{
    class Quiz
    {
        private string rodzaj;
        public string Rodzaj
        {
            get { return rodzaj; }
            set { rodzaj = value; }
        }

        private Pytanie [] listaPytan;
        public Pytanie[] ListaPytan
        {
            get { return listaPytan; }
            set { listaPytan = value; }
        }

        private int iloscPytan;
        public int IloscPytan
        {
            get { return iloscPytan; }
            set { iloscPytan = value; }
        }
    }
}
