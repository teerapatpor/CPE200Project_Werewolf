using System.Collections.Generic;

namespace WerewolfAPIModel
{
    [System.Serializable]
    public class Player
    {
        public long id;
        public string name;
        public string password;
        public long gameid;
        public Role role;
        public string session;
        public string status;

    }

    //[alive, offline, not in game, votedead, shotdead, jaildead, holydead, killdead]
    public class PlayerState
    {
        public static string Alive = "alive";
        public static string Offline = "offline";
        public static string NotInGame = "not in game";
        public static string Votedead = "votedead";
        public static string Shotdead = "shotdead";
        public static string Jaildead = "jaildead";
        public static string Holydead = "holydead";
        public static string Killdead = "killdead";
    }

}
