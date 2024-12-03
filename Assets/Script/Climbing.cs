using Unity.VisualScripting;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public int speed = 4;
    private float inputY;

    bool isSteps;
    bool isClimb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputY = Input.GetAxis("Vertical");

        if(isSteps == true && Mathf.Abs(inputY) > 0){
            isClimb = true;
        }
    }

    void FixedUpdate(){
        if(isClimb == true){
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, inputY * speed);
        }else{
            rb.gravityScale = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Step"){
            isSteps = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Step"){
            isSteps = false;
            isClimb = false;
        }
    }
}
