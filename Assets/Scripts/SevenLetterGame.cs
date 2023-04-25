using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 
using TMPro;

public class SevenLetterGame : MonoBehaviour, GameController
{
    [SerializeField] private GameObject notAWordMessage;
    [SerializeField] private GameObject hintMessage;
    private bool coroutineRunning = false;
    private string wordToGuess;
    private List<char> letters;
    private List<Box> textLetters;
    [SerializeField] private List<GameObject> guesses;
    [SerializeField] private GameObject winOrLoseMenu;
    private List<GameObject> keysPressed;
    private bool menuOpen = false;
    private int currentIndex = 0;
    private int currentGuess = 0;

    void Start()
    {
        int randomWord = UnityEngine.Random.Range(0, (WordListGenerator.sevenLetterWords.Length - 1));

        wordToGuess = WordListGenerator.sevenLetterWords[randomWord];
        Debug.Log(wordToGuess);
        letters = new List<char>();
        textLetters = new List<Box>();
        keysPressed = new List<GameObject>();

        for (int i = 0; i < wordToGuess.Length; i++)
        {
            char letter = wordToGuess[i];
            letters.Add(letter);
        }

        PopulateTextLetters();
    }
    public void InputLetter(GameObject thisLetter)
    {
        Debug.Log("Pressed " + thisLetter.name);
        if (currentIndex < 7 && !menuOpen)
        {
            textLetters[currentIndex].Text = thisLetter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            keysPressed.Add(thisLetter);
            currentIndex++;
        }
    }

    public void RemoveLetter()
    {
        if (currentIndex > 0 && !menuOpen)
        {
            currentIndex--;
            textLetters[currentIndex].Text = "";
            keysPressed.RemoveAt(keysPressed.Count - 1);
        }
    }

    public void ConfirmWord()
    {
        string wordGuessed = "";
        foreach (var item in textLetters)
        {
            wordGuessed += item.Text;
        }
        if (currentIndex == 7)
        {
            if (WordListGenerator.sevenLetterWords.Contains(wordGuessed.ToLower()))
            {
                foreach (var key in keysPressed)
                {
                    if (key.GetComponent<Image>().color != Color.green && key.GetComponent<Image>().color != Color.yellow)
                        key.GetComponent<Image>().color = Color.gray;
                }
                CheckEachLetter();
                currentIndex = 0;
                if (currentGuess <= 5 && !menuOpen)
                {
                    currentGuess++;
                }
                if (currentGuess == 6)
                {
                    //Game Lost!
                    OpenMenu(false);
                }
                keysPressed.Clear();
                PopulateTextLetters();
            }
            else
            {
                if (!coroutineRunning)
                    StartCoroutine(ShowNotAWordMessage());
            }
        }
    }

    public void CheckDefinition()
    {
        Application.OpenURL($"https://www.merriam-webster.com/dictionary/{WordToGuess}");
    }

    public void WordHint()
    {
        if (!coroutineRunning)
            StartCoroutine(ShowHintMessage());
    } 

    IEnumerator ShowHintMessage()
    {
        coroutineRunning = true;
        hintMessage.SetActive(true);
        string message = $"First letter is {wordToGuess[0]} \n Last letter is {wordToGuess[6]}";
        hintMessage.GetComponent<TextMeshProUGUI>().text = message;
        yield return new WaitForSeconds(2f);
        hintMessage.SetActive(false);
        coroutineRunning = false;
    }

    IEnumerator ShowNotAWordMessage()
    {
        coroutineRunning = true;
        notAWordMessage.SetActive(true);
        yield return new WaitForSeconds(2f);
        notAWordMessage.SetActive(false);
        coroutineRunning = false;
    }

    private void PopulateTextLetters()
    {
        textLetters.Clear();
        guesses[currentGuess].SetActive(true);
        foreach(Box item in guesses[currentGuess].GetComponentsInChildren<Box>())
        {
            textLetters.Add(item);
            item.GetComponent<Image>().color = new Color (item.GetComponent<Image>().color.r, item.GetComponent<Image>().color.g, item.GetComponent<Image>().color.b, 1f);
        }
    }

    private void CheckEachLetter()
    {
        int lettersCorrect = 0;
        for (int i = 0; i < wordToGuess.Length; i++)
        {
            int sameLetterCount = wordToGuess.Split(wordToGuess[i]).Length - 1;
            int count = 0;
            if (textLetters[i].Text.Equals(letters[i].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                lettersCorrect++;
                textLetters[i].Letter.color = Color.green;
                keysPressed[i].GetComponent<Image>().color = Color.green;
            }
            for (int j = 0; j < wordToGuess.Length; j++)
            {
                if ((textLetters[j].Text.Equals(letters[i].ToString(), StringComparison.OrdinalIgnoreCase) && count < sameLetterCount))
                {
                    if (!textLetters[i].IsGreen)
                    {
                        Debug.Log($"{textLetters[j].Text} equals {letters[i].ToString()}");
                        Debug.Log(textLetters[j].Letter.color);
                        textLetters[j].Letter.color = Color.yellow;
                        Debug.Log(textLetters[j].Letter.color);
                        if (keysPressed[j].GetComponent<Image>().color != Color.green)
                            keysPressed[j].GetComponent<Image>().color = Color.yellow;
                        count++;
                    }

                }
            }
        }
        if (lettersCorrect == 7)
        {
            //Game Won!
            OpenMenu(true);
        }
    }

    private void OpenMenu(bool wonGame)
    {
        winOrLoseMenu.SetActive(true);
        menuOpen = true;
        TextMeshProUGUI title = winOrLoseMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (wonGame)
        {
            title.text = "You Got It!";
        }
        else
        {
            title.text = "Try Again!";
        }
        TextMeshProUGUI theWord = winOrLoseMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        theWord.text = wordToGuess.ToUpper();
    }

    public string WordToGuess
    {
        get { return wordToGuess; }
    }
}
