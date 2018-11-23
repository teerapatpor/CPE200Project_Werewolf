using WerewolfAPIModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using Proyecto26;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainClient : MonoBehaviour{

    private static MainClient _instance;

    private Player mainPlayer;
    //private readonly string baseUrl = "http://localhost:2343/werewolf/";
    private readonly string baseUrl = "http://project-ile.net:2342/werewolf/";
    private RequestHelper currentRequest;
    private Game gameState;
    

    //sigleton
    public static MainClient Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MainClient>();
                if (_instance == null)
                {
                    GameObject sigleton = new GameObject();
                    _instance = sigleton.AddComponent<MainClient>();
                    sigleton.name = typeof(MainClient).Name;

                    DontDestroyOnLoad(sigleton);
                }
            }

            return _instance;
        }
    }

    /// --------------------------------------------------------------------------
    /// Start Action API
    /// --------------------------------------------------------------------------
    // Get lists of actions
    public void ActionGet()
    {
        RestClient.GetArray<Action>(baseUrl + "action/")
            .Then(res => EditorUtility.DisplayDialog("Get action list", JsonHelper.ArrayToJsonString<Action>(res, true) , "ok"))
            .Catch(err => EditorUtility.DisplayDialog("Error", err.Message, "ok"));
    }

    //find action by role id
    public void FindActionsByRoleId(int roleId)
    {
        RestClient.GetArray<Action>(baseUrl + "/action/findByRole/" + roleId)
            .Then(res => EditorUtility.DisplayDialog("Get action list by role", JsonHelper.ArrayToJsonString<Action>(res, true), "ok"))
            .Catch(err => EditorUtility.DisplayDialog("Error", err.Message, "ok"));
    }

    // find action by id
    public void GetActionById(int actionId)
    {
        RestClient.Get<Action>(baseUrl + "/action/" + actionId)
            .Then(res => EditorUtility.DisplayDialog("Get action by id", JsonUtility.ToJson(res, true), "ok"))
            .Catch(err => EditorUtility.DisplayDialog("Error", err.Message, "ok"));
    }
    /// --------------------------------------------------------------------------
    /// End Action API
    /// --------------------------------------------------------------------------

    /// --------------------------------------------------------------------------
    /// Start Role API
    /// --------------------------------------------------------------------------
    public void GetRole(Text roleText)
    {
        RestClient.GetArray<Role>(baseUrl + "role/")
            .Then(res =>
            {
                roleText.text = "";
                foreach (Role role in res)
                {
                    roleText.text += "\n----------------------------------------------------------------------\n";
                    roleText.text += "\nname : " + role.name;
                    roleText.text += "\ndescription : " + role.description;
                    roleText.text += "\nAction : ";
                    foreach (Action action in role.actions)
                    {
                        roleText.text += "\n\t" + action.name;
                        roleText.text += "\n\tdescription : " + action.description;
                    }
                    roleText.text += "\n-----------------------------------------------------------------------\n";
                }

            }).Catch(err => EditorUtility.DisplayDialog("Error", err.Message, "ok"));

      

    }

    /// --------------------------------------------------------------------------
    /// End Role API
    /// --------------------------------------------------------------------------

    /// --------------------------------------------------------------------------
    /// Start Player API
    /// --------------------------------------------------------------------------
    //Get list of all player on server
    public void GetPlayerList()
    {
        RestClient.GetArray<Player>(baseUrl + "player").Then(players =>
        {
            EditorUtility.DisplayDialog("Array", JsonHelper.ArrayToJsonString<Player>(players, true), "Ok");
        })
        .Catch(err => EditorUtility.DisplayDialog("Error", err.Message, "ok")); ;
    }

    //find players by game id
    public void GetPlayerByGame(int id)
    {
        RestClient.GetArray<Player>(baseUrl + "player/" + id)
            .Then(res => EditorUtility.DisplayDialog("Get player by id", JsonUtility.ToJson(res, true), "ok"))
            .Catch(err => EditorUtility.DisplayDialog("Error", err.Message, "ok"));
    }

    //find player by id
    public void GetPlayerById(int id)
    {
        RestClient.Get<Player>(baseUrl + "player/" + id)
            .Then(res => EditorUtility.DisplayDialog("Get player by id", JsonUtility.ToJson(res, true), "ok"))
            .Catch(err => EditorUtility.DisplayDialog("Error", err.Message, "ok"));
    }


    // Add a new player to the system
    public void AddPlayer(Player newPlayer)
    {
        string jsonData = "{\"name\":\"" + newPlayer.name + "\", \"password\":\"" + newPlayer.password + "\"}";
        
        currentRequest = new RequestHelper
        {
            Uri = baseUrl + "player",
            BodyString = jsonData,
        };

        RestClient.Post<Player>(currentRequest)
            .Then(res => {
                EditorUtility.DisplayDialog("SignUp Completed", "Welcome " + JsonUtility.ToJson(res,true) , "Ok");
                })
            .Catch(err => EditorUtility.DisplayDialog("Error", "Player with that name already exist\n" + err.Message, "Ok"));
    }

    // Deletes a player
    public void DeletePlayer(int id)
    {

        RestClient.Delete(baseUrl + "player/" + id, (err, res) => {
            if (err == null)
            {
                EditorUtility.DisplayDialog("Delete successfull", "Goodbye.\n response:" + res.StatusCode, "Ok");
            }
            else
            {
                EditorUtility.DisplayDialog("SomethingWrong", " err : " + err.Message, "Ok");
            }
        });
    }

    // update existing player
    public void UpdatePlayer(Player newPlayer)
    {
        string jsonData = "{\"name\":\"" + newPlayer.name + "\", \"password\":\"" + newPlayer.password + "\"}";
        RestClient.Put<Player>(baseUrl + "player",
            jsonData,
            (err, res, player) => {
                if (err != null)
                {
                    EditorUtility.DisplayDialog("Error", "Please try again\n" + err.Message, "Ok");
                }
                else
                {
                    EditorUtility.DisplayDialog("Updated Completed", "your new password =" + player.password + "\n response:" + res.StatusCode, "Ok");
                }
            });
    }

    //login to Player account
    public void LoginPlayer(Player player)
    {
        
        string jsonData = "{\"name\":\"" + player.name + "\", \"password\":\"" + player.password + "\"}";

        currentRequest = new RequestHelper
        {
            Uri = baseUrl + "player/login",
            BodyString = jsonData,
        };

        RestClient.Post<Player>(currentRequest)
            .Then(res => {
                mainPlayer = res;
                SceneManager.LoadScene(1);
            })
            .Catch(err => EditorUtility.DisplayDialog("Login unsuccessfull", err.Message, "ok"));
    }

    //player logout
    public void LogoutPlayer()
    {
        if (mainPlayer != null && mainPlayer.session != "")
        {
            RestClient.Get(baseUrl + "player/logout/" + mainPlayer.session)
                .Then(res => {
                    mainPlayer = null;
                    SceneManager.LoadScene(0);
                 })
                .Catch(err => EditorUtility.DisplayDialog("err", err.Message, "Ok"));
        }
    }
    /// --------------------------------------------------------------------------
    /// End Player API
    /// --------------------------------------------------------------------------
    /// 
    /// --------------------------------------------------------------------------
    /// Start Game API
    /// --------------------------------------------------------------------------
    // join player into game
    public void JoinGame()
    {
        if (mainPlayer != null && mainPlayer.session != "")
        {
            currentRequest = new RequestHelper
            {
                Uri = baseUrl + "/game/session/" + mainPlayer.session,
            };

            RestClient.Post<Game>(currentRequest)
                .Then(res => {
                    
                    SceneManager.LoadScene(2);
                })
                .Catch(err => EditorUtility.DisplayDialog("err", err.Message, "Ok"));
        }
    }

    //get state of game from server
    public void GetGameServer()
    {
        if (mainPlayer != null && mainPlayer.session != "")
        {
            currentRequest = new RequestHelper
            {
                Uri = baseUrl + "/game/session/" + mainPlayer.session,
            };

            RestClient.Get<Game>(currentRequest)
                .Then(res => {
                    gameState = res;
                    if(mainPlayer.role == null)
                    {
                        foreach(Player player in res.players)
                        {
                            if(player.id == mainPlayer.id)
                            {
                                mainPlayer.role = player.role;
                            }
                        }
                    }
                    MainGame.Instance.UpdateGame(gameState);
                })
                .Catch(err => {
                    EditorUtility.DisplayDialog("err", err.Message, "Ok");
                    Debug.Log(err.StackTrace);
                    });
        }
    }

    // leave game because it's boring
    public void LeaveGame()
    {
        if (mainPlayer != null && mainPlayer.session != "" )
        {
            RestClient.Delete(baseUrl + "/game/session/" + mainPlayer.session, (err, res) => {
                if (err == null)
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", " err : " + err.Message, "Ok");
                }
            });
        }
    }

    // do something (vote, action)
    public void PerformAction(int actionId, int targetId)
    {
        currentRequest = new RequestHelper
        {
            Uri = baseUrl + string.Format("game/action/{0}/{1}/{2}",mainPlayer.session,actionId,targetId),
        };

        RestClient.Post(currentRequest)
            .Then(res => {
                EditorUtility.DisplayDialog("response", res.Text, "ok");
            })
            .Catch(err => EditorUtility.DisplayDialog("Error", "Can't perform action\n"+err.Message, "ok"));
    }
    /// --------------------------------------------------------------------------
    /// End Game API
    /// --------------------------------------------------------------------------
    /// 


    public Player GetMainPlayer()
    {
        return mainPlayer;
    }



}
