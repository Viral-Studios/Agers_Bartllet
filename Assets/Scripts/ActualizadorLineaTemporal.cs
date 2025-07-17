using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualizadorLineaTemporal : MonoBehaviour {

    public GameObject padreVirtual;

    /*
    public Herramientas_Add script;

    public void Awake(){
        actualiza();
    }

    public bool actu;
    public int num;
    private void Update(){
        if (actu){
            Invoke("ActualizaLapso", 0.05f);
            actu = false;
        }
    }

    public void actualiza(){
        for (int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<BloqueLineaTemporal>().numerito.text = "" + (i + 1);
            transform.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura.GetComponent<InputField>().text = "" + (i + 1);
            transform.GetChild(i).GetComponent<BloqueLineaTemporal>().posHijoInicial = i;
        }
    }

    public void ActualizaLapso(){

        print("entra aquí");
        for (int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<BloqueLineaTemporal>().numerito.text = "" + (i + 1);
            transform.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura.GetComponent<InputField>().text = "" + (i + 1);
            transform.GetChild(i).GetComponent<BloqueLineaTemporal>().posHijoInicial = i;
        }

        if (transform.childCount > 8){
            if (num == 2){
                GetComponent<RectTransform>().sizeDelta -= Vector2.up * 110;
                num = 0;
            }else{
                num++;

                if (script.num > 0){
                    script.num--;
                }
            }
        }
    }

    public void destruir(int num){

        Destroy(transform.GetChild(num).gameObject);
        actualiza();
    }
    */
}
