using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Empty,
    Wall,
    Player,
    Event,
}

public class MapManager : MonoBehaviour
{
    public TileType[,] _tile;
    public int _size;
    public int playerStartingPoint_X, playerStartingPoint_Y;
    private int playerNowPoint_X, playerNowPoint_Y;
    public GameObject[] Tile;
    public Dictionary<string, string> TileDic = new Dictionary<string, string>();
    public Transform Player;
    public GameObject playerMoving;
    bool isPlayerReady = false;

    public bool isMove = false;
    public bool isKight=false;

    void Start()
    {
        Initialize(_size);
    }

    public Camera mainCamera;
    Vector3 mousePos = Vector3.zero;

    public void Initialize(int size)
    {
        _tile = new TileType[size, size];
        _size = size;
        string tilePos;

        int aa = 0;
        for (int y = 0; y < _size; y++)
        {
            for (int x = 0; x < _size; x++)
            {
                _tile[y, x] = TileType.Empty;
                tilePos = string.Format("{0},{1}", x, y);

                TileDic.Add(Tile[aa++].name, tilePos);
            }


        }

        playerNowPoint_X = playerStartingPoint_X;
        playerNowPoint_Y = playerStartingPoint_Y;

        _tile[playerStartingPoint_X, playerStartingPoint_Y] = TileType.Player;

        for (int z = 0; z < 10; z++)
        {
            int a = Random.Range(0, _size);
            int b = Random.Range(0, _size);
            if (_tile[a, b] == TileType.Empty)
            {
                _tile[a, b] = TileType.Event;
            }
            else
            {
                Debug.Log("Event 중복입니다");
                z--;
            }

        }


    }

    void Update()
    {
        if (isMove) return;
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("Water");
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
            {
                if (raycastHit.transform.name == "Player")
                {
                    Debug.Log("이동 준비 완료");
                    isPlayerReady = true;
                }

                else if (isPlayerReady == true)
                {
                    string xy;
                    TileDic.TryGetValue(raycastHit.transform.name, out xy);

                    string[] a = xy.Split(',');

                    int x = int.Parse(a[0].ToString());
                    int y = int.Parse(a[1].ToString());
                    PlayerMove(x, y);
                    isPlayerReady = false;
                    Debug.Log("이동 준비 해제");
                }
            }
        }

    }

    public void PlayerMove(int x, int y)
    {
        int b = playerNowPoint_X + 2;
        int a = playerNowPoint_X - 2;
        int d = playerNowPoint_Y + 2;
        int c = playerNowPoint_Y - 2;

        
        if (isKight)
        {
            //나이트 조건
            if ((x == playerNowPoint_X+1 && (y == playerNowPoint_Y +2|| y == playerNowPoint_Y - 2)) || (x == playerNowPoint_X - 1 && (y == playerNowPoint_Y + 2 || y == playerNowPoint_Y - 2)) || 
                (x == playerNowPoint_X + 2 && (y == playerNowPoint_Y + 1 || y == playerNowPoint_Y - 1)) || (x == playerNowPoint_X - 2 && (y == playerNowPoint_Y + 1 || y == playerNowPoint_Y - 1)))
            {
                if (_tile[x, y] == TileType.Empty)
                {
                    StartCoroutine(MovingPlayer(x, y));
                    isKight = false;
                }
                else if (_tile[x, y] == TileType.Wall)
                {
                    Debug.Log("벽이 있는 타일은 갈 수 없습니다.");
                }
                else if (_tile[x, y] == TileType.Event)
                {
                    Debug.Log("이벤트 발생");
                    StartCoroutine(MovingPlayer(x, y));
                    //추후 이벤트 발생 함수 호출 필요함
                }

            }
            else
            {
                Debug.Log("해당 칸은 이동 범위가 아니므로 이동할 수 없습니다.");
            }

        }

        //플레이어 이동이 체스 킹 기준일 때만 작성되어있음
        else
        {
            if (a < x && x < b && y < d && c < y)
            {
                if (_tile[x, y] == TileType.Empty)
                {
                    StartCoroutine(MovingPlayer(x, y));
                }
                else if (_tile[x, y] == TileType.Wall)
                {
                    Debug.Log("벽이 있는 타일은 갈 수 없습니다.");
                }
                else if (_tile[x, y] == TileType.Event)
                {
                    Debug.Log("이벤트 발생");
                    StartCoroutine(MovingPlayer(x, y));
                    //추후 이벤트 발생 함수 호출 필요함
                }

            }
            else
            {
                Debug.Log("해당 칸은 이동 범위가 아니므로 이동할 수 없습니다.");
            }
        }

        
        {
            //string tilePos = TileDic.FirstOrDefault(x => x.Value.name == tileName).Key;
            //1. x,y와 플레이어 사이가 이동할 수 있는 거리 체크
            //2. 이동할 수 있으면 이동하는 x,y 타일 이벤트 체크
            //3. 타일 종류 따라서 이동


            /*
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
            else Debug.Log("범위 밖입니다.");*/
        }
    }

    IEnumerator MovingPlayer(int x, int y)
    {
        isMove = true;

       //플레이어 애니메이션으로 천천히 이동시키기

        _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
        Player.position += new Vector3((x - playerNowPoint_X) * 10, 0, (y - playerNowPoint_Y) * 10);
        playerNowPoint_X = x;
        playerNowPoint_Y = y;
        _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;

        //플레이어 이동에 걸리는 시간
        yield return new WaitForSeconds(1f);

        //--몬스터 이동 함수--


        //몬스터 이동에 걸리는 시간
        yield return new WaitForSeconds(1f);


        //이동 활성화
        isMove = false;

    }


}
