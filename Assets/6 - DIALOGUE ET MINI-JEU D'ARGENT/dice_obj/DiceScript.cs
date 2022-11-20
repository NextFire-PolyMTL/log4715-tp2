using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiceScript : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;
    public static bool is_allowed_to_launch = false;
    public static bool turn = true; // true = tour opposant, false = tour joueur
    public static int score_player = 0;
    public static int score_opponent = 0;
    public static bool start_opening = false;

    [SerializeField]
    RectTransform fader;

    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody> (); 
        if (start_opening) 
        {   
            start_opening = false;
            LeanTween.scale (fader, new Vector3 (10, 10, 10), 0);
            fader.gameObject.SetActive (true);
            LeanTween.scale (fader, Vector3.zero, 1f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            fader.gameObject.SetActive (false);
            });
        }
        else
        {
            fader.gameObject.SetActive (false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;

        if (BeginTextScript.begin && Input.GetKeyDown (KeyCode.Y))
        {   
            Money_increase.value = Money_increase.value - 50;
            Money_increase.update_money = true;
            Ennemi_turn();
        }

        if (!turn && Input.GetKeyDown (KeyCode.Space) && is_allowed_to_launch) // Lancer le dé --> barre espace lorsqu'il est immobile au sol
        {   
            turn = true; // à l'opposant de jouer ensuite
            is_allowed_to_launch = false;
            DiceNumberTextScript.diceNumber = 0;
            float dirX = Random.Range(0, 1500);
            float dirY = Random.Range(0, 1500);
            float dirZ = Random.Range(0, 1500);
            transform.position = new Vector3(0, 4, 0);
            transform.rotation = Quaternion.identity;
            rb.AddForce (transform.up * 1500);
            rb.AddTorque (dirX, dirY, dirZ);
            DiceCheckZone.playsound = true;
            DiceNumberTextScript.result_anim = true;
        }

        if (BeginTextScript.begin && Input.GetKeyDown (KeyCode.Escape)) // Quitter la partie --> touche Echap lorsque le dé est immobile au sol
        {
            CloseScene();
        }
        
    }
    void Ennemi_turn()
    {   
        if(DiceNumberTextScript.init) 
        {
            DiceNumberTextScript.init = false;
        }
        turn = false; // au joueur de jouer ensuite
        DiceNumberTextScript.diceNumber = 0;
        float dirX = Random.Range(0, 1500);
        float dirY = Random.Range(0, 1500);
        float dirZ = Random.Range(0, 1500);
        transform.position = new Vector3(0, 4, 0);
        transform.rotation = Quaternion.identity;
        rb.AddForce (transform.up * 1500);
        rb.AddTorque (dirX, dirY, dirZ);
        BeginTextScript.begin = false; // On efface le texte pour rejouer
        DiceCheckZone.playsound = true;
    }

        void CloseScene()
    {   
        LeanTween.scale (fader, Vector3.zero, 0f);
        fader.gameObject.SetActive (true);
        LeanTween.scale (fader, new Vector3 (10, 10, 10), 0.7f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            Invoke ("LoadGame", 1f);
        });

    }

    private void LoadGame () {
        DialogueScript.start_opening = true;
        Hero_PlayerControler.start_opening = true;
        SceneManager.LoadScene("Gym_6");
    }
}
