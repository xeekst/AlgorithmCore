﻿using System;
using System.Collections.Generic;

namespace ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {

            EdgeWeightedDigraph g = new EdgeWeightedDigraph(8);
            g.AddEdge(new DirectedEdge(4, 5, 0.35));
            g.AddEdge(new DirectedEdge(5, 4, 0.35));
            g.AddEdge(new DirectedEdge(4, 7, 0.37));
            g.AddEdge(new DirectedEdge(5, 7, 0.28));
            g.AddEdge(new DirectedEdge(7, 5, 0.28));
            g.AddEdge(new DirectedEdge(5, 1, 0.32));
            g.AddEdge(new DirectedEdge(0, 4, 0.38));
            g.AddEdge(new DirectedEdge(0, 2, 0.26));
            g.AddEdge(new DirectedEdge(7, 3, 0.39));
            g.AddEdge(new DirectedEdge(1, 3, 0.29));
            g.AddEdge(new DirectedEdge(2, 7, 0.34));
            g.AddEdge(new DirectedEdge(6, 2, 0.40));
            g.AddEdge(new DirectedEdge(3, 6, 0.52));
            g.AddEdge(new DirectedEdge(6, 0, 0.58));
            g.AddEdge(new DirectedEdge(6, 4, 0.93));

            DijkstraSP dsp = new DijkstraSP(g, 0);
            Console.WriteLine("DijkstraSP");
            foreach (var e in dsp.PathTo(6))
            {
                Console.WriteLine($"{e} ");
            }

            var edges = new List<string>(){
                "41 1 7 9",
                "51 2",
                "50",
                "36",
                "38",
                "45",
                "21 3 8",
                "32 3 8",
                "32 2",
                "29 4 6"
            };
            var cpm = new CPM(10, edges);

            var dedges = new List<string>(){
                "4 5  0.35",
                "5 4 -0.66",
                "4 7  0.37",
                "5 7  0.28",
                "7 5  0.28",
                "5 1  0.32",
                "0 4  0.38",
                "0 2  0.26",
                "7 3  0.39",
                "1 3  0.29",
                "2 7  0.34",
                "6 2  0.40",
                "3 6  0.52",
                "6 0  0.58",
                "6 4  0.93"
            };
            int N = 8;
            EdgeWeightedDigraph spt = new EdgeWeightedDigraph(N);
            for (int i = 0; i < N; i++)
            {
                var e = dedges[i].Split(' ');
                double duration = Convert.ToDouble(e[e.Length-1]);
                int v = Convert.ToInt32(e[0]);
                int w = Convert.ToInt32(e[1]);
                spt.AddEdge(new DirectedEdge(v, w, duration));
            }

            BellmanFordSP bsp = new BellmanFordSP(spt,0);

            Console.WriteLine(bsp.HasNegativeCycle());
            Console.WriteLine("Hello World!");
        }
    }
}
