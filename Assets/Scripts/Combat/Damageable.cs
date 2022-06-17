using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour {

    [SerializeField] private int maxHealth;

    private Killable killable;
    private SpriteRenderer renderer;
    
    private int currentHealth;
    
    private void Awake() {
        killable = GetComponent<Killable>();
        renderer = GetComponent<SpriteRenderer>();
        
        currentHealth = maxHealth;
    }

    public void Revive() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        StartCoroutine(Blink());
        
        if (currentHealth <= 0) {
            killable.Die();
        }
    }

    private IEnumerator Blink() {
        renderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        renderer.enabled = true;
        
        yield return new WaitForSeconds(0.1f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        renderer.enabled = true;
        
        yield return new WaitForSeconds(0.1f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        renderer.enabled = true;
    }
}