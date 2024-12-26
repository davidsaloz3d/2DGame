using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControls : MonoBehaviour
{
   [SerializeField] GameObject menuControl;
   [SerializeField] GameObject menu;
   public void Level1(){
    SceneManager.LoadScene("Level1Alternative");
   }

   public void Level2(){
    SceneManager.LoadScene("Level2");
   }

   public void Controles(){
    menu.SetActive(false);
    menuControl.SetActive(true);
   }

   public void Atras(){
    menu.SetActive(true);
    menuControl.SetActive(false);
   }

   public void ExitGame(){
    Application.Quit();
   }
}
