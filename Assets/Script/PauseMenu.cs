using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play(){
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void nivel2(){
        SceneManager.LoadScene("Level2");
    }

    public void credits(){
        SceneManager.LoadScene("Credits");
    }

    public void returnMenu(){
        SceneManager.LoadScene("Menu");
    }
}
