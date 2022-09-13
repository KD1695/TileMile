using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TileType
{
    None,
    Normal,
    Slide,
    Jump,
    Wall,
    Restart,
    RandomTp
}

public class PathGenerator : MonoBehaviour
{
    [SerializeField] Tile tilePrefab;
    [SerializeField] Transform parent;

    [SerializeField] List<int> tileProbability = new List<int> { 35, 55, 75, 88, 96, 100 };

    // Start is called before the first frame update
    void Start()
    {
        CreateTileMainPath();
        DisplayTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTileMainPath()
    {
        int i = -1, j = -1;
        TileType previousType = TileType.None;
        int previou_i = -1, previou_j = -1;
        MoveDirection moveDirection = MoveDirection.Right;
        for (int it = 0; it < GameState.Instance.pathWidth; it++)
        {
            GameState.Instance.tiles.Add(new List<TileType>(new TileType[GameState.Instance.pathLength]));
        }
        while (i < GameState.Instance.pathLength)
        {
            int randomSeed = Random.Range(0, 100);

            //decide j

            if (previousType == TileType.None)
            {
                i = 0;
                j = Random.Range(0, GameState.Instance.pathWidth);
            }
            else
            {
                List<List<int>> nextMoves = new List<List<int>>(0);
                if (i + 1 < GameState.Instance.pathLength)
                {
                    nextMoves.Add(new List<int> { i + 1, j });
                }
                if (j + 1 < GameState.Instance.pathWidth)
                {
                    nextMoves.Add(new List<int> { i, j + 1 });
                }
                if (j - 1 >= 0)
                {
                    nextMoves.Add(new List<int> { i, j - 1 });
                }

                int selectedMove = Random.Range(0, nextMoves.Count);
                previou_i = i;
                previou_j = j;
                i = nextMoves[selectedMove][0];
                j = nextMoves[selectedMove][1];
                if (i > previou_i)
                    moveDirection = MoveDirection.Right;
                else if (j < previou_j)
                    moveDirection = MoveDirection.Up;
                else
                    moveDirection = MoveDirection.Down;
            }

            int x = tileProbability.IndexOf(tileProbability.FirstOrDefault(_val =>
            {
                return randomSeed < _val && randomSeed < tileProbability[2];
            }));
            try
            {
                GameState.Instance.tiles[j][i] = (TileType)(x + 1);  //skip none tiletype index
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
            previousType = GameState.Instance.tiles[j][i];
            switch (GameState.Instance.tiles[j][i])
            {
                case TileType.Slide:
                    switch (moveDirection)
                    {
                        case MoveDirection.Up:
                            j = Mathf.Max(j - 1, 0);
                            break;
                        case MoveDirection.Down:
                            j = Mathf.Min(j + 1, GameState.Instance.pathWidth - 1);
                            break;
                        case MoveDirection.Right:
                            i++;
                            break;
                    }
                    break;
                case TileType.Jump:
                    if (Random.Range(0, 2) == 0)
                        i+=2;
                    break;
            }
        }
    }

    private void DisplayTiles()
    {
        foreach (List<TileType> list in GameState.Instance.tiles)
        {
            foreach (TileType type in list)
            {
                var obj = Instantiate<Tile>(tilePrefab, parent);
                obj.SetTileColor(GetTileColor(type));
            }
        }
    }

    private Color GetTileColor(TileType type)
    {
        switch(type)
        {
            case TileType.None:
                return Color.black;
            case TileType.Normal:
                return Color.green;
            case TileType.Slide:
                return Color.blue;
            case TileType.Jump:
                return Color.red;
            case TileType.Wall:
                return Color.white;
            case TileType.Restart:
                return Color.cyan;
            case TileType.RandomTp:
                return Color.grey;
        }
        return Color.black;
    }
}
