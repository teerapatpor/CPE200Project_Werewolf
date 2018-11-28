using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WerewolfAPIModel;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour{

    [SerializeField]
    private Text roleTxt, playerName, statusTxt;
    private Player currentPlayer;
    private MainGame main;
    private void Start()
    {
        statusTxt.text = "";
        roleTxt.text = "";
        playerName.text = "";
        main = GetComponentInParent<MainGame>();
        gameObject.GetComponent<Button>().onClick.AddListener(clicked);
    }

    public void updateGUI(Player player,string gameState)
    {

        currentPlayer = player;
       
        if (player.role != null && gameState != GameSate.Waiting)
            roleTxt.text = player.role.name;
        else
            roleTxt.text = "";

        playerName.text = player.name;
        statusTxt.text = player.status;
    }

    public void clicked()
    {
        Debug.Log("hello");
        main.PerFormedActionRequested(currentPlayer.id);
    }
}
