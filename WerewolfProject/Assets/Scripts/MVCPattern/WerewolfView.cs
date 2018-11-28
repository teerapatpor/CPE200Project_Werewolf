using WerewolfAPIModel;
using UnityEngine;
using System.Collections.Generic;

// Rendering/Interface/Detection
public class WerewolfView : WerewolfElement {
    
    private GameObject loginView;
    private GameObject mainMenu;
    private GameObject roleView;
    [SerializeField]
    private GameObject gamePlayView;
    private Camera cam;

    protected WerewolfCommand cmd;

    private void Awake()
    {
        cmd = new WerewolfCommand();
        loginView = FindObjectOfType<Login>().gameObject;
        mainMenu = FindObjectOfType<MainMenu>().gameObject;
        roleView = FindObjectOfType<RoleView>().gameObject;
        gamePlayView = GameObject.FindGameObjectWithTag("gameplay");
        cam = Camera.main;

        ChangeCamToLogin();
        
    }

    public void ChangeCamToLogin()
    {
        cam.transform.position = new Vector3(
            loginView.transform.position.x,
            cam.transform.position.y,
            cam.transform.position.z);
    }

    public void ChangeCamToMainMenu()
    {
        cam.transform.position = new Vector3(
            mainMenu.transform.position.x,
            cam.transform.position.y,
            cam.transform.position.z);
    }

    public void ChangeCamToRole(Role[] roleInformation)
    {
        cam.transform.position = new Vector3(
            roleView.transform.position.x,
            cam.transform.position.y,
            cam.transform.position.z);

        roleView.GetComponent<RoleView>().updateRole(roleInformation);
    }

    public void ChangeCamToGamePlay()
    {
        cam.transform.position = new Vector3(
            gamePlayView.transform.position.x,
            cam.transform.position.y,
            cam.transform.position.z);
    }

    public void GetUpdateInformation(Game currentGame)
    {
        gamePlayView.GetComponent<MainGame>().UpdateGameView(currentGame);

    }

    public void GetAction(Action[] actions)
    {
        gamePlayView.GetComponent<MainGame>().UpdateActionBtn(actions);
    }



}
