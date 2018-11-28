using UnityEngine;
using WerewolfAPIModel;
using UnityEngine.UI;

public class RoleView : WerewolfView {
    [SerializeField]
    private Text roleInformation;
    
    public void updateRole(Role[] roles)
    {
        roleInformation.text = "";
        foreach (Role role in roles)
        {
            roleInformation.text += "\n----------------------------------------------------------------------\n";
            roleInformation.text += "\nname : " + role.name;
            roleInformation.text += "\ndescription : " + role.description;
            roleInformation.text += "\nAction : ";
            foreach (Action action in role.actions)
            {
                roleInformation.text += "\n\t" + action.name;
                roleInformation.text += "\n\tdescription : " + action.description;
            }
            roleInformation.text += "\n-----------------------------------------------------------------------\n";
        }
    }

    public void BackBtnPressed()
    {
        ChangeCamToMainMenu();
    }
}
