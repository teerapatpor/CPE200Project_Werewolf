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

    public class RoleType
    {
        // Villager, Wolf, Neutral
        public static string Villager = "Villager";
        public static string Wolf = "Wolf";
        public static string Neutral = "Neutral";
    }
}