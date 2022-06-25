using UnityEngine;

public class NPCController : MonoBehaviour, Interactable {

    [SerializeField] private float moveSpeed;
    [SerializeField] private WanderRange wanderRange;
    
    private Animator1D animator;

    private void Awake() {
        animator = GetComponent<Animator1D>();
    }

    private void FixedUpdate() {
        // todo: Pushes player out of the way if there's collision 
        Move();
    }

    private void Move() {
        if (transform.position != wanderRange.WanderDestination) {
            animator.Direction = (wanderRange.WanderDestination - transform.position).x;
            animator.IsMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, wanderRange.WanderDestination, moveSpeed * Time.deltaTime);
        } else {
            animator.IsMoving = false;
        }
    }

    public void Interact(Transform initiator) {
        DialogController.Instance.AddMessageToQueue("Hello! How are you?");
    }
}