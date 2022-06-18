using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] private GameObject healthBlock;
    [SerializeField] private Color fillColor;
    [SerializeField] private Color emptyColor;

    private int maxHealth;
    private int currentHealth;
    private List<Image> healthBlocks; 
    
    public void Setup(int health) {
        healthBlocks = new List<Image>();
        maxHealth = health;
        currentHealth = health;
        
        for (int i = 0; i < health; i += 1) {
            var block = Instantiate(healthBlock, transform);
            var image = block.GetComponent<Image>();
            image.color = fillColor;
            healthBlocks.Add(image);
        }
    }

    public void RemoveHealth(int damage) {
        if (currentHealth <= 0) return;
        
        for (int i = 0; i < damage; i += 1) {
            healthBlocks[currentHealth - 1].color = emptyColor;
            currentHealth -= 1;
        
            if (currentHealth <= 0) break;
        }
    }

    public void Reset() {
        currentHealth = maxHealth;
        healthBlocks.ForEach(image => image.color = fillColor);
    }
}