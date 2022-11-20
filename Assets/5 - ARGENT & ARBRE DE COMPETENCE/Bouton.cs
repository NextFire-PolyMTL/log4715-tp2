using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Windows.Forms;
using UnityEngine.UI;
//using nombre_xp;
public class Bouton : MonoBehaviour
{
    private PlayerControlerTechnoFrag NombreS;
 //   private Script nombreXP;

 //   public Button bouton1;
  //  private Button[] Boutons;
    private List<Button> bouton_list;
    private List<GameObject> fleche_list;
    public int[] active_list;
    private List<GameObject> coûts;
    private int[] coûts_list;
    // Start is called before the first frame update
    void Awake(){
      bouton_list=new List<Button>();
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp1").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp2").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp3").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp10").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp4").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp6").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp5").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp8").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp7").GetComponent<Button>());
      bouton_list.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp9").GetComponent<Button>());
      active_list=new int[]{0,0,0,0,0,0,0,0,0,0};
      //nombreScript=GetComponent<nombre_xp>();
      //NombreS=GameObject.Find("Arbre/Canvas/nombre");
      NombreS=GameObject.Find("Character/MaleFree1").GetComponent<PlayerControlerTechnoFrag>();
    //  nombreXP=NombreS.GetComponent<Script>();
    //  bouton1=GameObject.Find("Arbre/Canvas/Boutons/Comp1").GetComponent<Button>();
     // Boutons.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp1").GetComponent<Button>());
    // Boutons[0]=GameObject.Find("Arbre/Canvas/Boutons/Comp1").GetComponent<Button>();
      fleche_list=new List<GameObject>();
      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche1/noir1"));
      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche1/noir2"));
      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche1/noir3"));

      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche2/noir1"));
      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche2/noir2"));

      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche3/noir1"));
      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche3/noir2"));

      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche4/noir1"));

      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche5/noir1"));
      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche5/noir2"));

      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche6/noir1"));
      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche6/noir2"));

      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche7/noir1"));

      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche8/noir1"));

      fleche_list.Add(GameObject.Find("Arbre/Canvas/Fleches/Fleche9/noir1"));

      coûts=new List<GameObject>();
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp1/niv1"));
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp2/niv2"));
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp3/niv3"));
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp10/niv4"));
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp4/niv5"));
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp6/niv6"));
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp5/niv7"));
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp8/niv8"));
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp7/niv9"));
      
      coûts.Add(GameObject.Find("Arbre/Canvas/Boutons/Comp9/niv10"));

      coûts_list=new int[]{130,200,200,250,340,340,400,470,470,620};
      
    }   
    void Start()
    {
      for (int i=1; i<10 ; i++){
        bouton_list[i].interactable=false;
        ColorBlock couleur=bouton_list[i].colors;
        couleur.disabledColor=Color.black;
        bouton_list[i].colors=couleur;
      }
      
 

    }

    // Update is called once per frame
    void Update()
    {
        //int nb=NombreS.GetComponent<nombre_xp>().xp;
        int nb=NombreS.xp;
       // List<Button> BoutonsActifs=new List<Button>();
        for (int i=0; i<10;i++){
          if (bouton_list[i].interactable){
            if (nb-coûts_list[i]>0){
              coûts[i].GetComponent<TMPro.TextMeshProUGUI>().color=Color.green;
            }
            else{
              coûts[i].GetComponent<TMPro.TextMeshProUGUI>().color=Color.red;
            }
           // BoutonsActifs.Add(bouton_list[i]);
          }
        }
        
        
     //   for (int i=0;i<10;i++){
      //    if (nb-coûts_list[i]>0){
      //      coûts[i].GetComponent<TMPro.TextMeshProUGUI>().color=Color.green;
      //    } 
      //  }
        
    }
    public void DebloqueSkill1(){

      //bool c1=NombreS.GetComponent<nombre_xp>().EnleverXP(130);
      bool c1=NombreS.EnleverXP(130);
      if (c1==true){
        bouton_list[1].interactable=true;
        ColorBlock couleur=bouton_list[1].colors;
        couleur.disabledColor=Color.white;
        bouton_list[1].colors=couleur;

        bouton_list[2].interactable=true;
        ColorBlock couleur2=bouton_list[2].colors;
        couleur2.disabledColor=Color.white;
        bouton_list[2].colors=couleur2;

        bouton_list[0].interactable=false;
        active_list[0]=1;
        ColorBlock couleur3=bouton_list[0].colors;
        couleur3.disabledColor=Color.green;
        bouton_list[0].colors=couleur3;

        fleche_list[0].SetActive(false);
        fleche_list[1].SetActive(false);
        fleche_list[2].SetActive(false);
        coûts[0].SetActive(false);
       // coûts.RemoveAt(0);
      }
       

    }
    public void DebloqueSkill2(){
      //bool c2=NombreS.GetComponent<nombre_xp>().EnleverXP(200);
      bool c2=NombreS.EnleverXP(200);
      if (c2==true){
        bouton_list[1].interactable=false;
        active_list[1]=1;

        ColorBlock couleur=bouton_list[1].colors;
        couleur.disabledColor=Color.green;
        bouton_list[1].colors=couleur;

        fleche_list[3].SetActive(false);
        fleche_list[4].SetActive(false);
     //   bouton_list[1].disabledColor=Color.green;
        if (active_list[2]==1){
          bouton_list[3].interactable=true;
        }
        bouton_list[4].interactable=true;
        coûts[1].SetActive(false);
        //coûts.RemoveAt(1);
      } 
     
    }

    public void DebloqueSkill3(){
      //bool c3=NombreS.GetComponent<nombre_xp>().EnleverXP(200);
      bool c3=NombreS.EnleverXP(200);
      if (c3==true){
        bouton_list[2].interactable=false;
        active_list[2]=1;


        ColorBlock couleur=bouton_list[2].colors;
        couleur.disabledColor=Color.green;
        bouton_list[2].colors=couleur;
     //   bouton_list[2].disabledColor=Color.green;
        if (active_list[1]==1){
          bouton_list[3].interactable=true;
        }
        bouton_list[5].interactable=true;
        
        fleche_list[5].SetActive(false);
        fleche_list[6].SetActive(false);
        coûts[2].SetActive(false);
      //  coûts.RemoveAt(2);
      }
         

      
    }
    public void DebloqueSkill4(){
      //bool c4=NombreS.GetComponent<nombre_xp>().EnleverXP(250);
      bool c4=NombreS.EnleverXP(250);
      if (c4==true){
        bouton_list[3].interactable=false;
        active_list[3]=1;


        ColorBlock couleur=bouton_list[3].colors;
        couleur.disabledColor=Color.green;
        bouton_list[3].colors=couleur;
   //     bouton_list[3].disabledColor=Color.green;
        if (active_list[4]==1 && active_list[5]==1){
          bouton_list[6].interactable=true;
        }
        fleche_list[7].SetActive(false);
        coûts[3].SetActive(false);
    //    coûts.RemoveAt(3);
      }
     
    }
    public void DebloqueSkill5(){
      //bool c5=NombreS.GetComponent<nombre_xp>().EnleverXP(340);
      //Debug.Log(c5);
      bool c5=NombreS.EnleverXP(340);
      
      ColorBlock couleur=bouton_list[4].colors;
      if (c5){
        bouton_list[4].interactable=false;
        active_list[4]=1;
        couleur.disabledColor=Color.green;
        bouton_list[4].colors=couleur;
   //     bouton_list[4].disabledColor=Color.green;
        if (active_list[3]==1 && active_list[5]==1){
          bouton_list[6].interactable=true;
        }
        bouton_list[7].interactable=true;
        fleche_list[8].SetActive(false);
        fleche_list[9].SetActive(false);
        coûts[4].SetActive(false);
     //   coûts.RemoveAt(4);
      } 
        
      //if (c5==false){
        
     // }    
    }
    public void DebloqueSkill6(){
      //bool c6=NombreS.GetComponent<nombre_xp>().EnleverXP(340);
      bool c6=NombreS.EnleverXP(340);
      if (c6==true){
        bouton_list[5].interactable=false;
        active_list[5]=1;

        ColorBlock couleur=bouton_list[5].colors;
        couleur.disabledColor=Color.green;
        bouton_list[5].colors=couleur;
   //     bouton_list[5].disabledColor=Color.green;
        if (active_list[3]==1 && active_list[4]==1){
          bouton_list[6].interactable=true;
        }
        bouton_list[8].interactable=true;
        fleche_list[10].SetActive(false);
        fleche_list[11].SetActive(false);
        coûts[5].SetActive(false);
     //   coûts.RemoveAt(5);
      }

      
      
    }
    public void DebloqueSkill7(){
      //bool c7=NombreS.GetComponent<nombre_xp>().EnleverXP(400);
      bool c7=NombreS.EnleverXP(400);
      if (c7==true){
        active_list[6]=1;
        bouton_list[6].interactable=false;

        ColorBlock couleur=bouton_list[6].colors;
        couleur.disabledColor=Color.green;
        bouton_list[6].colors=couleur;
   //     bouton_list[6].disabledColor=Color.green;
        if (active_list[7]==1 && active_list[8]==1){
          bouton_list[9].interactable=true;
        }
        fleche_list[13].SetActive(false);
        coûts[6].SetActive(false);
     //   coûts.RemoveAt(6);
      }

      
    }
    public void DebloqueSkill8(){
      //bool c8=NombreS.GetComponent<nombre_xp>().EnleverXP(470);
      bool c8=NombreS.EnleverXP(470);
      if (c8==true){
        active_list[7]=1;
        bouton_list[7].interactable=false;

        ColorBlock couleur=bouton_list[7].colors;
        couleur.disabledColor=Color.green;
        bouton_list[7].colors=couleur;
     //   bouton_list[7].disabledColor=Color.green;
        if (active_list[6]==1 && active_list[8]==1){
          bouton_list[9].interactable=true;
        }
        fleche_list[12].SetActive(false);
        coûts[7].SetActive(false);
      //  coûts.RemoveAt(7);
      }

      
    }
    public void DebloqueSkill9(){
      //bool c9=NombreS.GetComponent<nombre_xp>().EnleverXP(470);
      bool c9=NombreS.EnleverXP(470);
      if (c9==true){
        active_list[8]=1;
        bouton_list[8].interactable=false;

        ColorBlock couleur=bouton_list[8].colors;
        couleur.disabledColor=Color.green;
        bouton_list[8].colors=couleur;
     //   bouton_list[8].disabledColor=Color.green;
        if (active_list[7]==1 && active_list[6]==1){
          bouton_list[9].interactable=true;
        }
        fleche_list[14].SetActive(false);
        coûts[8].SetActive(false);
      //  coûts.RemoveAt(8);
      }

      
    }
    public void DebloqueSkill10(){
      //bool c10=NombreS.GetComponent<nombre_xp>().EnleverXP(620);
      bool c10=NombreS.EnleverXP(620);
      if (c10==true){
        active_list[9]=1;
        bouton_list[9].interactable=false;

        ColorBlock couleur=bouton_list[9].colors;
        couleur.disabledColor=Color.green;
        bouton_list[9].colors=couleur;
    //    bouton_list[9].disabledColor=Color.green;
        coûts[9].SetActive(false);
     //   coûts.RemoveAt(9);
      }

    }

     

}
