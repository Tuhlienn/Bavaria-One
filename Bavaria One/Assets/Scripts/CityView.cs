﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityView : MonoBehaviour
{
    public Dictionary<City, GameObject> Cities;
    public GameObject CityPrefab;
    // Use this for initialization
    void Start()
    {
        this.Cities = new Dictionary<City, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddCity(Vector2 position)
    {
        City city = new City(position,
            GameManager.Instance.Connections,
            GameManager.Instance.Map,
            CityNameGenerator.GenerateName());

        GameManager.Instance.Cities.Add(city);

        Cities.Add(city, Instantiate(CityPrefab));

        GameObject cityGO;
        Cities.TryGetValue(city, out cityGO);
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

    public void SetText(City city, string text, int i)
    {
        GameObject cty;
        Cities.TryGetValue(city, out cty);

        if(cty != null)
        {
            GameObject nameBox = cty.transform.GetChild(i).gameObject;
            nameBox.GetComponent<Text>().text = name;
        }
    }

    public void SetName(City city, string name)
    {
        SetText(city, name, 0);
    }

    public void SetLevel(City city, string level)
    {
        SetText(city, name, 1);
    }
}