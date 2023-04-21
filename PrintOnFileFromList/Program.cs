using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PrintOnFileFromList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // DataStore<int> dataStoreNumber = new DataStore<int>();
            DataStore<Person> dataStorePerson = new DataStore<Person>();

            dataStorePerson.PrintProperties(new Person());
            //dataStoreNumber.Add(10);
            //dataStoreNumber.Remove(20);

            //dataStorePerson.Add(new Person());
            //dataStorePerson.Remove(10); 

            string filePath = "C:\\Users\\faust\\source\\repos\\PrintOnFileFromList\\log\\log.csv";
            List<Person> people = new List<Person>();
            people.Add(new() { Nome = "Bruno", SurName = "bubba", CF = "brbb123" });
            people.Add(new() { Nome = "Paolo", SurName = "bubbo", CF = "brbc123" });
            people.Add(new() { Nome = "Luca", SurName = "bubbe", CF = "brbd123" });
            people.Add(new() { Nome = "Simone", SurName = "bubbi", CF = "brbe123" });
            TextFileGenerator.SaveToFile(people, filePath);

        }
    }

    public class DataStore<T> where T : class, new()
    {
        static int _index = 4;
        public T[] _data = new T[_index];  //string[]  
        static T entry = new T();


        public void Add(T person)
        {

        }
        public void Remove(T item)
        {
            if (_data[_data.Length - 1] != null)
            {
                GetMoreSpace();
            }
            // prendo il primo posto libero == null 
            var element = Array.IndexOf(_data, Array.Find(_data, x => x == null));
            _data[element] = item;
            // inserire l'emento del posto libeor che ha trovato 
        }
        public void GetMoreSpace()
        {
            T[] _nextData = new T[_data.Length + 4];
            Array.Copy(_data, _nextData, _data.Length);
            _data = _nextData;
        }

        public void PrintProperties(T person)
        {
            var campi = person.GetType().GetProperties();

            foreach (var prop in campi)
            {

                Console.WriteLine(prop.Name); // Nome della property / Campo             


            }
        }
    }
    public class Person
    {
        public string Nome { get; set; }
        public string SurName { get; set; }
        public string CF { get; set; }
    }

    public static class TextFileGenerator
    {

        // Serialization 

        public static void SaveToFile<T>(List<T> data, string filePath)
        {

            //    Name|Surname|CF 

            List<string> lines = new List<string>();
            StringBuilder line = new StringBuilder();

            // check if data != null || == 0 
            if (data == null || data.Count == 0) return;


            var cols = data[0].GetType().GetProperties();


            // File does not esist! 
            if (!File.Exists(filePath))
            {
                foreach (var col in cols)
                {

                    string prop = col.Name;

                    // build my output string  by columns' Name 

                    line.Append(string.Format(prop));
                    if (col != cols.Last())
                    {
                        line.Append(';');

                    }
                    else
                    {
                        line.Append('\n');
                    }

                }

                File.AppendAllText(filePath, line.ToString());
            }


            foreach (var row in data)
            {
                // new object of T type

                line = new StringBuilder();

                foreach (var col in cols)
                {
                    // build my output string by columns' Values 
                    var value = col.GetValue(row);
                    line.Append($"{value}");
                    if (col != cols.Last())
                    {
                        line.Append(';');

                    }
                    else
                    {
                        line.Append('\n');
                    }


                }
                // Write all Lines into a CSV file

                File.AppendAllText(filePath, line.ToString());



            }



        }

        // Deserialization 
        public static List<T> LoadFromCSVFile<T>(string filePath, ) where T : class, new()
        {
            var cols = data[0].GetType().GetProperties();

            foreach (var col in cols)
            {
            List<string> lines = System.IO.File.ReadLines(filePath).ToList();

                //  Read From file and convert values to T Type!
                var convertedValue = Convert.ChangeType(value, col.PropertyType);
                col.SetValue(, convertedValue);
                // -> TODO
            }

            return new List<T>;
        }
    }
}
