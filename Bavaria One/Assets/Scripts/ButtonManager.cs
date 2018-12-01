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
            switch (i) //why no array :(
            {
                case 0:
                    resourceTextFields[i].text = GameManager.Instance.Resources.money.ToString();
                    break;

                case 1:
                    resourceTextFields[i].text = GameManager.Instance.Resources.beer.ToString();
                    break;

                case 2:
                    resourceTextFields[i].text = GameManager.Instance.Resources.steel.ToString();
                    break;

                case 3:
                    resourceTextFields[i].text = GameManager.Instance.Resources.concrete.ToString();
                    break;

                case 4:
                    resourceTextFields[i].text = GameManager.Instance.Resources.energy.ToString();
                    break;
            }
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
