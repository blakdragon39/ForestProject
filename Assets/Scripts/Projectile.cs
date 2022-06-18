using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float timeToLive;

    private new Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        var damageable = col.gameObject.GetComponentInParent<Damageable>();
        if (damageable != null && gameObject.layer != damageable.gameObject.layer) {
            damageable.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void Fire(Vector3 from, Vector3 towards, LayerMask firingLayer) {
        gameObject.layer = firingLayer;
        
        var fireDir = towards - from;
        float rotDeg = Mathf.Atan2(fireDir.y * -1f, fireDir.x * -1f) * Mathf.Rad2Deg;
        var rotation = new Vector3(0, 0, rotDeg);
        
        rigidbody.velocity = fireDir.ToVector2().normalized * speed;
        transform.rotation = Quaternion.Euler(rotation);

        StartCoroutine(DestroyCo());
    }

    private IEnumerator DestroyCo() {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}