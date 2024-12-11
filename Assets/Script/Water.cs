using UnityEngine;

public class Water : MonoBehaviour
{

    private float damageInterval = 2f; // Intervalo de daño en segundos
    private float nextDamageTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
{
    if (other.gameObject.tag == "Player" && !GameManager.invulnerable)
    {
        if (Time.time >= nextDamageTime)
        {
            other.gameObject.GetComponent<PlayerControl>().damage();
            nextDamageTime = Time.time + damageInterval;
        }
    }
}
}
