using System.Collections;
using System.Collections.Generic;
using WerewolfAPIModel;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : WerewolfView {

    private PlayerDisplay[] playerDisplays;

    [SerializeField]
    private Text gameStateShow;
    [SerializeField]
    private Text gamePeriodShow;
    [SerializeField]
    private Text gameDaysShow;
    [SerializeField]
    private Button[] actionBtns;
    [SerializeField]
    private Text timerTxt;

    private long currentActionChosen = 0;
    private int timerCount = 0;

    private void Start()
    {
        playerDisplays = GetComponentsInChildren<PlayerDisplay>();

        foreach(Button actionbtn in actionBtns)
        {
            actionbtn.gameObject.SetActive(false);
        }
        
    }

    private void RequestUpdateMainGame()
    {

        cmd.Action = WerewolfCommand.CommandEnum.RequestUpdate;
        MainApp.Notify(cmd, this);
    }

    public void LeaveBtnPressed()
    {

        cmd.Action = WerewolfCommand.CommandEnum.LeaveGame;
        MainApp.Notify(cmd, this);
    }

    public void UpdateGameView(Game game)
    {
        if (game == null)
            return;

        //period change reset timecount
        if (game.status == GameSate.Playing && gamePeriodShow.text != game.period)
            timerCount = 0;


        //timer event
        timerCount++;
        timerTxt.text = "Time : " + timerCount;

        //text showing
        gameStateShow.text = game.status;
        gamePeriodShow.text = game.period;
        gameDaysShow.text = "Days : " + game.day;

        //update each player
        for (int i = 0; i < game.players.Count; i++)
        {
            //playerDisplays[i].gameObject.SetActive(true);
            playerDisplays[i].updateGUI(game.players[i], game.status);
        }

        
        
        if (game.status != GameSate.Ended)
            StartCoroutine(UpdateRequest()); // game not end update


    }

    public void UpdateActionBtn(Action[] actions)
    {

        for(int i = 0; i < actions.Length; i++)
        {
            actionBtns[i].gameObject.SetActive(true);
            actionBtns[i].GetComponentInChildren<Text>().text = actions[i].name;
            long actionId = actions[i].id;
            actionBtns[i].onClick.AddListener(delegate { SetAction(actionId); });
        }

    }

    public void SetAction(long actionId)
    {
        currentActionChosen = actionId;
    }

    public void PerFormedActionRequested(long targetId)
    {
        if (currentActionChosen != 0 && currentActionChosen != MainApp.model.Player.id)
        {
            switch (currentActionChosen)
            {
                case 1:
                case 6:
                    //vote
                    cmd.Action = WerewolfCommand.CommandEnum.Vote;
                    MainApp.Notify(cmd, MainApp.model, currentActionChosen, targetId);
                    break;
                default:
                    cmd.Action = WerewolfCommand.CommandEnum.Action;
                    MainApp.Notify(cmd, MainApp.model, currentActionChosen, targetId);
                    break;
            }

        }
    }

    //request update every sec
    public IEnumerator UpdateRequest()
    {
        yield return new WaitForSeconds(1);  
        RequestUpdateMainGame();
    }
        


    

}
