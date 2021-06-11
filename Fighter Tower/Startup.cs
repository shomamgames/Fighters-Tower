using System.Collections.Generic;

namespace Fighter_Tower
{
    public class Startup
    {
        public static void NpcAdd(List<Player> npclist)
        {
            npclist.Add(new Player()
            {
                Id = 1,
                charname = "Бродяга",
                str = 6,
                dex = 6,
                vit = 8,
                hp = HpCulc(8),
                npcdescription = "Бродяга, одетый в лохмотья и с кастетом в руках"
            });
            npclist.Add(new Player()
            {
                Id = 2,
                charname = "Дворовой Задира",
                str = 12,
                dex = 8,
                vit = 12,
                hp = HpCulc(12),
                npcdescription = "Майка алкашка, порваные джинсы, в одной руке нож, в другой разбитая бутылка"
            });
            npclist.Add(new Player()
            {
                Id = 3,
                charname = "Уличный Боец",
                str = 12,
                dex = 16,
                vit = 20,
                hp = HpCulc(20),
                npcdescription = "Голый торс, сверкающий пресс, забинтованные кулаки"
            });
            npclist.Add(new Player()
            {
                Id = 4,
                charname = "Беглый Солдат",
                str = 18,
                dex = 14,
                vit = 26,
                hp = HpCulc(26),
                npcdescription = "Красная берета, военая майка, черные штаны и берцы. В руке держит штык-нож"
            });
            npclist.Add(new Player()
            {
                Id = 5,
                charname = "Буйный Мексиканец",
                str = 20,
                dex = 14,
                vit = 30,
                hp = HpCulc(30),
                npcdescription = "Старое самбреро, шрам на правой стороне лица, орудует огромным мачете"
            });
        }

        static public int HpCulc(int vit)
        {
            int hp = vit * 10;
            return hp;
        }

        public static void SkillsAdd(List<PassiveSkill> skillsList)
        {
            skillsList.Add(new PassiveSkill
            {
                SkillsId = 1,
                SkillsName = "|Газовая граната|",
                SkillsDescription = "Каждый ход, отнимает часть здоровья у противника в процентном соотношении.",
                Skillslvl = 0,

            });
            skillsList.Add(new PassiveSkill
            {
                SkillsId = 2,
                SkillsName = "|Смертельный удар|",
                SkillsDescription = "Увеличивает множитель критического урона.",
                Skillslvl = 0,

            });
            skillsList.Add(new PassiveSkill
            {
                SkillsId = 3,
                SkillsName = "|Лечебный Алтарь|",
                SkillsDescription = "На протяжении всего боя, востанавливает вам определенное количество здоровья раз в ход.",
                Skillslvl = 0,

            });
            skillsList.Add(new PassiveSkill
            {
                SkillsId = 4,
                SkillsName = "|Увеличение Характеристик|",
                SkillsDescription = "Дает вас 3 очка навыков (SP) которые вы можете потратить на увеличение характеристик.",

            });
        }

        public static void ClassesAdd(List<Player> charlist)
        {
            charlist.Add(new Player()
            {
                Id = 1,
                charclass = "Монах",
                str = 10,
                dex = 10,
                vit = 10,
                hp = HpCulc(10),
                ultimatename = "\"Двойной Удар\"",
                ultimate = "\"Двойной Удар\" " +
                "\n||Можно выбрать два вектора атаки. (Используется в фазе атаки)  Стоимость - 1 UP.||"
            });
            charlist.Add(new Player()
            {
                Id = 2,
                charclass = "Ниндзя",
                str = 10,
                dex = 12,
                vit = 6,
                hp = HpCulc(6),
                ultimatename = "\"Поиск бреши\"",
                ultimate = "\"Поиск бреши\" " +
                "\n||Атакует и всегда попадает в цель. При критическом уроне, множитель 3,5х. (Используется в фазе атаки)  Стоимость - 1 UP.||"
            });
            charlist.Add(new Player()
            {
                Id = 3,
                charclass = "Паладин",
                str = 12,
                dex = 2,
                vit = 16,
                hp = HpCulc(16),
                ultimatename = "\"Исцеление\"",
                ultimate = "\"Исцеление\" " +
                "\n||Востанавливает пермаментно определенный процент здоровья. (Используется в фазе защиты) Стоимость - 1 UP.||"
            });
            charlist.Add(new Player()
            {
                Id = 4,
                charclass = "Фаталист",
                str = 16,
                dex = 8,
                vit = 6,
                hp = HpCulc(6),
                ultimatename = "\"Смертельный Удар\"",
                ultimate = "\"Смертельный Удар\" " +
                "\n||Возвращает 100% урона врагу, при этом, сам тоже получает урон. (Используется в фазе защиты) Стоимость - 1 UP.||"
            });
        }
    }
}