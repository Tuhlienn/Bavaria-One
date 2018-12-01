using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class GameManager : MonoBehaviour{
    private static GameManager instance;
    public static GameManager Instance{
        get {
            return instance == null ? new GameManager() : instance;
        }
    }
    Map map;
    List<City> cities;
    List<Train> trains;
    Graph connections;
    public ResourceCount resources;


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
}
