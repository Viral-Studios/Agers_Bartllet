using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auxiliar : MonoBehaviour {

    public Sprite sprit;

    public void AnimatorOff(){
        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().enabled = false;
        print("animatorOff");

        if (sprit != null){
            GetComponent<UnityEngine.UI.Image>().sprite = sprit;
        }

        return;
    }

    public void AnimatorOff_Guardaspaldas () {

        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().enabled = false;
        print("animatorOff");

        if (sprit != null){ GetComponent<UnityEngine.UI.Image>().sprite = sprit; }

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        return;
    }

    public void InicioOff(){
        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().enabled = false;

        if (GetComponent<AudioSource>()){
            GetComponent<AudioSource>().enabled = false;
        }
    }

    public void AnimatorOff_Tlf(){
        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().enabled = false;
        transform.GetChild(2).GetComponent<UnityEngine.UI.Button>().interactable = false;

        print("animatorOff_Tlf");
    }

    public void AnimatorGameObjectOff(){
        GetComponent<Animator>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
