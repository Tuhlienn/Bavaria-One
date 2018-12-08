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

    void Awake() 
    {
        this.cities = new List<City>();
        this.trains = new List<Train>();
        this.tickingConnections = new List<Connection>();
        this.connections = new Graph(width + 1, height + 1);
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

    public static void UpdateResourceCounts(City city)
    {
        for(int i = -1; i < 1; i++)
        {
            for(int j = -1; j < 1; j++)
            {
                int x = (int)city.position.x + i + Instance.width / 2;
                int y = (int)city.position.y + j + Instance.height / 2;
                ResourceCount MapResource = Instance.map.tiles[x, y].resource;
                ResourceCount CityMultiResource = city.production.MultiResources();

                float amount = 1.0f;
                if (MapResource.money > 0 && CityMultiResource.money > 0)
                {
                    amount = 1.25f;
                }
                else if(MapResource.beer > 0 && CityMultiResource.beer > 0)
                {
                    amount = 1.25f;
                }
                else if (MapResource.steel > 0 && CityMultiResource.steel > 0)
                {
                    amount = 1.25f;
                }
                else if (MapResource.concrete > 0 && CityMultiResource.concrete > 0)
                {
                    amount = 1.25f;
                }
                else if (MapResource.energy > 0 && CityMultiResource.energy > 0)
                {
                    amount = 1.25f;
                }
                Instance.AddToResourceDisplay(x, y, amount);
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

    public void AddToResourceDisplay(int w, int h, float amount)
    {
        var icon = ResourceIcons[w, h];
        if(icon != null) 
        {
            icon.transform.GetChild(1).gameObject.GetComponent<ResourceIconAmount>().CurrentAmount += amount;
        }
    }
}
