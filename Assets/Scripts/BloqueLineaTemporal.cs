using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BloqueLineaTemporal : MonoBehaviour {

    public Herramientas_Add script;
    public GameObject miniatura_I;
    public GameObject miniatura_II;

    public Text numerito;

    public Transform parent;
    public Transform padreVirtual;

    public bool scroll_I;
    public bool hijoRegresa;
    public bool tocaHermano;
    public bool destruir;
    public bool bloqueo;

    public int posHijoInicial;
    public int posHijo;
    float distance = 10;

    public void Awake(){

        script = transform.root.GetComponent<GenericSave>().scriptHerramientas_Add;
        parent = transform.parent;
        padreVirtual = transform.root.gameObject.GetComponent<Root>().padreVirtual;

        miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().gestor = script;
        miniatura_II.GetComponent<BloqueLineaTemporalMiniatura>().gestor = script;

        //this.gameObject.name = "BloqueLineaTemporal";
    }
       
    public void OnMouseDown(){
        if (script.modoPapeleraLineaTemporal){
            script.listaBloques.Remove(this.gameObject);
            //script.LineaTemporal_Update();
            script.num--;
            Destroy(this.gameObject.GetComponent<BloqueLineaTemporal>().miniatura_I.gameObject);
            Destroy(this.gameObject.GetComponent<BloqueLineaTemporal>().miniatura_II.gameObject);
            Destroy(this.gameObject);
        }
    }
    public void OnMouseDrag(){

        if (!script.modoPapeleraLineaTemporal && !bloqueo){
            // SI LA MINIATURA ESTÁ OCULTA...
            /*if (!miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().primeraVez){
                miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().primeraVez = true;
                //miniatura.SetActive(true);
            }*/

            // SI LA MINIATURA NO ESTÁ OCULTA...
            //else{
                // SE PUEDE ARRASTRAR
                //if (!miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().primeraVez){
                    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                    Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    this.gameObject.transform.position = new Vector3(objPosition.x, 0, objPosition.z);

                    // SE EMPARENTA CON EL PADRE VIRTUAL...
                    if (transform.parent != padreVirtual.transform){
                        transform.SetParent(padreVirtual.transform);
                    }
                //}
            //}
        }
    }
    public void OnMouseDrop(){

        if (!script.modoPapeleraLineaTemporal){
            /*if (hijoRegresa) { miniatura.SetActive(false); }*/

            //if (miniatura_I.activeSelf && !miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().volver){

                transform.SetParent(parent.transform);

                if (tocaHermano){
                    transform.SetSiblingIndex(posHijo);
                    posHijoInicial = posHijo;
                    tocaHermano = false;

                    for (int i = 0; i < transform.parent.childCount-1; i++){
                        transform.parent.GetChild(i).GetComponent<BloqueLineaTemporal>().posHijoInicial = i;
                    }
                }else{
                    transform.SetSiblingIndex(posHijoInicial);
                }
                GetComponent<BoxCollider>().size = new Vector3(280.0f, 100.0f, 1.0f);
                //print("sueltas el padre");
            //}
        }
    }

    public void OnTriggerEnter(Collider other){

        /*if (other.gameObject == miniatura_I)
        {
            hijoRegresa = true;
        }*/

        /*if (miniatura_I.activeSelf && !miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().volver){
            if (other.gameObject.name == ("contenidoI")){
                scroll_I = true;
            }
        }*/
    }
    public void OnTriggerExit(Collider other){
        /*if (other.gameObject == miniatura_I)
        {
            hijoRegresa = false;
        }*/

        if (other.gameObject.tag == "BloqueLT"){
            tocaHermano = false;
        }

        if (other.gameObject.name == "zona"){
            bloqueo = true;
            //print("activa bloqueo");
        }
    }
    public void OnTriggerStay(Collider other){

        //if (miniatura_I.activeSelf){
            if (other.gameObject.tag == ("BloqueLT")){
                for (int i = 0; i < parent.transform.childCount; i++){
                    if (other.gameObject == parent.transform.GetChild(i).gameObject && posHijo != i){
                        //print("pos hijo I " + i);
                        posHijo = i;
                    }
                }
            }

            if (other.gameObject.name == ("fin") && !tocaHermano){
                //print("está dentro");
                tocaHermano = true;
                posHijo = (parent.transform.childCount - 1);
            }
        //}

        if (other.gameObject.tag == "BloqueLT" && !tocaHermano){
            tocaHermano = true;
        }

        if (other.gameObject.name == "zona" && bloqueo){
            bloqueo = false;
            //print("desactiva bloqueo");
        }
    }
}

#region
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloqueLineaTemporal : MonoBehaviour {

    public Transform parent;
    public Transform scrollI;
    public Transform scrollII;
    public Transform scrollIII;

    public bool scroll_I;
    public bool scroll_II;
    public bool scroll_III;

    public string frase;

    public GameObject clone;

    public void Awake(){
        //parent = GameObject.Find ("Bartler").gameObject.GetComponent<Root>().parent;
        //scrollI = GameObject.Find("Bartler").gameObject.GetComponent<Root>().scrollI;
        //scrollII = GameObject.Find("Bartler").gameObject.GetComponent<Root>().scrollII;
        //scrollIII = GameObject.Find("Bartler").gameObject.GetComponent<Root>().scrollIII;

        //this.gameObject.name = "BloqueLineaTemporal";
    }

    float distance = 10;

    public void OnMouseDrag(){
       
        if (transform.parent != parent.transform){
            transform.SetParent(parent.transform);

            if (transform.parent == scrollI.transform && frase != GetComponent<InputField>().text){
                print("guarda la frase");
                frase = "" + GetComponent<InputField>().text;
            }
        }

        if (transform.parent == scrollI.transform && !scroll_I){
            scroll_I = true;
        }

        if (transform.parent == scrollII.transform && !scroll_II){
            scroll_II = true;
        }

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        this.gameObject.transform.position = new Vector3(objPosition.x, 0, objPosition.z); 
    }

    public void OnMouseDrop(){

        if (scroll_I){
            transform.SetParent(scrollI.transform);

            if (cambioHijo){
                transform.SetSiblingIndex(posHijo);
                GetComponent<InputField>().text = "" + frase;
            }
            GetComponent<BoxCollider>().size = new Vector3(280.0f, 100.0f, 1.0f);
        }
        if (scroll_II){
            transform.SetParent(scrollII.transform);

            if (cambioHijo){
                transform.SetSiblingIndex(posHijo);

                if (GetComponent<InputField>().text != ""){
                    GetComponent<InputField>().text = "";
                }
            }
            GetComponent<BoxCollider>().size = new Vector3 (50, 50, 1);
            GetComponent<InputField>().text = "" + posHijo;
        }

        if (scroll_III){
            transform.SetParent(scrollIII.transform);

            if (cambioHijo){
                transform.SetSiblingIndex(posHijo);

                if (GetComponent<InputField>().text != ""){
                    GetComponent<InputField>().text = "";
                }
            }
            GetComponent<BoxCollider>().size = new Vector3(50, 50, 1);
            GetComponent<InputField>().text = "" + posHijo;
        }
    }

    public int posHijo;
    public bool cambioHijo;

    public void OnTriggerEnter(Collider other){

        #region
        if (other.gameObject.name == ("contenidoI")){
            scroll_I = true;
            scroll_II = false;
            scroll_III = false;
        }

        if (other.gameObject.name == ("contenidoII")){
            scroll_II = true;
            scroll_I = false;
            scroll_III = false;
        }

        if (other.gameObject.name == ("contenidoIII")){
            scroll_III = true;
            scroll_II = false;
            scroll_I = false;
        }
        #endregion
    }

    public void OnTriggerStay(Collider other){

        if (other.gameObject.name == ("BloqueLineaTemporal")){


            if (scrollI){
                for (int i = 0; i < scrollI.transform.childCount; i++){
                    if (other.gameObject == scrollI.transform.GetChild(i).gameObject){
                        print("pos hijo I " + i);
                        posHijo = i;
                    }
                }
                cambioHijo = true;
            }

            if (scrollII){
                for (int i = 0; i < scrollII.transform.childCount; i++){
                    if (other.gameObject == scrollII.transform.GetChild(i).gameObject){
                        print("pos hijo II " + i);
                        posHijo = i;
                    }
                }
                cambioHijo = true;
            }

            if (scrollIII){
                for (int i = 0; i < scrollIII.transform.childCount; i++){
                    if (other.gameObject == scrollIII.transform.GetChild(i).gameObject){
                        print("pos hijo III " + i);
                        posHijo = i;
                    }
                }
                cambioHijo = true;
            }
        }
    }

    public void OnTriggerExit(Collider other){
        if (other.gameObject.name == ("BloqueLineaTemporal")){
            Invoke("CambioOff", 0.05f);
        }
    }

    void CambioOff() {
        if (cambioHijo) cambioHijo = false;
    }
}


*/
#endregion