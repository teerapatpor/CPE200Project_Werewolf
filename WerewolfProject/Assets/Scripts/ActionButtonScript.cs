using UnityEngine;

public class ActionButtonScript : MonoBehaviour {

    public enum ActiveTimeEnum
    {
        Day = 1,
        Night = 2,
        Both = 3,
    };

    public ActiveTimeEnum activeTime { get; set; }

}
