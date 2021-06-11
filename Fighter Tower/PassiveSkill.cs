using System.Collections.Generic;
using System.Linq;

namespace Fighter_Tower
{
    public class PassiveSkill : Skill
    {
        public double critboostvalue = 0;
        public int poison = 0;
        public int healing = 0;
        public static PassiveSkill ObjectPassSkills (List<PassiveSkill> skillsList, int skillnum)
        {
            var poisonGranade = skillsList.First(x => x.SkillsId == 1);
            var mortalHit = skillsList.First(x => x.SkillsId == 2);
            var healingAltar = skillsList.First(x => x.SkillsId == 3);
            var paramets = skillsList.First(x => x.SkillsId == 4);
            int skillnumber = skillnum;
            switch (skillnumber)
            {
                case 1:
                    return poisonGranade;
                   

                case 2:
                    return mortalHit;
                    

                case 3:
                    return healingAltar;
                   

                case 4:
                    return paramets;
                  

                default:
                    return null;
                  
            }
        }
         
    }

    public class PoisonSkill : Skill
    {
        public int poisonDmg = 0;

        
    }

    public abstract class Skill
    {
        public int SkillsId { get; set; }
        public string SkillsName { get; set; }
        public string SkillsDescription { get; set; }
        public int Skillslvl { get; set; }
    }

    
   
}
