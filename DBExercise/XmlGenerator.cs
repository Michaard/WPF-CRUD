using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DBExercise
{
    class XmlGenerator
    {
        private string Filename = "database.xml";
        public void GenerateXml(List<Osoba> people)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement header = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, header);

            XmlElement elPeople = doc.CreateElement("osoby");
            doc.AppendChild(elPeople);

            for (int i = 0; i < people.Count(); i++)
            {
                XmlElement elPerson = doc.CreateElement("osoba");
                XmlAttribute atId = doc.CreateAttribute("id");
                XmlAttribute atName = doc.CreateAttribute("imie");
                XmlAttribute atSurname = doc.CreateAttribute("nazwisko");
                XmlAttribute atAge = doc.CreateAttribute("wiek");
                XmlAttribute atDateOfBirth = doc.CreateAttribute("data_urodzenia");

                atId.Value = people[i].Id.ToString();
                atName.Value = people[i].Imie;
                atSurname.Value = people[i].Nazwisko;
                atAge.Value = people[i].Wiek.ToString();
                atDateOfBirth.Value = people[i].DataUrodzenia.ToString();

                elPerson.Attributes.Append(atId);
                elPerson.Attributes.Append(atName);
                elPerson.Attributes.Append(atSurname);
                elPerson.Attributes.Append(atAge);
                elPerson.Attributes.Append(atDateOfBirth);

                elPeople.AppendChild(elPerson);
            }

            doc.Save(Filename);
        }
    }
}
