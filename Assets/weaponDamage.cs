using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnColliderEnter(Collision vision){
        
        if(vision.gameObject.tag=="Enemy"){
            GameObject.Destroy(vision.gameObject);
        }
        
    }
}
