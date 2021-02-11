using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
 
 
    public void playGame() {
        SceneManager.LoadScene("MainLevel");
    }
 
    public void options() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
 
    public void back() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void playAgain() {
      SceneManager.LoadScene("MainLevel");
    }

    public void exitMainMenu() {
      SceneManager.LoadScene("MainMenu");
    }

    public void exitGame() {
        Application.Quit();
    }
}