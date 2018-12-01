using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class GameManager : MonoBehaviour{
    public int width, height;
    public ResourceCount startResources;

    private static GameManager instance;
    private Map map;
    private List<City> cities;
    private List<Train> trains;
    private Graph connections;
    private ResourceCount resources;

    private GameManager() {
        this.map = new Map(width, height);
        this.cities = new List<City>();
        this.trains = new List<Train>();
        this.connections = new Graph(width + 1, height + 1);
        this.resources = startResources;
    }

    public void Update()
    {
        // Call Tick() on all Ticking objects 
        foreach(Train train in trains) {
            train.Tick();
        }
    }

    public void addCity(Vector2 position) {
        addCity(new City(position, Instance.connections, Instance.map));
    }

    public void addCity(City city) {
        Instance.cities.Add(city);
    }

    public void addTrain(Train train) {
        Instance.trains.Add(train);
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
