using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityView : MonoBehaviour
{
    public Dictionary<City, GameObject> Cities;
    public GameObject CityPrefab;
    public GameObject MunichPrefab;
    bool Test = false;
    // Use this for initialization
    void Start()
    {
        this.Cities = new Dictionary<City, GameObject>();
        AddMunich(new Vector2(0, 0));
        AddConnection(true, new Vector2(0, 0), new Vector2(1, 0));
        AddConnection(true, new Vector2(1, 0), new Vector2(2, 0));
        AddConnection(true, new Vector2(2, 0), new Vector2(3, 0));
        AddCity(new Vector2(3, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCity(Vector2 position)
    {
        if (GameManager.Instance.Resources.beer < 1 || GameManager.Instance.Resources.steel < 2 || GameManager.Instance.Resources.concrete < 4)
            return;
        GameManager.Instance.Resources += new ResourceCount(0, -1, -2, -4, 0);

        City city = new City(position,
            GameManager.Instance.Connections,
            GameManager.Instance.Map,
            CityNameGenerator.GenerateName());

        GameManager.addCity(city);

        Cities.Add(city, Instantiate(CityPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity));
        if (city.path != null)
        {
            GameManager.addTrain(new Train(city.position, city));
            GameManager.Instance.connected.Add(city);
        }

        SetName(city, city.cityName);
        SetLevel(city, "" + city.upgradeLevel);
    }

    public void AddMunich(Vector2 position)
    {
        if (GameManager.Instance.Resources.beer < 1 || GameManager.Instance.Resources.steel < 2 || GameManager.Instance.Resources.concrete < 4)
            return;
        GameManager.Instance.Resources += new ResourceCount(0, -1, -2, -4, 0);

        City city = new City(position,
            GameManager.Instance.Connections,
            GameManager.Instance.Map,
            "Neu-München");

        GameManager.addCity(city);

        Cities.Add(city, Instantiate(MunichPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity));

        SetName(city, city.cityName);
        SetLevel(city, "" + city.upgradeLevel);
    }

    public void AddConnection(bool isStammstrecke,Vector2 left, Vector2 right)
    {
        if (GameManager.Instance.Resources.beer < 1 || GameManager.Instance.Resources.steel < 1 || GameManager.Instance.Resources.concrete < 1)
            return;
        
        if(!GameManager.Instance.Connections.Connect(left, right, isStammstrecke, 1))
        {
            return;
        }

        GameManager.Instance.Resources += new ResourceCount(0, -1, -1, -1, 0);

        HashSet<City> adjecent = new HashSet<City>();
        foreach(City cty in GameManager.Instance.Cities)
        {
            if(cty.position == left || cty.position == right)
            {
                adjecent.Add(cty);
            }
        }

        foreach(City cty in adjecent)
        {
            if(!GameManager.Instance.connected.Contains(cty))
            {
                GameManager.addTrain(new Train(cty.position, cty));
                GameManager.Instance.connected.Add(cty);
            }
        }

        foreach(City cty in GameManager.Instance.Cities)
        {
            cty.CalculatePaths(GameManager.Instance.Connections);
        }
    }

    public void UpgradeCity(Vector2 position)
    {
        foreach(City city in GameManager.Instance.Cities) 
        {
            if (city.position == position)
            {   
                UpgradeCity(city);
            }
        }
    }

    public void UpgradeCity(City city) 
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
                cty.transform.Find("StadtLV2").gameObject.SetActive(true);
            }
            else
            {
                cty.transform.Find("StadtLV3").gameObject.SetActive(true);
                cty.transform.Find("StadtLV2").gameObject.SetActive(false);
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
