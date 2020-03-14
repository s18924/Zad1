﻿using System;
using System.Collections.Generic;
 using System.Security.Principal;
 using System.Text;
 using System.Xml.Serialization;

 namespace Cwiczenia_II
{
   public class Student
    {
      //
      //
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Data_urodzenia { get; set; }
        public string Email { get; set; }
        public string Imie_matki { get; set; }
        public string Imie_Ojca { get; set; }
        [XmlAttribute("indexNumber")]

        public string eska { get; set; }

        public Studia Studia { get; set; }



        /* public Student(string imie, string nazwisko, string data_urodzenia, string email, string imie_matki, string imie_Ojca)
         {
             Imie = imie;
             Nazwisko = nazwisko;
             Data_urodzenia = data_urodzenia;
             Email = email;
             Imie_matki = imie_matki;
             Imie_Ojca = imie_Ojca;
         }*/
    }

    public class Studia
    {
        public string name  { get; set; }
        public string mode { get; set; }

    }
}
