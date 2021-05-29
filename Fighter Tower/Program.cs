using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fighter_Tower
{
    class Player
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

        
    }

    

    class Program
    {
        static public List<Player> npclist = new List<Player>();
        static public List<Player> charlist = new List<Player>();
        static int HpCulc (int vit)
        {
            int hp = vit * 15;
            return hp;
        }
        static void NpcAdd()
        {
            npclist.Add(new Player()
            {
                Id = 1,
                charname = "Бродяга",
                str = 6,
                dex = 6,
                vit = 6,
                hp = HpCulc(6),
                npcdescription = "Бродяга, одетый в лохмотья и с кастетом в руках"
            });
            npclist.Add(new Player()
            {
                Id = 2,
                charname = "Дворовой Задира",
                str = 8,
                dex = 6,
                vit = 10,
                hp = HpCulc(10),
                npcdescription = "Майка алкашка, порваные джинсы, в одной руке нож, в другой разбитая бутылка"
            });
            npclist.Add(new Player()
            {
                Id = 3,
                charname = "Уличный Боец",
                str = 10,
                dex = 8,
                vit = 10,
                hp = HpCulc(10),
                npcdescription = "Голый торс, сверкающий пресс, забинтованные кулаки"
            });
            npclist.Add(new Player()
            {
                Id = 4,
                charname = "Беглый Солдат",
                str = 10,
                dex = 8,
                vit = 10,
                hp = HpCulc(10),
                npcdescription = "Красная берета, военая майка, черные штаны и берцы. В руке держит штык-нож"
            });
            npclist.Add(new Player()
            {
                Id = 5,
                charname = "Буйный Мексиканец",
                str = 16,
                dex = 4,
                vit = 12,
                hp = HpCulc(12),
                npcdescription = "Старое самбреро, шрам на правой стороне лица, орудует огромным мачете"
            });
        }
        static void ClassesAdd ()
        {
            

            charlist.Add(new Player()
            {
                Id = 1,
                charclass = "Монах",
                str = 10,
                dex = 10,
                vit = 10,
                hp = HpCulc(10),
                ultimate = "\"Двойной Удар\" \n||Раз за бой," +
                " можно выбрать два вектора атаки.||"
            });
            charlist.Add(new Player()
            {
                Id = 2,
                charclass = "Ниндзя",
                str = 8,
                dex = 36,
                vit = 6,
                hp = HpCulc(6),
                ultimate = "\"Бросок Сюрикена\" \n||Раз за бой," +
               " атакует с дистанции, при этом не получая атаки в ответ.||"
            });
            charlist.Add(new Player()
            {
                Id = 3,
                charclass = "Дикарь",
                str = 12,
                dex = 2,
                vit = 16,
                hp = HpCulc(16),
                ultimate = "\"Запасной Щит\" \n||Раз за бой," +
               " может защищаться в двух направлениях.||"
            });
            charlist.Add(new Player()
            {
                Id = 4,
                charclass = "Фаталист",
                str = 16,
                dex = 8,
                vit = 6,
                hp = HpCulc(6),
                ultimate = "\"Смертельный Удар\" \n||Раз за бой," +
               " производит мощный удар, при попадании наносит х3 урон.||"
            });
        }
        static Player CharSelect()
        {
            Console.WriteLine("\tВыбор персонажа");

            foreach (Player c in charlist)
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine(c.Id + " " + c.charclass + ":\nСила: |" + c.str + "|" + "\nЛовкость: |" + c.dex + "|" +
                    "\nВыносливость: |" + c.vit + "|" + "\nЗдоровье: |" + c.hp + "|" + "\nУльта: " + c.ultimate );
            }
            Console.Write("Введите ID персонажа: ");
            int inputId = Convert.ToInt32(Console.ReadLine());
            var player = charlist.First(x => x.Id == inputId);
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.Write("Введите имя персонажа: ");
            string playername = Console.ReadLine();
            Console.Clear();
            player.charname = playername;
            return player;
        }
        static Player GetNpc(int floorlvl)
        {
            int currentfloor = floorlvl;
            var npc = npclist.First(x => x.Id == currentfloor);
            return npc;
        }
        static void NpcInfoPrint (int npcnum)
        {
            var npc = npclist.First(x => x.Id == npcnum);
            Console.WriteLine(npc.charname + ":\nСила: |" + npc.str + "|" + "\nЛовкость: |" + npc.dex + "|" +
                    "\nВыносливость: |" + npc.vit + "|" + "\nЗдоровье: |" + npc.hp + "|" + "\n\"" + npc.npcdescription + "\"");
        }
       
        static void Main(string[] args)
        {
            ClassesAdd();
            NpcAdd();

            Player player = CharSelect();

            FirstFloor(player);
            

        }
        static void FirstFloor(Player player)
        {
            int floor = 1;
            Player player1 = player;
            Player npc = GetNpc(floor);
            
            
            
           
            Fight(player1, npc, floor);
        }
        static void Fight(Player player, Player npc, int floor)
        {
            Random rnd = new Random();
            int pHp = player.hp;
            int nHp = npc.hp;
            int dmgDone;
            int dmgRecived;

            while (true)
            {
                Console.WriteLine("Начало боя!!!");
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.Write("Ваш соперник ");
                NpcInfoPrint(floor);
                Console.WriteLine("");
                Console.WriteLine("\t|| Здоровье " + player.charname + ": " + pHp + " ||" + "\t || Здоровье " + npc.charname + ": " + nHp);
                Console.WriteLine("");
                Console.WriteLine("");
                if (nHp <= 0)
                {
                    Console.WriteLine("- - - - - Вы Победили - - - - -");
                    break;
                }
                if (pHp <= 0)
                {
                    Console.WriteLine("- - - - - Вы Проиграли - - - - -");
                    break;
                }
                Console.WriteLine("Выберите действие атаки");
                Console.WriteLine("");
                Console.WriteLine("Пробел : Ульта \nСтрелка Вверх : Удар в голову \nСтрелка В Право : Удар в тело" +
                    " \nСтрелка Вниз : Удар по ногам" );
                ConsoleKey pAttackInput = Console.ReadKey().Key;
                int npcDef = rnd.Next(1, 4);
                if (pAttackInput == ConsoleKey.UpArrow || pAttackInput == ConsoleKey.DownArrow || pAttackInput == ConsoleKey.RightArrow)
                {
                    dmgDone = DmgOut(player.str, npcDef, pAttackInput, player);
                    if (dmgDone > 0)
                    {
                        int critwasp = CritChance(player.dex);
                        if (pAttackInput == ConsoleKey.UpArrow)
                        {
                            PrintSuccessAttack(1, player, npc);
                        }
                        if (pAttackInput == ConsoleKey.RightArrow)
                        {
                            PrintSuccessAttack(2, player, npc);
                        }
                        if (pAttackInput == ConsoleKey.DownArrow)
                        {
                            PrintSuccessAttack(3, player, npc);
                        }
                        if (critwasp == 1)
                        {
                            Console.WriteLine(">>>>> " + player.charname + " нанес " + dmgDone + " урона <<<<<");
                            nHp = nHp - dmgDone;
                        }
                        if (critwasp > 1)
                        {
                            dmgDone = dmgDone * critwasp;
                            Console.WriteLine("");
                            Console.WriteLine("- - - - - \tКритический Урон!!! \t- - - - -");
                            Console.WriteLine("");
                            Console.WriteLine(">>>>> " + player.charname + " нанес " + dmgDone + " урона <<<<<"); 
                            nHp = nHp - dmgDone;
                        }


                    }
                    if (dmgDone < 0)
                    {
                        if (pAttackInput == ConsoleKey.UpArrow)
                        {
                            PrintFailedAttack(1, player, npc);
                        }
                        if (pAttackInput == ConsoleKey.RightArrow)
                        {
                            PrintFailedAttack(2, player, npc);
                        }
                        if (pAttackInput == ConsoleKey.DownArrow)
                        {
                            PrintFailedAttack(3, player, npc);
                        }
                    }
                }
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Выберите действие защиты");
                Console.WriteLine("");
                Console.WriteLine("Стрелка Вверх : Защита головы \nСтрелка В Право : Защита корпуса" +
                   " \nСтрелка Вниз : Защита ног");
                ConsoleKey pDefInput = Console.ReadKey().Key;
                
                int npcAttack = rnd.Next(1, 4);

               
                dmgRecived = DmgIn(npc.str, npcAttack, pDefInput, npc);
                if (dmgRecived > 0)
                {
                    int critwasn = CritChance(npc.dex);
                    if (npcAttack == 1)
                    {
                        PrintFailedEvade(1, player, npc);
                    }
                    if (npcAttack == 2)
                    {
                        PrintFailedEvade(2, player, npc);
                    }
                    if (npcAttack == 3)
                    {
                        PrintFailedEvade(3, player, npc);
                    }
                    if (critwasn == 1)
                    {
                        Console.WriteLine(">>>>> " + npc.charname + " нанес " + dmgRecived + " урона <<<<<");
                        pHp = pHp - dmgRecived;
                    }
                    if (critwasn > 1)
                    {
                        dmgRecived = dmgRecived * critwasn;
                        Console.WriteLine("");
                        Console.WriteLine("- - - - - \tКритический Урон!!! \t- - - - -");
                        Console.WriteLine("");
                        Console.WriteLine(">>>>> " + npc.charname + " нанес " + dmgRecived + " урона <<<<<");
                        pHp = pHp - dmgRecived;
                    }
                   
                }
                if (dmgRecived < 0)
                {
                    if (npcAttack == 1)
                    {
                        PrintSuccessEvade(1, player, npc);
                    }
                    if (npcAttack == 2)
                    {
                        PrintSuccessEvade(2, player, npc);
                    }
                    if (npcAttack == 3)
                    {
                        PrintSuccessEvade(3, player, npc);
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("Нажмите любую кнопку для продолжения.");
                Console.ReadKey();
                Console.Clear();


            }
          
        }
        static int DmgOut(int str, int nDef, ConsoleKey inputkeyA, Player player)
        {
            Random minmax = new Random();
            int pAttack = KeyToInt(inputkeyA);
            int pDmg = str * 2;
            player.minDmg = (int) pDmg - (int) Math.Round(pDmg * 0.20);
            player.maxDmg = (int) pDmg + (int) Math.Round(pDmg * 0.20);
            int totalDmg = 0;

            if (pAttack != nDef)
            {
                totalDmg = minmax.Next(player.minDmg, player.maxDmg);
            }
            if (pAttack == nDef)
            {
                totalDmg = -1;
            }

            return totalDmg;
        }
        static int DmgIn (int str, int nAttack, ConsoleKey inputkeyD, Player npc)
        {
            Random minmax = new Random();
            int pDef = KeyToInt(inputkeyD);
            int nDmg = str * 2;
            npc.minDmg = (int)nDmg - (int)Math.Round(nDmg * 0.20);
            npc.maxDmg = (int)nDmg + (int)Math.Round(nDmg * 0.20);
            int totalDmg = 0;

            if (nAttack != pDef)
            {
                totalDmg = minmax.Next(npc.minDmg, npc.maxDmg);
            }
            if (nAttack == pDef)
            {
                totalDmg = -1;
            }

            return totalDmg;
        }
        static int CritChance (int dex)
        {
            Random critrnd = new Random();
            int critchance = dex * 2;
            int crit = critrnd.Next(1, 101);
            if (crit <= critchance)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        static int KeyToInt(ConsoleKey consoleKey)
        {
            int outputkey = 0;
            if (consoleKey == ConsoleKey.UpArrow)
            {
                outputkey = 1;
            }
            if (consoleKey == ConsoleKey.RightArrow)
            {
                outputkey = 2;
            }
            if (consoleKey == ConsoleKey.DownArrow)
            {
                outputkey = 3;
            }
            return outputkey;
        }
        static void PrintSuccessAttack (int dir, Player player, Player npc)
        {
            Random rndEvent = new Random();
            int _event = rndEvent.Next(1, 4);
            if (dir == 1)
            {
                if (_event == 1)
                {
                    Console.WriteLine(player.charname + " Наносит молниеносный удар с разворота, в голову, что аж " + npc.charname +
                        " потерял четыре зуба.");
                }
                if (_event == 2)
                {
                    Console.WriteLine("Свирепый " + player.charclass + " со всего маху дает подзатыльник! Теперь " + npc.charname +
                        " уж точно не сможет затянуть язык обратно.");
                }
                if (_event == 3)
                {
                    Console.WriteLine(player.charname + " скачком приближается и наносит сокрушительный удар головой в нос " + 
                        npc.charname + " потребуется пластическая операция.");
                }
            }
            if (dir == 2)
            {
                if (_event == 1)
                {
                    Console.WriteLine(player.charname + " вспоминает тренеровки каратэ, и исполняет грациозный майигере. У " + npc.charname +
                        " теперь, животик бо-бо.");
                }
                if (_event == 2)
                {
                    Console.WriteLine("Кувырком " + player.charclass + " сближается с " + npc.charname +
                        " и наносит удар в солнечное сплетение, тем самым задев и лунное сплетение.");
                }
                if (_event == 3)
                {
                    Console.WriteLine(player.charname + " разгоняется и с двух ног залетает " +
                        npc.charname + " прямо в грудак, оставляя мятины.");
                }
            }
            if (dir == 3)
            {
                if (_event == 1)
                {
                    Console.WriteLine(player.charname + " пробивает мощный лоу-кик, что " + npc.charname +
                        " от этой подсечки будет крутиться в воздухе еще мин 5. Ничего, мы подождем.");
                }
                if (_event == 2)
                {
                    Console.WriteLine("Коварный " + player.charclass + " садится на шпагат и дает по яйам! " + npc.charname +
                        " теперь имеет очень тонкий голос");
                }
                if (_event == 3)
                {
                    Console.WriteLine(player.charname + " одичал, схватился за ногу, посолил и начал кусать! " + 
                        npc.charname + " это не очень нравится");
                }
            }
        }
        static void PrintFailedAttack (int dir, Player player, Player npc)
        {
            Random rndEvent = new Random();
            int _event = rndEvent.Next(1, 4);
            if (dir == 1)
            {
                if (_event == 1)
                {
                    Console.WriteLine(player.charname + " Попытался дать с руки по башке, но " + npc.charname +
                        " подскользнувшись, увернулся от удара.");
                }
                if (_event == 2)
                {
                    Console.WriteLine("Неуклюжий " + player.charclass + " с разворота хотел ударить в голову и упал! " + npc.charname +
                        " над вами смеется.");
                }
                if (_event == 3)
                {
                    Console.WriteLine(player.charname + " хотел взять с разгона на баш, но устал так и не добежав. Пора бросать курить.");
                }
            }
            if (dir == 2)
            {
                if (_event == 1)
                {
                    Console.WriteLine(player.charname + " пробовал вспомнить тренеровки каратэ, так и не вспомнил. Решил пропустить ход.");
                }
                if (_event == 2)
                {
                    Console.WriteLine("Кувырком " + player.charclass + " сближается с " + npc.charname +
                        " и понимает, что вестибулярный аппарат подводит! Теперь сильно кружится голова.");
                }
                if (_event == 3)
                {
                    Console.WriteLine(player.charname + " разгоняется и с двух ног залетает в тень " +
                        npc.charname + ". Надо подлечить зрение.");
                }
            }
            if (dir == 3)
            {
                if (_event == 1)
                {
                    Console.WriteLine(player.charname + " хватается за ногу " + npc.charname +
                        "... А, нет, это нога " + player.charname + ".");
                }
                if (_event == 2)
                {
                    Console.WriteLine("Коварный " + player.charclass + " бьет головой в ногу " + npc.charname +
                        ". Ему совсем не больно... Надо менять тактику.");
                }
                if (_event == 3)
                {
                    Console.WriteLine(player.charname + " хотел сделать проход в ноги, а сделал проход между ног. " +
                        player.charname + ", ты не Хабиб, успокойся.");
                }
            }
        }
        static void PrintSuccessEvade (int dir, Player player, Player npc)
        {
            if (dir == 1)
            {
                Console.WriteLine(player.charname + " успешно увернулся от удара в голову");
            }
            if (dir == 2)
            {
                Console.WriteLine(player.charname + " успешно заблокировал удар в живот");
            }
            if (dir == 3)
            {
                Console.WriteLine(player.charname + " подпрыгнул во время удара по ногам");
            }
        }
        static void PrintFailedEvade (int dir, Player player, Player npc)
        {
            if (dir == 1)
            {
                Console.WriteLine(player.charname + " получил в табло");
            }
            if (dir == 2)
            {
                Console.WriteLine(player.charname + " пропустил удар в корпус");
            }
            if (dir == 3)
            {
                Console.WriteLine(npc.charname + " дал подсечку, " + player.charname + " упал на лицо");
            }
        }
    }

}
