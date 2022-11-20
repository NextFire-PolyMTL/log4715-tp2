using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DiceNumberTextScript : MonoBehaviour
{   

    TMP_Text text1;
    public static int diceNumber;
    public static bool init = true; // Etat initial
    public static bool result_anim = true;

    // Start is called before the first frame update
    void Start()
    {
        text1 = GetComponent<TMP_Text> ();        
    }

    // Update is called once per frame
    void Update()
    {   
        if (init)
        {
            text1.text = "";
        }
        else if (diceNumber != 0 && !DiceScript.turn)
        {   
            DiceScript.score_opponent = diceNumber;
            text1.text = "Score opposant : " + diceNumber.ToString();
        }
        else if (diceNumber != 0 && DiceScript.turn)
        {
            DiceScript.score_player = diceNumber;
            text1.text = "Score joueur : " + diceNumber.ToString();
            // On affiche l'écran de résultat
            if (result_anim)
            {   
                result_anim = false;
                if (DiceScript.score_player > DiceScript.score_opponent) { // Le joueur a gagné
                    Win_launch.win_animation = true;
                    Money_increase.value = Money_increase.value + 100;
                    Money_increase.update_money = true;
                }
                else if (DiceScript.score_player < DiceScript.score_opponent) { // Le joueur a perdu
                    Lose_launch.lose_animation = true;
                }
                else if (DiceScript.score_player == DiceScript.score_opponent) { // Il y a égalité
                    Draw_launch.draw_animation = true;
                    Money_increase.value = Money_increase.value + 50;
                    Money_increase.update_money = true;
                }
            }
        }
        else if (!DiceScript.turn)
        {
            text1.text = "Score opposant : ";
        }
        else if (DiceScript.turn)
        {
            text1.text = "Score joueur : ";
        }
    }
}
