using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script_Descanso : MonoBehaviour {

    public bool selecc;
    public bool mover;
    public float velocidad;
    public Vector3[] posMapa = new Vector3[2];

	void Start () {
		
	}
	
	void Update () {

        if (selecc){
            if (!mover){
                if (Input.GetMouseButtonDown(0)){Raycast();}
            }else{
                transform.position = Vector3.MoveTowards(transform.position, posMapa[1], Time.deltaTime * velocidad);
                print(transform.position);

                if (transform.position == posMapa[1]){
                    Llegada();
                    print("Llega");
                }
            }
        }
	}

    private void Llegada(){
        posMapa[0] = Vector3.zero;
        posMapa[1] = Vector3.zero;
        selecc = false;
        mover = false;
    }

    private void Raycast() {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)){
                if (hit.collider.name == ("Zona")){
                    posMapa[1] = hit.point;
                    mover = true;
                }
            }
    }

    private void OnTriggerEnter(Collider other){
        print("Entra en zona");
    }

    public void SeleccPersonaje(){
        selecc = !selecc;

        Color aux = GetComponent<Image>().color;
        if (selecc) {
            GetComponent<Image>().color = new Vector4(aux.r, aux.g, aux.b, 255);
            posMapa[0] = transform.position;
        }else { GetComponent<Image>().color = new Vector4(aux.r, aux.g, aux.b, 0); }
    }
}
