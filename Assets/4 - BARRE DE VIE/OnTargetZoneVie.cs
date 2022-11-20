using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTargetZoneVie : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private enemyMoveVie enemy_move;
    public bool targetOnView=false;
    private Animator _Anim;
    // Start is called before the first frame update
    void Start()
    {
        enemy_move=enemy.GetComponent<enemyMoveVie>();
        _Anim = enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider vision){
        //Debug.Log(vision.gameObject.name=="MaleFree1");
        targetOnView=true;
        if(vision.gameObject.tag=="Hero"){
         //   Debug.Log("fire");
            _Anim.SetBool("Target", true); 
            _Anim.CrossFade("Idle_Shoot_Ar",0.1f);
            enemy_move.fire_on_target=true;
        }
    }  
    
    void OnTriggerExit(Collider vision){
        if(vision.gameObject.tag=="Hero"){

            enemy_move.fire_on_target=false;
            _Anim.SetBool("Target", false);
            _Anim.SetBool("Shot", false);
        }
        
    }
}
