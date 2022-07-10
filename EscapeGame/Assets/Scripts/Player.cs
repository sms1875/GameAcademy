using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;
    public float gravity = 9.81F;
    public int Hp = 3;
    public bool KeyTrigger = false;
    public float setTime = 60f;
    CharacterController charCtrl;
    //Animator anim;

    //카메라설정
    public float mouseSensitivity = 800;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI timeText;

    public bool cursor = false;

    void Start()
    {

        charCtrl = GetComponent<CharacterController>();
        //anim = GetComponentInChildren<Animator>();//unitychan프리팹의 animator
    }

    void Update()
    {
        setTime -= Time.deltaTime;
        if(setTime<0)
            SceneManager.LoadScene("Lose");
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir = transform.TransformDirection(dir);
        dir.y -= gravity * Time.deltaTime;
        charCtrl.Move(dir * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
            cursor = true;
        if (Input.anyKey)
            cursor = false;

        setCursor();
        setUI();
        CameraRotation();
        keyCheck();
    }

    private void setCursor()
    {
        if (cursor)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void setUI()
    {
        hpText.text = ("HP : " + Hp.ToString());

        if (GameObject.FindGameObjectsWithTag("Key").Length < 1)
            keyText.text = ("Key : Clear!");
        else
            keyText.text = ("Key : " + GameObject.FindGameObjectsWithTag("Key").Length.ToString());

        timeText.text = ("Time : ") + ((int)setTime).ToString();
    }

    private void CameraRotation()//캐릭터 좌우회전
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void keyCheck()
    {
    if (GameObject.FindGameObjectsWithTag("Key").Length < 1)
        {
            KeyTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Cheese":
                Destroy(other.gameObject);
                Hp++;
                Debug.Log("hp:"+Hp);
                break;
            case "Key":
                Destroy(other.gameObject);
                break;
            case "Enemy":
                Hp--;
                Debug.Log("enemy:"+Hp );
           
                if (Hp == 0)
                {
                    SceneManager.LoadScene("Lose");
                    break;
                }
                break;
        }

    }

}
