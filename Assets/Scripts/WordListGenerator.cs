using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class WordListGenerator
{

    public static string[] fiveLetterWords;
    public static string[] sixLetterWords;
    public static string[] sevenLetterWords;

    public static void GenerateFiveWordList()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("AllFiveLetterWords");
        string textFileAsString = textAsset.text;
        
        fiveLetterWords = textFileAsString.Split("\n"[0]);
        for (int i = 0; i < fiveLetterWords.Length; i++)
        {
            fiveLetterWords[i] = fiveLetterWords[i].Substring(0, 5); 
        }
    }

    public static void GenerateSixWordList()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("AllSixLetterWords");
        string textFileAsString = textAsset.text;
        
        sixLetterWords = textFileAsString.Split("\n"[0]);
        for (int i = 0; i < sixLetterWords.Length; i++)
        {
            sixLetterWords[i] = sixLetterWords[i].Substring(0, 6); 
        }
    }

    public static void GenerateSevenWordList()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("AllSevenLetterWords");
        string textFileAsString = textAsset.text;
        
        sevenLetterWords = textFileAsString.Split("\n"[0]);
        for (int i = 0; i < sevenLetterWords.Length; i++)
        {
            sevenLetterWords[i] = sevenLetterWords[i].Substring(0, 7); 
        }
    }
}
