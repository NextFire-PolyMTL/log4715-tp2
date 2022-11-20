using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Win_launch : MonoBehaviour
{
    TMP_Text text1;
    public static bool win_animation = false;
    private Animation anim;
    
    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (win_animation)
        {   
            source.PlayOneShot(clip);
            anim.Play();
            win_animation = false;
        }
        
    }
}
