using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonManager : MonoBehaviour {

    //Resource Fields
    public Text[] resourceTextFields = new Text[5];

    //Upgrade Popup Menu
    public GameObject popUpUpgrade;
    private RectTransform popUpTransform;
    public Text upgradeText;
    public Text costText;

    //Build mode
    public MouseGridMovement mgm;
    public bool jpopUpFixed = false; //It's 2am, I fucking wrote jpop

    private void Start()
    {
        popUpTransform = popUpUpgrade.GetComponent<RectTransform>();
    }

    public void OnToggleBuildMode()
    {
        mgm.ToggleSelectMode();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
        //Debug for Schildchen
        if (Input.GetMouseButtonDown(0) && mgm.selectMode && !jpopUpFixed)
        {
            popUpUpgrade.SetActive(true);

            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)popUpTransform.parent, Input.mousePosition, null, out point);
            popUpTransform.anchoredPosition = point;
        }

        //Modes
        if (Input.GetKeyDown("1"))
        {
            OnToggleBuildMode();
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
        //Debug.Log("YAS"); werks
        popUpUpgrade.SetActive(false);
        jpopUpFixed = false;
    }

}
