using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    GameObject _LeftWall { get; set; }

    void Awake()
    {
        _LeftWall = GameObject.Find("Wall Left");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is on top of the button
        if (collision.contacts[0].normal.y < 0)
        {
            _LeftWall.SetActive(false);
        }
    }
}
