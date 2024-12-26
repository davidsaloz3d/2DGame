using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6; // Número máximo de corazones
    public static int currentHealth; // Vidas iniciales
    [SerializeField] private Image heartPrefab; // Prefab de un corazón
    [SerializeField] private Sprite corazonLleno; // Sprite de corazón lleno
    [SerializeField] private Sprite corazonVacio; // Sprite de corazón vacío
    [SerializeField] private Transform heartContainer; // Contenedor de corazones en el HUD

    private List<Image> corazones = new List<Image>();

    void Start()
    {
        InitializeHearts();
        UpdateHearts();
    }

    public void InitializeHearts()
    {
        currentHealth = 3;
        // Elimina corazones existentes si reinicias la escena
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        corazones.Clear();

        // Genera el número máximo de corazones en el contenedor
        for (int i = 0; i < maxHealth; i++)
        {
            Image heart = Instantiate(heartPrefab, heartContainer);
            corazones.Add(heart);
        }
        UpdateHearts();
        Debug.Log(currentHealth);
    }

    public void UpdateHearts()
    {
        // Actualiza los sprites de corazones
        for (int i = 0; i < corazones.Count; i++)
        {
            if (i < currentHealth)
                corazones[i].sprite = corazonLleno; // Corazones llenos
            else
                corazones[i].sprite = corazonVacio; // Corazones vacíos
        }
        Debug.Log(currentHealth);
    }

    public void LoseLife(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateHearts();
    }

    public void GainLife(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHearts();
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        InitializeHearts();
        UpdateHearts();
    }
}