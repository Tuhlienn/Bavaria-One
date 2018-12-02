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
    private Vector3 upgradePosition;
    public Vector3 UpgradePosition 
    {
        get { return upgradePosition; }
        set { upgradePosition = value; }
    }

    //Build mode
    public MouseGridMovement gridMovement;
    public bool popUpFixed = false;

    private void Start()
    {
        popUpTransform = popUpUpgrade.GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Modes
        if (Input.GetKeyDown("1"))
        {
            OnToggleBuildMode();
        }
    }

    private void LateUpdate()
    {
        UpdateResources();
    }


    public void OnToggleBuildMode()
    {
        gridMovement.ToggleSelectMode();
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
            var resources = GameManager.Instance.Resources;
            switch (i) //why no array :(
            {
                case 0:
                    resourceTextFields[i].text = resources.money.ToString(); 
                    break;

                case 1:
                    resourceTextFields[i].text = resources.beer.ToString();
                    break;

                case 2:
                    resourceTextFields[i].text = resources.steel.ToString();
                    break;

                case 3:
                    resourceTextFields[i].text = resources.concrete.ToString();
                    break;

                case 4:
                    resourceTextFields[i].text = resources.energy.ToString();
                    break;
            }
        }
    }

    public void showPopup(Vector3 position) 
    {
        if (!popUpFixed)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

            Vector2 anchoredPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)popUpTransform.parent, screenPoint, null, out anchoredPosition);
            popUpTransform.anchoredPosition = anchoredPosition;
            
            popUpUpgrade.SetActive(true);
            upgradePosition = position;
        }
    }

    public void TogglePopUp(bool enabled)
    {

    }

    public void Upgrade()
    {
        //Debug.Log("YAS"); werks
        popUpUpgrade.SetActive(false);
        popUpFixed = false;
    }

}
