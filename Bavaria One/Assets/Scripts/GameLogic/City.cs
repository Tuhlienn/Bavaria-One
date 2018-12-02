﻿using System.Collections;
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
                if (i < 0 || i >= width || j < 0 || j >= height)
                    continue;
               // production += map.tiles[(int)vec.x, (int)vec.y].resource;
            }
        }
    }

    public void CalculatePaths(Graph graph) {
        path = graph.ToStammstrecke(position);
    }
}
