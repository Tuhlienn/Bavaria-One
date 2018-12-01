using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class GameManager : MonoBehaviour{
    public int width, height;
    public ResourceCount startResources;

    public float ResourceFrequency = 0.01f;
    public int ResourceOctaves = 5;
    public float ResourceSeed = 111.68465165f;

    private static GameManager instance;
    private Map map;
    private List<City> cities;
    private List<Train> trains;
    private List<Connection> tickingConnections;
    private Graph connections;
    private ResourceCount resources;

    private GameManager() {
        this.map = new Map(width, height, ResourceFrequency, ResourceOctaves, ResourceSeed);
        this.cities = new List<City>();
        this.trains = new List<Train>();
        this.tickingConnections = new List<Connection>();
        this.connections = new Graph(width + 1, height + 1);
        this.resources = startResources;
    }

    public void Update()
    {
        // Call Tick() on all Ticking objects 
        foreach(Train train in trains) {
            train.Tick();
        }
        foreach(Connection con in tickingConnections)
        {
            con.Tick();
        }
        tickingConnections = new List<Connection>();
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

    public static GameManager Instance
    {
        get
        {
            return instance == null ? new GameManager() : instance;
        }
    }
}
