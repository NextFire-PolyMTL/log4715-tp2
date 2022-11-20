using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteControler : MonoBehaviour
{
    [SerializeField] private Animator Porte;
    [SerializeField] private bool ouverte;
    [SerializeField] private bool fermée;
    private Animator prisonnier;
    private GameObject cameraMain;


    void Awake(){
        prisonnier=GameObject.Find("Character/Prisoner").GetComponent<Animator>();
    }
    void Start(){
      
    }
    void Update(){

        
    }
    private void OnTriggerEnter(Collider coll){
        if (coll.gameObject.tag=="Player"){
                Porte.Play("Ouverture",0,0.0f);
                gameObject.SetActive(false);
                prisonnier.Play("AvanceMain",0,0.0f);
           
        }
        if (coll.gameObject.tag=="Prisoner"){
            Porte.Play("Fermeture",0,0.0f);
            //prisonnier.Play("idle");
            gameObject.SetActive(false);
        }
    }
}
