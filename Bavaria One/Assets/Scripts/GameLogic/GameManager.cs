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

    private Map map;
    private List<City> cities;
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
    }

    void Start()
    {
        this.map = new Map(width, height, ResourceFrequency, ResourceOctaves, ResourceSeed);
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
