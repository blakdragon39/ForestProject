using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float speed;

    private new Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 velocity, Vector3 direction) {
        rigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }
}