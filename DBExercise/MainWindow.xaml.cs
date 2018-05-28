using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace DBExercise
{
    public partial class MainWindow : Window
    {

        private const string CONNECTION_STRING = "server=desktop-a7j81j7; database=Graniczka_M_.Net_Database;Trusted_Connection=True;";
        private const string TABLE_NAME = "TestTableLB";
        private int CurrentId = 0;
        private int SelectedId = 0;
        private Osoba SelectedPerson;
        private List<Osoba> AllPeople;

        private const int ONLINE_MODE = 1;
        private const int OFFLINE_MODE = 2;

        private int MODE;

        public MainWindow()
        {
            InitializeComponent();
            TestConnection();
            FillDataGrid_Main(TABLE_NAME);
        }

        private void TestConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(CONNECTION_STRING);
                connection.Open();
                connection.Close();
                MODE = ONLINE_MODE;
            }
            catch (Exception)
            {
                MODE = OFFLINE_MODE;
                MessageBox.Show("Błąd połączenia z bazą danych. Wszystkie operacje będą przeprowadzane na plikach XML.");
            }
        }

        private void FillDataGrid_Main(string tableName)
        {
            switch (MODE)
            {
                case ONLINE_MODE:
                    {
                        try
                        {
                            SqlConnection connection = new SqlConnection(CONNECTION_STRING);
                            string query = "SELECT ID,Imie,Nazwisko,Wiek,convert(varchar(10), DataUrodzenia, 120) AS 'Data Urodzenia' FROM " + tableName + ";";

                            connection.Open();

                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();

                            DataTable dt = GetDataTableFromDataBase(cmd, tableName);
                            DataGrid_Main.ItemsSource = dt.DefaultView;
                            SetCurrentIdAndFillListOfPeople(dt);

                            connection.Close();
                            NullifySelectedId();
                            ClearTextBoxes();
                            SaveToXmlFile();
                            break;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            break;
                        }
                    }
                case OFFLINE_MODE:
                    {
                        LoadFromXmlFile();
                        DataGrid_Main.ItemsSource = ConvertListToDataTable(AllPeople).DefaultView;
                        SetCurrentId();
                        NullifySelectedId();
                        ClearTextBoxes();
                        break;
                    }
            }
        }

        private DataTable GetDataTableFromDataBase(SqlCommand cmd, string tableName)
        {
            try
            {
                DataTable dt = new DataTable(tableName);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private void SetCurrentIdAndFillListOfPeople(DataTable dt)
        {
            int id, wiek;
            string imie, nazwisko, dataUrodzenia;
            AllPeople = new List<Osoba>();
            Osoba osoba;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                imie = dt.Rows[i]["Imie"].ToString();
                nazwisko = dt.Rows[i]["Nazwisko"].ToString();
                wiek = Convert.ToInt32(dt.Rows[i]["Wiek"].ToString());
                dataUrodzenia = dt.Rows[i]["Data Urodzenia"].ToString();

                osoba = new Osoba(id, imie, nazwisko, new Date(dataUrodzenia));
                AllPeople.Add(osoba);

                if (id > CurrentId)
                    CurrentId = id;
            }
        }

        private void SetCurrentId()
        {
            int id;
            for (int i = 0; i < AllPeople.Count(); i++)
            {
                id = AllPeople[i].Id;
                if (id > CurrentId)
                    CurrentId = id;
            }
        }

        private void DataGrid_Main_MouseClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGrid_Main.CurrentItem == null) return;
            int id, wiek;
            string imie, nazwisko, dataUrodzenia;
            id = Convert.ToInt32(GetDataFromGridCell(0));
            imie = GetDataFromGridCell(1);
            nazwisko = GetDataFromGridCell(2);
            wiek = Convert.ToInt32(GetDataFromGridCell(3));
            dataUrodzenia = GetDataFromGridCell(4);

            SelectedPerson = new Osoba(id, imie, nazwisko, new Date(dataUrodzenia));
            SelectedId = id;

            TextBox_Imie.Text = imie;
            TextBox_Nazwisko.Text = nazwisko;
            TextBox_DataUrodzenia.Text = dataUrodzenia;
        }

        private string GetDataFromGridCell(int column)
        {
            var cellInfo = DataGrid_Main.SelectedCells[column];
            var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            return content.ToString();
        }

        private bool CorrectDateInput(string date)
        {
            
            if (!date.Contains("-"))
                return false;
            int count = date.Split('-').Count();
            int lenght = date.Length;
            int lettersContained = Regex.Matches(date, @"[a-zA-Z]").Count;
            if (count < 3 || count > 3)
                return false;
            else if (lenght < 10 || lenght > 10)
                return false;
            else if (lettersContained > 0)
                return false;

            string[] dateSplited = date.Split('-');
            if (dateSplited[1].Count() != 2 || dateSplited[2].Count() != 2)
                return false;
            else if (Convert.ToInt32(dateSplited[1]) > 12)
                return false;
            else if (Convert.ToInt32(dateSplited[2]) > 31)
                return false;

            return true;
        }

        private void InsertIntoDataBase(Osoba osoba)
        {
            switch (MODE)
            {
                case ONLINE_MODE:
                    {
                        try
                        {
                            AllPeople.Add(osoba);
                            SqlConnection connection = new SqlConnection(CONNECTION_STRING);
                            connection.Open();

                            string query = "INSERT INTO " + TABLE_NAME + " VALUES ( " +
                                osoba.Id + ", '" + osoba.Imie + "', '" + osoba.Nazwisko + "', " + osoba.Wiek + ", '" + osoba.DataUrodzenia.ToString()
                                + "'); ";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();

                            connection.Close();
                            break;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            break;
                        }
                    }
                case OFFLINE_MODE:
                    {
                        AllPeople.Add(osoba);
                        SaveToXmlFile();
                        break;
                    }
            }
            
        }

        private void DeleteFromDataBase(Osoba osoba)
        {
            switch (MODE)
            {
                case ONLINE_MODE:
                    {
                        try
                        {
                            AllPeople.Remove(osoba);
                            SqlConnection connection = new SqlConnection(CONNECTION_STRING);
                            connection.Open();

                            string query = "DELETE FROM " + TABLE_NAME + " WHERE ID=" + SelectedId + ";";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();

                            connection.Close();
                            break;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            break;
                        }
                    }
                case OFFLINE_MODE:
                    {
                        for (int i = 0; i < AllPeople.Count(); i++)
                        {
                            if (AllPeople[i].Id == osoba.Id)
                            {
                                AllPeople.RemoveAt(i);
                                break;
                            }
                        }
                        SaveToXmlFile();
                        break;
                    }
            }
            
        }

        private void UpdateDataBase(Osoba osoba)
        {
            switch (MODE)
            {
                case ONLINE_MODE:
                    {
                        try
                        {
                            for (int i = 0; i < AllPeople.Count(); i++)
                            {
                                if (AllPeople[i].Id == osoba.Id)
                                {
                                    AllPeople[i] = osoba;
                                    break;
                                }
                            }
                            SqlConnection connection = new SqlConnection(CONNECTION_STRING);
                            connection.Open();

                            string query = "UPDATE " + TABLE_NAME + " SET Imie = '" + osoba.Imie + "', Nazwisko = '" + osoba.Nazwisko + "', Wiek = " + osoba.Wiek + ", DataUrodzenia = '" + osoba.DataUrodzenia.ToString() + "' WHERE ID=" + osoba.Id + ";";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();

                            connection.Close();
                            break;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            break;
                        }
                    }
                case OFFLINE_MODE:
                    {
                        for (int i = 0; i < AllPeople.Count(); i++)
                        {
                            if (AllPeople[i].Id == osoba.Id)
                            {
                                AllPeople[i] = osoba;
                                break;
                            }
                        }
                        SaveToXmlFile();
                        break;
                    }
            }
            
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string imie = TextBox_Imie.Text;
            string nazwisko = TextBox_Nazwisko.Text;
            string dataUrodzenia = TextBox_DataUrodzenia.Text;
            if (imie.Count() == 0 || nazwisko.Count() == 0 || dataUrodzenia.Count() == 0)
                MessageBox.Show("Wszystkie pola muszą być wypełnione!");
            else if (!CorrectDateInput(dataUrodzenia))
                MessageBox.Show("Błędnie wpisana data!");
            else
            {
                CurrentId++;
                Osoba osoba = new Osoba(CurrentId, imie, nazwisko, new Date(dataUrodzenia));
                InsertIntoDataBase(osoba);
                FillDataGrid_Main(TABLE_NAME);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            string imie = TextBox_Imie.Text;
            string nazwisko = TextBox_Nazwisko.Text;
            string dataUrodzenia = TextBox_DataUrodzenia.Text;
            if (imie.Count() == 0 || nazwisko.Count() == 0 || dataUrodzenia.Count() == 0 || SelectedId==0)
                MessageBox.Show("Nie wybrano elementu do skasowania!");
            else
            {
                Osoba osoba = new Osoba(SelectedId, imie, nazwisko, new Date(dataUrodzenia));
                DeleteFromDataBase(osoba);
                FillDataGrid_Main(TABLE_NAME);
            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            string imie = TextBox_Imie.Text;
            string nazwisko = TextBox_Nazwisko.Text;
            string dataUrodzenia = TextBox_DataUrodzenia.Text;
            if (imie.Count() == 0 || nazwisko.Count() == 0 || dataUrodzenia.Count() == 0)
                MessageBox.Show("Wszystkie pola muszą być wypełnione!");
            else if (!CorrectDateInput(dataUrodzenia))
                MessageBox.Show("Błędnie wpisana data!");
            else
            {
                int keptId = SelectedPerson.Id;
                Osoba osoba = new Osoba(keptId, imie, nazwisko, new Date(dataUrodzenia));
                UpdateDataBase(osoba);
                FillDataGrid_Main(TABLE_NAME);
            }
        }

        private void TextBox_Input_Event(object sender, RoutedEventArgs e)
        {
            NullifySelectedId();
        }

        private void NullifySelectedId()
        {
            SelectedId = 0;
        }

        private void ClearTextBoxes()
        {
            TextBox_Imie.Clear();
            TextBox_Nazwisko.Clear();
            TextBox_DataUrodzenia.Clear();
        }

        private void SaveToXmlFile()
        {
            XmlGenerator g = new XmlGenerator();
            g.GenerateXml(AllPeople);
        }

        private void LoadFromXmlFile()
        {
            XmlParser p = new XmlParser();
            AllPeople = p.Parse();
        }

        private DataTable ConvertListToDataTable(List<Osoba> list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Imie");
            dt.Columns.Add("Nazwisko");
            dt.Columns.Add("Wiek");
            dt.Columns.Add("Data Urodzenia");

            for (int i = 0; i < list.Count(); i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["ID"] = list[i].Id.ToString();
                dt.Rows[i]["Imie"] = list[i].Imie;
                dt.Rows[i]["Nazwisko"] = list[i].Nazwisko;
                dt.Rows[i]["Wiek"] = list[i].Wiek.ToString();
                dt.Rows[i]["Data Urodzenia"] = list[i].DataUrodzenia.ToString();
            }

            return dt;
        }
    }
}
