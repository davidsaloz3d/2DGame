using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrabControl : MonoBehaviour
{
    [SerializeField] int speed = 3;
    [SerializeField] Vector3 endPosition;
    Vector3 startPosition;
    bool goingToTheEnd = true;

    [SerializeField] SpriteRenderer sprite;
    float PosPreviaX;

    public static bool MeDio = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        PosPreviaX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (goingToTheEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                goingToTheEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                goingToTheEnd = true;
            }
        }

        if (transform.position.x > PosPreviaX)
        {
            sprite.flipX = true;
        }
        else if (transform.position.x < PosPreviaX)
        {
            sprite.flipX = false;
        }

        PosPreviaX = transform.position.x;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !GameManager.invulnerable)
        {
            other.gameObject.GetComponent<PlayerControl>().damage();
            MeDio = true;
        }
    }
}
