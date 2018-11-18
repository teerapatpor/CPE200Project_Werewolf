using WerewolfAPIModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Login : MonoBehaviour {
    [SerializeField]
    private InputField usrTxt;
    [SerializeField]
    private InputField passTxt;
    [SerializeField]
    private GameObject mainMenu;
    
	public void LoginPressed()
    {
        if (usrTxt.text == "" || passTxt.text == "")
        {
            EditorUtility.DisplayDialog("Error", "Blank string Not Allowed", "Try Again");

        }
        else
        {

            Player player = new Player
            {
                name = usrTxt.text,
                password = passTxt.text
            };

            MainClient.Instance.LoginPlayer(player);
            
            //mainMenu.SetActive(true);
            //gameObject.SetActive(false);
        }

    }

    public void SignUpPressed()
    {
        if (usrTxt.text == "" || passTxt.text == "")
        {
            EditorUtility.DisplayDialog("Error", "Blank string Not Allowed", "Try Again");
            return;
        }
        else
        {

            Player player = new Player
            {
                name = usrTxt.text,
                password = passTxt.text
            };

            MainClient.Instance.AddPlayer(player);
        }
    }

}
