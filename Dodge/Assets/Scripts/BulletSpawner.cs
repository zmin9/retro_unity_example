using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; //bullet prefab
    public float spawnRateMin = 0.5f;   //최소 생성주기
    public float spawnRateMax = 3f; //최대 생성주기

    private Transform target;   //player
    private float spawnRate;    //생성주기
    private float timeAfterSpawn;   //가장 최근 생성 시점으로부터 지난 시간

    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
    }

    
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        //생성주기만큼의 시간이 지나야지만 새로운 bullet을 생성
        if (timeAfterSpawn>=spawnRate)
        {
            //생성주기 초기화
            timeAfterSpawn = 0f;

            //using 'bullet' Prefab, create Bullet object
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //생성된 bullet이 target(player)을 바라보도록 -> 그 뱡항으로 나아가게 될 것
            bullet.transform.LookAt(target);

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
