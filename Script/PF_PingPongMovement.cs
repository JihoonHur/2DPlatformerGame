using UnityEngine;

public class PF_PingPongMovement : MonoBehaviour
{
    public enum PatrolType { Horizontal, Vertical }
    [SerializeField] PatrolType patrolType = PatrolType.Horizontal;  // Inspector 드롭다운

    public float moveSpeed = 2f;      // 이동 속도
    public float moveDistance = 3f;   // 왔다갔다 할 거리

    private Vector2 startPosition;
    private int direction = 1; // 1 = 오른쪽/위쪽, -1 = 왼쪽/아래쪽

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector2 moveDir = (patrolType == PatrolType.Horizontal) ? Vector2.right : Vector2.up;
        // 이동
        transform.Translate(moveDir * direction * moveSpeed * Time.deltaTime, Space.World);

        // 시작 위치에서 moveDistance만큼만 이동하면 방향 반전만
        float distance = (patrolType == PatrolType.Horizontal) ? Mathf.Abs(transform.position.x - startPosition.x) : Mathf.Abs(transform.position.y - startPosition.y);

        if (distance >= moveDistance)
        {
            direction *= -1;
        }
    }
}