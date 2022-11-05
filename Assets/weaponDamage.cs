using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDamage : MonoBehaviour
{
    public bool damage_mode=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision vision){
        Debug.Log("enemy:");
        Debug.Log(vision.gameObject.tag);
        if(vision.gameObject.tag=="Enemy"){
            Debug.Log("touché!! ah ah ah");
            GameObject.Destroy(vision.gameObject);
        }
        
    }
    void OnTriggerEnter(Collider vision){
        Debug.Log("enemy:");
        Debug.Log(vision.gameObject.tag);
        if(vision.gameObject.tag=="Enemy" && damage_mode){
            Debug.Log("touché!! ah ah ah");
            vision.gameObject.GetComponent<Animator>().CrossFade("Die",0.1f);
            GameObject.Destroy(vision.gameObject,3f);
        }
        
    }
}
