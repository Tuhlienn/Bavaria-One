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
    public GameObject GeldPrefab;
    public GameObject BierPrefab;
    public GameObject StahlPrefab;
    public GameObject BetonPrefab;
    public GameObject StromPrefab;

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
        this.map = new Map(width, height, ResourceFrequency, ResourceOctaves, ResourceSeed);
        ResourceIcons = new GameObject[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (map.tiles[i,j].resource.money > 0)
                {
                    ResourceIcons[i, j] = Instantiate(GeldPrefab, new Vector3(i - width / 2.0f + 0.6f, 0.1f, j - height / 2.0f + 0.1f), Quaternion.Euler(60.0f, 45.0f, 0.0f), this.transform);
                }
                else if (map.tiles[i, j].resource.beer > 0)
                {
                    ResourceIcons[i, j] = Instantiate(BierPrefab, new Vector3(i - width / 2.0f + 0.6f, 0.1f, j - height / 2.0f + 0.1f), Quaternion.Euler(60.0f, 45.0f, 0.0f), this.transform);
                }
                else if (map.tiles[i, j].resource.steel > 0)
                {
                    ResourceIcons[i, j] = Instantiate(StahlPrefab, new Vector3(i - width / 2.0f + 0.6f, 0.1f, j - height / 2.0f + 0.1f), Quaternion.Euler(60.0f, 45.0f, 0.0f), this.transform);
                }
                else if (map.tiles[i, j].resource.concrete > 0)
                {
                    ResourceIcons[i, j] = Instantiate(BetonPrefab, new Vector3(i - width / 2.0f + 0.6f, 0.1f, j - height / 2.0f + 0.1f), Quaternion.Euler(60.0f, 45.0f, 0.0f), this.transform);
                }
            }
        }
        DeltaTime = 0.0f;
    }

    void Update()
    {
        if (IsPaused)
        {
            return;
        }
        DeltaTime += Time.deltaTime;
        if (DeltaTime >= (5.0f / Speed))
        {
            // Call Tick() on all Ticking objects 
            foreach (Train train in trains)
            {
                train.Tick();
            }
            foreach (Connection con in tickingConnections)
            {
                con.Tick();
            }
            tickingConnections = new List<Connection>();
            DeltaTime += 5.0f / Speed;
        }
    }

    public static void addCity(Vector2 position) {
        addCity(new City(position, Instance.connections, Instance.map, CityNameGenerator.GenerateName()));
    }

    public static void addCity(City city) {
        Instance.cities.Add(city);
    }

    public static City GetCity(Vector2 position) {
        foreach(City city in Instance.Cities) {
            if(city.position == position)
                return city;
        }
        return null;
    }

    public static void addTrain(Train train) {
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
}
