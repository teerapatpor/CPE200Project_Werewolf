using UnityEngine;
using UnityEngine.UI;

public class Login : WerewolfView {

    [SerializeField]
    private InputField usrTxt;
    [SerializeField]
    private InputField passTxt;
    [SerializeField]
    private Text ShowServer;

    public int playerServer = 2;

    public void SignUp()
    {
        if (usrTxt.text == null || passTxt.text == null)
            return;
        cmd.Action = WerewolfCommand.CommandEnum.SignUp;
        MainApp.Notify(cmd, this, usrTxt.text, passTxt.text);
    }

    public void LogIn()
    {
        if (usrTxt.text == null || passTxt.text == null)
            return;
        cmd.Action = WerewolfCommand.CommandEnum.LogIn;
        MainApp.Notify(cmd, this, usrTxt.text, passTxt.text, playerServer);
    }

    public void ChangeServer2Player()
    {
        playerServer = 2;
        ChangeServerRequest();
    }

    public void ChangeServer4Player()
    {
        playerServer = 4;
        ChangeServerRequest();
        
    }

    public void ChangeServer16Player()
    {
        playerServer = 16;
        ChangeServerRequest();
    }

    private void ChangeServerRequest()
    {
        cmd.Action = WerewolfCommand.CommandEnum.ChangeServer;
        MainApp.Notify(cmd, this, playerServer);
        ShowServer.text = string.Format("Current Server: {0} Player", playerServer);
    }

    



}
