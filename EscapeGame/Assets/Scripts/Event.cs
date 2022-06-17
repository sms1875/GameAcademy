using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                SceneManager.LoadScene("Win");
                break;
        }

    }
}
