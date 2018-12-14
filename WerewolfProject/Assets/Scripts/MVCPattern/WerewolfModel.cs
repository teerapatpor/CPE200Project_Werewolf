using Proyecto26;
using UnityEditor;
using UnityEngine;
using WerewolfAPIModel;


public class WerewolfModel : WerewolfElement {

    private Player _player = null;
    public Player Player { get { return _player; } }
    private Game _game = null;
    public Role playerRole { get; private set; }
    public Action[] playerActions { get; private set; }

    private RequestHelper currentRequest;
    private string baseUrl = "http://localhost:2343/werewolf/"; // self server
    //[SerializeField]
    //private string baseUrl = "http://project-ile.net:2342/werewolf/"; // online server 2 player

    public void ChangeBaseUrl(int playerAmount)
    {
        baseUrl = string.Format("http://project-ile.net:234{0}/werewolf/", playerAmount);
    }

    // Add player to server
    public void PlayerSignUp(string name, string password)
    {
        string jsonData = "{\"name\":\"" + name + "\", \"password\":\"" + password + "\"}";

        currentRequest = new RequestHelper
        {
            Uri = baseUrl + "player/",
            BodyString = jsonData,
        };

        RestClient.Post<Player>(currentRequest)
            .Then(res => {
                _player = res;
                EditorUtility.DisplayDialog("SignUp successfull", "Welcome "+_player.name, "ok");
            })
            .Catch(err => EditorUtility.DisplayDialog("SignUp unsuccessfull", err.Message + "\n" + err.Source, "ok"));
    }

    // login player to game
    public void LoginPlayer(string name, string password)
    {

        string jsonData = "{\"name\":\"" + name + "\", \"password\":\"" + password + "\"}";

        currentRequest = new RequestHelper
        {
            Uri = baseUrl + "player/login",
            BodyString = jsonData,
        };

        RestClient.Post<Player>(currentRequest)
            .Then(res => {
                _player = res;
                MainApp.view.ChangeCamToMainMenu();
                
            })
            .Catch(err => EditorUtility.DisplayDialog("Login unsuccessfull", err.Message, "ok"));
    }
    
    // Get information of all role
    public void GetAllRole()
    {
        RestClient.GetArray<Role>(baseUrl + "role/")
            .Then(res =>
            {
                MainApp.view.ChangeCamToRole(res);
            }).Catch(err => EditorUtility.DisplayDialog("GetAllRole Error", err.Message, "ok"));
    }

    // logout player to login page
    public void LogoutPlayer()
    {
        if (_player != null && _player.session != "")
        {
            RestClient.Get(baseUrl + "player/logout/" + _player.session)
                .Then(res => {
                    _player = null;
                    MainApp.view.ChangeCamToLogin();
                })
                .Catch(err => EditorUtility.DisplayDialog("Logout error", err.Message, "Ok"));
        }
    }

    // play the game
    public void JoinGame()
    {
        if (_player != null && _player.session != "")
        {
            currentRequest = new RequestHelper
            {
                Uri = baseUrl + "/game/session/" + _player.session,
            };

            RestClient.Post<Game>(currentRequest)
                .Then(res => {
                    _game = null;
                    MainApp.view.ChangeCamToGamePlay();
                    UpdateGamplay();
                })
                .Catch(err =>
                {
                    EditorUtility.DisplayDialog("JoinGame error", err.Message + "\n" , "Ok");

                });
        }
    }

    
    public void UpdateGamplay()
    {
        RestClient.Get<Game>(baseUrl + "/game/session/" + _player.session)
            .Then(res => {

                if(res.status == GameState.Playing && (_game == null || _game.status == GameState.Waiting))
                {
                    //Just come to Play
                    playerRole = res.GetPlayerById(_player.id).role;
                    GetAllActionFromRole();
                    _game = res;
                    return;
                }
                else if (res.status == GameState.Ended && _game.status == GameState.Playing)
                {
                    //game just ended
                    playerRole = null;
                    playerActions = null;

                }

                
                if (res.status != GameState.Ended)
                {
                    //game not ended sent res
                    _game = res;
                    MainApp.view.GetUpdateInformation(_game);
                }
                else
                {
                    //game ended set game to null and sent last information of game
                    _game = null;
                    MainApp.view.GetUpdateInformation(res);
                }

            })
            .Catch(err =>
            {
                 EditorUtility.DisplayDialog("UpdateGamplay Error", err.Message + "\n" + err.Source, "Ok");
                if (_game != null)
                     MainApp.view.GetUpdateInformation(_game);
                
            });

    }

    public void PlayerLeaveGame()
    {
        
         RestClient.Delete(baseUrl + "/game/session/" + _player.session, (err,res) =>
         {
            if (err == null)
            {
                _game = null;
                MainApp.view.ChangeCamToMainMenu();
            }
            else
            {
                EditorUtility.DisplayDialog("Leave Game Error", " err : " + err.Message, "Ok");
            }
         });


    }

    public void GetAllActionFromRole()
    {
        RestClient.GetArray<Action>(baseUrl + "action/findByRole/" + playerRole.id)
            .Then(res =>
            {
                playerActions = res;
                MainApp.view.GetAction(playerActions);
                MainApp.view.GetUpdateInformation(_game);

            }).Catch(err =>
            {
                EditorUtility.DisplayDialog("GetAllActionFromRole Error", err.Message + "\n" + err.Source, "Ok");
            });
    }

    // perform action on another player
    public void PerformAction(long actionId, long targetId)
    {
        currentRequest = new RequestHelper
        {
            Uri = baseUrl + "/game/action/" + _player.session +"/"+ actionId +"/"+ targetId,
        };

        RestClient.Post(currentRequest)
            .Then(res =>
            {
                Debug.Log(res);
            })
            .Catch(err =>
            {
                EditorUtility.DisplayDialog("PerformAction error", "You performed invalid action\n"+err.Message + "\n", "Ok");
                Debug.Log(err.Data);
            });
    }

} 
