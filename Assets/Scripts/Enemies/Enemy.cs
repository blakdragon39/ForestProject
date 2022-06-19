using UnityEngine;

enum EnemyState {
    Patrolling,
    Attacking
}

public class Enemy : MonoBehaviour, Killable {
    
    [SerializeField] private Vector3 patrolPosStart;
    [SerializeField] private Vector3 patrolPosEnd;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attacksPerSecond;
    [SerializeField] private Projectile projectile;

    private new Rigidbody2D rigidbody;
    private Animator1D animator;
    private Transform target;

    private EnemyState currentState;
    private Vector3 patrolTarget;
    private float timeSinceLastAttack;
    
    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator1D>();
        target = GameObject.FindWithTag("Player").transform;

        transform.position = patrolPosStart;
        currentState = EnemyState.Patrolling;
        patrolTarget = patrolPosEnd;
        timeSinceLastAttack = 0;
    }

    private void Update() {
        timeSinceLastAttack += Time.deltaTime;
        
        if (Vector3.Distance(transform.position, target.position) <= attackRadius) {
            currentState = EnemyState.Attacking;
            Attack();
        } else {
            currentState = EnemyState.Patrolling;
        } 
        
        if (transform.position == patrolPosStart) {
            patrolTarget = patrolPosEnd;
        } else if (transform.position == patrolPosEnd) {
            patrolTarget = patrolPosStart;
        }
    }

    private void FixedUpdate() {
        if (currentState == EnemyState.Patrolling) {
            Move();
        }
    }

    private void Move() {
        var newPos = Vector3.MoveTowards(transform.position, patrolTarget, moveSpeed * Time.deltaTime);
        var direction = patrolTarget.x - newPos.x;

        animator.Direction = direction;
        animator.IsMoving = patrolTarget != newPos;
        
        rigidbody.MovePosition(newPos);
    }

    private void Attack() {
        if (timeSinceLastAttack > attacksPerSecond) {
            FireAttack();
            timeSinceLastAttack = 0;
        }
    }
    
    private void FireAttack() {
        var ball = Instantiate(projectile, transform.position, Quaternion.identity);
        ball.Fire(transform.position, target.position, gameObject.layer);
    }

    public void Die() {
        Destroy(gameObject);
    }
}