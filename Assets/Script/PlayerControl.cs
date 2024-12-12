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
    public int intentos = 3;


    public static bool right = true;

    public float vulnera = 0.4f;

    [SerializeField] TMP_Text tVidas, tItems, tTime;

    [SerializeField] GameObject tLoser, tVictory;

    AudioSource audioSrc;
    [SerializeField] AudioClip sJump, sItem, sShoot, sDamage;

    [SerializeField] GameObject menuPausa;

    bool endGame = false;

    bool quieto = true;
    bool jumping = false;

    bool falling = false;

    [SerializeField] private float shootCooldown = 2f; // Intervalo de disparo en segundos
    private float lastShoot = 0f;


    private Vector3 lastCheckpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        GameManager.invulnerable = false;
        rb = GetComponent<Rigidbody2D>();
        tVidas.text = "Vidas: " + lives;
        tItems.text = "Items: " + items;
        tTime.text = time.ToString();

        audioSrc = GetComponent<AudioSource>();

        lastCheckpoint = transform.position;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!endGame)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuPausa.SetActive(true);
                Time.timeScale = 0;
            }


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
                quieto = false;
            }
            else
            {
                anim.SetBool("isRunning", false);
                quieto = true;
            }


            if (grounded())
            {
                anim.SetBool("isJump", false);
                jumping = false;
            }
            else if (!grounded() && jumping)
            {
                anim.SetBool("isJump", true);

            }

            if (Input.GetKeyDown(KeyCode.Space) && grounded())
            {
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                audioSrc.PlayOneShot(sJump);
                jumping = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastShoot + shootCooldown)
            {
                Instantiate(shot, new Vector3(transform.position.x, transform.position.y + 1.7f, 0), Quaternion.identity);
                anim.SetBool("isShoot", true);
                audioSrc.PlayOneShot(sShoot);
                lastShoot = Time.time;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (grounded() && !quieto)
                {
                    anim.SetBool("VeryRun", true);
                    speed = 9;
                }
                else
                {
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

        if (other.gameObject.tag == "Live")
        {
            Destroy(other.gameObject);
            if (lives <= 5)
            {
                lives++;
                tVidas.text = "Vidas: " + lives;
            }
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
                Time.timeScale = 0;
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

        if (lives <= 0)
        {
            RestoreCheckpoint();
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

    public void SaveCheckpoint(Vector3 checkpointPosicion)
    {
        lastCheckpoint = checkpointPosicion;
    }

    public void RestoreCheckpoint()
    {
        transform.position = lastCheckpoint;

        intentos--;

        if (intentos == 0)
        {
            tLoser.SetActive(true);
            Time.timeScale = 0;
            Invoke("goToMenu", 3);
        }

        lives = 3;

        Debug.Log("Restaurado al checkpoint en: " + lastCheckpoint);
        UpdateUI();
    }

    void UpdateUI()
    {
        tVidas.text = "Vidas: " + lives;
        tItems.text = "Items: " + items;
    }
}
