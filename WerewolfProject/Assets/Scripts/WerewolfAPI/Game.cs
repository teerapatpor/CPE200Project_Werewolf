using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WerewolfAPIModel
{ 
    [System.Serializable]
    public class Game
    {
        public long? id;
        public string hash;
        public int? day;
        public string status;
        public string period;
        public string outcome;
        public List<Player> players;
        
    }
}
