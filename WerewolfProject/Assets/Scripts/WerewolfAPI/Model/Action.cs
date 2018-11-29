using System.Collections.Generic;

namespace WerewolfAPIModel
{
    [System.Serializable]
    public class Action
    {
        public long id;
        public string name;
        public string description;

        public enum AllAction
        {
            DayVote = 1,
            ThrowHolyWater = 2,
            Shoot = 3,
            Jail = 4,
            Enchant = 5,
            NightVote = 6,
            Guard = 7,
            Heal = 8,
            Kill = 9,
            Reveal = 10,
            Aura = 11,
            Revive = 12
        };
    }

}
