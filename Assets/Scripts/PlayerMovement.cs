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
        if(Input.GetKeyDown(KeyCode.W))
        {
            GameState.Instance.CheckMovementResult(new Movement
            {
                previousPosition = GameState.Instance.GetPlayerPosition(),
                newPosition = new Vector2(GameState.Instance.GetPlayerPosition().x, GameState.Instance.GetPlayerPosition().y+1),
                direction = MoveDirection.Up
            });
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameState.Instance.CheckMovementResult(new Movement
            {
                previousPosition = GameState.Instance.GetPlayerPosition(),
                newPosition = new Vector2(GameState.Instance.GetPlayerPosition().x, GameState.Instance.GetPlayerPosition().y + 1),
                direction = MoveDirection.Up
            });
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameState.Instance.CheckMovementResult(new Movement
            {
                previousPosition = GameState.Instance.GetPlayerPosition(),
                newPosition = new Vector2(GameState.Instance.GetPlayerPosition().x, GameState.Instance.GetPlayerPosition().y + 1),
                direction = MoveDirection.Up
            });
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jump
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //undo
        }
    }
}
