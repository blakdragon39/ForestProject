using UnityEngine;

enum EnemyState {
    PatrolToEnd,
    PatrolToStart
}

public class Enemy : MonoBehaviour {

    [SerializeField] private int health;
    [SerializeField] private Vector2 patrolPosStart;
    [SerializeField] private Vector2 patrolPosEnd;
    [SerializeField] private float moveSpeed;

    private new Rigidbody2D rigidbody;
    private Animator animator;
    
    private int directionHash = Animator.StringToHash("direction");
    private int isMovingHash = Animator.StringToHash("isMoving");

    private EnemyState currentState;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        transform.position = patrolPosStart.ToVector3();
        currentState = EnemyState.PatrolToEnd;
    }

    private void Update() {
        var curPos = transform.position.ToVector2();
        if (curPos == patrolPosStart) {
            currentState = EnemyState.PatrolToEnd;
        } else if (curPos == patrolPosEnd) {
            currentState = EnemyState.PatrolToStart;
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        var targetPos = (currentState == EnemyState.PatrolToEnd) ? patrolPosEnd : patrolPosStart;
        var newPos = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        rigidbody.MovePosition(newPos);
    }
}