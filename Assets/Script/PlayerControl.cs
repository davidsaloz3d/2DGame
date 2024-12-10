using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 4;
    public int jump = 5;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator anim;
    [SerializeField] int lives = 3;
    [SerializeField] GameObject shot;

    [SerializeField] int items = 0;
    [SerializeField] float time = 300;


    public static bool right = true;

    public float vulnera = 0.4f;

    [SerializeField] TMP_Text tVidas, tItems, tTime;

    [SerializeField] GameObject tLoser, tVictory;

    AudioSource audioSrc;
    [SerializeField] AudioClip sJump, sItem, sShoot, sDamage;

    bool endGame = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.invulnerable = false;
        rb = GetComponent<Rigidbody2D>();
        tVidas.text = "Vidas: " + lives;
        tItems.text = "Items: " + items;
        tTime.text = time.ToString();

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!endGame)
        {


            float inputX = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);

            if (inputX > 0)
            {
                sprite.flipX = false;
                right = true;
            }
            else if (inputX < 0)
            {
                sprite.flipX = true;
                right = false;
            }

            //Animaciones
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }


            if (grounded() == false)
            {
                anim.SetBool("isJump", true);
            }
            else
            {
                anim.SetBool("isJump", false);
            }



            if (Input.GetKeyDown(KeyCode.Space) && grounded())
            {
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                audioSrc.PlayOneShot(sJump);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(shot, new Vector3(transform.position.x, transform.position.y + 1.7f, 0), Quaternion.identity);
                anim.SetBool("isShoot", true);
                audioSrc.PlayOneShot(sShoot);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (grounded())
                {
                    anim.SetBool("VeryRun", true);
                    speed = 9;
                }else{
                    anim.SetBool("VeryRun", false);
                    speed = 9;
                }

            }
            else
            {
                anim.SetBool("VeryRun", false);
                speed = 4;
            }

            time = time - Time.deltaTime;
            if (time < 0)
            {
                time = 0;
                endGame = true;
                tLoser.SetActive(true);
                Invoke("goToMenu", 3);
            }

            float min, sec;
            min = Mathf.Floor(time / 60);
            sec = Mathf.Floor(time % 60);
            tTime.text = min.ToString("00") + ":" + sec.ToString("00");

        }
        else
        {
            sprite.gameObject.SetActive(false);
        }
    }


    bool grounded()
    {
        RaycastHit2D touch = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);

        if (touch.collider == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            Destroy(other.gameObject);
            sprite.color = new Color(0, 1, 1, 1);
            GameManager.invulnerable = true;
            Invoke("becomeVulnerable", 5);
        }

        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
            items++;
            audioSrc.PlayOneShot(sItem);
            tItems.text = "Items: " + items;
            if (items == 10)
            {
                endGame = true;
                tVictory.SetActive(true);
                Invoke("goToCredits", 3);
            }
        }


    }

    void becomeVulnerable()
    {
        sprite.color = Color.white;
        GameManager.invulnerable = false;
    }

    public void damage()
    {
        lives--;
        audioSrc.PlayOneShot(sDamage);
        sprite.color = Color.red;
        GameManager.invulnerable = true;
        Invoke("becomeVulnerable", vulnera);

        if (lives < 0)
        {
            lives = 0;
            endGame = true;
            tLoser.SetActive(true);
            Invoke("goToMenu", 3);

        }

        tVidas.text = "Vidas: " + lives;
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void goToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

}
