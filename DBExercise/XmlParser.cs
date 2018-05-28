using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DBExercise
{
    class XmlParser
    {
        private string Filename = "database.xml";

        public List<Osoba> Parse()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Filename);

            XmlNode elPeople = doc.DocumentElement.SelectSingleNode("/osoby");
            XmlNodeList elPersonList = elPeople.ChildNodes;

            int id, wiek;
            string imie, nazwisko;
            Date dataUrodzenia;

            List<Osoba> listOfPeople = new List<Osoba>();
            Osoba person;

            foreach(XmlNode item in elPersonList)
            {
                id = Convert.ToInt32(item.Attributes["id"].InnerText.ToString());
                imie = item.Attributes["imie"].InnerText.ToString();
                nazwisko = item.Attributes["nazwisko"].InnerText.ToString();
                wiek = Convert.ToInt32(item.Attributes["wiek"].InnerText.ToString());
                dataUrodzenia = new Date(item.Attributes["data_urodzenia"].InnerText.ToString());
                person = new Osoba(id, imie, nazwisko, dataUrodzenia);
                listOfPeople.Add(person);
            }

            return listOfPeople;
        }
    }
}
