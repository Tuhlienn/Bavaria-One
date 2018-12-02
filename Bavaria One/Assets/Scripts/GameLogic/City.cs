using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class City {
	public Vector2 position;
	public ResourceCount production;
    public Queue<Vector2> path;
    public int upgradeLevel;
    public string cityName;

    public City (Vector2 position, Graph graph, Map map, string name) {
        this.position = position;
        this.production = new ResourceCount(0, 0, 0, 0, 0);
        this.path = new Queue<Vector2>();
        this.upgradeLevel = 1;
        CalculatePaths(graph);
        CalculateProduction(map);
        cityName = name;
    }

    private void CalculateProduction(Map map) {
        int width = GameManager.Instance.width;
        int height = GameManager.Instance.height;

        for (int i = -1; i <= 0; i++) {
            for (int j = -1; j <= 0; j++) {
                Vector2 vec = position + new Vector2(i, j);
                if (vec.x < -(width / 2) || vec.x >= (width / 2) || vec.y < -(width / 2) || vec.y >= height / 2)
                    continue;
                production += map.tiles[(int)vec.x + GameManager.Instance.width / 2, (int)vec.y + GameManager.Instance.height / 2].resource;
            }
        }
    }

    public void CalculatePaths(Graph graph) {
        path = graph.ToStammstrecke(position);
    }
}
