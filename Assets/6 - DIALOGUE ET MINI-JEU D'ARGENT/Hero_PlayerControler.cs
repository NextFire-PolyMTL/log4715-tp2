using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_PlayerControler : MonoBehaviour
{
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(6, 1.666666f, 1);//new Vector3(8.333332f, 1.666666f, -0.7666666f);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-6, 1.666666f, -1);//new Vector3(-8.333332f, 1.666666f, 0.7666666f);

    // Déclaration des variables
    bool _Grounded { get; set; }
    bool _Flipped { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }

    public static bool freeze = false; // Freeze le joueur si égal à true
    public static Vector3 player_pos; // Position initiale du joueur si on relance la scène
    public static bool player_flip = false;
    public static bool start_opening = false;

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float JumpForce = 7.0f;

    [SerializeField]
    LayerMask WhatIsGround;

    [SerializeField] 
    GameObject visualCue;

    [SerializeField]
    AudioSource source;

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
        if (start_opening)
        {
            start_opening = false;
            _Rb.position = player_pos;
            if (player_flip)
            {
                _Flipped = true;
                transform.Rotate(FlipRotation);
                _MainCamera.transform.Rotate(-FlipRotation);
                _MainCamera.transform.localPosition = InverseCameraPosition;
            }
        }
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {   
        player_pos = _Rb.position;
        player_pos.y = player_pos.y + 0.7f; // On met le joueur un peu plus haut au spawn
        if (_Rb.position.z > 1.5 && _Rb.position.z < 4.5 )
        {
            visualCue.SetActive(true);
            DialogueScript.begin_dialogue = true;
        }
        else
        {
            visualCue.SetActive(false);
            DialogueScript.begin_dialogue = false;
            
        }
        var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
        
        if (!freeze)
        {
            HorizontalMove(horizontal);
            FlipCharacter(horizontal);
            CheckJump();
        }
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
            player_flip = true;
            transform.Rotate(FlipRotation);
            _MainCamera.transform.Rotate(-FlipRotation);
            _MainCamera.transform.localPosition = InverseCameraPosition;
        }
        else if (horizontal > 0 && _Flipped)
        {
            _Flipped = false;
            player_flip = false;
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