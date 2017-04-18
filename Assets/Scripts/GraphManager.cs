using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{

    private GraphProject.Graph<Transform> graph;
    private GraphProject.Solver<Transform> solver;

    public Transform pathStart, pathEnd;

    [HideInInspector]
    public List<Transform> path;


    public bool undirected = true;
    public bool generateGrid = true;

    public int rows;
    public int columns;
    public float spacing;

    public List<Transform> start, end;

    public bool StackOrQue = false;
    public bool genGridKeep = false;

    private static float diff(Transform a, Transform b)
    {
        return Vector3.Distance(a.position, b.position);
    }




    private void OnValidate()
    {
        if (!generateGrid) return;
        generateGrid = false;

        if (genGridKeep == false)
        {
            foreach (Transform child in transform)
            {
                UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(child.gameObject); };
            }

            start.Clear();
            end.Clear();
            Transform[][] t = new Transform[rows][];
            for (int i = 0; i < rows; ++i)
            {
                t[i] = new Transform[columns];
            }

            for (int i = 0; i < rows; i++)
            {
                Vector3 StartPoint = new Vector3(0, i * spacing, 0);
                for (int j = 0; j < columns; j++)
                {
                    StartPoint += new Vector3(spacing, 0, 0);
                    var a = new GameObject(GetInstanceID() + "_gwp_" + ((i * rows) + j));
                    a.transform.parent = transform;
                    a.transform.localPosition = StartPoint;
                    t[i][j] = a.transform;
                }
            }

            for (int i = 0; i < rows; i++)
            {
                //if (i != 0) { ChildNum += rows; }
                for (int j = 0; j < columns - 1; j++)
                {
                    start.Add(t[i][j]);
                    end.Add(t[i][j + 1]);
                }
            }
            for (int i = 0; i < rows - 1; i++)
            {
                //if (i != 0) { ChildNum += rows; }
                for (int j = 0; j < columns; j++)
                {
                    start.Add(t[i][j]);
                    end.Add(t[i + 1][j]);
                }
            }
            for (int i = 0; i < rows - 1; i++)
            {
                //if (i != 0) { ChildNum += rows; }
                for (int j = 0; j < columns - 1; j++)
                {
                    start.Add(t[i][j]);
                    end.Add(t[i + 1][j + 1]);
                }
            }
            for (int i = 0; i < rows - 1; i++)
            {
                //if (i != 0) { ChildNum += rows; }
                for (int j = columns - 1; j > 0; j--)
                {
                    start.Add(t[i][j]);
                    end.Add(t[i + 1][j - 1]);
                }
            }
        }


        genGridKeep = true;
        InitializeGraph();


        //if (StackOrQue == false)
        //{
        //    InitializeGraph();
        //    if (pathStart != null && pathEnd != null)
        //    {
        //        solver.Qinit(pathStart, pathEnd, diff, 100.0f);
        //        while (solver.Qstep()) ;
        //        path = solver.solution;
        //    }
        //}
        //else
        //{
        //    InitializeGraph();
        //    if (pathStart != null && pathEnd != null)
        //    {
        //        solver.init(pathStart, pathEnd, diff, 100.0f);
        //        while (solver.step()) ;
        //        path = solver.solution;
        //    }
        //}





    }
    public List<Vector3> FindPathBetween(Transform a, Transform b)
    {
        if (solver == null || graph == null)
            InitializeGraph();

        solver.Qinit(a, b, diff, 100.0f);
        while (solver.Qstep()) ;
        path = solver.solution;

        if (path == null)
            return null;

        List<Vector3> retval = new List<Vector3>();
        Transform source = path[0];
        retval.Add(source.position);
        for (int i = 1; i < path.Count; ++i)
        {
            //if (!ValidateEdge(source, path[i], source != path[0]) && path[i] != b)
            {
                source = path[i - 1];
                retval.Add(source.position);

            }          
        }
        retval.Add(path[path.Count - 1].position);

        return retval;
    }

    bool ValidateEdge(Transform start, Transform end, bool UseObstruction = true)
    {
        var diff = end.position - start.position;
        RaycastHit info;
        bool result = Physics.SphereCast(start.position, 0.1f, diff.normalized, out info, Vector3.Magnitude(diff));

        return (result || UseObstruction && IsObsrtructed(start) || UseObstruction && IsObsrtructed(end));
    }
    bool IsObsrtructed(Transform t)
    {
        return t == null || Physics.CheckSphere(t.position, 0.1f);
    }

    void InitializeGraph()
    {
        graph = new GraphProject.Graph<Transform>();
        solver = new GraphProject.Solver<Transform>();
        solver.graph = graph;

        HashSet<Transform> set = new HashSet<Transform>();

        set.UnionWith(start);
        set.UnionWith(end);

        set.RemoveWhere(IsObsrtructed);

        for (int i = 0; i < start.Count; ++i)
        {
            if (start[i] != null && end[i] != null && ValidateEdge(start[i], end[i]))
            {
                start[i] = null;
                end[i] = null;

            }
        }

        //for (int i = 0; i < start.Count; ++i)
        //{
        //    if (IsObsrtructed(start[i]))
        //    {
        //        start[i] = null;
        //    }
        //}
        //for (int i = 0; i < end.Count; ++i)
        //{
        //    if (IsObsrtructed(end[i]))
        //    {
        //        end[i] = null;
        //    }
        //}


        foreach (var trans in set)
        {
            if (trans != null)
                graph.AddNode(trans);
        }

        for (int i = 0; i < start.Count; ++i)
        {
            if (start[i] != null && end[i] != null)
                graph.addEdge(start[i], end[i], diff, 0.0001f, diff(start[i], end[i]));
        }



    }




    // Use this for initialization
    void Awake()
    {
        InitializeGraph();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            if (t != null)
                Gizmos.DrawWireSphere(t.position, .1f);
        }
        for (int i = 0; i < start.Count; ++i)
        {
            if (start[i] != null && end[i] != null)
            {
                Gizmos.DrawSphere(start[i].position, .2f);
                Gizmos.DrawSphere(end[i].position, .2f);
                Gizmos.DrawLine(start[i].position, end[i].position);
            }

        }
        //foreach(var t in path)
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireSphere(t.position, .3f);
        //}
    }

}
