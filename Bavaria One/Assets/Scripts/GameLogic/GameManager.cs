using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    private static GameManager instance;
    Map map;
    List<City> cities;
    List<Train> trains;
    Graph connections;
    ResourceCount resources;

    public static GameManager Instance(){
        return instance == null ? new GameManager() : instance;
    }

    public void Update()
    {
        // Call Tick() on all Ticking objects 
        foreach(Train train in trains) {
            train.Tick();
        }
    }
}
