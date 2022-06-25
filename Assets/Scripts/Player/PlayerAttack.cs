using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    
    [SerializeField] private Projectile projectile;

    private void Update() {
        if (GameController.Instance.CurrentState != GameState.PlayerControl) return;
        
        if (Input.GetButtonDown("Fire1")) {
            FireAttack();
        }
    }

    private void FireAttack() {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var ball = Instantiate(projectile, transform.position, Quaternion.identity);
        ball.Fire(transform.position, mousePos, gameObject.layer);
    }
}