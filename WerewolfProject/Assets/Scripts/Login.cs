using WerewolfAPIModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Login : WerewolfView {

    [SerializeField]
    private InputField usrTxt;
    [SerializeField]
    private InputField passTxt;

    public void SignUp()
    {
        cmd.Action = WerewolfCommand.CommandEnum.SignUp;
        MainApp.Notify(cmd, this, usrTxt.text, passTxt.text);
    }

    public void LogIn()
    {
        cmd.Action = WerewolfCommand.CommandEnum.LogIn;
        MainApp.Notify(cmd, this, usrTxt.text, passTxt.text);
    }

}
