using UnityEngine;

public class WerewolfCommand 
{
    public enum CommandEnum
    {
        SignUp = 1,
        LogIn = 2,
        SignOut = 3,
        JoinGame = 4,
        RequestUpdate = 5,
        LeaveGame = 6,
        Vote = 7,
        Action = 8,
        GetRoleInformation = 9,
        GetUpdateInformation = 10,
    };
    public CommandEnum Action { get; set; }
}

public class WerewolfController : WerewolfElement
{
    public void OnNotification(WerewolfCommand cmd, Object p_target, params object[] p_data)
    {
        switch (cmd.Action)
        {
            case WerewolfCommand.CommandEnum.SignUp:            
                MainApp.model.PlayerSignUp((string)p_data[0], (string)p_data[1]);
                break;
            case WerewolfCommand.CommandEnum.LogIn:
                MainApp.model.LoginPlayer((string)p_data[0], (string)p_data[1]);
                break;
            case WerewolfCommand.CommandEnum.SignOut:
                MainApp.model.LogoutPlayer();
                break;
            case WerewolfCommand.CommandEnum.JoinGame:
                MainApp.model.JoinGame();
                break;
            case WerewolfCommand.CommandEnum.RequestUpdate:
                MainApp.model.UpdateGamplay();
                break;
            case WerewolfCommand.CommandEnum.LeaveGame:
                MainApp.model.PlayerLeaveGame();
                break;
            case WerewolfCommand.CommandEnum.Vote:
                MainApp.model.PerformAction((long)p_data[0], (long)p_data[1]);
                break;
            case WerewolfCommand.CommandEnum.Action:
                MainApp.model.PerformAction((long)p_data[0], (long)p_data[1]);
                break;
            case WerewolfCommand.CommandEnum.GetRoleInformation:
                MainApp.model.GetAllRole();
                break;
        }

    }
}

