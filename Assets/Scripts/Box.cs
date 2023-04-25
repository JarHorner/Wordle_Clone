using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letter;

    public bool IsGreen
    {
        get { return letter.color == Color.green; }
    }
    public TextMeshProUGUI Letter
    {
        get { return letter; }
    }
    public string Text
    {
        get { return letter.text; }
        set { letter.text = value; }
    }
}
