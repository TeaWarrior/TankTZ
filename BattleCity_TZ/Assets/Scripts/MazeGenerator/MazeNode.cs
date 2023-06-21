using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeState
{
    Available,
    Current,
    Completed
}
public class MazeNode : MonoBehaviour
{

    [SerializeField] GameObject[] walls;
    [SerializeField] SpriteRenderer floor;

    public void SetState(NodeState state)
    {
        switch (state) {
            case NodeState.Available:
                floor.color = Color.white;
                break;

            case NodeState.Current:
                floor.color = Color.yellow;
                break;
            case NodeState.Completed:
                floor.color = Color.blue;
                break;


        }
    }

    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false);
    }
}
