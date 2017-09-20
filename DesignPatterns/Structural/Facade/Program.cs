using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Facade
{
    /*
    * Návrhový vzor Facade (fasáda) se používá k vytvoření jednotného rozhraní pro celou logickou skupinu tříd, které se tak sdruží do subsystému.
    *
    * Motivace
    * Mnohdy v naší aplikaci pracujeme s několika rozhraními různých tříd. To může být často nepřehledné, zejména pokud jsou rozhraní tříd složitá. 
    * Pokud spolu třídy logicky souvisí, můžeme je sdružit do subsystému pomocí fasády (Facade). Získáme tak jednotné rozhraní pro funkcionalitu, kterou nám subsystém poskytuje.
    *
    * Fasáda (Facade) je poměrně jednoduchý vzor, který se skládá z jedné třídy, která fasádu tvoří. Ta je napojena na další třídy, se kterými pracuje. 
    * Zvenku je však vidět jen fasáda (od toho název), a ta zastupuje rozhraní pro celý subsystém. Celá složitá struktura tříd je v pozadí.
    *
    * Sníží se tím počet tříd, se kterými komunikujeme. Subsystém se lépe používá i testuje. Jedná se tedy opět o prostředníka a je tu určitá podobnost se vzorem Adapter (Wrapper) .
    * My ovšem obalujeme rovnou několik tříd do jednoho logického subsystému.
    *
    * 
    */

    class Program
    {
        static void Main(string[] args)
        {
            // prime pouziti Adaptee
            Adaptee a = new Adaptee();
            double x = a.SpecificRequest(5, 6);

            // pouziti adapteru
            ITarget b = new Adapter();
            int y = b.Request(5);

            Console.WriteLine($"5/6 = {x} [through Adapter {y}]");
            Console.ReadLine();
        }
    }

    class Adaptee
    {
        public double SpecificRequest(double a, double b) => a / b;
    }

    interface ITarget
    {
        int Request(int i);
    }

    class Adapter : Adaptee, ITarget
    {
        public int Request(int i)
        {
            return (int) Math.Round(SpecificRequest(i, 6));
        }
    }
}
