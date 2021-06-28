using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동할 물체를 받아옴
    // 그렇다면 왜? transform을 사용하지 않는걸까? -> 속도를 사용하기 위해
    private Rigidbody playerRigidbody;
    public float speed = 8f;    //이동 속력
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); //rigidbody 컴포넌트 찾아옴
        //만약 없다면 null
    }

    void Update()   //매 프레임마다 실행되는 함수
    {
        // 축에 대한 입력값을 받아옴 wasd 와 방향키 전부 적용
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");


        Vector3 velocity = new Vector3(xInput * speed, 0f, yInput * speed);
        playerRigidbody.velocity = velocity;
    }

    public void Die()
    {
        // gameObject앞에 따로 변수가 지정되지 않은 경우 이 스크립트가 컴포넌트로 부착된 오브젝트를 받아옴
        // 죽을 경우 오브젝트 비활성화
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
