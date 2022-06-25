using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;



public class PlayerController : MonoBehaviour, Killable {

    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Collider2D interactionLeft;
    [SerializeField] private Collider2D interactionRight;

    private Rigidbody2D rigidBody;
    private Animator1D animator;
    private Damageable damageable;
    private PlayerAttack playerAttack;

    private Vector2 input;
    private Vector2 walkDirection;
    private bool isWalking;
    
    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator1D>();
        damageable = GetComponent<Damageable>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update() {
        if (GameController.Instance.CurrentState == GameState.PlayerControl) {
            UpdateInput();
            if (Input.GetButtonDown("Interact")) {
                CheckInteraction();
            }
        }
        
        UpdateAnimation();
    }

    private void FixedUpdate() {
        if (GameController.Instance.CurrentState == GameState.PlayerControl) {
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
        animator.Direction = walkDirection.x;
        animator.IsMoving = isWalking;
    }

    private void Move() {
        if (isWalking) {
            rigidBody.MovePosition((Vector2) transform.position + walkDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void CheckInteraction() {
        var interactionZone = animator.Direction < 0 ? interactionLeft : interactionRight;
        var interactable = Physics2D.OverlapArea(
            interactionZone.bounds.min, interactionZone.bounds.max, Layers.instance.InteractableLayer
        );

        if (interactable != null) {
            interactable.GetComponent<Interactable>().Interact(transform);
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
        GameController.Instance.CurrentState = GameState.ScriptControl;
        
        while (transform.position != destination) {
            walkDirection = destination - transform.position;
            isWalking = true;

            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        
        GameController.Instance.CurrentState = GameState.PlayerControl;
    }
}
