using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvPlayerController : MonoBehaviour
{
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(10, 1, 0);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-10, 1, 0);

    // Déclaration des variables
    bool _Grounded { get; set; }
    bool _Flipped { get; set; }
    bool _isOnIce { get; set; }
    bool _isOnTrampoline { get; set; }
    bool _isOnMud { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float JumpForce = 7f;

    [SerializeField]
    LayerMask WhatIsGround;

    [SerializeField]
    float trampoForce = 10f;

    [SerializeField]
    float mudEffect= 0.5f;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody>();
        _MainCamera = Camera.main;
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
        var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
        HorizontalMove(horizontal);
        FlipCharacter(horizontal);
        CheckJump();
    }

    // Gère le mouvement horizontal
    void HorizontalMove(float horizontal)
    {
        Debug.Log(_isOnMud);
        //_Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal);
        //_Rb.AddForce(new Vector3(0, 0, horizontal*0.5f)); 
        if (_isOnTrampoline)
        {
           //_Rb.AddForce(new Vector3(0, trampoForce*Mathf.Abs(_Rb.velocity.y),0));
           _Rb.AddForce(new Vector3(0, trampoForce*(Mathf.Abs(_Rb.velocity.y)+0.25f*Mathf.Abs(_Rb.velocity.z)),0));
           //_Rb.AddForce(new Vector3(0, trampoForce*(Mathf.Abs(_Rb.velocity.y)),trampoForce*Mathf.Abs(_Rb.velocity.z)));
           _isOnTrampoline=false;
           Debug.Log(trampoForce*Mathf.Abs(_Rb.velocity.z));
        }
        
        if (_isOnIce)
        {
           _Rb.AddForce(new Vector3(0, 0,horizontal*0.2f)); 
        } 
        else if(_isOnMud){
           _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y,horizontal*mudEffect);
        }
        else{
            _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y,horizontal);
        }
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
    void OnCollisionEnter(Collision coll)
    {        
        // On s'assure de bien être en contact avec le sol
        if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
            return;

        // Évite une collision avec le plafond
        if (coll.relativeVelocity.y > 0)
        {
            _Grounded = true;
            _Anim.SetBool("Grounded", _Grounded);
        }
        if (coll.gameObject.tag == "trampoline") {
            _isOnTrampoline=true;
        }else{
            _isOnTrampoline=false;
        }
        
        if (coll.gameObject.tag == "mud") {
            _isOnMud=true;
        }else{
            _isOnMud=false;
        }
        if (coll.gameObject.tag == "ice") {
            _isOnIce=true;
            Debug.Log("ice floor");
            GetComponent<Collider>().material.dynamicFriction = 0;
            Debug.Log(GetComponent<Collider>().material);
        }else {
            _isOnIce=false;
        }
    }
    
}