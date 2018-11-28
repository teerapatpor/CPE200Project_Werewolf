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
}