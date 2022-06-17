using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Player player;
    public TextMeshPro hpText;
    public TextMeshPro keyText;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        hpText = GetComponentInChildren<TextMeshPro>();
        keyText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = ("HP : " + player.Hp.ToString());
        keyText.text = ("Key : " + GameObject.FindGameObjectsWithTag("Key").Length.ToString());
    }
}
