using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Factory
{
    /*
    * Factory (nebo též počeštěně "faktorka") je jeden z nejdůležitějších návrhových vzorů, který umožňuje vyšší abstrakci při vytváření třídy než klasický konstruktor. 
    * Typicky se používá pro zapouzdření složitější inicializace instance a pro vytváření různých typů instancí podle řetězce.
    *
    * Motivace
    * V aplikacích se nám občas stává, že potřebujeme vytvořit instanci nějaké třídy a tu dodatečně inicializovat. Dobrým praktickým příkladem jsou formulářové komponenty, u kterých nestačí pouze instanci vytvořit, 
    * ale musíme ji také nastavit spoustu dalších vlastností (rozměry, titulek, pozici, barvu...). 
    * Pokud někde v aplikaci vytváříte 20 podobných tlačítek a vytvoření takového tlačítka zabírá 10 řádků, nutně vás napadne oddělit tento kód do metody. 
    * Gratuluji, právě jste vynalezli factory. (samozřejmě má vzor nějaké další konvence)
    *
    * Dále nám může faktorka uchovávat proměnné, které potřebujeme k vytváření objektu. Tyto proměnné potom nemusí prostupovat celým programem. 
    * Další výhodou je návratový typ, které nemusí být u faktorky specifikován přesně na typ objektu, který vytváříme. Můžeme vracet některou z rodičovských tříd nebo i rozhraní. Na každý z těchto faktorů se podíváme blíže.
    */

    /*
    * Varianta I.
    * Tovarna je v oddelene tride a obsahuje metodu pro vytvoreni instance Auta
    */
    class Auto
    {
        private string Znacka;
        private string Model;

        public Auto(string znacka, string model)
        {
            Znacka = znacka;
            Model = model;
        }
    }

    class TovarnaNaAuto
    {
        public Auto VytvorFelicii()
        {
            return new Auto("Skoda", "Felicie");
        }
    }

    /*
    * Varianta II.
    * Verze s privatnim kontruktorem, instance lze vytvorit pouze tovarni metodou.
    */
    class Auto2
    {
        private string Znacka;
        private string Model;

        private Auto2(string znacka, string model)
        {
            Znacka = znacka;
            Model = model;
        }

        public static Auto VytvorFelicii()
        {
            return new Auto("Skoda", "Felicie");
        }
    }

    /*
    * Varianta III.
    * Nevracime stejny typ, ale pokazde jiny typ, podle potreby
    * Proto implementace s rozhranim
    */
    interface IVykreslitelny
    {
        void Vykresli();
    }

    class Ctverec : IVykreslitelny
    {
        public void Vykresli() { }
    }

    class Kruh : IVykreslitelny
    {
        public void Vykresli() { }
    }

    class ObrazceFaktory
    {
        public IVykreslitelny Vytvor(string typ)
        {
            if (typ == "Ctverec") { return new Ctverec(); }
            else if (typ == "Kruh") { return new Kruh(); }
            else { return null; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Varianta I.
            TovarnaNaAuto tovarna = new TovarnaNaAuto();
            Auto Felicie = tovarna.VytvorFelicii();

            // Varianta II.
            Auto Felicie2 = Auto2.VytvorFelicii();

            // Varianta III.
            ObrazceFaktory faktory = new ObrazceFaktory();
            IVykreslitelny tvar = faktory.Vytvor("Kruh");
        }
    }
}
