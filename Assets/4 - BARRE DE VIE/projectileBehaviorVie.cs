using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBehaviorVie : MonoBehaviour
{
    Rigidbody _Rb { get; set; }
    Transform _Tr { get; set; }
    public Vector3 target;
    private Vector3 muzzle_position;
    private Vector3 target_direction;
    private float x_rotation;
    // Start is called before the first frame update
    void Start()
    {
        
        _Rb = GetComponent<Rigidbody>();
        _Tr = GetComponent<Transform>();
        
        muzzle_position=_Tr.position;
        target_direction= target-muzzle_position;
        target_direction=Vector3.Normalize(target_direction);
        x_rotation=Vector3.SignedAngle(-target_direction,new Vector3(0.0f,0.0f,1.0f),new Vector3(1.0f,0.0f,0.0f));

        _Tr.Rotate(new Vector3(x_rotation,0,0));


    }

    // Update is called once per frame
    void Update()
    {

       _Tr.position += target_direction*Time.deltaTime*10f; 
    }

    void OnCollisionEnter(Collision coll){

        
        if(coll.gameObject.tag=="Hero"){
            Destroy(gameObject);
        } 
        else if (coll.gameObject.tag=="floor"){
            Destroy(gameObject);
        }
    }
}
