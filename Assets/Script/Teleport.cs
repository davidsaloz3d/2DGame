using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject portal;
    private GameObject player;

    [SerializeField] SpriteRenderer sprite;

    public float posX;
    public float posY;

    public static bool pasado = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            player.transform.position = new Vector2(posX, posY);
            pasado = true;
        }
    }
}
