using System;
using System.Collections.Generic;
using System.Linq;

namespace Indexer
{

    public class Player
    {
        public int PlayerId { set; get; }
        public string Name { set; get; }
        public string Post { set; get; }
    }

    public class Team
    {
        private List<Player> list;

        public Team()
        {
            list = new List<Player>();
            list.Add(new Player { PlayerId = 1, Name = "Pavel Kouba", Post = "golman keeper" });
            list.Add(new Player { PlayerId = 2, Name = "Jan Stejskal", Post = "golman keeper" });
            list.Add(new Player { PlayerId = 3, Name = "Karel Poborsky", Post = "half-back" });
            list.Add(new Player { PlayerId = 4, Name = "Pavel Nedved", Post = "half-back" });
            list.Add(new Player { PlayerId = 5, Name = "Jan Koler", Post = "forward" });
            list.Add(new Player { PlayerId = 6, Name = "Pavel Kuka", Post = "forward" });
            list.Add(new Player { PlayerId = 7, Name = "Vladimir Smicer", Post = "forward" });
        }

        // Indexer, ktery prima playerId a vraci Jmeno hrace
        public string this[int playerId]
        {
            get
            {
                return list.FirstOrDefault(p => p.PlayerId == playerId).Name;
            }
        }

        // Indexer, ktery prima post hrace a vraci cely objekt
        public Player this[string name]
        {
            get
            {
                return list.FirstOrDefault(p => p.Name == name);
            }
        }

        // Indexer, ktery prijma typ postu a vraci pocet hracu na tomto postu
        public int this[string postType,  bool b]
        {
            get
            {
                return list.Count(p => p.Post == postType);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int x;

            for (int i = 1; i <= 10; i++)
            {
                x = 50;
                Console.WriteLine("{0} ^= {1}, {0} = {2}", x, i, x >> i);
            }

            Team team = new Team();
            Player tmpPlayer = team["Karel Poborsky"];
            Console.WriteLine("Player with name = Karel Poborsky has id = {0}", tmpPlayer.PlayerId);
            Console.WriteLine("Sum = {0}", team["forward", true]);
            Console.ReadLine();
        }
    }
}
