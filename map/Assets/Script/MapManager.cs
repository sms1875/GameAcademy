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
                Debug.Log("Event �ߺ��Դϴ�");
                z--;
            }
            
        }


    }

    public void PlayerMove(int x, int y)
    {
        //string tilePos = TileDic.FirstOrDefault(x => x.Value.name == tileName).Key;
        //1. x,y�� �÷��̾� ���̰� �̵��� �� �ִ� �Ÿ� üũ
        //2. �̵��� �� ������ �̵��ϴ� x,y Ÿ�� �̺�Ʈ üũ
        //3. Ÿ�� ���� ���� �̵�
        if (playerNowPoint_X >= 0 && playerNowPoint_Y >= 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
 
                if (_tile[playerNowPoint_X - 1, playerNowPoint_Y] == TileType.Event)
                {
                    Debug.Log(playerNowPoint_X - 1 + "," + playerNowPoint_Y + "�� �̺�Ʈ Ÿ���Դϴ�");

                }
                else if (_tile[playerNowPoint_X - 1, playerNowPoint_Y] == TileType.Wall)
                {
                    Debug.Log(playerNowPoint_X - 1 + "," + playerNowPoint_Y + "�� ���̹Ƿ� �̵��� �� �����ϴ�");
                }
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
                playerNowPoint_X--;
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_tile[playerNowPoint_X, playerNowPoint_Y - 1] == TileType.Event)
                {
                    Debug.Log(playerNowPoint_X + "," + playerNowPoint_Y + " -1 �� �̺�Ʈ Ÿ���Դϴ�");

                }
                else if (_tile[playerNowPoint_X - 1, playerNowPoint_Y - 1] == TileType.Wall)
                {
                    Debug.Log(playerNowPoint_X - 1 + "," + playerNowPoint_Y + " -1 �� ���̹Ƿ� �̵��� �� �����ϴ�");
                }
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
                playerNowPoint_Y--;
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (_tile[playerNowPoint_X + 1, playerNowPoint_Y] == TileType.Event)
                {
                    Debug.Log(playerNowPoint_X + 1 + "," + playerNowPoint_Y + "�� �̺�Ʈ Ÿ���Դϴ�");

                }
                else if (_tile[playerNowPoint_X + 1, playerNowPoint_Y] == TileType.Wall)
                {
                    Debug.Log(playerNowPoint_X + 1 + "," + playerNowPoint_Y + "�� ���̹Ƿ� �̵��� �� �����ϴ�");
                }
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
                playerNowPoint_X++;
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_tile[playerNowPoint_X, playerNowPoint_Y + 1] == TileType.Event)
                {
                    Debug.Log(playerNowPoint_X + "," + playerNowPoint_Y + 1 + "�� �̺�Ʈ Ÿ���Դϴ�");

                }
                else if (_tile[playerNowPoint_X, playerNowPoint_Y + 1] == TileType.Wall)
                {
                    Debug.Log(playerNowPoint_X + "," + playerNowPoint_Y + 1 + "�� ���̹Ƿ� �̵��� �� �����ϴ�");
                }
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
                playerNowPoint_Y++;
                _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("���� �÷��̾� ��ġ�� " + playerNowPoint_X + "," + playerNowPoint_Y + "�Դϴ�");
            }
        }
        else Debug.Log("���� ���Դϴ�.");
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
