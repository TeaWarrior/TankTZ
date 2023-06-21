using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MazeGenerator : MonoBehaviour
{

    public static MazeGenerator instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More Than One Instance");
            return;
        }
        instance = this;
    }
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int nodeSize;
    [SerializeField] GameObject Player;

    [SerializeField] GameObject enemy;
    public Vector3 PlayerSpawnPoint;

    public GameObject playerBase;

    public List<Transform> spawnPoints;
    [SerializeField] int spawnPointAmount;
    
  //  float nodePAthSize;
    private void Start()
    {

        // StartCoroutine(GenerateMaze(nodeSize));
        GenerateMaze(nodeSize);

    }

    void SetPathfinerSize()
    {
        var gg = AstarPath.active.data.gridGraph;
        var width = nodeSize.x*4;
        var depth = nodeSize.y*4;
        var nodePAthSize = 0.25f;

        gg.SetDimensions(width, depth, nodePAthSize);

        // Recalculate the graph

        AstarPath.active.Scan();
       // enemy.SetActive(true);
        Debug.Log("DonePath");
    }



   public void GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x * 0.5f), y - (size.y * 0.5f));
                MazeNode mazeNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(mazeNode);
                
            }
        }
        int playerBaseIndex = Random.Range(0, nodes.Count);
        playerBase.transform.position = nodes[playerBaseIndex].transform.position;

       
       


        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        //Start Node;

        int startNodeIndex = Random.Range(0, nodes.Count);
        if (startNodeIndex == playerBaseIndex)
        {
            while (startNodeIndex == playerBaseIndex)
            {
                startNodeIndex = Random.Range(0, nodes.Count);
            }
        }
        currentPath.Add(nodes[startNodeIndex]);
        currentPath[0].SetState(NodeState.Current);
        Player.transform.position = currentPath[0].transform.position;
        PlayerSpawnPoint = Player.transform.position;

        for (int i = 0; i < spawnPointAmount; i++)
        {
            int spawnPointIndex = Random.Range(0, nodes.Count);
            if (spawnPointIndex == playerBaseIndex || spawnPointIndex== startNodeIndex)
            {
                while(spawnPointIndex == playerBaseIndex || spawnPointIndex == startNodeIndex)
                {
                    spawnPointIndex = Random.Range(0, nodes.Count);
                }
            }
            spawnPoints.Add(nodes[Random.Range(0, nodes.Count)].transform);
        }

        while (completedNodes.Count <nodes.Count)
        {
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count -1]);

            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if( currentNodeX < size.x - 1)
            {

                //Check node to the right
                if(!completedNodes.Contains(nodes[currentNodeIndex +size.y]) && 
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))

                {


                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }

                
            }

            if(currentNodeX > 0)
            {

                //Check node to the left
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                     !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }

            }

            if(currentNodeY <size.y - 1)
            {
                //Check node Above
                if(!completedNodes.Contains(nodes[currentNodeIndex+1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            if (currentNodeY > 0)
            {
                //check node below
                if(!completedNodes.Contains(nodes[currentNodeIndex -1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex -1]))
                {

                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                    
                }
            }

            //Chose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode =nodes[ possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
              
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            
        }

        Debug.Log("Done");
        SetPathfinerSize();

    }
}
