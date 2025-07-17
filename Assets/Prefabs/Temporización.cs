using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporización : MonoBehaviour {

    public RectTransform ui;
    public bool mover;
    public bool escaladoIzq;
    public bool escaladoDer;

    public float xInicial;
    public float xActual;
    public float factor;
    public float sizeDelta_X;


    void Update(){

        if (Input.GetKeyDown(KeyCode.B)){
            Vector3 scale = GetComponent<RectTransform>().localScale;
        }

        if (Input.GetMouseButton(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)){
                if (!escaladoIzq || !escaladoDer){
                    if (hit.collider.transform.parent.gameObject == this.gameObject && hit.collider.gameObject == transform.GetChild(0).gameObject){

                        //ui.pivot = new Vector2(1, 0.5f);
                        if (!escaladoIzq){
                            xInicial = Input.mousePosition.y;
                        }

                        sizeDelta_X = ui.sizeDelta.x;
                        escaladoIzq = true;
                        transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = false;
                        print("0");
                    }

                    if (hit.collider.transform.parent.gameObject == this.gameObject && hit.collider.gameObject == transform.GetChild(1).gameObject){

                        //ui.pivot = new Vector2(0, 0.5f);

                        if (!escaladoDer){
                            xInicial = -Input.mousePosition.y;
                        }

                        sizeDelta_X = ui.sizeDelta.x;
                        escaladoDer = true;
                        transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
                        print("1");
                    }
                }

            }
            if (escaladoIzq && !mover){
                print(this.gameObject);
                xActual = Input.mousePosition.y;
                factor = (xInicial - xActual) + sizeDelta_X;
                if (factor >= 0){
                    ui.sizeDelta = new Vector2(factor, ui.sizeDelta.y);
                }
            }

            if (escaladoDer && !mover){
                print(this.gameObject);
                xActual = -Input.mousePosition.y;
                factor = (xInicial - xActual) + sizeDelta_X;
                if (factor >= 0){
                    ui.sizeDelta = new Vector2(factor, ui.sizeDelta.y);
                }
            }

            if (mover && !escaladoIzq && !escaladoDer){
                xActual = -Input.mousePosition.y;
                factor = (xInicial - xActual) + sizeDelta_X;
                ui.anchoredPosition = new Vector2(factor, ui.anchoredPosition.y);
                
            }
        }

        // AL LEVANTAR EL DEDO REINICIO TODAS LAS VARIABLES...
        if (Input.GetMouseButtonUp(0) && (escaladoDer || escaladoIzq || mover)){
            print("levantas");
            escaladoDer = false;
            escaladoIzq = false;
            mover = false;

            transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = true;
            transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = true;

            xInicial = 0;
            xActual = 0;
            sizeDelta_X = 0;
            factor = 0;
        }
    }

    public void OnMouseDown(){

        if (!escaladoIzq || !escaladoDer){
            print("vas a desplazar..");
            xInicial = -Input.mousePosition.y;
            sizeDelta_X = ui.anchoredPosition.x;
            mover = true;
            transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
            transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
