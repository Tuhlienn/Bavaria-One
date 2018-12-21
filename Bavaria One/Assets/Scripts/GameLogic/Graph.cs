using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Linq;

public class Graph
{
    List<Connection> connections;
    private delegate bool IsGoal(Vector2 position);

    public Graph () 
    {
        connections = new List<Connection>();
    }

    /*
     *  Returns path from this node to Stammstrecke
     */
    public Queue<Vector2> ToStammstrecke(Vector2 from) 
    {
        return BFS(from, IsStammstrecke);
    }

    /*
     * Returns connection between two nodes
     */
    public Connection ConnectionAt(Vector2 left, Vector2 right) 
    {
        foreach(Connection con in connections)
        {
            if( (con.left == left && con.right == right) || (con.right == left && con.left == right))
            {
                return con;
            }
        }
        return null;
    }

    private List<Connection> ConnectionsWith(Vector2 vec)
    {
        List<Connection> cons = new List<Connection>();
        foreach(Connection con in connections)
        {
            if (con.left == vec || con.right == vec)
                cons.Add(con);
        }

        return cons;
    }

    public List<Vector2> AStar (Vector2 left, Vector2 right)
    {        
        Coord start = new Coord(left);
        Coord goal = new Coord(right);
        Coord current;

        PriorityList<Coord> frontier = new PriorityList<Coord>();
		Dictionary<Coord, Coord> cameFrom = new Dictionary<Coord, Coord>();
		Dictionary<Coord, float> costSoFar = new Dictionary<Coord, float>();

        cameFrom.Add(start, start);
		costSoFar.Add(start, 0.0f);

		frontier.Enqueue(start, 0.0f);

		while (!frontier.isEmpty()) {
			current = frontier.Dequeue();

			if (current.Equals(goal)) 
            {
                //Create result
				List<Vector2> path = new List<Vector2>();

                current = goal;
                path.Add(current.toVector2());
                while (!start.Equals(current))
                {
                    current = cameFrom[current];
                    path.Add(current.toVector2());
                }
                return path;
			}

			foreach (Coord next in getNeighbours(current)) 
            {
                var connection = ConnectionAt(current.toVector2(), next.toVector2());
				float newCost = costSoFar[current] + (connection == null ? 1 : 0.9f);

				if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next]) 
                {
					costSoFar.Remove(next);
					costSoFar.Add(next, newCost);
					float priority = newCost + next.manhattanDistance(goal);				
					frontier.Enqueue(next, priority);
					cameFrom.Remove(next);
					cameFrom.Add(next, current);
				}
			}
		}

        return null;
	}

    List<Coord> getNeighbours(Coord center)
    {
		int x = center.x;
		int y = center.y;
		return new List<Coord> () 
        {
            new Coord(x - 1, y),
            new Coord(x, y - 1),
            new Coord(x + 1, y),
            new Coord(x, y + 1)
        };
	}

    /*
     * Breadth First Search along Connections
     */
    private Queue<Vector2> BFS(Vector2 from, IsGoal isGoal)
    {
        Queue<Vector2> frontier = new Queue<Vector2>();
        HashSet<Vector2> set = new HashSet<Vector2>();
        Dictionary<Vector2, Vector2> dict = new Dictionary<Vector2, Vector2>();
        dict.Add(from, Vector2.one * int.MinValue);
        Vector2 curr;

        frontier.Enqueue(from);
        while (frontier.Count != 0)
        {
            curr = frontier.Dequeue();

            if (isGoal(curr)) 
            {
                return ConstructPath(curr, dict);
            }

            foreach(Connection con in ConnectionsWith(curr))
            {
                Vector2 vec = con.left == curr ? con.right : con.left;

                if (set.Contains(vec)) continue;

                if (!frontier.Contains(vec))
                    {
                        dict[vec] = curr;
                        frontier.Enqueue(vec);
                    }

            }
            set.Add(curr);
        } 
        return null;
    }

    /*
     * Constucts path from BFS result
     */
    private Queue<Vector2> ConstructPath(Vector2 state, Dictionary<Vector2, Vector2> dict) 
    {
        Queue<Vector2> queue = new Queue<Vector2>();
        do
        {
            queue.Enqueue(state);
            dict.TryGetValue(state, out state);
        } 
        while (state != Vector2.one * int.MinValue);

        if (queue.Count == 0) return null;
        return queue;
    }

    /*
     * Returns true if this node is connected to a Stammstrecke edge
     */ 
    private bool IsStammstrecke(Vector2 position)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Connection con = ConnectionAt(position, position + new Vector2(i, j));
                if (con != null && con.isStammstrecke)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /*
     *  Connect nodes (Bidirectional)
     */
    public bool Connect(Vector2 first, Vector2 second, bool isStammstrecke, int upgradeLevel) 
    {
        foreach(Connection con in connections)
        {
            if ((con.left == first && con.right == second) || (con.left == second && con.right == first))
                return false;
        }
        connections.Add(new Connection(isStammstrecke, upgradeLevel, first, second));
        return true;
    }
}