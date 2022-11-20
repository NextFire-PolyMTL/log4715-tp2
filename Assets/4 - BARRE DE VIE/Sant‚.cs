using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Lifebar;
public class Santé : MonoBehaviour
{
    //Variable pour le setup de la lifebar;
    public int PV_max=10;
    public int PV_actuels;
    [SerializeField] BarDeVie bar_de_vie; 
    [SerializeField] GameObject gameover;
    [SerializeField] int Dégats_Projectiles;
    [SerializeField] int Dégats_Lave;

    // Variables pour la lave
    //[SerializeField] LayerMask ThisFloorIsLava;
    bool _Lavaed { get; set; }
    private float intervalle; 

    // Variables pour le piège:
    private GameObject eboul;
    private GameObject press;
    private bool active;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        PV_actuels=PV_max;
        bar_de_vie=GetComponent<BarDeVie>();
        gameover=GameObject.Find("Character/MaleFree1/Canvas/GameOver");
        //eboul=GetComponent<GameObject>();
        _Lavaed=false;
        intervalle=0;

        eboul=GameObject.Find("Level/eboul");
        press=GameObject.Find("Level/Pressure");
        active=false;
        time=0;
        Dégats_Projectiles=2;
        Dégats_Lave=2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            PV_actuels=PV_max;

        }
        if (_Lavaed){
            intervalle+=Time.deltaTime;
            if (intervalle>0.5){
                Degats(Dégats_Lave);
                intervalle=0;
            }
            
        }
        if (PV_actuels<=0){
            gameover.SetActive(true);
        }
        if (PV_actuels>0){
            gameover.SetActive(false);
        }


        if (active==true){
            //trap1.SetActive(true);
            //trap2.SetActive(true);
            //trap3.SetActive(true);
            //trap4.SetActive(true);
            //trap5.SetActive(true);
            eboul.SetActive(true);
            time+=Time.deltaTime;
        }
        else{
            eboul.SetActive(false);
        }
        if (time==0.8){
            Degats(5);
        }
        if (time>1){
            //trap1.SetActive(false);
            //trap2.SetActive(false);
            //trap3.SetActive(false);
            //trap4.SetActive(false);
            //trap5.SetActive(false);
            eboul.SetActive(false);
        }

    }

    void Degats(int degats)
    {
        if (PV_actuels-degats>=0)
        {
            PV_actuels=PV_actuels-degats;
        }
        else 
        {
            PV_actuels=0;
        }
        
        bar_de_vie.setLife(PV_actuels);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == 9){
            _Lavaed=true;
        }  //créer une animation?
        if (coll.gameObject.layer == 10){
            active=true;
        }
        if (coll.gameObject.layer == 11){
            Degats(Dégats_Projectiles);
            
        }
    }

}
