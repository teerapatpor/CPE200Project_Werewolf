using System.Collections.Generic;

namespace WerewolfAPIModel
{
    [System.Serializable]
    public class ChatMessage
    {
        public long? id;
        public Player player;
        public string message;
        public string channel; // chat channel
       
    }
}