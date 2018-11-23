using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WerewolfAPIModel;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour {

    [SerializeField]
    private Text role, playerName, actionTxt;
    Player currentPlayer;

    private void Start()
    {
        

    }

    public void updateGUI(Player player)
    {
        currentPlayer = player;

        if (player.id == MainClient.Instance.GetMainPlayer().id)
            role.text = (string)player.role.name;
        else
            role.text = "";

        playerName.text = (string)player.name;
    }

    public void clicked()
    {
        Player mainPlayer = MainClient.Instance.GetMainPlayer();
        if (mainPlayer.role != null && mainPlayer.id != currentPlayer.id)
        {
            //performed action on player

        }
    }
}
