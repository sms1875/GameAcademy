using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : MonoBehaviour
{
    public TileType[,] _tile;
    public int _size;
    public int playerStartingPoint_X, playerStartingPoint_Y;
    private int playerNowPoint_X, playerNowPoint_Y;
    public GameObject[] Tile;
    public Dictionary<string, string> TileDic = new Dictionary<string, string>();


    public enum TileType
    {
        Empty,
        Wall,
        Player,
        Event,
    }

    public void Initialize(int size)
    {
        _tile = new TileType[size, size];
        _size = size;
        string tilePos;

        int aa = 0;
        for (int y=0; y<_size; y++)
        {
            for (int x=0; x<_size; x++)
            {
                _tile[y, x] = TileType.Empty;
                tilePos = string.Format("{0},{1}",x,y) ;
                
                TileDic.Add(Tile[aa++].name,tilePos);
            }
        

        }

        playerNowPoint_X = playerStartingPoint_X;
        playerNowPoint_Y = playerStartingPoint_Y;

        _tile[playerStartingPoint_X, playerStartingPoint_Y] = TileType.Player;

        for (int z=0; z<10; z++)
        {
            int a= Random.Range(0, _size);
            int b = Random.Range(0, _size);
            if (_tile[a, b] == TileType.Empty)
            {
                _tile[a, b] = TileType.Event;
                Debug.Log(a + "," + b + "= Event" + z) ;
            }
            else
            {
                Debug.Log("Event 중복입니다");
                z--;
            }
            
        }


    }

    public void PlayerMove(int x, int y)
    {
        //string tilePos = TileDic.FirstOrDefault(x => x.Value.name == tileName).Key;
        //1. x,y와 플레이어 사이가 이동할 수 있는 거리 체크
        //2. 이동할 수 있으면 이동하는 x,y 타일 이벤트 체크
        //3. 타일 종류 따라서 이동
        if (playerNowPoint_X >= 0 && playerNowPoint_Y >= 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
 
                if (_tile[playerNowPoint_X - 1, playerNowPoint_Y] == TileType.Event)
                {
                    Debug.Log(playerNowPoint_X - 1 + "," + playerNowPoint_Y + "는 이벤트 타일입니다");

                }
                else if (_tile[playerNowPoint_X - 1, playerNowPoint_Y] == TileType.Wall)
                {
                    Debug.Log(playerNowPoint_X - 1 + "," + playerNowPoint_Y + "는 벽이므로 이동할 수 없습니다");
                }
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
                playerNowPoint_X--;
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_tile[playerNowPoint_X, playerNowPoint_Y - 1] == TileType.Event)
                {
                    Debug.Log(playerNowPoint_X + "," + playerNowPoint_Y + " -1 는 이벤트 타일입니다");

                }
                else if (_tile[playerNowPoint_X - 1, playerNowPoint_Y - 1] == TileType.Wall)
                {
                    Debug.Log(playerNowPoint_X - 1 + "," + playerNowPoint_Y + " -1 는 벽이므로 이동할 수 없습니다");
                }
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
                playerNowPoint_Y--;
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (_tile[playerNowPoint_X + 1, playerNowPoint_Y] == TileType.Event)
                {
                    Debug.Log(playerNowPoint_X + 1 + "," + playerNowPoint_Y + "는 이벤트 타일입니다");

                }
                else if (_tile[playerNowPoint_X + 1, playerNowPoint_Y] == TileType.Wall)
                {
                    Debug.Log(playerNowPoint_X + 1 + "," + playerNowPoint_Y + "는 벽이므로 이동할 수 없습니다");
                }
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
                playerNowPoint_X++;
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_tile[playerNowPoint_X, playerNowPoint_Y + 1] == TileType.Event)
                {
                    Debug.Log(playerNowPoint_X + "," + playerNowPoint_Y + 1 + "는 이벤트 타일입니다");

                }
                else if (_tile[playerNowPoint_X, playerNowPoint_Y + 1] == TileType.Wall)
                {
                    Debug.Log(playerNowPoint_X + "," + playerNowPoint_Y + 1 + "는 벽이므로 이동할 수 없습니다");
                }
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
                playerNowPoint_Y++;
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("지금 플레이어 위치는 " + playerNowPoint_X + "," + playerNowPoint_Y + "입니다");
            }
        }
        else Debug.Log("범위 밖입니다.");
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize(_size);
    }
    public Camera mainCamera;
    Vector3 mousePos = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                Debug.Log("test: " + raycastHit.transform.name);
                string xy;
                TileDic.TryGetValue(raycastHit.transform.name, out xy);
                Debug.Log(xy);

                string[] a = xy.Split(",");
                Debug.Log(a[0]);
                Debug.Log(a[1]);
            }
        }

    }
}
