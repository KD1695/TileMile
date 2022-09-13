using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Movement
{
    public Vector2 previousPosition;
    public Vector2 newPosition;
    public MoveDirection direction;
};

public enum MoveDirection
{
    Up,
    Down,
    Right,
    None
}

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    [SerializeField] public int pathLength = 10;
    [SerializeField] public int pathWidth = 3;

    public List<List<TileType>> tiles = new List<List<TileType>>();
    private Vector2 playerPosition = new Vector2(-1, 0);
    private bool jumpEnabled = false;

    void Awake()
    {
        Instance = this;
    }

    public void SetPlayerPosition(Vector2 position)
    {
        playerPosition = position;
    }

    public Vector2 GetPlayerPosition()
    {
        return playerPosition;
    }

    public Movement CheckMovementResult(Movement movement)
    {
        
        return new Movement { previousPosition = movement.previousPosition, direction = MoveDirection.None };
    }
}
