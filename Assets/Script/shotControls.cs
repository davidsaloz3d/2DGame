using UnityEngine;

public class shotControls : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed = 10;

    public static bool impacto = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (PlayerControl.right == true)
        {
            rb.linearVelocity = Vector2.right * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.left * speed;
        }

        Invoke("DestroyShot", 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyShot()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            DestroyShot();
            impacto = true;
        }

        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().ReduccionDeVida();
            impacto = true;
            DestroyShot();
        }
    }
}
