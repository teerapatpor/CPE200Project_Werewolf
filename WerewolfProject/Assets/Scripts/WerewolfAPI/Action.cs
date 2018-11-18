using System.Collections.Generic;

namespace WerewolfAPIModel
{
    [System.Serializable]
    public class Action
    {
        public long? id;
        public string name;
        public List<Role> roles;
        public string description;
    }

}
