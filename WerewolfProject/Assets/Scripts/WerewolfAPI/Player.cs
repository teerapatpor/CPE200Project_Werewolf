namespace WerewolfAPIModel
{
    [System.Serializable]
    public class Player
    {
        public long? id;
        public string name;
        public string password;
        public long? gameid;
        public Role role;
        public string session;
        public string status;

    }

}
