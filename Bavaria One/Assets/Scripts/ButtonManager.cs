using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    //Resource Fields
    public Text[] resourceTextFields = new Text[5];

    //Upgrade Popup Menu
    public GameObject popUpUpgrade;
    public RectTransform popUpTransform;
    public GameObject popUpUpgradeInProgress;
    public RectTransform popUpInProgressTransform;
    public Text upgradeText;
    public Text costText;

    private void Start()
    {
        popUpTransform = popUpUpgrade.GetComponent<RectTransform>();
        popUpInProgressTransform = popUpUpgradeInProgress.GetComponent<RectTransform>();
    }

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)popUpTransform.parent, Input.mousePosition, null, out point);
            popUpTransform.anchoredPosition = point;
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
