using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Money_value : MonoBehaviour
{   
    TMP_Text text1;

    // Start is called before the first frame update
    void Start()
    {
        text1 = GetComponent<TMP_Text> ();
        text1.text = Money_increase.value.ToString();
    }
}
