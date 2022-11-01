using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnIcePC : MonoBehaviour
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

    Vector3 _lastPosition { get; set; }
    float _cumulativeTime { get; set; }
    bool _Iced { get; set; }
    bool _Freezed { get; set; }
    int _boolCumulateActions { get; set; }
    GameObject _FrozenCharacter { get; set; }

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float JumpForce = 10f;

    [SerializeField]
    LayerMask WhatIsGround;

    public string IceTag;

    // Awake se produit avait le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody>();
        _FrozenCharacter = GameObject.Find("FrozenCharacter");
    }

    // Utile pour régler des valeurs aux objets
    void Start()
    {
        _Grounded = false;
        _Flipped = false;
        _Iced = false;
        _Freezed = false;
        _FrozenCharacter.SetActive(false);
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        CheckFreeze();
        var horizontal = _Freezed ? 0 : Input.GetAxis("Horizontal") * MoveSpeed;
        HorizontalMove(horizontal);
        FlipCharacter(horizontal);
        CheckJump();
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
        if (!_Freezed && _Grounded)
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
        }
        else if (horizontal > 0 && _Flipped)
        {
            _Flipped = false;
            transform.Rotate(-FlipRotation);
        }
    }

    // Collision avec le sol
    void OnCollisionEnter(Collision coll)
    {
        // On s'assure de bien être en contact avec le sol
        if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
            return;

        if (coll.gameObject.tag == IceTag) _Iced = true;

        // Évite une collision avec le plafond
        if (coll.relativeVelocity.y > 0)
        {
            _Grounded = true;
            _Anim.SetBool("Grounded", _Grounded);
        }
    }

    void CheckFreeze()
    {
        if (_Freezed)
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Horizontal"))
            {
                if (++_boolCumulateActions >= 5)
                {
                    _cumulativeTime = 0;
                    _Freezed = false;
                    SwitchFrozen();
                }
            }

        }
        else if (_Iced && transform.position == _lastPosition)
        {
            _cumulativeTime += Time.deltaTime;
            if (_cumulativeTime >= 1)
            {
                _Freezed = true;
                _boolCumulateActions = 0;
                SwitchFrozen();
            }
        }
        else
        {
            _cumulativeTime = 0;
            _lastPosition = transform.position;
        }
    }

    void SwitchFrozen()
    {
        _FrozenCharacter.transform.position = transform.position;
        _FrozenCharacter.SetActive(!_FrozenCharacter.activeSelf);
    }
}
