using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public class Node
    {
        public float g;
        public float h;
        public float f;
    }
    Node start;
    public List<Node> openList;
    public List<Node> closedList;
    private void Start()
    {
        openList = new List<Node>();
        closedList = new List<Node>();
        start = new Node();
        start.g = 0;
        start.h = 0;
        start.f = start.g + start.h;
    }
    private void LookForPath(Vector2 PlayerPos)
    {

    }
}
