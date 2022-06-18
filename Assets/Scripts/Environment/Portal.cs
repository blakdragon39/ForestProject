using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField] private Portal destination;
    [SerializeField] private Transform walkOutDestination;

    private new BoxCollider2D collider;

    private void Awake() {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        var player = col.GetComponent<PlayerController>();
        if (player != null) {
            StartCoroutine(TeleportToDestination(player));
        }
    }

    private IEnumerator TeleportToDestination(PlayerController player) {
        collider.enabled = false;
        destination.collider.enabled = false;
        
        yield return player.WalkTo(transform.position);
        player.TeleportTo(destination.transform.position);
        yield return player.WalkTo(destination.walkOutDestination.position);
        
        collider.enabled = true;
        destination.collider.enabled = true;
    }
}