using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GanttOkapi : MonoBehaviour {

    public bool desplaza;
    public bool escalaIzq;
    public bool escalaDer;

    public float sizeInicial = 300;
    

    public Vector2 puntoInicial;
    public Vector2 puntoActual;
    public Vector2 distancia;

    public bool go;

	void Update () {

        if (escalaIzq && go){

            distancia = puntoActual - puntoInicial;

            //puntoActual = new Vector2 (Input.mousePosition.x, Input.mousePosition.z);

            //distancia = Vector2.Distance(puntoInicial, puntoActual);

            //GetComponent<RectTransform>().sizeDelta = new Vector2(300 + (distancia - Input.mousePosition.z), 50);
        }
	}
		
    public void Mov_On(){
        go = true;
    }

    public void Mov_Off(){
        go = false;
    }



	public void escalaIzquierda_ON () {

        escalaIzq = true;
        puntoInicial = new Vector2 (Input.mousePosition.x, Input.mousePosition.z);


        GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 0.5f);
        GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 0.5f);
        GetComponent<RectTransform>().pivot = new Vector2(1.0f, 0.5f);
    }

    public void escalaIzquierda_OFF(){
        escalaIzq = false;
    }
}
