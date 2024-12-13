using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControls : MonoBehaviour
{
   public void Level1(){
    SceneManager.LoadScene("Level1");
   }

   public void Level2(){
    SceneManager.LoadScene("Level2");
   }

   public void ExitGame(){
    Application.Quit();
   }
}
