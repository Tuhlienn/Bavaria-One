using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class City {
	Vector2 position;
	ResourceCount production;
    public Queue<Vector2> path;
    int upgradeLevel;

    public City (Vector2 position, Graph graph, Map map) {
        this.position = position;
        CalculatePaths(graph);
        CalculateProduction(map);
    }

    private void CalculateProduction(Map map) {
        for (int i = -1; i <= 0; i++) {
            for (int j = -1; j <= 0; j++) {
                Vector2 vec = position + new Vector2(i, j);
                production.beer = map.tiles[(int)vec.x, (int)vec.y].resource.beer;
                production.money = map.tiles[(int)vec.x, (int)vec.y].resource.money;
                production.steel = map.tiles[(int)vec.x, (int)vec.y].resource.steel;
                production.concrete = map.tiles[(int)vec.x, (int)vec.y].resource.concrete;
                production.energy = map.tiles[(int)vec.x, (int)vec.y].resource.energy;
            }
        }
    }

    private void CalculatePaths(Graph graph) {
        path = graph.ToStammstrecke(position);
    }
}
