using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Fighter_Tower
{



    class Program
    {
        static public List<Player> npclist = new List<Player>();
        static public List<Player> charlist = new List<Player>();
        static public List<PassiveSkill> skillsList = new List<PassiveSkill>();
        static public List<PassiveSkill> ownedSkills = new List<PassiveSkill>();
        static List<string> WparamsPrint = WparamsTrasher();
        static List<string> WparamsTrasher()
        {
            List<string> WparamsPrint = new List<string>();
            List<string> WparamsPrintT = new List<string>();
            WparamsPrintT.Add("Сила:");
            WparamsPrintT.Add("Ловкость:");
            WparamsPrintT.Add("Выносливость:");
            WparamsPrintT.Add("Здоровье:");
            for (int i = 0; i < WparamsPrintT.Count; i++)
            {
                string cutWparam = WparamsPrintT[i].Trim();
                WparamsPrint.Add(cutWparam);
            }
            int mostLong = 0;
            foreach (string w in WparamsPrint)
            {
                if (w.Length > mostLong)
                {
                    mostLong = w.Length;
                }
            }
            for (int i = 0; i < WparamsPrint.Count; i++)
            {
                WparamsPrint[i] = WparamsPrint[i].PadRight(mostLong + 1);
            }
            return WparamsPrint;
        }

     
    
        static Player CharSelect()
        {
            Console.WriteLine("\tВыбор персонажа");

            foreach (Player c in charlist)
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine(c.Id + " " + c.charclass + ":\n" + WparamsPrint[0] + "|" + c.str + "|" +
                    "\n" + WparamsPrint[1] + "|" + c.dex + "|" + "\n" + WparamsPrint[2] + "|" + c.vit + "|" + 
                    "\n" + WparamsPrint[3] + "|" + c.hp + "|" + "\nУльта: " + c.ultimate );
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
        static void Navigation (Player player)
        {
            while(true)
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("\tИнформация о персонаже - [Пробел] \n\tПовышение навыков - [Стрелка Вверх] " +
                    "\n\tТекущие навыки - [Стрелка Вниз] \n\tДалее - [ENTER]");
                ConsoleKey navKey = Console.ReadKey().Key;
                if (navKey == ConsoleKey.Spacebar)
                {
                    Player.PlayerInfo(player, WparamsPrint);
                }
                if (navKey == ConsoleKey.UpArrow)
                {
                    SkillsUp(player);
                }
                if (navKey == ConsoleKey.DownArrow)
                {
                    OwnedSkillsPrint(ownedSkills);
                }
                if (navKey == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
        static void SkillsUp(Player player)
        {
            while (player.Cp > 0 || player.SpCount > 0)
            {
                Console.WriteLine("Ваши очки распределения: |" + player.Cp + "| \tВаши очки характеристик: |" + player.SpCount + "|");
                foreach (PassiveSkill ps in skillsList)
                {
                    Console.WriteLine(ps.SkillsId + " - " + ps.SkillsName + " " + ps.Skillslvl + " уровень" + "\n" + ps.SkillsDescription); 
                }
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine(player.charclass + ":\n" + WparamsPrint[0] + "|" + player.str + "|" +
                    "\n" + WparamsPrint[1] + "|" + player.dex + "|" + "\n" + WparamsPrint[2] + "|" + player.vit + "|" +
                    "\n" + WparamsPrint[3] + "|" + player.hp + "|");
                Console.WriteLine("");
                Console.WriteLine("Выберите умение: ");
                SkillChoosePanelPrint(player);
                ConsoleKey chooseSkill = Console.ReadKey().Key;
                KeyEvent(chooseSkill, player);
                player.hp = Startup.HpCulc(player.vit);
                Console.Clear();
            }
        }
        static void OwnedSkillsPrint (List<PassiveSkill> ownedSkills)
        {
            Console.WriteLine("Действующите пассивные навыки");
            Console.WriteLine("");

            foreach (PassiveSkill sk in ownedSkills)
            {
                
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine(sk.SkillsName + " уровень навыка - " + sk.Skillslvl);
                Console.WriteLine("");
                Console.WriteLine(sk.SkillsDescription);
            }
            KeyToContinue();
            Console.Clear();
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
        static void SkillChoosePanelPrint (Player player)
        {
            var poisonGranade = PassiveSkill.ObjectPassSkills(skillsList, 1);
            var mortalHit = PassiveSkill.ObjectPassSkills(skillsList, 2);
            var healingAltar = PassiveSkill.ObjectPassSkills(skillsList, 3);
            var parametrs = PassiveSkill.ObjectPassSkills(skillsList, 4);
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine(poisonGranade.SkillsName + ": [Q]   " + mortalHit.SkillsName + " [W]   " +
                healingAltar.SkillsName + " [E]   " + parametrs.SkillsName + " [R]   \t>>> Использует Очки распределения");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("Стрелка влево: |Сила|\tСтрелка вверх: |Ловкость|\tСтрелка вправо: |Выносливость| " +
                "\t                        >>> Использует Очки характеристик");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("> > > Пробел: Далее < < <");
        }
        static void KeyEvent (ConsoleKey key, Player player)
        {
            ConsoleKey skillchoose = key;

            switch (skillchoose)
            {
                case ConsoleKey.Q:
                    var poison = PassiveSkill.ObjectPassSkills(skillsList, 1);
                    if (poison.Skillslvl < 3 && player.Cp != 0)
                    {
                        PoisonGranadeLvlUp();
                        player.Cp = player.Cp - 1;
                        break;
                    }
                    if (poison.Skillslvl >= 3)
                    {
                        Console.WriteLine(poison.SkillsName + " уже максимального уровня.");
                        KeyToContinue();
                        break;
                    }
                    if (player.Cp <= 0)
                    {
                        Console.WriteLine("У вас закончились очки распределения.");
                        KeyToContinue();
                    }
                    break;

                case ConsoleKey.W:
                    var mortal = PassiveSkill.ObjectPassSkills(skillsList, 2);
                    if (mortal.Skillslvl < 3 && player.Cp != 0)
                    {
                        MortalHitLvlUp();
                        player.Cp = player.Cp - 1;
                        break;
                    }
                    if (mortal.Skillslvl >= 3)
                    {
                        Console.WriteLine(mortal.SkillsName + " уже максимального уровня.");
                        KeyToContinue();
                        break;
                    }
                    if (player.Cp <= 0)
                    {
                        Console.WriteLine("У вас закончились очки распределения.");
                        KeyToContinue();
                    }
                    break;

                case ConsoleKey.E:
                    var altar = PassiveSkill.ObjectPassSkills(skillsList, 3);
                    if (altar.Skillslvl < 3 && player.Cp != 0)
                    {
                        HealingAltarLvlUp();
                        player.Cp = player.Cp - 1;
                        break;
                    }
                    if (altar.Skillslvl >= 3)
                    {
                        Console.WriteLine(altar.SkillsName + " уже максимального уровня.");
                        KeyToContinue();
                        break;
                    }
                    if (player.Cp <= 0)
                    {
                        Console.WriteLine("У вас закончились очки распределения.");
                        KeyToContinue();
                    }
                    break;

                case ConsoleKey.R:
                    if (player.Cp > 0 && player.Cp != 0)
                    {
                        ParamsUp(player);
                        player.Cp = player.Cp - 1;
                    }
                    if (player.Cp <= 0)
                    {
                        Console.WriteLine("У вас нет очков распределения.");
                        KeyToContinue();
                    }                   
                    break;

                case ConsoleKey.LeftArrow:
                    if (player.SpCount <= 0)
                    {
                        Console.WriteLine("У вас закончились очки характеристик.");
                        KeyToContinue();
                        break;
                    }
                    player.str = player.str + 1;
                    player.SpCount = player.SpCount - 1;
                    break;

                case ConsoleKey.UpArrow:
                    if (player.SpCount <= 0)
                    {
                        Console.WriteLine("У вас закончились очки характеристик.");
                        KeyToContinue();
                        break;
                    }
                    player.dex = player.dex + 1;
                    player.SpCount = player.SpCount - 1;
                    break;

                case ConsoleKey.RightArrow:
                    if (player.SpCount <= 0)
                    {
                        Console.WriteLine("У вас закончились очки характеристик.");
                        KeyToContinue();
                        break;
                    }
                    player.vit = player.vit + 1;
                    player.SpCount = player.SpCount - 1;
                    break;

                case ConsoleKey.Spacebar:
                    break;

                default:
                    Console.WriteLine("Неверный ввод, повторите еще раз.");
                    break;
            }
        }
        static void RollText(string textIn)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string s1 = textIn;
            char[] chrs = new char[s1.Length];
            char[] rollchrs = new char[s1.Length];
            int valuer = 0;
            foreach (char c1 in s1)
            {
                chrs[valuer] = c1;
                valuer++;
            }

            for (int i = 0; i < chrs.Length; i++)
            {
                rollchrs[i] = chrs[i];
                Console.WriteLine(rollchrs);
                Thread.Sleep(10);
                if (i == s1.Length - 1)
                {
                    break;
                }
                Console.Clear();
            }

            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Startup.ClassesAdd(charlist);
            Startup.NpcAdd(npclist);
            Startup.SkillsAdd(skillsList);
            RollText("Добро пожаловать в Fighters Tower!");
            Player player = CharSelect();
            try
            {
                FirstFloor(player);
                BetweenFloors(player);
                SecondFloor(player);
                BetweenFloors(player);
                ThirdFloor(player);
                BetweenFloors(player);
                FourthFloor(player);
                BetweenFloors(player);
                FivthFloor(player);
                BetweenFloors(player);
            }
            catch
            {
                Console.WriteLine("\t= = = = = Конец игры = = = = =");
            }
            

        }
        static void BetweenFloors(Player player)
        {
            player.Cp = player.Cp + 3;

            Navigation(player);       
        }
        static void ParamsUp(Player player)
        {
            player.SpCount = player.SpCount + 3;
        }
        static void FirstFloor(Player player)
        {
            int floor = 1;
            Player player1 = player;
            Player npc = GetNpc(floor);


            RollText("\tПервый этаж");
           
            Fight(player1, npc, floor);
        }
        static void SecondFloor(Player player)
        {
            int floor = 2;
            Player player1 = player;
            Player npc = GetNpc(floor);

            RollText("\tВторой этаж");

            Fight(player1, npc, floor);
        }
        static void ThirdFloor(Player player)
        {
            int floor = 3;
            Player player1 = player;
            Player npc = GetNpc(floor);

            RollText("\tТретий этаж");

            Fight(player1, npc, floor);
        }
        static void FourthFloor(Player player)
        {
            int floor = 4;
            Player player1 = player;
            Player npc = GetNpc(floor);

            RollText("\tЧетвертый этаж");

            Fight(player1, npc, floor);
        }
        static void FivthFloor(Player player)
        {
            int floor = 5;
            Player player1 = player;
            Player npc = GetNpc(floor);

            RollText("\tПятый этаж");

            Fight(player1, npc, floor);
        }
        static void Fight(Player player, Player npc, int floor)
        {
            Random rnd = new Random();
            player.uP = player.uP + 1;
            int pHp = player.hp;
            int nHp = npc.hp;
            int dmgDone = 0;
            int dmgRecived = 0;
            var poisonGranade = PassiveSkill.ObjectPassSkills(skillsList, 1);
            var mortalHit = PassiveSkill.ObjectPassSkills(skillsList, 2);
            var healingAltar = PassiveSkill.ObjectPassSkills(skillsList, 3);
            while (true)
            {
                Console.WriteLine("Начало боя!!!");
                Console.WriteLine("-----------------------------------------------------------------------");
                EnemyCard(player, npc, floor, pHp, nHp);
                int result = ResultCheck(pHp, nHp);
                if (result == 1)
                {
                    break;
                }
                while(true)
                {
                    ConsoleKey pAttackInput = DirChooseAttack();
                    int npcDef = rnd.Next(1, 4);
                    int exHp = ActionAttack(pAttackInput, player, npc, dmgDone, nHp, pHp, floor, npcDef, mortalHit);
                    if (exHp > -999)
                    {
                        nHp = exHp;
                        break;
                    }
                }
                
                while(true)
                {
                    ConsoleKey pDefInput = DirChooseDef();
                    int npcAttack = rnd.Next(1, 4);
                    Tuple<int, int> tuple = ActionDef(pDefInput, player, npc, npcAttack, dmgRecived, pHp, nHp, floor, 0, mortalHit);
                    int expHp = tuple.Item1;
                    int exnHp = tuple.Item2;
                    if(expHp > -999)
                    {
                        pHp = expHp;
                        nHp = exnHp;
                        break;
                    }
                }
                KeyToContinue();
                Console.Clear();
            }
            KeyToContinue();
            Console.Clear();
        }
        static ConsoleKey DirChooseAttack()
        {
            Console.WriteLine("Выберите действие атаки");
            Console.WriteLine("");
            Console.WriteLine("Пробел : Ульта \nСтрелка Вверх : Удар в голову \nСтрелка В Право : Удар в тело" +
                " \nСтрелка Вниз : Удар по ногам");
            ConsoleKey pAttackInput = Console.ReadKey().Key;
            return pAttackInput;
        }
        static ConsoleKey DirChooseDef()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Выберите действие защиты");
            Console.WriteLine("");
            Console.WriteLine("Ульта: Пробел \nСтрелка Вверх : Защита головы \nСтрелка В Право : Защита корпуса" +
               " \nСтрелка Вниз : Защита ног");
            ConsoleKey pDefInput = Console.ReadKey().Key;
            return pDefInput;
        }
        static int ActionAttack(ConsoleKey pAttackInput, Player player, Player npc, int dmgDone,  int npcHp, int plHp, int floor, int npcDef, PassiveSkill mortalHit)
        {
            int nHp = npcHp;
            if (pAttackInput == ConsoleKey.UpArrow || pAttackInput == ConsoleKey.DownArrow || pAttackInput == ConsoleKey.RightArrow)
            {
                nHp = BasicAttack(pAttackInput, player, npc, dmgDone, nHp, npcDef, mortalHit);
            }
            else if (pAttackInput == ConsoleKey.Spacebar)
            {
                if (player.Id != 1 && player.Id != 2)
                {
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine("Вы не можете использовать ульту в фазе атаки. Повторите ввод");
                    return -1000;
                }
                if (player.uP <= 0)
                {
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine("Не хватает очков UP. Повторите ввод");
                    return -1000;
                }

                nHp = Ultimate(pAttackInput, player, npc, dmgDone, nHp, plHp, floor, npcDef, mortalHit);

            }
            else
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Неверный ввод, повторите попытку");
                KeyToContinue();
                return -1000;
            }
            return nHp;
        }
        static Tuple<int, int> ActionDef(ConsoleKey pDefInput, Player player, Player npc, int npcAttack, int dmgRecived, int plHp, int nHp, int floor, int npcDef, PassiveSkill mortalHit)
        {
            int pHp = plHp;
            if (pDefInput == ConsoleKey.UpArrow || pDefInput == ConsoleKey.RightArrow || pDefInput == ConsoleKey.DownArrow)
            {
                pHp = BasicDef(pDefInput, player, npc, npcAttack, dmgRecived, pHp);
            }
            else if (pDefInput == ConsoleKey.Spacebar)
            {
                if (player.Id != 3 && player.Id != 4)
                {
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine("Вы не можете использовать ульту в фазе защиты. Повторите ввод");
                    int exMsg = -1000;
                    return new Tuple<int, int>(exMsg, nHp);
                }
                if (player.uP <= 0)
                {
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine("Не хватает очков UP. Повторите ввод");
                    int exMsg = -1000;
                    return new Tuple<int, int>(exMsg, nHp);
                }
                Tuple<int, int> tuple = UltimateDef(pDefInput, player, npc, dmgRecived, nHp, plHp, floor, npcAttack);
                pHp = tuple.Item1;
                nHp = tuple.Item2;
            }
            else
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Неверный ввод, повторите попытку");
                KeyToContinue();
                int exMsg = -1000;
                return new Tuple<int, int>(exMsg, nHp);
            }


            return new Tuple<int, int>(pHp, nHp);
        }

        static int BasicDef(ConsoleKey pDefInput, Player player, Player npc, int npcAttack, int dmgRecived, int pHp)
        {
            dmgRecived = DmgIn(npc.str, npcAttack, pDefInput, npc);
            if (dmgRecived > 0)
            {
                double kostil = 0;
                double critwasn = CritChance(npc.dex, kostil);
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
                    Console.WriteLine("");
                    int altarHeal = AltarHeal(player.hp);
                    int pplHp = pHp + altarHeal;
                    if (pplHp <= player.hp)
                    {
                        pHp = pplHp;
                    }
                    if (altarHeal > 0)
                    {
                        Console.WriteLine(player.charname + " полечился на " + altarHeal + " здоровья.");
                        Console.WriteLine("");
                    }
                    pHp = pHp - dmgRecived;
                }
                if (critwasn > 1)
                {
                    dmgRecived = (int)Math.Round(dmgRecived * critwasn);
                    Console.WriteLine("");
                    Console.WriteLine("- - - - - \tКритический Урон!!! \t- - - - -");
                    Console.WriteLine("");
                    Console.WriteLine(">>>>> " + npc.charname + " нанес " + dmgRecived + " урона <<<<<");
                    Console.WriteLine("");
                    int altarHeal = AltarHeal(pHp);
                    int pplHp = pHp + altarHeal;
                    if (pplHp <= player.hp)
                    {
                        pHp = pplHp;
                    }
                    if (altarHeal > 0)
                    {
                        Console.WriteLine(player.charname + " полечился на " + altarHeal + " здоровья.");
                        Console.WriteLine("");
                    }
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
                Console.WriteLine("");
                int altarHeal = AltarHeal(pHp);
                int pplHp = pHp + altarHeal;
                if (pplHp <= player.hp)
                {
                    pHp = pplHp;
                }
                if (altarHeal > 0)
                {
                    Console.WriteLine(player.charname + " полечился на " + altarHeal + " здоровья.");
                    Console.WriteLine("");
                }
            }
            return pHp;
        }
        static Tuple<int, int> BasicDefReflect(ConsoleKey pDefInput, Player player, Player npc, int npcAttack, int dmgRecived, int pHp)
        {
            dmgRecived = DmgIn(npc.str, npcAttack, pDefInput, npc);
            if (dmgRecived > 0)
            {
                double kostil = 0;
                double critwasn = CritChance(npc.dex, kostil);
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
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine(">>>>> " + npc.charname + " получил " + dmgRecived + " отраженного урона <<<<<");
                    Console.WriteLine("");
                    int altarHeal = AltarHeal(player.hp);
                    int pplHp = pHp + altarHeal;
                    if (pplHp <= player.hp)
                    {
                        pHp = pplHp;
                    }
                    if (altarHeal > 0)
                    {
                        Console.WriteLine(player.charname + " полечился на " + altarHeal + " здоровья.");
                        Console.WriteLine("");
                    }
                    pHp = pHp - dmgRecived;
                }
                if (critwasn > 1)
                {
                    dmgRecived = (int)Math.Round(dmgRecived * critwasn);
                    Console.WriteLine("");
                    Console.WriteLine("- - - - - \tКритический Урон!!! \t- - - - -");
                    Console.WriteLine("");
                    Console.WriteLine(">>>>> " + npc.charname + " нанес " + dmgRecived + " урона <<<<<");
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine(">>>>> " + npc.charname + " получил " + dmgRecived + " отраженного урона <<<<<");
                    Console.WriteLine("");
                    int altarHeal = AltarHeal(pHp);
                    int pplHp = pHp + altarHeal;
                    if (pplHp <= player.hp)
                    {
                        pHp = pplHp;
                    }
                    if (altarHeal > 0)
                    {
                        Console.WriteLine(player.charname + " полечился на " + altarHeal + " здоровья.");
                        Console.WriteLine("");
                    }
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
                Console.WriteLine("");
                int altarHeal = AltarHeal(pHp);
                int pplHp = pHp + altarHeal;
                if (pplHp <= player.hp)
                {
                    pHp = pplHp;
                }
                if (altarHeal > 0)
                {
                    Console.WriteLine(player.charname + " полечился на " + altarHeal + " здоровья.");
                    Console.WriteLine("");
                }
            }
            return new Tuple<int, int>(pHp, dmgRecived);
        }

        static int ResultCheck(int pHp, int nHp)
        {
            if (nHp <= 0)
            {
                Console.WriteLine("- - - - - Вы Победили - - - - -");
                return 1;
            }
            if (pHp <= 0)
            {
                Console.WriteLine("- - - - - Вы Проиграли - - - - -");
                throw new Exception();
            }
            return 0;
        }
        static int DmgOut(int str, int nDef, ConsoleKey inputkeyA, Player player)
        {
            Random minmax = new Random();
            int pAttack = KeyToInt(inputkeyA);
            int pDmg = (int) Math.Round (str * 1.5);
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
        static double CritChance (int dex, double critboost)
        {
            Random critrnd = new Random();
            int critchance = dex * 2;
            int crit = critrnd.Next(1, 101);
            double basicBoost = 1.5;
            if (crit <= critchance)
            {
                return basicBoost + critboost;
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
        public static void KeyToContinue()
        {
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Нажмите любую кнопку для продолжения.");
            Console.ReadKey();
        }
        static void EnemyCard(Player player, Player npc, int floor, int pHp, int nHp)
        {
            Console.Write("Ваш соперник ");
            Console.WriteLine("");
            NpcInfoPrint(floor);
            Console.WriteLine("");
            Console.WriteLine("UP: " + player.uP + "\t|| Здоровье " + player.charname + ": " + pHp + " ||" + "\t || Здоровье " + npc.charname + ": " + nHp + " ||");
            Console.WriteLine("");
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
        static void MortalHitLvlUp ()
        {
            var mortalHit = PassiveSkill.ObjectPassSkills(skillsList, 2);
            if (mortalHit.Skillslvl < 3)
            {
                mortalHit.Skillslvl = mortalHit.Skillslvl + 1;
                mortalHit.critboostvalue = mortalHit.critboostvalue + 0.5;
            }
            if (!ownedSkills.Contains(mortalHit))
            {
                ownedSkills.Add(mortalHit);
            }

        }
        static void PoisonGranadeLvlUp ()
        {
            var poisonGranade = PassiveSkill.ObjectPassSkills(skillsList, 1);
            if (poisonGranade.Skillslvl < 3)
            {
                poisonGranade.Skillslvl = poisonGranade.Skillslvl + 1;
            }
            if (!ownedSkills.Contains(poisonGranade))
            {
                ownedSkills.Add(poisonGranade);
            }
        }
        static void HealingAltarLvlUp()
        {
            var healingAltar = PassiveSkill.ObjectPassSkills(skillsList, 3);
            if (healingAltar.Skillslvl < 3)
            {
                healingAltar.Skillslvl = healingAltar.Skillslvl + 1;
            }
            if (!ownedSkills.Contains(healingAltar))
            {
                ownedSkills.Add(healingAltar);
            }
        }
        static int PoisonDmg(int npcHp)
        {
            var poisonskill = PassiveSkill.ObjectPassSkills(skillsList, 1);
            int poisonDmg = 0;
            if (poisonskill.Skillslvl == 0)
            {
                poisonDmg = 0;
            }
            if (poisonskill.Skillslvl == 1)
            {
                poisonDmg = (int)Math.Round(npcHp * 0.03);
            }
            if (poisonskill.Skillslvl == 2)
            {
                poisonDmg = (int)Math.Round(npcHp * 0.05);
            }
            if (poisonskill.Skillslvl == 3)
            {
                poisonDmg = (int)Math.Round(npcHp * 0.08);
            }

            return poisonDmg;
        }
        static int AltarHeal(int plHp)
        {
            var altarSkill = PassiveSkill.ObjectPassSkills(skillsList, 3);
            int altarHeal = 0;
            switch(altarSkill.Skillslvl)
            {
                case 0:
                    altarHeal = 0;
                    break;
                case 1:
                    altarHeal = (int)Math.Round(plHp * 0.05);
                    break;
                case 2:
                    altarHeal = (int)Math.Round(plHp * 0.08);
                    break;
                case 3:
                    altarHeal = (int)Math.Round(plHp * 0.12);
                    break;
            }
            return altarHeal;
        }
        static int BasicAttack(ConsoleKey pAttackInput, Player player, Player npc, int dmgDone, int npcHp, int npcDef, PassiveSkill mortalHit)
        {
            int nHp = npcHp;
            dmgDone = DmgOut(player.str, npcDef, pAttackInput, player);
            if (dmgDone > 0)
            {
                double critwasp = CritChance(player.dex, mortalHit.critboostvalue);
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
                    Console.WriteLine("");
                    int poisonDmg = PoisonDmg(npc.hp);
                    nHp = nHp - poisonDmg;
                    if (poisonDmg > 0)
                    {
                        Console.WriteLine(npc.charname + " получил " + poisonDmg + " урона от яда.");
                        Console.WriteLine("");
                    }
                    nHp = nHp - dmgDone;
                }
                if (critwasp > 1)
                {
                    dmgDone = (int)Math.Round(dmgDone * critwasp);
                    Console.WriteLine("");
                    Console.WriteLine("- - - - - \tКритический Урон!!! \t- - - - -");
                    Console.WriteLine("");
                    Console.WriteLine(">>>>> " + player.charname + " нанес " + dmgDone + " урона <<<<<");
                    Console.WriteLine("");
                    int poisonDmg = PoisonDmg(npc.hp);
                    nHp = nHp - poisonDmg;
                    if (poisonDmg > 0)
                    {
                        Console.WriteLine(npc.charname + " получил " + poisonDmg + " урона от яда.");
                        Console.WriteLine("");
                    }
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
                Console.WriteLine("");
                Console.WriteLine("");
                int poisonDmg = PoisonDmg(npc.hp);
                nHp = nHp - poisonDmg;
                if (poisonDmg > 0)
                {
                    Console.WriteLine(npc.charname + " получил " + poisonDmg + " урона от яда.");
                    Console.WriteLine("");
                }
            }

            return nHp;
        }
        static int Ultimate(ConsoleKey pAttackInput, Player player, Player npc, int dmgDone, int npcHp, int plHp, int floor, int npcDef, PassiveSkill mortalHit)
        {
            int ultId = player.Id;
            int nHp = npcHp;
            int pHp = plHp;
            
            switch(ultId)
            {
                case 1:
                    nHp = DoubleKick(pAttackInput, player, npc, dmgDone, npcHp, plHp, floor, npcDef, mortalHit);
                    player.uP = player.uP -1;
                    break;
                case 2:
                    nHp = AccAttack(pAttackInput, player, npc, dmgDone, npcHp, npcDef, mortalHit);
                    player.uP = player.uP - 1;
                    break;

            }
            return nHp;
        }
        static Tuple<int, int> UltimateDef(ConsoleKey pDefInput, Player player, Player npc, int dmgRecived, int npcHp, int plHp, int floor, int npcAttack)
        {
            int ultId = player.Id;
            int nHp = npcHp;
            int pHp = plHp;

            switch (ultId)
            {
                case 3:
                    pHp = Healing(pDefInput, player, npc, npcAttack, dmgRecived, pHp, nHp, floor);
                    player.uP = player.uP - 1;
                    break;
                case 4:
                    Tuple<int, int> tuple = Reflect(pDefInput, player, npc, npcAttack, dmgRecived, pHp, nHp, floor);
                    pHp = tuple.Item1;
                    nHp = tuple.Item2;
                    player.uP = player.uP - 1;
                    break;
                    
            }
            return new Tuple<int, int>(pHp, nHp);
        }
        static int DoubleKick(ConsoleKey pAttackInput, Player player, Player npc, int dmgDone, int npcHp, int pHp, int floor, int npcDef, PassiveSkill mortalHit)
        {
            Console.Clear();
            int nHp = npcHp;
            while(true)
            {
                EnemyCard(player, npc, floor, pHp, nHp);
                pAttackInput = DirChooseAttackUlt();
                if (pAttackInput == ConsoleKey.UpArrow || pAttackInput == ConsoleKey.RightArrow || pAttackInput == ConsoleKey.DownArrow)
                {
                    nHp = BasicAttack(pAttackInput, player, npc, dmgDone, nHp, npcDef, mortalHit);
                    break;
                }
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Выберите направление атаки");
                KeyToContinue();
                Console.Clear();              
            }
            while (true)
            {
                ConsoleKey pAttackSecond = DirChooseAttackUlt();
                if (pAttackSecond == ConsoleKey.UpArrow || pAttackSecond == ConsoleKey.RightArrow || pAttackSecond == ConsoleKey.DownArrow)
                {
                    nHp = BasicAttackSecondAttack(pAttackSecond, player, npc, dmgDone, nHp, npcDef, mortalHit);
                    break;
                }
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Выберите направление атаки");
                KeyToContinue();
                Console.Clear();
            }
            
            return nHp;
        }
        static ConsoleKey DirChooseAttackUlt()
        {
            Console.WriteLine("Выберите действие атаки");
            Console.WriteLine("");
            Console.WriteLine("Стрелка Вверх : Удар в голову \nСтрелка В Право : Удар в тело" +
                " \nСтрелка Вниз : Удар по ногам");
            ConsoleKey pAttackInput = Console.ReadKey().Key;
            return pAttackInput;
        }
        static ConsoleKey DirChooseDefUlt()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Выберите действие защиты");
            Console.WriteLine("");
            Console.WriteLine("Стрелка Вверх : Защита головы \nСтрелка В Право : Защита корпуса" +
               " \nСтрелка Вниз : Защита ног");
            ConsoleKey pDefInput = Console.ReadKey().Key;
            return pDefInput;
        }
        static int BasicAttackSecondAttack(ConsoleKey pAttackInput, Player player, Player npc, int dmgDone, int npcHp, int npcDef, PassiveSkill mortalHit)
        {
            int nHp = npcHp;
            dmgDone = DmgOut(player.str, 0, pAttackInput, player);
            if (dmgDone > 0)
            {
                double critwasp = CritChance(player.dex, mortalHit.critboostvalue);
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
                    Console.WriteLine("");
                    nHp = nHp - dmgDone;
                }
                if (critwasp > 1)
                {
                    dmgDone = (int)Math.Round(dmgDone * critwasp);
                    Console.WriteLine("");
                    Console.WriteLine("- - - - - \tКритический Урон!!! \t- - - - -");
                    Console.WriteLine("");
                    Console.WriteLine(">>>>> " + player.charname + " нанес " + dmgDone + " урона <<<<<");
                    Console.WriteLine("");
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
                Console.WriteLine("");
                Console.WriteLine("");
                int poisonDmg = PoisonDmg(npc.hp);
                nHp = nHp - poisonDmg;
                if (poisonDmg > 0)
                {
                    Console.WriteLine(npc.charname + " получил " + poisonDmg + " урона от яда.");
                    Console.WriteLine("");
                }
            }

            return nHp;
        }
        static int AccAttack(ConsoleKey pAttackInput, Player player, Player npc, int dmgDone, int npcHp, int npcDef, PassiveSkill mortalHit)
        {
            int nHp = npcHp;
            pAttackInput = RandomKey();
            dmgDone = DmgOut(player.str, 0, pAttackInput, player);
            double critwasp = CritChance(player.dex, mortalHit.critboostvalue);
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
                Console.WriteLine("");
                nHp = nHp - dmgDone;
            }
            if (critwasp > 1)
            {
                dmgDone = (int)Math.Round(dmgDone * 3.5);
                Console.WriteLine("");
                Console.WriteLine("- - - - - \tКритический Урон!!! \t- - - - -");
                Console.WriteLine("");
                Console.WriteLine(">>>>> " + player.charname + " нанес " + dmgDone + " урона <<<<<");
                Console.WriteLine("");
                nHp = nHp - dmgDone;
            }
            return nHp;
        }
        static ConsoleKey RandomKey()
        {
            ConsoleKey rndKeyOut;
            Random random = new Random();
            int randomKey = random.Next(1, 4);
            if (randomKey == 1)
            {
                rndKeyOut = ConsoleKey.UpArrow;
                return rndKeyOut;
            }
            if (randomKey == 2)
            {
                rndKeyOut = ConsoleKey.RightArrow;
                return rndKeyOut;
            }
            if (randomKey == 3)
            {
                rndKeyOut = ConsoleKey.DownArrow;
                return rndKeyOut;
            }
            return ConsoleKey.Enter;
        }
        static int Healing(ConsoleKey pDefInput, Player player, Player npc, int npcAttack, int dmgRecived, int pHp, int nHp, int floor)
        {
            Console.Clear();
            int heal = HealingCulc(player, pHp);
            pHp = pHp + heal;
            

            while (true)
            {
                EnemyCard(player, npc, floor, pHp, nHp);
                pDefInput = DirChooseDefUlt();
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine(player.charname + " востановил " + heal + " здоровья, умением " + player.ultimatename);
                Console.WriteLine("-----------------------------------------------------------------------");
                if (pDefInput == ConsoleKey.UpArrow || pDefInput == ConsoleKey.RightArrow || pDefInput == ConsoleKey.DownArrow)
                {
                    pHp = BasicDef(pDefInput, player, npc, npcAttack, dmgRecived, pHp);
                    break;
                }
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Выберите направление атаки");
                KeyToContinue();
                Console.Clear();
            }
            return pHp;

        }
        static Tuple<int, int> Reflect(ConsoleKey pDefInput, Player player, Player npc, int npcAttack, int dmgRecived, int pHp, int nHp, int floor)
        {
            Console.Clear();

            while (true)
            {
                EnemyCard(player, npc, floor, pHp, nHp);
                Console.WriteLine("");
                Console.WriteLine(player.ultimatename + " активирован.");
                pDefInput = DirChooseDefUlt();
                if (pDefInput == ConsoleKey.UpArrow || pDefInput == ConsoleKey.RightArrow || pDefInput == ConsoleKey.DownArrow)
                {
                    Tuple<int, int> tuple = BasicDefReflect(pDefInput, player, npc, npcAttack, dmgRecived, pHp);
                    pHp = tuple.Item1;
                    int reflect = tuple.Item2;
                    nHp = nHp - reflect;
                    break;
                }
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("Выберите направление атаки");
                KeyToContinue();
                Console.Clear();
            }
            return new Tuple<int, int>(pHp, nHp);
        }
        static int HealingCulc(Player player, int pHp)
        {
            int exHp = player.hp;
            exHp = (int) Math.Round (exHp * 0.20);
            return exHp;
        }
    }

}
