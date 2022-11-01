using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnViewZone : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private enemyMove enemy_move;
    public bool targetOnView=false;
    private Animator _Anim;
    // Start is called before the first frame update
    void Start()
    {
        enemy_move=enemy.GetComponent<enemyMove>();
        _Anim = enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider vision){
        //Debug.Log(vision.gameObject.name=="MaleFree1");
        if(vision.gameObject.name=="MaleFree1"){
            Debug.Log("alert");
            _Anim.SetBool("Alert", true);
        }
        
    }
    void OnTriggerExit(Collider vision){
        if(vision.gameObject.name=="MaleFree1"){
            Debug.Log("ouf");
            _Anim.SetBool("Alert", false);
        }
        
    }
}
