using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText; //게임오버시 화면에 뜰 텍스트 obj
    public Text timeText;   //현재게임 기록 Text
    public Text recordText; //최고 기록 Text

    private float surviveTime;  //현재 게임 기록
    private bool isGameover;    //게임오버여부

    void Start()
    {
        surviveTime = 0;
        isGameover = false;
    }
    
    void Update()
    {
        if (!isGameover)
        {
            //게임 시작 이후로의 시간
            surviveTime += Time.deltaTime;
            timeText.text = "Time : " + (int)surviveTime;
        }
        else
        {   //gameover한 경우
            if (Input.GetKeyDown(KeyCode.R))
            {
                //R키를 누를 시 scene 재시작
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);

        //"BestTime"키에 저장된 최장시간 불러오기
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (surviveTime > bestTime)
        {
            //만약 현재 생존시간이 더 길다면 다시 "BestTime"키에 저장
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", surviveTime);
        }
        //화면에 besttime 표시
        recordText.text = "Best Time : " + (int)bestTime;
    }
}
