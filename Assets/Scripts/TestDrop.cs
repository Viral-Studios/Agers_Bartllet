using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDrop : MonoBehaviour {

    public bool fuera;
    public List<Dropdown> Listas;
    public List<Image> elegirReproductor;

    public void ElegirReprod (int num){

        for (int i = 0; i < elegirReproductor.Count; i++){

            if (i == num){

                if (!Listas[i].gameObject.activeSelf){
                    elegirReproductor[i].color = Color.cyan;
                    Listas[i].gameObject.SetActive(true);
                }else{
                    elegirReproductor[i].color = Color.white;
                    Listas[i].gameObject.SetActive(false);
                }
            }
            else{
                elegirReproductor[i].color = Color.white;
                Listas[i].gameObject.SetActive(false);
            }
        }
    }
}
