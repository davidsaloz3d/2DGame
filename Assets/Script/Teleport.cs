using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject portal;
    private GameObject player;

    public float posX;
    public float posY;

    AudioSource audioSrc;
    [SerializeField] AudioClip sTeleport;

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
            audioSrc.PlayOneShot(sTeleport);
            player.transform.position = new Vector2(posX, posY);
        }
    }
}
