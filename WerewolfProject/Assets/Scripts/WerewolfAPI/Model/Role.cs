using System.Collections.Generic;

namespace WerewolfAPIModel
{
    [System.Serializable]
    public class Role
    {
        public long id;
        public string name;
        public string description;
        public List<Action> actions;
        public string type;

    }

    public class AllRole
    {
        public static string Seer = "Seer";
        public static string AuraSeer = "Aura Seer";
        public static string Priest = "Priest";
        public static string Doctor = "Doctor";
        public static string Werewolf = "Werewolf";
        public static string WerewolfSharman = "Werewolf Sharman";
        public static string AlphaWerewolf = "Alpha Werewolf";
        public static string WerewolfSeer = "Werewolf Seer";
        public static string Medium = "Medium";
        public static string Bodyguard = "Bodyguard";
        public static string Jailer = "Jailer";
        public static string Fool = "Fool";
        public static string HeadHunter = "Head Hunter";
        public static string SerialKiller = "Serial Killer";
        public static string Gunner = "Gunner";
    }

    public class RoleType
    {
        // Villager, Wolf, Neutral
        public static string Villager = "Villager";
        public static string Wolf = "Wolf";
        public static string Neutral = "Neutral";
    }
}