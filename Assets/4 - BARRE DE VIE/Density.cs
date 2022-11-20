using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Density : MonoBehaviour
{
    public float densité;
    Rigidbody _Rb { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        densité=0.9f;
        _Rb=GetComponent<Rigidbody>();
        _Rb.SetDensity(densité);
    }
    

    // Update is called once per frame

}
