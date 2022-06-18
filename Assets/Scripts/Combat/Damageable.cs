using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour {

    [SerializeField] private int maxHealth;
    [SerializeField] private HealthBar healthBar;

    private Killable killable;
    private new SpriteRenderer renderer;
    
    private int currentHealth;
    
    private void Awake() {
        killable = GetComponent<Killable>();
        renderer = GetComponent<SpriteRenderer>();
        
        currentHealth = maxHealth;
        
        healthBar.Setup(maxHealth);
    }

    public void Revive() {
        currentHealth = maxHealth;
        healthBar.Reset();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.RemoveHealth(damage);
        
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