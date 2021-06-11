using  System.Collections.Generic;
using System;
namespace Fighter_Tower
{
    public class Player
    {
        public int Id { get; set; }
        public string charclass { get; set; }
        public string charname { get; set; }
        public int hp { get; set; }
        public int str { get; set; }
        public int dex { get; set; }
        public int vit { get; set; }
        public int minDmg { get; set; }
        public int maxDmg { get; set; }
        public string ultimate { get; set; }
        public string ultimatename { get; set; }
        public string npcdescription { get; set; }

        public int SpCount = 0;
        public int Cp = 0;
        public int uP = 0;

        public List<Skill> Skils { get; set; }

        public static void PlayerInfo(Player player, List<string> WparamsPrint)
        {
            Console.WriteLine("Очки Распределения: " + player.Cp);
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine(player.charname + " " + player.charclass + ":\n" + WparamsPrint[0] + "|" + player.str + "|" +
                "\n" + WparamsPrint[1] + "|" + player.dex + "|" + "\n" + WparamsPrint[2] + "|" + player.vit + "|" +
                "\n" + WparamsPrint[3] + "|" + player.hp + "|" + "\nУльта: " + player.ultimate);
            Program.KeyToContinue();
            Console.Clear();
        }

    }

}
