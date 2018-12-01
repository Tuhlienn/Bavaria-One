using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class City {
	public Vector2 position;
	public ResourceCount production;
    public Queue<Vector2> path;
    public int upgradeLevel;

    public City (Vector2 position, Graph graph, Map map) {
        this.position = position;
        CalculatePaths(graph);
        CalculateProduction(map);
    }

    private void CalculateProduction(Map map) {
        for (int i = -1; i <= 0; i++) {
            for (int j = -1; j <= 0; j++) {
                Vector2 vec = position + new Vector2(i, j);
                production += map.tiles[(int)vec.x, (int)vec.y].resource;
            }
        }
    }

    private void CalculatePaths(Graph graph) {
        path = graph.ToStammstrecke(position);
    }
}
