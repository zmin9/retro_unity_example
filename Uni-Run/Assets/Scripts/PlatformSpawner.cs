using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -20); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        platforms = new GameObject[count];

        // 발판을 미리 생성해서 풀에 넣어둠
        for (int i = 0; i<count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }

        // 변수 초기화
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치

        // 게임오버 상태에선 배치하지 않음
        if (GameManager.instance.isGameover)
            return;

        // 배치 쿨타임이 다 찼는지
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // 마지막 배치 시점 현재시점으로 초기화
            lastSpawnTime = Time.time;

            // 새로운 배치 쿨타임 지정
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // 랜덤 위치 지정
            float yPos = Random.Range(yMin, yMax);

            // 현재 발판이 활성화 되어 있으므로 다시 비활성화->활성화 과정을 통해 OnEnable함수 실행
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // 발판 위치 이동
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            // 순번 조정
            currentIndex++;
            if (currentIndex >= count)
                currentIndex = 0;
        }
    }
}