using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janken : MonoBehaviour
{
    bool flagJanken = false;
    int modeJanken = 0;

    private void OnGUI()
    {
        if (!flagJanken)
        {
            flagJanken = (GUI.Button(new Rect(10, Screen.height - 110, 100, 100), "¹¬Âîºü"));
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flagJanken)
        {
            switch (modeJanken)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default :
                    break;
            }
        }
    }
}
