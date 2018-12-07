using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Train : Ticking
{
    Vector2 position;
    ResourceCount load;
    City myCity;
    private Queue<Vector2> queue;

    public Train (Vector2 position, City myCity) 
    {
        this.position = position;
        this.myCity = myCity;
        queue = new Queue<Vector2>();
    }

    override public void Tick()
    {
        if (myCity.path == null) return;

        if(queue.Count == 0) 
        {
            queue = new Queue<Vector2>(myCity.path);
            if (position == myCity.position) 
            {
                load = myCity.upgradeLevel * myCity.production;
                load = load + 0.25f * myCity.upgradeLevel * myCity.production.MultiResources();
                queue = new Queue<Vector2>(queue.Reverse());
            }
            else 
            {
                GameManager.Instance.Resources += load;
                load = 0 * load;
            }
        }
        if(queue.Count > 0) 
        {
            Vector2 NewPosition = queue.Dequeue();
            GameManager Instance = GameManager.Instance;
            GameManager.addConnection(Instance.Connections.ConnectionAt(position, NewPosition));
            position = NewPosition;
        }
    } 
}
