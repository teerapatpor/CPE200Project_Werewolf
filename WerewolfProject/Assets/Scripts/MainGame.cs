using System.Collections;
using System.Collections.Generic;
using WerewolfAPIModel;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour {

    private static MainGame _instance;

    private bool isEnd = false;
    private bool getData = false;
    private IEnumerator gameRunning;

    [SerializeField]
    private Text gameStatus;

    public List<GameObject> allPlayer;

    public static MainGame Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MainGame>();
                if (_instance == null)
                {
                    Debug.LogError("No instance of maingame");

                }
            }

            return _instance;
        }
    }


    // Use this for initialization
    void Start() {

        gameRunning = getStateOfGame();

        foreach (Transform player in transform)
        {
            allPlayer.Add(player.gameObject);
            player.gameObject.SetActive(false);
        }

        isEnd = false;

        StartCoroutine(gameRunning);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("press a");
            StopCoroutine(gameRunning);
        }
    }

    IEnumerator getStateOfGame()
    {
        while (!isEnd) {
            MainClient.Instance.GetGameServer();
            yield return new WaitForSeconds(3);
        }
    }

    public void UpdateGame(Game game)
    {
        gameStatus.text = game.status;
        for (int i = 0; i < game.players.Count; i++)
        {
            allPlayer[i].SetActive(true);  
            allPlayer[i].GetComponent<PlayerDisplay>().updateGUI(game.players[i]);
        }

    }

    public void LeaveGame()
    {
        MainClient.Instance.LeaveGame();
        StopCoroutine(gameRunning);
        isEnd = true;
    }
}
