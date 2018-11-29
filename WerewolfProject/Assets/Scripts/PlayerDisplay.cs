using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WerewolfAPIModel;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour{

    private Text roleTxt, playerName, statusTxt;
    private Player currentPlayer;
    private MainGame main;

    private void Start()
    {
        Text[] allTxt = GetComponentsInChildren<Text>();
        foreach(Text text in allTxt)
        {
            switch (text.gameObject.tag)
            {
                case "Role":
                    roleTxt = text;
                    break;
                case "Name":
                    playerName = text;
                    break;
                case "Status":
                    statusTxt = text;
                    break;
                default:
                    Debug.LogError("No match Tag: " + text.name);
                    break;
            }
        }

        statusTxt.text = "";
        roleTxt.text = "";
        playerName.text = "";
        main = GetComponentInParent<MainGame>();
        gameObject.GetComponent<Button>().onClick.AddListener(clicked);
    }

    public void updateGUI(Player player,string gameState)
    {

        currentPlayer = player;
       
        if (player.role != null && gameState != GameState.Waiting)
            roleTxt.text = player.role.name;
        else
            roleTxt.text = "";

        playerName.text = player.name;
        statusTxt.text = player.status;
    }

    public void clicked()
    {
        main.PerFormedActionRequested(currentPlayer.id);
    }
}
