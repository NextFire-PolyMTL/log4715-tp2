using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(10, 1, 0);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-10, 1, 0);

    // Déclaration des variables
    bool _Grounded { get; set; }
    bool _Flipped { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float JumpForce = 10f;

    [SerializeField]
    LayerMask WhatIsGround;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform firePoint;
    [SerializeField]
    float frameBtwFire=3f;
    public bool fire_on_target=false;


    //private float time = 0f;
    private float deltaTime=0f;
    private float lastTimeFire = 0f;
    private Vector3 targetPosition;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody>();
    }

    // Utile pour régler des valeurs aux objets
    void Start()
    {
        _Grounded = false;
        _Flipped = false;
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        
    }

    // Gère le mouvement horizontal
    void HorizontalMove(float horizontal)
    {
        _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal);
        _Anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
    }

    // Gère le saut du personnage, ainsi que son animation de saut
    void CheckJump()
    {
        if (_Grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
                _Grounded = false;
                _Anim.SetBool("Grounded", false);
                _Anim.SetBool("Jump", true);
            }
        }
    }

    // Gère l'orientation du joueur et les ajustements de la camera
    void FlipCharacter(float horizontal)
    {
        if (horizontal < 0 && !_Flipped)
        {
            _Flipped = true;
            transform.Rotate(FlipRotation);
            _MainCamera.transform.Rotate(-FlipRotation);
            _MainCamera.transform.localPosition = InverseCameraPosition;
        }
        else if (horizontal > 0 && _Flipped)
        {
            _Flipped = false;
            transform.Rotate(-FlipRotation);
            _MainCamera.transform.Rotate(FlipRotation);
            _MainCamera.transform.localPosition = CameraPosition;
        }
    }

    // Collision avec le sol
    
    /*
    void OnTriggerEnter(Collider vision){
        Debug.Log(vision.gameObject.name=="MaleFree1");
        if(vision.gameObject.name=="MaleFree1"){
            Debug.Log("alert");
            _Anim.SetBool("Alert", true);
        }
        
    }
    */
    
    void OnTriggerStay(Collider vision){
        //Debug.Log(vision.gameObject.name=="MaleFree1");
        if(vision.gameObject.name=="MaleFree1" && fire_on_target){
            targetPosition=vision.gameObject.transform.position;
            deltaTime=(Time.frameCount-lastTimeFire);
            if(deltaTime>frameBtwFire){
                _Anim.SetBool("Shot", true);
                _Anim.CrossFade("Shoot_SingleShot_AR 0",0.1f);
                Debug.Log("a");
                Debug.Log(_Anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_SingleShot_AR 0"));
                //if(_Anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_SingleShot_AR 0")){
                GameObject fire = GameObject.Instantiate(projectile, firePoint.position, firePoint.rotation) as GameObject;
                fire.GetComponent<projectileBehavior>().target=targetPosition;
                GameObject.Destroy(fire, 3f);
                deltaTime=0;
                lastTimeFire=Time.frameCount;
                //}
            }
            //fire.target=position
        }
        
    }
    /*
    void OnTriggerExit(Collider vision){
        if(vision.gameObject.name=="MaleFree1"){
            Debug.Log("ouf");
            _Anim.SetBool("Alert", false);
            _Anim.SetBool("Target", false);
        }
    }
    */
}