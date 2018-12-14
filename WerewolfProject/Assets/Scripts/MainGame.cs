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
    private ActionButtonScript[] actionBtns;
    [SerializeField]
    private Text timerTxt;
    [SerializeField]
    private Text outCome;
    [SerializeField]
    private Button leaveBtn;

    private Image background;

    private IEnumerator updateRequest;
    private long currentActionChosen = 0;
    private int timerCount = 0;

    private void Start()
    {
        playerDisplays = GetComponentsInChildren<PlayerDisplay>();
        actionBtns = GetComponentsInChildren<ActionButtonScript>();
 
        foreach(ActionButtonScript actionbtn in actionBtns)
        {
            actionbtn.gameObject.SetActive(false);
        }

        foreach(PlayerDisplay player in playerDisplays)
        {
            player.gameObject.SetActive(false);
        }

        outCome.text = "";

        background = GetComponent<Image>();
    }

    private void RequestUpdateMainGame()
    {

        cmd.Action = WerewolfCommand.CommandEnum.RequestUpdate;
        MainApp.Notify(cmd, this);
    }

    public void LeaveBtnPressed()
    {
        StopAllCoroutines();
        cmd.Action = WerewolfCommand.CommandEnum.LeaveGame;
        MainApp.Notify(cmd, this);
    }

    public void UpdateGameView(Game game)
    {
        if (game == null)
            return;


        if (game.status == GameState.Playing)
        {
            //if game is playing can't leave
            leaveBtn.enabled = false;

            if (gamePeriodShow.text != game.period) {
                //period change reset timecount
                timerCount = 0;
                ToggleDayNightImage(game.period);
                Debug.Log("period: " + game.period);
                //update actionBtn
                EnableAction(game.period);
            }
        }

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
            playerDisplays[i].gameObject.SetActive(true);
            playerDisplays[i].updateGUI(game.players[i], game.status);
        }


        if (game.status != GameState.Ended)
        {
            StartCoroutine(UpdateRequest()); // game not end update
        }
        else
        {
            //if game ended can leave and show outcome           
            leaveBtn.enabled = true;
            outCome.text = game.outcome;
        }


    }

    public void UpdateActionBtn(Action[] actions)
    {
        //update all btn
        for(int i = 0; i < actions.Length; i++)
        {
            actionBtns[i].gameObject.SetActive(true);
            actionBtns[i].GetComponentInChildren<Text>().text = actions[i].name;

            long actionId = actions[i].id;
            setBtnActiveTime(actionBtns[i], actionId);               
            actionBtns[i].GetComponent<Button>().onClick.AddListener(delegate { SetAction(actionId); });
        }


    }

    public void SetAction(long actionId)
    {
        currentActionChosen = actionId;
    }

    public void PerFormedActionRequested(long targetId)
    {
        if(targetId == MainApp.model.Player.id)
        {
            //can't performed on self!!!
            //some redish animation
            Debug.Log("Can't performed action on self");
            return;
        }
        else if (currentActionChosen != 0)
        {
            cmd = new WerewolfCommand();
            switch (currentActionChosen)
            {
                case (long)Action.AllAction.DayVote:

                    // performed some animation?
                    cmd.Action = WerewolfCommand.CommandEnum.Action;
                    MainApp.Notify(cmd, MainApp.model, currentActionChosen, targetId);
                    break;

                case (long)Action.AllAction.NightVote:

                    if (MainApp.model.Player.role.type == RoleType.Wolf)
                    {
                        //invalid action
                        //some redish animation
                        Debug.Log("can't nightvote on werwolf");
                    }
                    else
                    {
                        cmd.Action = WerewolfCommand.CommandEnum.Action;
                    }
                    break;

                default:
                    //do some action animation
                    cmd.Action = WerewolfCommand.CommandEnum.Action;
                    MainApp.Notify(cmd, MainApp.model, currentActionChosen, targetId);
                    break;
            }
            Debug.Log("performed action: " + currentActionChosen + "on target: " + targetId);
            MainApp.Notify(cmd, MainApp.model, currentActionChosen, targetId);

        }
    }

    //request update every sec
    private IEnumerator UpdateRequest()
    {
        yield return new WaitForSeconds(1);  
        RequestUpdateMainGame();
    }
      
    
    private void setBtnActiveTime(ActionButtonScript actionBtn, long actionId)
    {
        switch (actionId)
        {
            //night action
            case (long)Action.AllAction.NightVote:
            case (long)Action.AllAction.Enchant:
            case (long)Action.AllAction.Kill:
            case (long)Action.AllAction.Reveal:
            case (long)Action.AllAction.Aura:
            case (long)Action.AllAction.Heal:
                actionBtn.activeTime = ActionButtonScript.ActiveTimeEnum.Night;
                break;
            //day action
            case (long)Action.AllAction.DayVote:
            case (long)Action.AllAction.ThrowHolyWater:
            case (long)Action.AllAction.Shoot:
                actionBtn.activeTime = ActionButtonScript.ActiveTimeEnum.Day;
                break;
            default:
                actionBtn.activeTime = ActionButtonScript.ActiveTimeEnum.Both;
                break;

        }
    }

    private void EnableAction(string period)
    {
        foreach(ActionButtonScript action in actionBtns)
        {
            bool activate = false;
            switch (action.activeTime)
            {
                case ActionButtonScript.ActiveTimeEnum.Day:
                    activate = period == GameState.Day;
                    break;
                case ActionButtonScript.ActiveTimeEnum.Night:
                    activate = period == GameState.Night;
                    break;
                case ActionButtonScript.ActiveTimeEnum.Both:
                    activate = true;
                    break;
                default:
                    break;
            }

            action.GetComponent<Button>().enabled = activate;
        }
    }
    
    public void ToggleDayNightImage(string period)
    {
        if(period == "night")
        {
            //toggle night image
            background.sprite = MainApp.imageResource.night;
        }
        else if(period == "day")
        {
            //toggle day image
            background.sprite = MainApp.imageResource.day;
        }
    }


    

}
