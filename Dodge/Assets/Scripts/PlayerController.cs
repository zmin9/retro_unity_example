using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �̵��� ��ü�� �޾ƿ�
    // �׷��ٸ� ��? transform�� ������� �ʴ°ɱ�? -> �ӵ��� ����ϱ� ����
    private Rigidbody playerRigidbody;
    public float speed = 8f;    //�̵� �ӷ�
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); //rigidbody ������Ʈ ã�ƿ�
        //���� ���ٸ� null
    }

    void Update()   //�� �����Ӹ��� ����Ǵ� �Լ�
    {
        // �࿡ ���� �Է°��� �޾ƿ� wasd �� ����Ű ���� ����
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");


        Vector3 velocity = new Vector3(xInput * speed, 0f, yInput * speed);
        playerRigidbody.velocity = velocity;
    }

    public void Die()
    {
        // gameObject�տ� ���� ������ �������� ���� ��� �� ��ũ��Ʈ�� ������Ʈ�� ������ ������Ʈ�� �޾ƿ�
        // ���� ��� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
