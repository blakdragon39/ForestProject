using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D rigidBody;
    private Animator animator;

    private Vector2 input;
    private Vector2 lastInput;

    private readonly int directionHash = Animator.StringToHash("direction");
    private readonly int isWalkingHash = Animator.StringToHash("isWalking");
    
    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        UpdateInput();
        UpdateAnimation();
    }

    private void FixedUpdate() {
        Move();
    }

    private void UpdateInput() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero) {
            lastInput.x = input.x;
            lastInput.y = input.y;
        }
    }

    private void UpdateAnimation() {
        animator.SetFloat(directionHash, lastInput.x);
        animator.SetBool(isWalkingHash, input != Vector2.zero);
    }

    private void Move() {
        rigidBody.MovePosition((Vector2) transform.position + input * moveSpeed * Time.deltaTime);
    }
}
