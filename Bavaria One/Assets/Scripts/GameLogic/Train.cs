﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Train : Ticking{
    Vector2 position;
    ResourceCount load;
    City myCity;
    private Queue<Vector2> queue;

    public Train (Vector2 position, City myCity) {
        this.position = position;
        this.myCity = myCity;
    }

    override
    public void Tick()
    {
        if(queue.Count == 0) {
            queue.Concat(myCity.path);
            if (position.Equals(myCity.position)) {
                load = myCity.upgradeLevel * myCity.production;
            }
            else {
                GameManager.Instance.resources += load;
                load = 0 * load;
                queue.Reverse();
            }
        }
        position = queue.Dequeue();
    } 
}
