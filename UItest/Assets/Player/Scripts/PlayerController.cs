using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region ���º���
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

    [SerializeField] private float hp=100;//���� ���۽� ü��
    [SerializeField] private float dp =0;//���� ���۽� ����
    [SerializeField] private float STA = 100; //���� ���۽� ���¹̳�
    [SerializeField] private float dashGauge = 60;//���� ���۽� ����

    public float currentHp;
    public float currentDp;
    public float currentSTA;
    public float currentDashGauge;

    public Slider healthSlider;
    public Slider staminaSlider;

    public Gun[] currentGunList; // ���� ������ ��


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
        //�����͸Ŵ������� �Ѹ���Ʈ �޾ƿ���;
        init();
    }

   //�ʱ�ȭ
   private void init()
    {
        currentHp = hp;
        currentDp = dp;
        currentSTA = STA;
        currentDashGauge = dashGauge;
    }

    private void Update()
    { 
        //�ʴ� 6�� ������ ����
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

    private void SetAnimation()//�ִϸ��̼� ���� ����
    {
        anim.SetBool("Run", GetRun());
        anim.SetBool("Walk", GetWalk());
        anim.SetBool("Crouch", GetCrouch());

        //theCrosshair.WalkingAnimation(GetWalk());//ũ�ν���� �ȱ�
        //theCrosshair.RunningAnimation(GetRun());//ũ�ν���� �޸��⼳��
        //theCrosshair.CrouchingAnimation(GetCrouch());//ũ�ν���� �ɱ�
        //theCrosshair.JumpingAnimation(!GetIsGround());//ũ�ν���� ����
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

    public void TakeDamage(int damage)//������ ó��
    {
        currentHp -= damage - currentDp;
        Debug.Log("���� �÷��̾� ü��:"+currentHp);
        if (currentHp <= 0)
        {
            Debug.Log("����߽��ϴ�");
        }
    }

    #region ������ �����Ǻ� Get Set
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

//���� �߿� ray ���� ���� (���� �� ray �����ȿ� ������Ʈ�� ������ �ٷ� �ٴڿ� �پ ���� �ѹ� �� �ϰ�����)
//��� ������ ���� �����
//https://www.youtube.com/watch?v=n-KX8AeGK7E