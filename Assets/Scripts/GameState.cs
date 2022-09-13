using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

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

    [SerializeField] TextMeshProUGUI winText;

    public List<List<TileType>> tiles = new List<List<TileType>>();
    private Vector2 playerPosition = new Vector2(-1, 1);
    public bool jumpEnabled = false;

    List<Movement> actionStack = new List<Movement>();

    void Awake()
    {
        Instance = this;
    }

    public void SetPlayerPosition(Vector2 _position)
    {
        playerPosition = _position;
    }

    public void PushActionToStack(Movement movement)
    {
        actionStack.Add(movement);
    }

    public Movement PopActionStack()
    {
        if (actionStack.Count == 0)
            return new Movement();
        Movement movement = actionStack.Last();
        actionStack.Remove(movement);
        return movement;
    }

    public Vector2 GetPlayerPosition()
    {
        return playerPosition;
    }

    public Movement CheckMovementResult(Movement movement)
    {
        if (movement.newPosition.x == -1)
            return movement;
        if(movement.newPosition.x < pathLength && movement.newPosition.y < pathWidth)
        {
            switch(tiles[(int)movement.newPosition.y][(int)movement.newPosition.x])
            {
                case TileType.None:
                    jumpEnabled = false;
                    return new Movement
                    {
                        previousPosition = movement.previousPosition,
                        newPosition = movement.previousPosition,
                        direction = MoveDirection.None
                    };
                case TileType.Normal:
                    jumpEnabled = false;
                    return movement;
                case TileType.Slide:
                    jumpEnabled = false;
                    switch (movement.direction)
                    {
                        case MoveDirection.Up:
                            Vector2 _new = CheckMovementResult(new Movement
                            {
                                previousPosition = movement.newPosition,
                                newPosition = movement.newPosition + new Vector2(0, 1),
                                direction = movement.direction
                            }).newPosition;
                            return new Movement
                            {
                                previousPosition = movement.previousPosition,
                                newPosition = _new,
                                direction = movement.direction
                            };
                        case MoveDirection.Down:
                            _new = CheckMovementResult(new Movement
                            {
                                previousPosition = movement.newPosition,
                                newPosition = movement.newPosition + new Vector2(0, -1),
                                direction = movement.direction
                            }).newPosition;
                            return new Movement
                            {
                                previousPosition = movement.previousPosition,
                                newPosition = _new,
                                direction = movement.direction
                            };
                        case MoveDirection.Right:
                            _new = CheckMovementResult(new Movement
                            {
                                previousPosition = movement.newPosition,
                                newPosition = movement.newPosition + new Vector2(1, 0),
                                direction = movement.direction
                            }).newPosition;
                            return new Movement
                            {
                                previousPosition = movement.previousPosition,
                                newPosition = _new,
                                direction = movement.direction
                            };
                    }
                    return movement;
                case TileType.Jump:
                    jumpEnabled = true;
                    return movement;
            }
        }
        else if(movement.newPosition.x >= pathLength)
        {
            //win
            winText.enabled = true;
        }
        else if(movement.newPosition.y >= pathWidth || movement.newPosition.y < 0)
        {
            return new Movement { previousPosition = movement.previousPosition, direction = MoveDirection.None, newPosition = new Vector2(movement.previousPosition.x, (movement.newPosition.y >= pathWidth) ? pathWidth-1 : 0) };
        }
        return new Movement { previousPosition = movement.previousPosition, direction = MoveDirection.None, newPosition = movement.previousPosition };
    }
}
