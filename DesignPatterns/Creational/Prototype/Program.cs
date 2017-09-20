using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Prototype
{
  /*
  *  melká kopie - shallow copy - pomoci MemberwiseClone
  *  hluboka kopie - deep copy
  *  
  *  Shallow Copy | MemberwiseClone
  *     - kopiruje hodnoty vsech poli a referenci a vraci referenci na tuto kopie
  *     - NEKOPIRUJE na co reference v objektu ukazuje
  *     - tzn. vhodne pro objekty neobsahujici reference
  *     
  *  Deep Copy | Serialization
  *     - serializace muze byt provedena do ruznych umisteni vcetne disku nebo internetu
  *     - nejjednodussi (zejmena pro mensi objekty) je vhodne pouzit pamet
  *     - tzn. hluboka kopie se sklada ze serializace a deserializace 
  *     - oznaceni typu jako serializovatelny je provedeno promoci atributu [Serializable()]
  *     
  *     - POZOR: Serializace objektu je mozna pouze tehdy, pokud jsou serializovatelne take vsechny objekty, 
  *      na ktere vedou reference. Vyhnete se serializaci objektu, ktere obsahuji referenci na nejaky vnejsi 
  *      zdroj, jako je napr. otevreny soubor nebo pripojeni k databazi
  * /
  class Program
  {
    static void Main(string[] args)
    {
    }
  }
}
