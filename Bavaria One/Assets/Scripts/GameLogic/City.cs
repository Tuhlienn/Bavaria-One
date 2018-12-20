using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class City 
{
	public Vector2 position;
	public ResourceCount production;
    public Queue<Vector2> path;
    public int UpgradeLevel { set; get; }
    public int UpgradeCost { get { return UpgradeLevel + 2; } }
    public string cityName;

    public City (Vector2 position, Graph graph, Map map, string name) 
    {
        this.position = position;
        this.path = new Queue<Vector2>();
        this.UpgradeLevel = 1;
        CalculatePaths(graph);
        CalculateProduction(map);
        cityName = name;
    }

    private void CalculateProduction(Map map) 
    {
        production = new ResourceCount(0, 0, 0, 0, 0);
        int width = GameManager.Instance.width;
        int height = GameManager.Instance.height;

        for (int i = -1; i <= 0; i++) 
        {
            for (int j = -1; j <= 0; j++) 
            {
                Vector2 vec = position + new Vector2(i, j);
                if (vec.x < -(width / 2) || vec.x >= (width / 2) || vec.y < -(width / 2) || vec.y >= height / 2)
                {
                    continue;
                }
                production += map.tiles[(int)vec.x + width / 2, (int)vec.y + height / 2].resource;
            }
        }
    }

    public bool CalculatePaths(Graph graph) 
    {
        var temp = graph.ToStammstrecke(position);
        if(temp != null && temp != path)
        {
            path = temp;
            return true;
        }
        
        return false;
    }
}
