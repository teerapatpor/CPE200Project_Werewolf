using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text roleText;
    public GameObject Scroll;

    private void Awake()
    {
        this.gameObject.SetActive(true);
        Scroll.SetActive(false);
    }

    public void LogOutPressed()
    {
        MainClient.Instance.LogoutPlayer();
    }

    public void RolePressed()
    {
        MainClient.Instance.GetRole(roleText);
        this.gameObject.SetActive(false);
        Scroll.SetActive(true);
    }

    public void BackBtnPressed()
    {
        this.gameObject.SetActive(true);
        Scroll.SetActive(false);
    }

    public void PlayBtnPressed()
    {
        MainClient.Instance.JoinGame();
    }
}
