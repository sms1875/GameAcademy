using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCheck : MonoBehaviour
{
    System.DateTime now;
    int nowMonth;
    int nowDay;

    private AudioSource uniVoice;

    public AudioClip[] clips;

    private AudioClip[,] voiceDate = new AudioClip[12 + 1, 31 + 1];

    void Start()
    {
        now = System.DateTime.Now;
        nowMonth = now.Month;
        nowDay = now.Day;

        voiceDate[1, 1] = clips[0];
        voiceDate[1, 15] = clips[1];
        voiceDate[2, 3] = clips[2];
        voiceDate[2, 11] = clips[3];
        voiceDate[2, 14] = clips[4];
        voiceDate[3, 3] = clips[5];
        voiceDate[3, 14] = clips[6];
        voiceDate[3, 19] = clips[7];
        voiceDate[4, 1] = clips[8];
        voiceDate[4, 21] = clips[9];
        voiceDate[4, 22] = clips[10];
        voiceDate[5, 3] = clips[11];
        voiceDate[5, 4] = clips[12];
        voiceDate[5, 5] = clips[13];
        voiceDate[6, 2] = clips[14];
        voiceDate[7, 7] = clips[15];
        voiceDate[7, 20] = clips[16];
        voiceDate[8, 13] = clips[17];
        voiceDate[9, 15] = clips[18];
        voiceDate[9, 22] = clips[19];
        voiceDate[10, 8] = clips[20];
        voiceDate[10, 10] = clips[21];
        voiceDate[11, 3] = clips[22];
        voiceDate[11, 23] = clips[23];
        voiceDate[12, 24] = clips[24];
        voiceDate[12, 25] = clips[25];
        voiceDate[12, 31] = clips[26];

        

        int oldMonth = PlayerPrefs.GetInt("Month");
        int oldDay = PlayerPrefs.GetInt("Day");
        Debug.Log("이전실행일 : " + oldMonth + " 월" + oldDay + " 일\n" +
           "현재실행일 : " + nowMonth + " 월" + nowDay + " 일\n" );

        uniVoice = GetComponent<AudioSource>();
        if (voiceDate[nowMonth, nowDay] != null && (oldMonth!=nowMonth || oldDay!=nowDay))
            uniVoice.PlayOneShot(voiceDate[nowMonth, nowDay]);
        PlayerPrefs.SetInt("Month", nowMonth);
        PlayerPrefs.SetInt("Day", nowDay);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
