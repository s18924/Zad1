﻿﻿using System;
using System.Collections.Generic;
using System.IO;
 using System.Reflection.Metadata.Ecma335;
 using System.Security.Principal;
 using System.Xml;
 using System.Xml.Serialization;
 using Cwiczenia_II;

 namespace Cwiczenia_II
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Student>();
            using (StreamWriter fw = new StreamWriter("log.txt"))
            {


                Console.WriteLine("Podaj lokalizację pliku!");
                string par1 = Console.ReadLine();

                var lokalizacja = (String.IsNullOrEmpty(par1) ? "dane.csv" : par1);

                if (!File.Exists(lokalizacja))
                {
                    Console.Error.Write("File not found!");
                    fw.Write("File not found! : " + lokalizacja + " exiting program!");
                    fw.Dispose();
                    Environment.Exit(1);
                }

                Console.WriteLine("Podaj plik wyjściowy!");
                string par2 = Console.ReadLine();

                var output = (String.IsNullOrEmpty(par2) ? "result.xml" : par2);

                Console.WriteLine(lokalizacja);
                Console.WriteLine(output);

                var lines = File.ReadAllLines(lokalizacja); //tablica stringow jeden to jedna linijka


                bool czyZapisac = true;



                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Split(",");
                    czyZapisac = true;


                    foreach (var z in line)
                    {
                        if (String.IsNullOrEmpty(z))
                        {
                            fw.Write("Niekompletny student: " + lines[i] + "\n");
                            czyZapisac = false;
                            break;
                        }
                    }

                    if (czyZapisac)
                    {
                        foreach (var z in list)
                        {
                            if (z.eska == line[4])
                            {
                                fw.Write("Duplikat studenta: " + lines[i] + "\n");
                                czyZapisac = false;
                                break;
                            }
                        }
                    }

                    if (czyZapisac)
                    {
                        Studia s = new Studia
                        {
                            name = line[2],
                            mode = line[3]
                        };

                        list.Add(new Student
                        {
                            Imie = line[0],
                            Nazwisko = line[1],
                            Data_urodzenia = line[5],
                            Email = line[6],
                            Imie_matki = line[7],
                            Imie_Ojca = line[8],
                            eska = line[4],
                            Studia = s
                        });

                    }

                }


                Dictionary<string, int> hash = new Dictionary<string, int>();

                for (int i = 0; i < list.Count; i++)
                {
                    if (hash.ContainsKey(list[i].Studia.name))
                        hash[list[i].Studia.name]++;

                    if (!hash.ContainsKey(list[i].Studia.name))
                        hash.Add(list[i].Studia.name, 1);

                }

                List<Studia> l = new List<Studia>();

                foreach (var i in hash)
                {
                    Console.WriteLine(i.Key + " " + i.Value);
                    l.Add(new Studia()
                    {
                        name2 = i.Key,
                        numberOfStudents = i.Value
                    });
                }


                FileStream writer = new FileStream(output, FileMode.Create);

                XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                Uczelnia u = new Uczelnia()
                {
                    createdAt = DateTime.Today.ToShortDateString(),
                    Author = "Szymon Pierzchała",
                    studenci = list,
                    activeStudies = l

                };
                serializer.Serialize(writer, u, ns);
                writer.Dispose();
            }
        }


    }

}

 public class Uczelnia
 {
     [XmlAttribute("createdAt")]
     public String createdAt { get; set; }
     [XmlAttribute("author")]
     public String Author { get; set; }

     public List<Student> studenci { get; set; }

     public List<Studia> activeStudies { get; set; }

 }