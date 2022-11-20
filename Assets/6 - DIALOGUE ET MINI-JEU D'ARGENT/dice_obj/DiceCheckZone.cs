using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour
{   
    Vector3 diceVelocity;
    public static bool playsound = false;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dice;


    // Update is called once per frame
    void FixedUpdate()
    {
        diceVelocity = DiceScript.diceVelocity;        
    }

    void OnTriggerStay (Collider Col)
    {   

        if (playsound)
        {   
            source.PlayOneShot(clip_dice);
            playsound = false;
        }

        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {   
            if (DiceScript.turn)
            {
                BeginTextScript.begin = true;
            }
            else
            {
                DiceScript.is_allowed_to_launch = true;
            }

            switch (Col.gameObject.name)
            {
            case "Side1" :
                DiceNumberTextScript.diceNumber = 6;
                break;
            case "Side2" :
                DiceNumberTextScript.diceNumber = 5;
                break;
            case "Side3" :
                DiceNumberTextScript.diceNumber = 4;
                break;
            case "Side4" :
                DiceNumberTextScript.diceNumber = 3;
                break;
            case "Side5" :
                DiceNumberTextScript.diceNumber = 2;
                break;
            case "Side6" :
                DiceNumberTextScript.diceNumber = 1;
                break;
            }
        }
        else 
        {
            DiceNumberTextScript.diceNumber = 0;
        }
    }
}
