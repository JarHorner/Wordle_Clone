using UnityEngine;
public interface GameController
{
    void InputLetter(GameObject thisLetter);

    void RemoveLetter();

    void ConfirmWord();

    void CheckDefinition();
    
    void WordHint();
}
