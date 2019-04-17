using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityView : MonoBehaviour
{
    public static CityView Instance;

    [SerializeField] protected GameObject cityPrefab;
    [SerializeField] protected GameObject munichPrefab;
    [SerializeField] protected AudioClip railSound;

    public Dictionary<City, GameObject> Cities;

    protected GridRenderer Grid;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Grid = Camera.main.GetComponent<GridRenderer>();
        
        this.Cities = new Dictionary<City, GameObject>();
        AddCity(new Vector2(0, 0), "Neu-München", munichPrefab);
        AddConnection(new Vector2(0, 0), new Vector2(1, 0), true);
        AddConnection(new Vector2(1, 0), new Vector2(2, 0), true);
        AddConnection(new Vector2(2, 0), new Vector2(3, 0), true);
        AddCity(new Vector2(3, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildCity(Vector2 position) 
    {
        var resourceCost = new ResourceCount(0, 1, 2, 4, 0);
        if (GameManager.Instance.Resources < resourceCost)
        {
            return;
        }
        GameManager.Instance.Resources -= resourceCost;

        AddCity(position);
    }

    public void AddCity(Vector2 position, string name, GameObject prefab) 
    {
        City city = new City(position,
            GameManager.Instance.Connections,
            GameManager.Instance.Map,
            name);

        AddCity(city, prefab);        
    }

    public void AddCity(Vector2 position)
    {
        City city = new City(position,
            GameManager.Instance.Connections,
            GameManager.Instance.Map,
            CityNameGenerator.GenerateName());

        AddCity(city, cityPrefab);
    }

    public void AddCity(City city, GameObject prefab)
    {
        GameManager.addCity(city);

        Cities.Add(city, Instantiate(prefab, new Vector3(city.position.x, 0, city.position.y), Quaternion.identity));
        if (city.path != null)
        {
            GameManager.addTrain(new Train(city.position, city));
            GameManager.Instance.connected.Add(city);
        }

        SetName(city, city.cityName);
        SetLevel(city, "" + city.UpgradeLevel);
    }

    public bool BuildConnection(Vector2 left, Vector2 right, bool isStammstrecke)
    {
        var resourceCost = new ResourceCount(0, 1, 1, 1, 0);
        if (GameManager.Instance.Resources < resourceCost)
        {
            return false;
        }
        if(!AddConnection(left, right, isStammstrecke)) 
        {
            return false;
        }
        GameManager.Instance.Resources -= resourceCost;
        SoundManager.Instance.Play(railSound);
        return true;
    }

    public bool AddConnection(Vector2 left, Vector2 right, bool isStammstrecke)
    {
        if(!GameManager.Instance.Connections.Connect(left, right, isStammstrecke, 1))
        {
            return false;
        }

        HashSet<City> newConnected = new HashSet<City>();
        foreach(City cty in GameManager.Instance.Cities)
        {
            if(cty.CalculatePaths(GameManager.Instance.Connections))
            {
                newConnected.Add(cty);
            }
        }

        foreach(City cty in newConnected)
        {
            if(!GameManager.Instance.connected.Contains(cty))
            {
                GameManager.addTrain(new Train(cty.position, cty));
                GameManager.Instance.connected.Add(cty);
            }
        }

        Grid.AddConnectionToBuilt(left, right);

        return true;
    }

    public bool BuildEnergyPanel(Vector2 position)
    {
        ResourceCount resourceCost = new ResourceCount(0, 3, 2, 0, 0);
        if(GameManager.Instance.Resources < resourceCost)
        {
            return false;
        }
        GameManager.Instance.Resources -= resourceCost;

        GameManager.Instance.Map.tiles[(int)position.x, (int)position.y].resource = new ResourceCount(0, 0, 0, 0, 1);

        GameManager.UpdateResourceCounts(position);
        return true;
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
        int cost = city.UpgradeCost;
        var resourceCost = new ResourceCount(cost, cost, cost, cost, 0);
        if (GameManager.Instance.Resources < resourceCost)
        {
            return;
        }
        GameManager.Instance.Resources -= resourceCost;

        city.UpgradeLevel++;
        SetLevel(city, "" + city.UpgradeLevel);
        GameManager.UpdateResourceCounts(city);

        GameObject cty;
        Cities.TryGetValue(city, out cty);

        if (cty != null)
        {
            cty.transform.Find("stadtLV1").gameObject.SetActive(false);
            if (city.UpgradeLevel == 2)
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
