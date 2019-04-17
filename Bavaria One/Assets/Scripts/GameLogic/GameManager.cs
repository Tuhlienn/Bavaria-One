using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class GameManager : Singleton<GameManager>
{
    public int width = 100;
    public int height = 100;
    public ResourceCount startResources;

    public float Speed = 1.0f;
    public bool IsPaused = false;

    public float ResourceFrequency = 0.1f;
    public int ResourceOctaves = 6;
    public float ResourceSeed = 111.68465165f;

    public GameObject[,] ResourceIcons;
    public GameObject GoldPrefab;
    public GameObject BeerPrefab;
    public GameObject SteelPrefab;
    public GameObject ConcretePrefab;
    public GameObject EnergyPrefab;
    public GameObject TrainPrefab;

    public AudioClip BackgroundMusic;

    private Map map;
    private List<City> cities;
    public HashSet<City> connected;
    private List<Train> trains;
    private List<Connection> tickingConnections;
    private Graph connections;
    private ResourceCount resources;
    private float DeltaTime = 0.0f;

    public Map Map
    {
        get
        {
            return map;
        }
    }

    public List<City> Cities
    {
        get
        {
            return cities;
        }
    }

    public List<Train> Trains
    {
        get
        {
            return trains;
        }
    }

    public Graph Connections
    {
        get
        {
            return connections;
        }
    }

    public ResourceCount Resources
    {
        get
        {
            return resources;
        }

        set
        {
            this.resources = value;
        }
    }

    public float DTime
    {
        get
        {
            return DeltaTime;
        }
    }

    void Awake() 
    {
        this.cities = new List<City>();
        this.trains = new List<Train>();
        this.tickingConnections = new List<Connection>();
        this.connections = new Graph();
        this.resources = startResources;
        this.connected = new HashSet<City>();
    }

    void Start()
    {
        this.map = new Map(width, height, ResourceFrequency, ResourceOctaves, ResourceSeed != 0 ? ResourceSeed : System.DateTime.Now.Millisecond);
        ResourceIcons = new GameObject[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var position = new Vector3(i - width / 2.0f, 0, j - height / 2.0f) + new Vector3(0.4f, 0.1f, 0.4f);
                var rotation = Quaternion.Euler(60.0f, 45.0f, 0.0f);
                
                if (map.tiles[i,j].resource.money > 0)
                {
                    ResourceIcons[i, j] = Instantiate(GoldPrefab, position, rotation, this.transform);
                }
                else if (map.tiles[i, j].resource.beer > 0)
                {
                    ResourceIcons[i, j] = Instantiate(BeerPrefab, position, rotation, this.transform);
                }
                else if (map.tiles[i, j].resource.steel > 0)
                {
                    ResourceIcons[i, j] = Instantiate(SteelPrefab, position, rotation, this.transform);
                }
                else if (map.tiles[i, j].resource.concrete > 0)
                {
                    ResourceIcons[i, j] = Instantiate(ConcretePrefab, position, rotation, this.transform);
                }
            }
        }
        DeltaTime = 0.0f;

        SoundManager.Instance.PlayMusic(BackgroundMusic, true);
    }

    void Update()
    {
        if (IsPaused)
        {
            return;
        }
        DeltaTime += Time.deltaTime;
        if (DeltaTime >= (1.0f / Speed))
        {
            foreach (Train train in trains)
            {
                train.Tick();
            }
            foreach (Connection con in tickingConnections)
            {
                if(con != null) con.Tick();
            }
            tickingConnections = new List<Connection>();
            DeltaTime -= 1.0f / Speed;
        }
    }

    public static void addCity(City city) 
    {
        Instance.cities.Add(city);
        UpdateResourceCounts(city);
    }

    public static void UpdateResourceCounts(Vector2 tile)
    {
        ResourceCount mapResource = Instance.Map.tiles[(int)tile.x, (int)tile.y].resource;
        int x = (int) tile.x;
        int y = (int) tile.y;

        float amount = 0.0f;
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                var pos = new Vector2(x + i - Instance.width / 2, y + j - Instance.width / 2);

                City city = GetCity(pos);
                if(city != null)
                {
                    amount += 1.0f;
                    var multiResources = city.production.MultiResources;
                    if ((mapResource.money > 0 && multiResources.money > 0)
                        || (mapResource.beer > 0 && multiResources.beer > 0)
                        || (mapResource.steel > 0 && multiResources.steel > 0)
                        || (mapResource.concrete > 0 && multiResources.concrete > 0)
                        || (mapResource.energy > 0 && multiResources.energy > 0))
                    {
                        amount += 0.25f;
                    }
                    amount *= city.UpgradeLevel;
                }
            }
        }
        Instance.SetResourceDisplay(x, y, amount);
    }

    public static void UpdateResourceCounts(City city)
    {
        for(int i = -1; i < 1; i++)
        {
            for(int j = -1; j < 1; j++)
            {
                int x = (int)city.position.x + i + Instance.width / 2;
                int y = (int)city.position.y + j + Instance.height / 2;
                UpdateResourceCounts(new Vector2(x, y));
            }
        }
    }

    public static City GetCity(Vector2 position) 
    {
        foreach(City city in Instance.Cities) 
        {
            if(city.position == position)
                return city;
        }
        return null;
    }

    public static void addTrain(Train train) 
    {
        Instance.trains.Add(train);
    }
    
    public static void addConnection(Connection connection)
    {
        Instance.tickingConnections.Add(connection);
    }

    public void SetResourceDisplay(int x, int y, float amount)
    {
        var icon = ResourceIcons[x, y];
        if(icon != null) 
        {
            icon.transform.GetChild(1).gameObject.GetComponent<ResourceIconAmount>().CurrentAmount = amount;
        }
    }
}
