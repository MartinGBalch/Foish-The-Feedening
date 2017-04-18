using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace GraphProject
{
    public class Graph<T>
    {
      [DebuggerDisplay("id = {uid}, val = {data}")]
        public class Node
        {
            
            public Node(T a_data, uint a_id)
            {
                data = a_data;
                edges = new List<Edge>();
                uid = a_id;
            }
            public uint uid { private set; get; }
            public T data { private set; get; }
            public List<Edge> edges { private set; get; }
        }

        public class Edge
        {
            public Edge(Node a_start, Node a_end, float A_weight = 1)
            {
                start = a_start;
                end = a_end;
                weight = A_weight;
            }
        public Node start { private set; get; }
        public Node end { private set; get; }
        public float weight { private set; get; }
    }
        public List<Node> nodes { private set; get; }
        public List<Edge> edges { private set; get; }
        uint id_counter;
        
        public Graph()
        {
            nodes = new List<Node>();
            edges = new List<Edge>();
            id_counter = 0;
        }

       public Node AddNode(T a_data)
        {
            //TODO: try and ensure there are no duplicate referances
            Node n = new Node(a_data, id_counter);
            id_counter++;
            nodes.Add(n);
            return n;
        }
        public Edge addEdge(Node a_start, Node a_end, bool undirected, float A_weight)
        {
            if(!nodes.Contains(a_start) || !nodes.Contains(a_end))
            {
                return null;
            }

           Edge a = new Edge(a_start, a_end, A_weight);
            a_start.edges.Add(a);
            edges.Add(a);
            if(undirected)
            {
                Edge b = new Edge(a_end, a_start, A_weight);
                a_end.edges.Add(b);
                edges.Add(b);
            }
            return a;
        }
        public delegate float FindDelegate(T a, T b);


        public Edge addEdge(T a_start, T a_end, FindDelegate a_finder,float a_threshold=0.0001f, float a_weight = 1, bool undirected = true)
        {
            return addEdge(FindNode(a_start, a_finder, a_threshold),
                            FindNode(a_end, a_finder), undirected, a_weight);
        }

        public Node FindNode(T a_query, FindDelegate a_finder, float a_threshold = 0.0001f)
        {
            Node best = null;
            float min = 0;

            foreach(Node n in nodes)
            {
                float res = a_finder(a_query, n.data);
                if((best== null || res<min) && res < a_threshold)
                {
                    best = n;
                    min = res;
                }
            }
            return best;
        }
    }
}
