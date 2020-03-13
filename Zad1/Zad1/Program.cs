﻿using System;
using System.Collections.Generic;
using System.IO;
 using System.Xml;
 using System.Xml.Serialization;

namespace Cwiczenia_II
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj lokalizację pliku!");
            string par1 = Console.ReadLine();

            Console.WriteLine("Podaj plik wyjściowy!");
            string par2 = Console.ReadLine();

            var lokalizacja = (String.IsNullOrEmpty(par1) ? "dane.csv" : par1);
            var output = (String.IsNullOrEmpty(par2) ? "result.xml" : par2);

            Console.WriteLine(lokalizacja);
            Console.WriteLine(output);

            var lines = File.ReadAllLines(lokalizacja); //tablica stringow jeden to jedna linijka

            var list = new List<Student>();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(",");

                for (int j = 0; j < line.Length; j++)
                {
                    Console.Write(line[j]);
                    Studia s = new Studia
                    {
                        name = line[6],
                        mode = line[7]
                    };

                    list.Add(new Student
                    {
                        Imie = line[0],
                        Nazwisko = line[1],
                        Data_urodzenia = line[2],
                        Email = line[3],
                        Imie_matki = line[4],
                        Imie_Ojca = line[5],
                        Studia = s
                    }) ;
                    //list.Add(new Student(line[0], line[1], line[2], line[3], line[4], line[5]));

                }
                Console.WriteLine();
            }

            Console.WriteLine(par1);
            FileStream writer = new FileStream(@"data.xml", FileMode.Create);   
            //?
           // var attr = new XmlRootAttribute("uczelnia");
           Studia ss = new Studia()
           {
               mode = "1112",
               name = "wewe"
           };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("ssss"));

            serializer.Serialize(writer, list);
        }


    }

  /*  FileStream writer = new FileStream(@"data.xml", FileMode.Create);
    XmlSerializer serializer = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("uczelnia"));
    var list = new List<Student>();
    list.Add(new Student { Imie = "Jan", Nazwisko = "Kowalski" });
    
     serializer.Serialize(writer, list);*/
}