using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityView : MonoBehaviour
{
    public Dictionary<City, GameObject> Cities;
    public GameObject CityPrefab;
    bool Test = false;
    // Use this for initialization
    void Start()
    {
        this.Cities = new Dictionary<City, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Test)
        {
            AddCity(new Vector2(0, 0));
            Test = true;
        }
    }
    public void AddCity(Vector2 position)
    {
        City city = new City(position,
            GameManager.Instance.Connections,
            GameManager.Instance.Map,
            CityNameGenerator.GenerateName());

        GameManager.addCity(city);

        Cities.Add(city, Instantiate(CityPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity));

        SetName(city, city.cityName);
        SetLevel(city, "" + city.upgradeLevel);
    }

    public void UpgradeCity(Vector2 position)
    {
        foreach(City city in GameManager.Instance.Cities) 
        {
            if (city.position == position)
            {   
                city.upgradeLevel++;
                SetLevel(city, "" + city.upgradeLevel);

                GameObject cty;
                Cities.TryGetValue(city, out cty);

                if (cty != null)
                {
                    cty.transform.Find("stadtLV1").gameObject.SetActive(false);
                    if (city.upgradeLevel == 2)
                    {
                        cty.transform.Find("StadtLv2").gameObject.SetActive(true);
                    }
                    else
                    {
                        cty.transform.Find("StadtLv3").gameObject.SetActive(true);
                        cty.transform.Find("StadtLv2").gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void SetText(City city, string text, string name)
    {
        GameObject cty;
        Cities.TryGetValue(city, out cty);

        if (cty != null)
        {
            Transform nameBox = null;
            for (int i = 0; i < cty.transform.childCount; i++)
            {
                Transform nb = cty.transform.GetChild(i).Find(name);
                nameBox = nb == null ? nameBox : nb;
            }
             if (nameBox != null)
            {
                TextMesh txt = nameBox.GetComponentInChildren<TextMesh>();
                if(txt != null)
                {
                    txt.text = text;
                }
            }
        }
    }

    public void SetName(City city, string name)
    {
        SetText(city, name, "Name Box");
    }

    public void SetLevel(City city, string level)
    {
        SetText(city, level, "Lvl Box");
    }
}
