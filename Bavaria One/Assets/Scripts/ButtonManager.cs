using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    //Resource Fields
    public Text[] resourceTextFields = new Text[5];

    //Upgrade Popup Menu
    public GameObject popUpUpgrade;
    public GameObject popUpUpgradeInProgress;
    public Text upgradeText;
    public Text costText;

    public void OnToggleBuildMode()
    {
        //gamemanager.buildmode = !buildmode
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateResources()
    {
        for(int i = 0; i < resourceTextFields.Length; i ++)
        {
            //resourceTextField[i].text = gamemanager.resources[i].value.toString();
        }
    }

    //Every Update, alternatively on every change
    private void LateUpdate()
    {
        UpdateResources();
    }

    public void TogglePopUp(bool enabled)
    {

    }

    public void Upgrade()
    {
        // gamemanager.specificobject.lvl.Upgrade();
    }


}
