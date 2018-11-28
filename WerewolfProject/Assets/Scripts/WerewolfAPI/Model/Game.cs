using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WerewolfAPIModel
{ 
    [System.Serializable]
    public class Game
    {
        public long id;
        public string hash;
        public int day;
        public string status;
        public string period;
        public string outcome;
        public List<Player> players;

        public Player GetPlayerById(long id)
        {
            foreach(Player player in players)
            {
                if(player.id == id)
                {
                    return player;
                }
            }

            return null;
        }

    }

    public class GameSate
    {
        //playing, waiting, ended
        public static string Playing = "playing";
        public static string Waiting = "waiting";
        public static string Ended = "ended";
    }
}
