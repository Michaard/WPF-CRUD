using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExercise
{
    class Osoba
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public Date DataUrodzenia { get; set; }

        public Osoba(int id,string imie,string nazwisko,Date dataUrodzenia)
        {
            this.Id = id;
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.DataUrodzenia = dataUrodzenia;

            int wiek = DateTime.Today.Year - dataUrodzenia.Year;
            if ((DateTime.Today.Month == dataUrodzenia.Month && DateTime.Today.Day < dataUrodzenia.Day) || DateTime.Today.Month < dataUrodzenia.Month)
                wiek--;
            this.Wiek = wiek;
        }
    }
}
