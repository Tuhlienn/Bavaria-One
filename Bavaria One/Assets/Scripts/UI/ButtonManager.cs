using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour 
{
    public static ButtonManager Instance;

    //Resource Fields
    public Text[] ResourceTextFields = new Text[5];

    //Upgrade Popup Menu
    public GameObject PopUpUpgrade;
    public Text UpgradeText;
    public Text CostText;
    private RectTransform popUpTransform;
    private Vector3 upgradePosition;
    public Vector3 UpgradePosition 
    {
        get { return upgradePosition; }
        set { upgradePosition = value; }
    }

    //Toggle Buttons
    public Transform ConnectionBuilderButton;
    private bool connectionBuilderMode;
    public bool ConnectionBuilderMode 
    {
        get { return connectionBuilderMode; }
        set { connectionBuilderMode = value; }
    }
    public Transform CityBuilderButton;
    private bool cityBuilderMode;
    public bool CityBuilderMode 
    {
        get { return cityBuilderMode; }
        set { cityBuilderMode = value; }
    }

    //Pause Menu
    public GameObject PauseObject;
    public GameObject CreditsObject;


    public MouseGridMovement GridMovement;
    public AudioClip CitySound;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        
        popUpTransform = PopUpUpgrade.GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Modes
        if (Input.GetKeyDown("1"))
        {
            ToggleConnectionBuilder();
        }
        if (Input.GetKeyDown("2"))
        {
            ToggleCityBuilder();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    private void LateUpdate()
    {
        UpdateResources();
    }

    public void ToggleConnectionBuilder()
    {
        connectionBuilderMode = !connectionBuilderMode;

        Transform child1 = ConnectionBuilderButton.Find("Unchecked");
        child1.gameObject.SetActive(!connectionBuilderMode);

        Transform child2 = ConnectionBuilderButton.Find("Checked");
        child2.gameObject.SetActive(connectionBuilderMode);

        if(connectionBuilderMode && cityBuilderMode)
        {
            ToggleCityBuilder();
        }
    }

    public void ToggleCityBuilder()
    {
        cityBuilderMode = !cityBuilderMode;

        Transform child1 = CityBuilderButton.Find("Unchecked");
        child1.gameObject.SetActive(!cityBuilderMode);

        Transform child2 = CityBuilderButton.Find("Checked");
        child2.gameObject.SetActive(cityBuilderMode);

        if(connectionBuilderMode && cityBuilderMode)
        {
            ToggleConnectionBuilder();
        }
    }

    public void TogglePause()
    {
        PauseObject.SetActive(!PauseObject.activeInHierarchy);
        GameManager.Instance.IsPaused = PauseObject.activeInHierarchy;
        if (PauseObject.activeInHierarchy) ToggleCredits();
    }

    public void ToggleCredits()
    {
        CreditsObject.SetActive(!CreditsObject.activeInHierarchy);
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
        for(int i = 0; i < ResourceTextFields.Length; i ++)
        {
            var resources = GameManager.Instance.Resources;
            switch (i) //why no array :(
            {
                case 0:
                    ResourceTextFields[i].text = resources.money.ToString(); 
                    break;

                case 1:
                    ResourceTextFields[i].text = resources.beer.ToString();
                    break;

                case 2:
                    ResourceTextFields[i].text = resources.steel.ToString();
                    break;

                case 3:
                    ResourceTextFields[i].text = resources.concrete.ToString();
                    break;

                case 4:
                    ResourceTextFields[i].text = resources.energy.ToString();
                    break;
            }
        }
    }

    public void showPopup(Vector3 position) 
    {
        var city = GridMovement.selectedCity;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)popUpTransform.parent, screenPoint, null, out anchoredPosition);
        popUpTransform.anchoredPosition = anchoredPosition;
        
        PopUpUpgrade.SetActive(true);
        upgradePosition = position;

        UpgradeText.text = "Level: " + (city == null ? 0 : city.UpgradeLevel);
        CostText.text = "Cost: " + (city == null ? 2 : city.UpgradeCost);
    }

    public void BuildCity()
    {
        PopUpUpgrade.SetActive(false);

		SoundManager.Instance.Play(CitySound);
        if(GridMovement.selectedCity == null) 
        {
            var position = new Vector2(upgradePosition.x, upgradePosition.z);
            CityView.Instance.BuildCity(position);
        }
        else 
        {
            GameObject.Find("CityManager").GetComponent<CityView>().UpgradeCity(GridMovement.selectedCity);
        }
        
    }

    public void UpdateEffectsVolume(float volume) 
    {
        SoundManager.Instance.EffectsVolume = volume;
    }

    public void UpdateMusicVolume(float volume) 
    {
        SoundManager.Instance.MusicVolume = volume;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
