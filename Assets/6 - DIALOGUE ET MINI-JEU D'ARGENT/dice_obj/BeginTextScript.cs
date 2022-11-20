using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BeginTextScript : MonoBehaviour
{
    TMP_Text text1;
    public static bool begin = true;
    // Start is called before the first frame update
    void Start()
    {
        text1 = GetComponent<TMP_Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (begin)
        {
            text1.text = " Nouvelle manche ?\nOui : 'Y' / Non : Esc";
        }
        else if (DiceScript.is_allowed_to_launch)
        {
            text1.text = "Lancez le dé avec 'Espace'";
        }
        else
        {
            text1.text = "";
        }
    }
}
