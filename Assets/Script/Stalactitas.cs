using UnityEngine;

public class Stalactitas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !GameManager.invulnerable)
        {
            other.gameObject.GetComponent<PlayerControl>().damage();
        }
    }
}
