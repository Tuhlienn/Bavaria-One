using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Train : Ticking
{
    Vector2 position;
    Vector2 nextPosition;
    ResourceCount load;
    City myCity;
    private Queue<Vector2> queue;
    TrainObject modelTrain;
    public GameObject trainPrefab;

    public Train (Vector2 position, City myCity) 
    {
        this.position = position;
        this.nextPosition = position;
        this.myCity = myCity;
        queue = new Queue<Vector2>();
        Vector3 pos = new Vector3(myCity.position.x, 0.0f, myCity.position.y);
        modelTrain = GameObject.Instantiate(GameManager.Instance.TrainPrefab, pos, Quaternion.identity).transform.GetComponent<TrainObject>();
    }

    override public void Tick()
    {
        if (myCity.path != null && myCity.path.Count == 1)
        {
            modelTrain.Hide();
            return;
        }

        if(queue.Count == 0) {
            modelTrain.Hide();
            queue = new Queue<Vector2>(myCity.path);
            if (position == myCity.position) 
            {
                load = myCity.production;
                load += 0.25f * myCity.production.MultiResources;
                load *= myCity.UpgradeLevel;
                queue = new Queue<Vector2>(queue.Reverse());
                modelTrain.PackUp();
            }
            else 
            {
                GameManager.Instance.Resources += load;
                load = 0 * load;
                modelTrain.Unload();
            }
        }
        if(queue.Count > 0) 
        {
            Vector2 newPosition = queue.Dequeue();
            GameManager.addConnection(GameManager.Instance.Connections.ConnectionAt(position, newPosition));
            if(queue.Count > 0)
            {
                nextPosition = queue.Peek();
                modelTrain.SetTarget(nextPosition);
            }
            position = newPosition;
        }
    }
}
