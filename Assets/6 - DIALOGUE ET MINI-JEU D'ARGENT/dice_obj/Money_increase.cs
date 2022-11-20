using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Money_increase : MonoBehaviour
{   
    TMP_Text text1;
    public static int value = 1000;
    public static int previous_value = 1000;
    public static bool update_money = false;

    // Start is called before the first frame update
    void Start()
    {
        text1 = GetComponent<TMP_Text> ();
    }

    // Update is called once per frame
    void Update()
    {   
        if (update_money)
        {
            StartCoroutine(ChangeMoney());
            update_money = false;
        }
        
    }

    IEnumerator ChangeMoney()
    {   
        if (previous_value > value) // On a perdu de l'argent
        {
            for (int i = previous_value; i > value; i--)
            {   
                previous_value = previous_value - 1;
                #if DEBUG
                if( text1.text==null ) Debug.LogWarning($"You forgot to assign '{nameof(text1.text)}' field in the inspector",gameObject);
                #endif
                text1.text = previous_value.ToString();
                yield return new WaitForSeconds(0.01F);
            } 
        }
        else // On a gagné de l'argent
        {
            for (int i = previous_value; i < value; i++)
            {   
                previous_value = previous_value + 1;
                text1.text = previous_value.ToString();
                yield return new WaitForSeconds(0.01F);
            } 
        } 
    }
}
