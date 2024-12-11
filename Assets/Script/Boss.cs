using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] int balazos = 10;

    [SerializeField] float tiempoDamage = 0.2f;

    [SerializeField] SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduccionDeVida()
    {
        balazos--; // Reducir los balazos
        Debug.Log("Boss recibió un balazo. Restan: " + balazos);
        sprite.color = new Color(1, 0, 0, 1);
        Invoke("cambioColor",tiempoDamage);

        if (balazos == 0)
        {
            Destroy(gameObject); // Destruir al jefe si los balazos llegan a 0
            Debug.Log("Boss destruido");
        }
    }

    void cambioColor(){
        sprite.color = Color.white;
    }
}
