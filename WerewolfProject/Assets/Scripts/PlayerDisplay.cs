using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WerewolfAPIModel;
using UnityEngine.UI;

public class PlayerDisplay : WerewolfElement{

    private Text playerName, roleTxt;
    private Player currentPlayer;
    private MainGame main;
    private Image roleImage;

    private void Start()
    {
        Text[] allTxt = GetComponentsInChildren<Text>();
        foreach(Text text in allTxt)
        {
            switch (text.gameObject.tag)
            {
                case "Name":
                    playerName = text;
                    break;
                case "Role":
                    roleTxt = text;
                    break;
                default:
                    Debug.LogError("No match Tag: " + text.name);
                    break;
            }
        }

        roleTxt.text = "";
        playerName.text = "";

        roleImage = GetComponentInChildren<Image>();
        main = GetComponentInParent<MainGame>();
        gameObject.GetComponent<Button>().onClick.AddListener(clicked);
    }

    public void updateGUI(Player player,string gameState)
    {

        currentPlayer = player;
       
        if (player.role != null && gameState != GameState.Waiting)     
            ChangeImageWithRole(player.role);

        playerName.text = player.name;
        //roleTxt.text = player.status;
    }

    public void clicked()
    {
        main.PerFormedActionRequested(currentPlayer.id);
    }

    private void ChangeImageWithRole(Role role)
    {
        if(role != null)
        {
            switch (role.name)
            {
                case "Gunner":
                    roleImage.sprite = MainApp.imageResource.gunner;
                    break;
                case "Werewolf":
                    roleImage.sprite = MainApp.imageResource.werewolf;
                    break;
                case "Seer":
                    roleImage.sprite = MainApp.imageResource.seer;
                    break;
                case "Aura Seer":
                    roleImage.sprite = MainApp.imageResource.aura_seer;
                    break;
                case "Priest":
                    roleImage.sprite = MainApp.imageResource.priest;
                    break;
                case "Doctor":
                    roleImage.sprite = MainApp.imageResource.doctor;
                    break;
                case "Werewolf Sharman":
                    roleImage.sprite = MainApp.imageResource.werewolf_shaman;
                    break;
                case "Alpha Werewolf":
                    roleImage.sprite = MainApp.imageResource.alpha_werewolf;
                    break;
                case "Werewolf Seer":
                    roleImage.sprite = MainApp.imageResource.werewolf_seer;
                    break;
                case "Medium":
                    roleImage.sprite = MainApp.imageResource.medium;
                    break;
                case "Bodyguard":
                    roleImage.sprite = MainApp.imageResource.body_guard;
                    break;
                case "Jailer":
                    roleImage.sprite = MainApp.imageResource.jailer;
                    break;
                case "Fool":
                    roleImage.sprite = MainApp.imageResource.fool;
                    break;
                case "Head Hunter":
                    roleImage.sprite = MainApp.imageResource.head_hunter;
                    break;
                case "Serial Killer":
                    roleImage.sprite = MainApp.imageResource.serial_killer;
                    break;
            }

            roleTxt.text = role.name;
        }
        else
        {
            // blank charactor
            roleImage.sprite = MainApp.imageResource.blank;
        }
    }

}
