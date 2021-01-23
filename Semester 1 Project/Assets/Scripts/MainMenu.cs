using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    public void PlayGame()
    {
        //This is used when hitting play game on the main menu putting you into the next unity scene through a iEnum
        StartCoroutine(LoadScene());
    }

    public void QuitGame()
    {
        //Used for quit game on the menu
        Application.Quit();
    }

    public void Menu()
    {
        //Used for quit game on the menu
        SceneManager.LoadScene("Main Menu");
    }


    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}

