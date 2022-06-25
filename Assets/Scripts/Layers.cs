using UnityEngine;

public class Layers : MonoBehaviour {

    public static Layers instance;

    public LayerMask InteractableLayer => interactableLayer;
    
    [SerializeField] private LayerMask interactableLayer;
    
    private void Awake() {
        instance = this;
    }
}