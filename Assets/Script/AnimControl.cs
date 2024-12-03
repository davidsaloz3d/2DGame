using UnityEngine;

public class AnimControl : MonoBehaviour
{
    
    public void endShoot(){
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isShoot",false);
    }    
    
}
