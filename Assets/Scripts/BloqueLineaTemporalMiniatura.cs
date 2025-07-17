using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloqueLineaTemporalMiniatura : MonoBehaviour{

    public Herramientas_Add gestor;

    public Transform padrePrefab;
    public Transform parent;
    public Transform scrollII;
    public Transform scrollIII;
    public Transform hermanoTocado;

    public bool scroll_II;
    public bool scroll_III;

    public int posHijo;
    public bool cambioHijo;
    public bool volver = false;
    public bool candado = true;

    float distance = 10;

    public void Start (){

        gestor = padrePrefab.GetComponent<BloqueLineaTemporal>().script;
        parent = padrePrefab.GetComponent<BloqueLineaTemporal>().padreVirtual;
        scrollII = padrePrefab.GetComponent<BloqueLineaTemporal>().script.comInterna_gameob.transform.GetChild(0).transform;
        scrollIII = padrePrefab.GetComponent<BloqueLineaTemporal>().script.comExterna_gameob.transform.GetChild(0).transform;

        OnMouseDrop();
    }

    public void OnMouseDrag(){

        if (!padrePrefab.GetComponent<BloqueLineaTemporal>().bloqueo && transform.parent != parent){
            transform.SetParent(parent.transform);

            if (GetComponent<BoxCollider>().size.x == 50.0f){
                GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 0);
            }
        }

        // SI EL PADRE ES EL VIRTUAL DRAGGEAMOS LA MINIATURA...
        if (transform.parent == parent){
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            this.gameObject.transform.position = new Vector3(objPosition.x, 0, objPosition.z);
        }
    }

    public void OnMouseDrop(){

        if (!gestor.modoPapeleraLineaTemporal) {

            if (volver) {
                transform.SetParent(padrePrefab.transform);

                if (this.gameObject.tag == "MiniaturaLT_Izq"){
                    transform.localPosition = new Vector3 (115, 20, 0);
                }

                if (this.gameObject.tag == "MiniaturaLT_Der"){
                    transform.localPosition = new Vector3(115, -20, 0);
                }

                //print("Sueltas correctamente");
                volver = false;
                candado = true;
            }

            if (scroll_II) {

                if (transform.parent != scrollII.transform){
                    transform.SetParent(scrollII.transform);

                    if (cambioHijo){
                        //print("cambias");
                        transform.SetSiblingIndex(posHijo);
                    }else{
                        //print("final de la cola");
                        transform.SetAsLastSibling();
                    }
                }

                
                GetComponent<BoxCollider>().size = new Vector3(50, 50, 1);
                GetComponent<InputField>().text = padrePrefab.GetComponent<BloqueLineaTemporal>().numerito.text + "";

                candado = false;
            }

            if (scroll_III) {

                if (transform.parent != scrollIII.transform){
                    transform.SetParent(scrollIII.transform);

                    if (cambioHijo){
                        transform.SetSiblingIndex(posHijo);
                    }else{
                        //print("final de la cola");
                        transform.SetAsLastSibling();
                    }
                }

                GetComponent<BoxCollider>().size = new Vector3(50, 50, 1);
                GetComponent<InputField>().text = padrePrefab.GetComponent<BloqueLineaTemporal>().numerito.text + "";

                candado = false;
            }

            if (GetComponent<BoxCollider>().size.x != 50.0f){
                GetComponent<BoxCollider>().size = new Vector3(50.0f, 50.0f, 0);
            }

            cambioHijo = false;
        }
    }

    #region OnTriggers...
    public void OnTriggerEnter(Collider other) {

        #region 
        if (other.gameObject.name == "contenidoII"){
            scroll_II = true;
            scroll_III = false;
        }

        if (other.gameObject.name == "contenidoIII")
        {
            scroll_III = true;
            scroll_II = false;
        }
        #endregion
    }

    public void OnTriggerStay(Collider other){

        if (other.gameObject == padrePrefab.gameObject && !volver){
            volver = true;
        }

        if (hermanoTocado == null && transform.parent == padrePrefab.GetComponent<BloqueLineaTemporal>().padreVirtual && !candado 
            && (other.gameObject.tag == "MiniaturaLT_Der" || other.gameObject.name == "MiniaturaLT_Izq")){

            hermanoTocado = other.gameObject.transform;
            //print("Hermano Tocado reasignado " + other.gameObject.name);
        }

        if (other.gameObject.transform.parent == scrollII){
            for (int i = 0; i < scrollII.transform.childCount; i++){
                if (other.gameObject == scrollII.transform.GetChild(i).gameObject){

                    cambioHijo = true;

                    if (posHijo != i && hermanoTocado != scrollII.transform.GetChild(i).gameObject){
                        //print("pos hijo II " + i);
                        posHijo = i;
                    }
                }
            }

            if (candado) { candado = false; }
        }

        if (other.gameObject.transform.parent == scrollIII){
            for (int i = 0; i < scrollIII.transform.childCount; i++){
                if (other.gameObject == scrollIII.transform.GetChild(i).gameObject){

                    cambioHijo = true;

                    if (posHijo != i && hermanoTocado != scrollIII.transform.GetChild(i).gameObject){
                        //print("pos hijo III " + i);
                        posHijo = i;
                    }
                }
            }

            if (candado) { candado = false; }
        }
    }

    public void OnTriggerExit(Collider other){

        if (other.gameObject == padrePrefab.gameObject && volver){
            volver = false;
        }

        if (hermanoTocado != null && other.gameObject == hermanoTocado.gameObject){
            hermanoTocado = null;
            cambioHijo = false;
        }

        if (other.gameObject.name == "contenidoII" || other.gameObject.name == "contenidoIII")
        {
            scroll_II = false;
            scroll_III = false;
            cambioHijo = false;

            if (candado) { candado = false; }
        }

        if (other.gameObject.name == "BloqueLineaTemporalMiniatura"){
            cambioHijo = false;
        }
    }
    #endregion
}
