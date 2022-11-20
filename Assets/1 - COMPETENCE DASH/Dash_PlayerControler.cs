using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_PlayerControler : MonoBehaviour
{
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(8.333332f, 1.666666f, -0.7666666f);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-8.333332f, 1.666666f, 0.7666666f);

    // Déclaration des variables
    bool _Grounded { get; set; }
    bool _Flipped { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }
    
    int last_orient = 1;
    bool canDash = true;
    bool isDashing;

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float JumpForce = 7.0f;

    [SerializeField]
    LayerMask WhatIsGround;


    [SerializeField]
    float dashingPower = 18f;

    [SerializeField]
    float dashingTime = 0.2f;

    [SerializeField]
    float dashingCooldown = 1f;

    [SerializeField]
    TrailRenderer tr;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip_dash;

    [SerializeField]
    AudioClip clip_saut;

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
        if (isDashing)
        {
            return;
        }

        var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
        HorizontalMove(horizontal);
        FlipCharacter(horizontal);
        CheckJump();
        
        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {   
            StartCoroutine(Dash(horizontal));
        }

        //Debug.Log("y = " + _Rb.position.y );
        if (_Rb.position.y <= -1.5) 
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }

    // Gère le mouvement horizontal
    void HorizontalMove(float horizontal)
    {
        _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal);
        _Anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
    }

    IEnumerator Dash(float horizontal)
    {   
        canDash = false;
        isDashing = true;
        _Rb.useGravity = false;
        _Rb.velocity = new Vector3(_Rb.velocity.x, 0f, last_orient * dashingPower);
        tr.emitting = true;
        source.PlayOneShot(clip_dash);
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        _Rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true; 
        
    }

    // Gère le saut du personnage, ainsi que son animation de saut
    void CheckJump()
    {
        if (_Grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                source.PlayOneShot(clip_saut);
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
            last_orient = -1;
            transform.Rotate(FlipRotation);
            _MainCamera.transform.Rotate(-FlipRotation);
            _MainCamera.transform.localPosition = InverseCameraPosition;
        }
        else if (horizontal > 0 && _Flipped)
        {   
            last_orient = 1;
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
    }
}