using System;
using System.Collections.Generic;
using Priority_Queue;

public class PQNode : FastPriorityQueueNode
{
    public (int, int) Node;

    public PQNode((int, int) Node)
    {
        this.Node = Node;
    }

    public PQNode(int x, int y)
    {
        this.Node = (x, y);
    }
}

class Astar : IRouteFinder
{
    int mapSize;
    (int, int) startLocation;
    (int, int) endLocation;
    FastPriorityQueue<PQNode> openSet;
    (int, int)[,] cameFrom;
    float[,] gScore;
    float[,] fScore;
    float d = 1;
    float dDiagonal = 1.414f;
    List<(int, int)> blocks;

    public List<(int, int)> CalculateRoute(int mapSize, (int, int) startLocation, (int, int) endLocation, List<(int, int)> blocks)
    {
        this.mapSize = mapSize;
        this.startLocation = startLocation;
        this.endLocation = endLocation;
        this.blocks = blocks;

        openSet = new FastPriorityQueue<PQNode>(mapSize * mapSize);

        (int, int) current;
        cameFrom = new (int, int)[mapSize, mapSize];
        gScore = new float[mapSize, mapSize];
        fScore = new float[mapSize, mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                fScore[i, j] = float.MaxValue;
                gScore[i, j] = float.MaxValue;
                cameFrom[i, j] = (-1, -1);
            }
        }
        gScore[startLocation.Item1, startLocation.Item2] = 0;
        fScore[startLocation.Item1, startLocation.Item2] = h(startLocation);
        openSet.Clear();
        openSet.Enqueue(new PQNode(startLocation), 0);

        while (openSet.Count != 0)
        {
            current = openSet.Dequeue().Node;
            if (current == endLocation)
            {
                return ReconstructPath();
            }

            Neighbour(current.Item1 - 1, current.Item2 - 1, dDiagonal, current);
            Neighbour(current.Item1 - 1, current.Item2, d, current);
            Neighbour(current.Item1 - 1, current.Item2 + 1, dDiagonal, current);

            Neighbour(current.Item1, current.Item2 - 1, d, current);
            Neighbour(current.Item1, current.Item2 + 1, d, current);

            Neighbour(current.Item1 + 1, current.Item2 - 1, dDiagonal, current);
            Neighbour(current.Item1 + 1, current.Item2, d, current);
            Neighbour(current.Item1 + 1, current.Item2 + 1, dDiagonal, current);
        }

        return new List<(int, int)>(); //empty route, if goal was never reached
    }

    public virtual float h((int, int) node)
    {
        float dx = Math.Abs(node.Item1 - endLocation.Item1);
        float dy = Math.Abs(node.Item2 - endLocation.Item2);
        return 1 * (dx + dy) + (1.414f - 2 * 1) * Math.Min(dx, dy); //octile distance
    }

    public void Neighbour(int x, int y, float alt, (int, int) root)
    {
        if ((x < 0 || x > mapSize - 1 || y < 0 || y > mapSize - 1))
        {
            return;
        }

        if (blocks.Contains((x, y)))
        {
            return;
        }

        float tentative_gScore = gScore[root.Item1, root.Item2] + alt;
        if (tentative_gScore < gScore[x, y])
        {
            cameFrom[x, y] = root;
            gScore[x, y] = tentative_gScore;
            fScore[x, y] = gScore[x, y] + h((x, y));
            if (!openSet.Contains(new PQNode(x, y)))
            {
                openSet.Enqueue(new PQNode(x, y), fScore[x, y]);
            }
        }
    }

    List<(int, int)> ReconstructPath()
    {
        List<(int, int)> route = new List<(int, int)>();
        (int, int) nextLocation = cameFrom[endLocation.Item1, endLocation.Item2];
        while (nextLocation != startLocation)
        {
            route.Add(nextLocation);

            nextLocation = cameFrom[nextLocation.Item1, nextLocation.Item2];
        }
        return route;
    }
}


