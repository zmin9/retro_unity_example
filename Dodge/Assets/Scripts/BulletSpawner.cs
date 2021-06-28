using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; //bullet prefab
    public float spawnRateMin = 0.5f;   //�ּ� �����ֱ�
    public float spawnRateMax = 3f; //�ִ� �����ֱ�

    private Transform target;   //player
    private float spawnRate;    //�����ֱ�
    private float timeAfterSpawn;   //���� �ֱ� ���� �������κ��� ���� �ð�

    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
    }

    
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        //�����ֱ⸸ŭ�� �ð��� ���������� ���ο� bullet�� ����
        if (timeAfterSpawn>=spawnRate)
        {
            //�����ֱ� �ʱ�ȭ
            timeAfterSpawn = 0f;

            //using 'bullet' Prefab, create Bullet object
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //������ bullet�� target(player)�� �ٶ󺸵��� -> �� �������� ���ư��� �� ��
            bullet.transform.LookAt(target);

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
