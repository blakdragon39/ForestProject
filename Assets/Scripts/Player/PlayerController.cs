using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

enum PlayerState {
    PlayerControl,
    ScriptControl,
}

public class PlayerController : MonoBehaviour, Killable {

    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Damageable damageable;

    private PlayerState currentState;
    private Vector2 input;
    private Vector2 walkDirection;
    private bool isWalking;

    private readonly int directionHash = Animator.StringToHash("direction");
    private readonly int isWalkingHash = Animator.StringToHash("isWalking");
    
    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

        currentState = PlayerState.PlayerControl;
    }

    private void Update() {
        if (currentState == PlayerState.PlayerControl) {
            UpdateInput();    
        }
        
        UpdateAnimation();
    }

    private void FixedUpdate() {
        if (currentState == PlayerState.PlayerControl) {
            Move();
        }
    }

    private void UpdateInput() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        isWalking = input != Vector2.zero;

        if (input != Vector2.zero) {
            walkDirection.x = input.x;
            walkDirection.y = input.y;
        }
    }

    private void UpdateAnimation() {
        animator.SetFloat(directionHash, walkDirection.x);
        animator.SetBool(isWalkingHash, isWalking);
    }

    private void Move() {
        if (isWalking) {
            rigidBody.MovePosition((Vector2) transform.position + walkDirection * moveSpeed * Time.deltaTime);
        }
    }

    public void Die() {
        damageable.Revive();
        transform.position = spawnPos;
    }

    public void TeleportTo(Vector3 destination) {
        transform.position = destination;
    }

    public IEnumerator WalkTo(Vector3 destination) {
        currentState = PlayerState.ScriptControl;
        
        while (transform.position != destination) {
            walkDirection = destination - transform.position;
            isWalking = true;

            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        
        currentState = PlayerState.PlayerControl;
    }
}
