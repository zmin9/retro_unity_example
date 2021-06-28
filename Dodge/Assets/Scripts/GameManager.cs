using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText; //���ӿ����� ȭ�鿡 �� �ؽ�Ʈ obj
    public Text timeText;   //������� ��� Text
    public Text recordText; //�ְ� ��� Text

    private float surviveTime;  //���� ���� ���
    private bool isGameover;    //���ӿ�������

    void Start()
    {
        surviveTime = 0;
        isGameover = false;
    }
    
    void Update()
    {
        if (!isGameover)
        {
            //���� ���� ���ķ��� �ð�
            surviveTime += Time.deltaTime;
            timeText.text = "Time : " + (int)surviveTime;
        }
        else
        {   //gameover�� ���
            if (Input.GetKeyDown(KeyCode.R))
            {
                //RŰ�� ���� �� scene �����
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);

        //"BestTime"Ű�� ����� ����ð� �ҷ�����
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (surviveTime > bestTime)
        {
            //���� ���� �����ð��� �� ��ٸ� �ٽ� "BestTime"Ű�� ����
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", surviveTime);
        }
        //ȭ�鿡 besttime ǥ��
        recordText.text = "Best Time : " + (int)bestTime;
    }
}
