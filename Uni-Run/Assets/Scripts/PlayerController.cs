using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   }

   private void Update() {
        // 사망 시 종료
        if (isDead)
            return;

        // 점프 시
        // 이단 점프 가능 - jumpCount 이용
        if (Input.GetMouseButtonDown(0)&& jumpCount<2)
        {
            jumpCount++;
            // 플레이어에게 힘을 가해주기 전에 이전에 가해지던 힘을 초기화함
            playerRigidbody.velocity = Vector2.zero;
            // 위쪽 방향으로 jumpForce만큼의 힘을 가해줌
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        // 마우스를 짧게 클릭하게 되면 낮은 높이로 뜀
        else if (Input.GetMouseButtonUp(0)&&playerRigidbody.velocity.y > 0)
        {
            // 아직 위로 올라가는 중에 마우스버튼에서 손가락을 뗀다면 감속
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
   }

   private void Die() {
        // 애니메이터 트리거 파라미터 Die를 set
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;    // 속도를 0으로 만듦
        isDead = true;

        GameManager.instance.OnPlayerDead();
   }

   private void OnTriggerEnter2D(Collider2D other) {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if (other.tag == "Dead" && !isDead)
            Die();
   }

   private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
   }

   private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
   }
}