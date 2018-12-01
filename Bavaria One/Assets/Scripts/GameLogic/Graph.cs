using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class Graph
{
    public static Connection NO_CONNECTION = new Connection(false, false, 0);

    Connection[,] connections;
    int width, height;
    private delegate bool IsGoal(Vector2 position);

    public Graph (int width, int height) {
        this.width = width;
        this.height = height;
        connections = new Connection[width * height, width * height];
    }

    public Queue<Vector2> ToStammstrecke(Vector2 from) {
        return BFS(from, IsStammstrecke);
    }

    private Connection ConnectionAt(Vector2 firstPos, Vector2 secondPos) 
    {
        int first = (int)(width * (int) firstPos.y) + ((int) firstPos.x % width);
        int second= (int)(width * (int) secondPos.y) + ((int) secondPos.x % width);
        return connections[first, second];
    }

    private Queue<Vector2> BFS (Vector2 from, IsGoal isGoal){

        Queue<Vector2> frontier = new Queue<Vector2>();
        HashSet<Vector2> set = new HashSet<Vector2>();
        Dictionary<Vector2, Vector2> dict = new Dictionary<Vector2, Vector2>();
        dict.Add(from, Vector2.one * -1);

        frontier.Enqueue(from);
        while (frontier.Count != 0)
        {
            int fromIndex = (int)(width * (int)from.y) + ((int)from.x % width);

            if(isGoal(from)) {
                return ConstructPath(from, dict);
            }

            for (int i = 0; i < connections.GetLength(fromIndex); i++)
            {
                if (!connections[fromIndex, i].Equals(NO_CONNECTION))
                {
                    Vector2 vec = new Vector2(i % width, (float)(i / width));
                    if (!set.Contains(vec))
                    {
                        dict.Add(vec, from);
                        frontier.Enqueue(vec);
                    }
                }
            }
            set.Add(from);
            from = frontier.Dequeue();
        } 

        return null;
    }

    private Queue<Vector2> ConstructPath(Vector2 state ,Dictionary<Vector2, Vector2> dict) {
        Queue<Vector2> queue = new Queue<Vector2>();
        do
        {
            queue.Enqueue(state);
            dict.TryGetValue(state, out state);
        } while (state != Vector2.one * -1);
        return queue;
    }

    private bool IsStammstrecke(Vector2 position)
    {
        for (int i = 0; i < height; i++)
        {
            if (ConnectionAt(position, new Vector2(position.x, (float)i)).isStammstrecke)
            {
                return true;
            }
        }
        return false;
    }
}