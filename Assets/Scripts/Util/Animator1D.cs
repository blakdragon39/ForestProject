using UnityEngine;

public class Animator1D : MonoBehaviour {

    public float Direction { get; set; }
    public bool IsMoving { get; set; }
    
    [SerializeField] private string directionKey;
    [SerializeField] private string isMovingKey;
    
    private Animator animator;
    private int directionHash;
    private int isMovingHash;

    private void Start() {
        animator = GetComponent<Animator>();
        directionHash = Animator.StringToHash(directionKey);
        isMovingHash = Animator.StringToHash(isMovingKey);
    }

    private void Update() {
        animator.SetFloat(directionHash, Direction);
        animator.SetBool(isMovingHash, IsMoving);
    }
}