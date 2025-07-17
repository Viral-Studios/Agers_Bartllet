using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dafo : MonoBehaviour {

    public GameObject padrePetalos;
    public List<bool> petaloSelecc = new List<bool> (4) { false, false, false, false };

    public void dafoSelecc (int num){
        for (int i = 0; i < 4; i++){

            if (num == i){
                if (!petaloSelecc[i]){
                    petaloSelecc[i] = true;
                    padrePetalos.gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Animator>().enabled = true;
                }
                else{
                    petaloSelecc[i] = false;
                    padrePetalos.gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Animator>().SetTrigger("1");
                }
            }
            else{
                if (petaloSelecc[i]){
                    petaloSelecc[i] = false;
                    padrePetalos.gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Animator>().SetTrigger("1");
                }
            }
        }
    }
}
