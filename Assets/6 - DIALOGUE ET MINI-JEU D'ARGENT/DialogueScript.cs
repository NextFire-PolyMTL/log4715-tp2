using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour
{
    [SerializeField]
    GameObject image_dialogue_marchand;

    [SerializeField]
    GameObject image_dialogue_heros;

    [SerializeField]
    GameObject text_marchand;

    [SerializeField]
    GameObject text_marchand2;

    [SerializeField]
    GameObject text_heros;

    [SerializeField]
    RectTransform fader;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dialogue;

    public static bool affiche_d_m = false;
    public static bool affiche_d_h = false;
    public static bool begin_dialogue = false;
    public static bool start_opening = false;

    // Start is called before the first frame update
    void Start()
    {   
        if (start_opening) 
        {   
            start_opening = false;
            LeanTween.scale (fader, new Vector3 (10, 10, 10), 0);
            fader.gameObject.SetActive (true);
            LeanTween.scale (fader, Vector3.zero, 1.5f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
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
        if (affiche_d_m)
        {   
            image_dialogue_marchand.SetActive(true);
            text_marchand.SetActive(true);
            text_marchand2.SetActive(true);
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
        }
        else if (affiche_d_h)
        {   
            image_dialogue_heros.SetActive(true);
            text_heros.SetActive(true);
            image_dialogue_marchand.SetActive(false);
            text_marchand.SetActive(false);
            text_marchand2.SetActive(false);
        }
        else
        {
            image_dialogue_heros.SetActive(false);
            text_heros.SetActive(false);
            image_dialogue_marchand.SetActive(false);
            text_marchand.SetActive(false);
            text_marchand2.SetActive(false);
        }

    }

    void FixedUpdate()
    {
        if (begin_dialogue && Input.GetKeyDown (KeyCode.I)) // Phase 1 dialogue : le marchand parle
        {   
            source.PlayOneShot(clip_dialogue);
            Hero_PlayerControler.freeze = true; // On gèle les mouvements du joueur
            affiche_d_m = true;
            affiche_d_h = false;
        }

        if (affiche_d_m && Input.GetKeyDown (KeyCode.Space)) // Phase 2 dialogue : le joueur répond
        {   
            source.PlayOneShot(clip_dialogue);
            affiche_d_m = false;
            affiche_d_h = true;
        }

        if (affiche_d_h && Input.GetKeyDown (KeyCode.Escape))
        {   
            source.PlayOneShot(clip_dialogue);
            affiche_d_h = false;
            Hero_PlayerControler.freeze = false;
        }

        if (affiche_d_h && Input.GetKeyDown (KeyCode.Y))
        {   
            source.PlayOneShot(clip_dialogue);
            affiche_d_h = false;
            Hero_PlayerControler.freeze = false;
            CloseScene();
        }
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
        DiceScript.start_opening = true;
        SceneManager.LoadScene("Dice-game");
    }
}
