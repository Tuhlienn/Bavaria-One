using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : Ticking{
    Vector2 position;
    ResourceCount load;
    City myCity;

    override
    public void Tick()
    {
        // TODO - Move in step, check if at destination the unload and back to city
    } 
}
