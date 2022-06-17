using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] private Projectile projectile;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            FireAttack();
        }
    }

    private void FireAttack() {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var fireDir = mousePos - transform.position;

        float rotDeg = Mathf.Atan2(fireDir.y * -1f, fireDir.x * -1f) * Mathf.Rad2Deg;
        var rotation = new Vector3(0, 0, rotDeg);
        
        var ball = Instantiate(projectile, transform.position, Quaternion.identity);
        ball.Fire(fireDir.ToVector2(), rotation);
    }
}