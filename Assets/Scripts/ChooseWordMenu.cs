using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseWordMenu : MonoBehaviour
{
    public void StartGame(int wordLength)
    {
        if (wordLength == 5)
        {
            SceneManager.LoadScene("5LetterGame");
        }
        if (wordLength == 6)
        {
            SceneManager.LoadScene("6LetterGame");
        }
        else if (wordLength == 7)
        {
            SceneManager.LoadScene("7LetterGame");
        }
    }

    public void ExitMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
