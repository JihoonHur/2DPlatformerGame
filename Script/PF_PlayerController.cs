using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PF_PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    [SerializeField] float jumpForce = 15f;

    [SerializeField] float graftScale = 3f;
    [SerializeField] float moveSpeed = 15f;
    private float xDirection;
    [SerializeField] bool isGround = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidbody2D.gravityScale = graftScale;
    }

    void Update()
    {
        // 방향키의 X 방향 값을 받는다. -> 키는 1, <- 키는 -1, 아무것도 안누르면 0을 출력함.
        xDirection = Input.GetAxisRaw("Horizontal");
        if(xDirection < 0 )
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(xDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        animator.SetInteger("isRun", Mathf.Abs((int)xDirection));


        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            // 스페이스 바를 누르면 릿지드바디2D 컴포넌트에 Y축으로 힘을 부여한다.
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        isGround = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 1.1f, LayerMask.GetMask("Platforms"));
        animator.SetBool("isGround", isGround);
    }

    void FixedUpdate()
    {
        // 플레이어 리지드바디2D 컴포넌트에 위에서 계산한 값을 부여한다.
        rigidbody2D.linearVelocity = new Vector2(xDirection * moveSpeed, rigidbody2D.linearVelocity.y);
    }

    
}
