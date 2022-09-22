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
                Debug.Log("Event �ߺ��Դϴ�");
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
                    Debug.Log("�̵� �غ� �Ϸ�");
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
                    Debug.Log("�̵� �غ� ����");
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
            //����Ʈ ����
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
                    Debug.Log("���� �ִ� Ÿ���� �� �� �����ϴ�.");
                }
                else if (_tile[x, y] == TileType.Event)
                {
                    Debug.Log("�̺�Ʈ �߻�");
                    StartCoroutine(MovingPlayer(x, y));
                    //���� �̺�Ʈ �߻� �Լ� ȣ�� �ʿ���
                }

            }
            else
            {
                Debug.Log("�ش� ĭ�� �̵� ������ �ƴϹǷ� �̵��� �� �����ϴ�.");
            }

        }

        //�÷��̾� �̵��� ü�� ŷ ������ ���� �ۼ��Ǿ�����
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
                    Debug.Log("���� �ִ� Ÿ���� �� �� �����ϴ�.");
                }
                else if (_tile[x, y] == TileType.Event)
                {
                    Debug.Log("�̺�Ʈ �߻�");
                    StartCoroutine(MovingPlayer(x, y));
                    //���� �̺�Ʈ �߻� �Լ� ȣ�� �ʿ���
                }

            }
            else
            {
                Debug.Log("�ش� ĭ�� �̵� ������ �ƴϹǷ� �̵��� �� �����ϴ�.");
            }
        }

        
        {
            //string tilePos = TileDic.FirstOrDefault(x => x.Value.name == tileName).Key;
            //1. x,y�� �÷��̾� ���̰� �̵��� �� �ִ� �Ÿ� üũ
            //2. �̵��� �� ������ �̵��ϴ� x,y Ÿ�� �̺�Ʈ üũ
            //3. Ÿ�� ���� ���� �̵�


            /*
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
            else Debug.Log("���� ���Դϴ�.");*/
        }
    }

    IEnumerator MovingPlayer(int x, int y)
    {
        isMove = true;

       //�÷��̾� �ִϸ��̼����� õõ�� �̵���Ű��

        _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Empty;
        Player.position += new Vector3((x - playerNowPoint_X) * 10, 0, (y - playerNowPoint_Y) * 10);
        playerNowPoint_X = x;
        playerNowPoint_Y = y;
        _tile[playerNowPoint_X, playerNowPoint_Y] = TileType.Player;

        //�÷��̾� �̵��� �ɸ��� �ð�
        yield return new WaitForSeconds(1f);

        //--���� �̵� �Լ�--


        //���� �̵��� �ɸ��� �ð�
        yield return new WaitForSeconds(1f);


        //�̵� Ȱ��ȭ
        isMove = false;

    }


}
