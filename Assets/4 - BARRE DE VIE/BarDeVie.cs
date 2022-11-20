using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarDeVie : MonoBehaviour
{
    public Slider santé;
    
    public void setLife(int pv)
    {
        santé.value=pv;
    }
    public void setMaxLife(int maxpv)
    {
        santé.maxValue=maxpv;
        santé.value=maxpv;
    }
    private void Start(){
        //player_vie= gameObject.FindGameObjectWithTag("Player").GetComponent<Santé>;
        //santé.value=player_vie.PV_actuels;
        //santé.maxValue=player_vie.PV_max;
    }
}
