using System;

namespace SparuvianConnection.Adoptatron.Gameplay.Skills
{
    public enum SkillName
    {
        Sit,
        Come,
        Patience,
        MPatience,
        EMPatience
    }


    public static class SkillNameExtensions
    {
        public static string ToString(SkillName skillName)
        {
            switch (skillName)
            {
                case SkillName.Sit:
                    return "Sit";
                case SkillName.Come:
                    return "Come";
                case SkillName.Patience:
                    return "Patience";
                case SkillName.MPatience:
                    return "More patience";
                case SkillName.EMPatience:
                    return "Even more patience";
                default:
                    throw new ArgumentOutOfRangeException(nameof(skillName), skillName, null);
            }
        }
    }
}