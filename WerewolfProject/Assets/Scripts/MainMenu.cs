using UnityEngine;
using UnityEngine.UI;

public class MainMenu : WerewolfView {

    public void RolePressed()
    {
        cmd.Action = WerewolfCommand.CommandEnum.GetRoleInformation;
        MainApp.Notify(cmd, this);
    }

    public void PlayBtnPressed()
    {
        cmd.Action = WerewolfCommand.CommandEnum.JoinGame;
        MainApp.Notify(cmd, this);
    }

    public void BackBtnPressed()
    {
        cmd.Action = WerewolfCommand.CommandEnum.SignOut;
        MainApp.Notify(cmd, this);
    }
}
