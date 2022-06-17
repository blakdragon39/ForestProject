using UnityEngine;

public class Damageable : MonoBehaviour {

    [SerializeField] private int maxHealth;

    private Killable killable;
    
    private int currentHealth;
    
    private void Awake() {
        killable = GetComponent<Killable>();
        
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        Debug.Log($"{gameObject.name} took damage. Health: {currentHealth}");
        
        if (currentHealth <= 0) {
            killable.Die();
        }
    }
}