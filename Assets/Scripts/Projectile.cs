using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float speed;

    private new Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        Destroy(gameObject); //todo hit target
    }

    public void Fire(Vector3 from, Vector3 towards) {
        var fireDir = towards - from;
        float rotDeg = Mathf.Atan2(fireDir.y * -1f, fireDir.x * -1f) * Mathf.Rad2Deg;
        var rotation = new Vector3(0, 0, rotDeg);
        
        rigidbody.velocity = fireDir.ToVector2().normalized * speed;
        transform.rotation = Quaternion.Euler(rotation);
    }
}