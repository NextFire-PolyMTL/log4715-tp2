using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Draw_launch : MonoBehaviour
{
    TMP_Text text1;
    public static bool draw_animation = false;
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
        if (draw_animation)
        {   
            source.PlayOneShot(clip);
            anim.Play();
            draw_animation = false;
        }
        
    }
}
