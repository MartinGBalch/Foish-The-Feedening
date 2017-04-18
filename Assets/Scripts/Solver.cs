using System;
using System.Collections.Generic;
using System.Text;

namespace GraphProject
{
    class Solver<T>
    {
        class Meta
        {
            public enum VisitState { undiscovered, frontier, explored};
            public VisitState state;
            public Graph<T>.Node prev;
            public float g;
            public float h;
            public float f { get { return g + h; } }
            public uint depth;
            public Meta()
            {
                g = 0;
                h = 0;
                depth = 0;
                state = VisitState.undiscovered;
                
            }
        };

        public Graph<T> graph;

        private Meta[] metadata;
        private Stack<Graph<T>.Node> frontier;



        private T start,goal;
        private float threshold;
        private Graph<T>.FindDelegate searcher;
        private Graph<T>.Node goalNode;

        public List<T> solution
        {
            get
            {
                List<T> retval = new List<T>();
                retval.Add(goal);
                var n = goalNode;
                while(n!= null)
                {
                    retval.Add(n.data);
                    n = metadata[n.uid].prev;
                }
                retval.Add(start);
                retval.Reverse();
                return retval;
            }
        }

        private List<Graph<T>.Node> Qfrontier;
        //cleanup, setup and start our seach
        public void init(T a_start,T a_goal ,Graph<T>.FindDelegate a_searcher, float a_search_threshold = 0.0001f)
        {
            start = a_start;
            goal = a_goal;
            searcher = a_searcher;
            threshold = a_search_threshold;
            goalNode = graph.FindNode(goal, searcher, threshold);

            metadata = new Meta[graph.nodes.Count];
            frontier = new Stack<Graph<T>.Node>();

            var snode = graph.FindNode(start, searcher, threshold);
            for (int i = 0; i < metadata.Length; ++i)
                metadata[i] = new Meta();

            metadata[snode.uid].state = Meta.VisitState.frontier;
            frontier.Push(snode);
        }
        // represent 1 loop of the search
            // pop a node from the stack
            // print the nodes ID
            //set the nodes metadata to explored
            // add all of the nodes undiscovered neighbors to the stack
        public bool step()
        {
            
            var current = frontier.Pop();
            metadata[current.uid].state = Meta.VisitState.explored;
        

            // stop if we've reached the goal
            if(current.uid == goalNode.uid)
            {
                
                return false;
            }


            for(int i =0; i < current.edges.Count; i++)
            {
                if (metadata[current.edges[i].end.uid].state == Meta.VisitState.undiscovered)
                {
                    //update the previous
                    metadata[current.edges[i].end.uid].state = Meta.VisitState.frontier;
                    metadata[current.edges[i].end.uid].prev = current;
                    frontier.Push(current.edges[i].end);
                }
             
            }




            return frontier.Count != 0;
        }

        public void Qinit(T a_start, T a_goal, Graph<T>.FindDelegate a_searcher, float a_search_threshold = 0.0001f)
        {
            start = a_start;
            goal = a_goal;
            searcher = a_searcher;
            threshold = a_search_threshold;
            goalNode = graph.FindNode(goal, searcher, threshold);

            metadata = new Meta[graph.nodes.Count];
            Qfrontier = new List<Graph<T>.Node>();

            var snode = graph.FindNode(start, searcher, threshold);
            for (int i = 0; i < metadata.Length; ++i)
                 metadata[i] = new Meta();
            
                

            metadata[snode.uid].state = Meta.VisitState.frontier;
            Qfrontier.Add(snode);
        }

        private int dijkstra(Graph<T>.Node a, Graph<T>.Node b)
        {

            if (metadata[a.uid].g < metadata[b.uid].g)
            {
                return -1;
            }
            if (metadata[a.uid].g > metadata[b.uid].g)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private int astar(Graph<T>.Node a, Graph<T>.Node b)
        {
          
            if (metadata[a.uid].f < metadata[b.uid].f)
            {
                return -1;
            }
            if (metadata[a.uid].f > metadata[b.uid].f)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public bool Qstep()
        {


            Qfrontier.Sort(dijkstra);
            var current = Qfrontier[0];
                Qfrontier.RemoveAt(0);
            metadata[current.uid].state = Meta.VisitState.explored;
           

            if (current.uid == goalNode.uid)
            {
                
                return false;
            }


            for (int i = 0; i < current.edges.Count; i++)
            {
                float g = current.edges[i].weight + metadata[current.uid].g;
                uint d = 1 + metadata[current.uid].depth;

                if (metadata[current.edges[i].end.uid].state == Meta.VisitState.undiscovered)
                {
                    metadata[current.edges[i].end.uid].state = Meta.VisitState.frontier;
                    Qfrontier.Add(current.edges[i].end);
                    metadata[current.edges[i].end.uid].h = searcher(current.edges[i].end.data, goalNode.data);
                }
                if(metadata[current.edges[i].end.uid].state == Meta.VisitState.frontier)
                {
                    if(g<metadata[current.edges[i].end.uid].g || metadata[current.edges[i].end.uid].prev == null)
                    {
                        metadata[current.edges[i].end.uid].prev = current;
                        metadata[current.edges[i].end.uid].g = g;
                        metadata[current.edges[i].end.uid].depth = d;
                    }
                }

            }




            return Qfrontier.Count !=0;
        }





        // Bookkeeping about our graph
        // List of visited nodes
        // Keeptrack of frontier
        //could be a stack or a queue
        // Driver to actually perform the traversal 



    }
}
