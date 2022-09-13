using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement _movement = new Movement
        {
            previousPosition = GameState.Instance.GetPlayerPosition(),
            newPosition = GameState.Instance.GetPlayerPosition()
        };
        if(Input.GetKeyDown(KeyCode.W))
        {
            _movement = GameState.Instance.CheckMovementResult(new Movement
            {
                previousPosition = GameState.Instance.GetPlayerPosition(),
                newPosition = new Vector2(GameState.Instance.GetPlayerPosition().x, GameState.Instance.GetPlayerPosition().y - 1),
                direction = MoveDirection.Up
            });
            MoveCharacter(_movement, false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _movement = GameState.Instance.CheckMovementResult(new Movement
            {
                previousPosition = GameState.Instance.GetPlayerPosition(),
                newPosition = new Vector2(GameState.Instance.GetPlayerPosition().x, GameState.Instance.GetPlayerPosition().y + 1),
                direction = MoveDirection.Down
            });
            MoveCharacter(_movement, false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _movement = GameState.Instance.CheckMovementResult(new Movement
            {
                previousPosition = GameState.Instance.GetPlayerPosition(),
                newPosition = new Vector2(GameState.Instance.GetPlayerPosition().x + 1, GameState.Instance.GetPlayerPosition().y),
                direction = MoveDirection.Right
            });
            MoveCharacter(_movement, false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameState.Instance.jumpEnabled)
            {
                _movement = GameState.Instance.CheckMovementResult(new Movement
                {
                    previousPosition = GameState.Instance.GetPlayerPosition(),
                    newPosition = new Vector2(GameState.Instance.GetPlayerPosition().x + 2, GameState.Instance.GetPlayerPosition().y),
                    direction = MoveDirection.Right
                });
                MoveCharacter(_movement, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //undo
            _movement = GameState.Instance.PopActionStack();
            _movement.newPosition = _movement.previousPosition;
            _movement.previousPosition = GameState.Instance.GetPlayerPosition();
            _movement.direction = MoveDirection.None;
            MoveCharacter(_movement, true);
        }
        
    }

    void MoveCharacter(Movement _movement, bool isUndo)
    {
        int newX = (int)(transform.position.x + (_movement.newPosition.x - _movement.previousPosition.x) * 10);
        int newY = (int)(transform.position.y + (_movement.previousPosition.y - _movement.newPosition.y) * 10);
        GameState.Instance.SetPlayerPosition(_movement.newPosition);
        if(!isUndo)
            GameState.Instance.PushActionToStack(_movement);
        this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, newY, 66), 100);
    }
}
