using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region 상태변수
    private bool isWalk = false;
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = false;
    #endregion

    public Animator anim;

    public CharacterController charController;
    private GunController theGunController;
    //private Crosshair theCrosshair;
    private DataManager dataManager;

    [SerializeField] private float hp=100;//게임 시작시 체력
    [SerializeField] private float dp =0;//게임 시작시 방어력
    [SerializeField] private float STA = 100; //게임 시작시 스태미너
    [SerializeField] private float dashGauge = 60;//게임 시작시 방어력

    public float currentHp;
    public float currentDp;
    public float currentSTA;
    public float currentDashGauge;

    public Slider healthSlider;
    public Slider staminaSlider;

    public Gun[] currentGunList; // 현재 장착된 총


    public AudioSource[] PlayerSound;

    public static PlayerController instance;
    private void Awake()
    {
        PlayerController.instance = this;


        charController = GetComponent<CharacterController>();
        //theCrosshair = FindObjectOfType<Crosshair>();
        theGunController = GetComponent<GunController>();
        anim = GetComponent<Animator>();
        //dataManager = FindObjectOfType<DataManager>();
        //currentGunList = dataManager.currentGunList;
    }

    private void Start()
    {        
        Cursor.lockState = CursorLockMode.Locked;
        //데이터매니저에서 총리스트 받아오기;
        init();
    }

   //초기화
   private void init()
    {
        currentHp = hp;
        currentDp = dp;
        currentSTA = STA;
        currentDashGauge = dashGauge;
    }

    private void Update()
    { 
        //초당 6씩 게이지 증가
        if (currentDashGauge < dashGauge)
        {
            currentDashGauge += 0.1f;
        }
    }

    private void FixedUpdate()
    {
        SetAnimation();
        setSound();
    }

    private void SetAnimation()//애니메이션 상태 설정
    {
        anim.SetBool("Run", GetRun());
        anim.SetBool("Walk", GetWalk());
        anim.SetBool("Crouch", GetCrouch());

        //theCrosshair.WalkingAnimation(GetWalk());//크로스헤어 걷기
        //theCrosshair.RunningAnimation(GetRun());//크로스헤어 달리기설정
        //theCrosshair.CrouchingAnimation(GetCrouch());//크로스헤어 앉기
        //theCrosshair.JumpingAnimation(!GetIsGround());//크로스헤어 점프
    }

  private void setSound()
    {
        if (isRun)
        {
            PlayerSound[1].Play();
        }
        else
            PlayerSound[1].Stop();

        if (isWalk)
            PlayerSound[0].Play();
        else
            PlayerSound[0].Stop();




    }

    public void TakeDamage(int damage)//데미지 처리
    {
        currentHp -= damage - currentDp;
        Debug.Log("현재 플레이어 체력:"+currentHp);
        if (currentHp <= 0)
        {
            Debug.Log("사망했습니다");
        }
    }

    #region 움직임 상태판별 Get Set
    public bool GetRun()
    {
        return isRun;
    }
    public bool GetWalk()
    {
        return isWalk;
    }
    public bool GetCrouch()
    {
        return isCrouch;
    }
    public bool GetIsGround()
    {
        return charController.isGrounded;
    }
    public void SetRun(bool run)
    {
        isRun = run;
    }
    public void SetWalk(bool walk)
    {
        isWalk = walk;
    }
    public void SetCrouch(bool crouch)
    {
        isCrouch = crouch;
    }
    #endregion
}

//점프 중에 ray 판정 개선 (점프 중 ray 범위안에 오브젝트가 있으면 바로 바닥에 붙어서 점프 한번 더 하고있음)
//계단 오르는 판정 만들기
//https://www.youtube.com/watch?v=n-KX8AeGK7E