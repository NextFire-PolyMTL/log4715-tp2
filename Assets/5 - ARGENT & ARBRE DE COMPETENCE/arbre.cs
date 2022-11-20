using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arbre : MonoBehaviour
{
    public GameObject canva;
    private bool allume;
    public GameObject TextMenu;
    public GameObject image;
    public GameObject argent;


    // Start is called before the first frame update
    void Start()
    {
        canva=GameObject.Find("Arbre/Canvas");
        allume=false;
        TextMenu=GameObject.Find("Level/Canvas");

    }


    // Update is called once per frame
    void Update()
    {
        menu();
    }

    void menu(){
        if(Input.GetKeyDown(KeyCode.M)){
            if (allume==false){
                allume=true;
            }
            else{
                allume=false;
            } 
        }
        canva.SetActive(allume);
        TextMenu.SetActive(!allume);
    }
    void Texte (){
        if (allume==false){

        }
    }
}
