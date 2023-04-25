using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        WordListGenerator.GenerateFiveWordList();
        WordListGenerator.GenerateSixWordList();
        WordListGenerator.GenerateSevenWordList();
    }

    public void OpenChooseWordLengthMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
