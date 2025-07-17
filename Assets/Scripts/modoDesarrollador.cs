using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modoDesarrollador : MonoBehaviour {
    
    bool modoDesar;
    public int toques;

    public bool cuentaAtras;
    public float cuenta;
    float tiempo;

    public void Update(){

        if (cuentaAtras){
            tiempo += Time.deltaTime;
            cuenta = Mathf.Floor(tiempo % 60);

            // Bien! Entras en modoDesarrollador
            if (toques == 5){
                print("liloli");
                transform.GetChild(0).gameObject.SetActive(true);
                cuentaAtras = false;
                toques = 0;
                cuenta = 0;
                tiempo = 0;
            }

            // mehhh, llegas tarde y se reinician las variables
            if (cuenta == 2){
                cuentaAtras = false;
                toques = 0;
                cuenta = 0;
                tiempo = 0;
            }
        }
    }

    public void modoDesarrolladorToques(){

        // INICIO DEL PROCESO...
        if (!cuentaAtras){
            cuentaAtras = true;
        }else{
             if (cuentaAtras){
                cuenta = 0;
                tiempo = 0;
            }
        }

        toques++;
    }

    public void modoDesarrolladorSalir(){
        transform.GetChild(0).gameObject.SetActive(false);
        toques = 0;
    }

    public void ReiniciarSlots(){
        PlayerPrefs.DeleteAll();
        //transform.root.GetComponent<GenericSave>().Delete();
    }
}
