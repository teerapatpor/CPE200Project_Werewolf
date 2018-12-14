using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all elements in this application.
public class WerewolfElement : MonoBehaviour
{
    // Gives access to the application and all instances.
    public WerewolfApplication MainApp { get { return GameObject.FindObjectOfType<WerewolfApplication>(); } }
}

public class WerewolfApplication : MonoBehaviour {

    public WerewolfModel model;
    public WerewolfView view;
    public WerewolfController controller;
    public ImageResource imageResource;

    public void Awake()
    {
        if(model == null)
            model = GetComponentInChildren<WerewolfModel>();
        if(view == null)
            view = GetComponentInChildren<WerewolfView>();
        if(controller == null)
            controller = GetComponentInChildren<WerewolfController>();
    }

    public void Notify(WerewolfCommand command, Object p_target, params object[] p_data)
    {
        WerewolfController[] controller_list = GetAllControllers();
        foreach (WerewolfController c in controller_list)
        {
            c.OnNotification(command, p_target, p_data);
        }
    }

    public WerewolfController[] GetAllControllers()
    {
        return GetComponentsInChildren<WerewolfController>();
    }
}


