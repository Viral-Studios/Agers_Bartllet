using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaHologramas : MonoBehaviour {

    public List<GameObject> hologramas;
    [HideInInspector] public GameObject USB_miniatura;

    // paneles botonera... [0] izq, [1] der
    public List <GameObject> panelesBotonera;
    public List<Image> imageBotoneraIzq;

    public GameObject panelesHoloInicial;
    public GameObject bandejaAccesos;
    public GameObject contenido;

    public bool usbExpulsado;

    [Header("")]
    public List<GameObject> celdas;

    public Teclado teclado;
    public OkapiBot okapib;

    public GameObject padreBotones_SituationRoom;
    public Animator protector;

    // ___________________________________________________________
    [Header("GRAFICAS")]
    public GameObject padreGraficasSituation;
    public GameObject mapaInicial;
    public List<Image> botonesGraficas;

    // Use this for initialization
    void Awake () {

        //protector.keepAnimatorControllerStateOnDisable = true;
        hologramas.Add(transform.GetChild(0).gameObject); hologramas.Add(transform.GetChild(1).gameObject);
        USB_miniatura = hologramas[1].transform.GetChild(0).gameObject;
    }

    public void BotoneraIzq(int num) {

        // PANEL IZQ
        if (num == 0) {

            if (!panelesBotonera[0].gameObject.activeSelf) {
                panelesBotonera[0].gameObject.SetActive(true);
                panelesBotonera[1].gameObject.SetActive(false);
                panelesBotonera[2].gameObject.SetActive(false);
            } else {
                panelesBotonera[0].gameObject.SetActive(false);
            }

            return;
        }

        if (num == 1) {

            if (!panelesBotonera[1].gameObject.activeSelf) {
                panelesBotonera[1].gameObject.SetActive(true);
                panelesBotonera[0].gameObject.SetActive(false);
                panelesBotonera[2].gameObject.SetActive(false);
            } else {
                panelesBotonera[1].gameObject.SetActive(false);
            }

            return;
        }

        if (num == 2) {

            if (!panelesBotonera[2].gameObject.activeSelf) {
                panelesBotonera[2].gameObject.SetActive(true);
                panelesBotonera[0].gameObject.SetActive(false);
                panelesBotonera[1].gameObject.SetActive(false);
            }
            else
            {
                panelesBotonera[2].gameObject.SetActive(false);
            }

            return;
        }

        for (int i = 0; i < 3; i++){
            if (panelesBotonera[i].gameObject.activeSelf){
                imageBotoneraIzq[i].color = Color.cyan;
            }else{
                imageBotoneraIzq[i].color = Color.white;
            }
        }
    }
       
    public List<AudioClip> hologrAudios;

    public void HologramaPC_OnOff(){

        if (GetComponent<AudioSource>().clip != hologrAudios[0]){
            GetComponent<AudioSource>().clip = hologrAudios[0];
        }else{
            GetComponent<AudioSource>().clip = hologrAudios[1];
        }
        
        GetComponent<AudioSource>().Play();

        if (hologramas[0].transform.GetChild(1).gameObject.activeSelf){
            hologramas[0].transform.GetChild(1).gameObject.SetActive(false);
        }else{
            hologramas[0].transform.GetChild(1).gameObject.SetActive(true);

            if (contenidoHerramientas.transform.parent != hologramas[1].gameObject.transform){
                print ("Está en situation");
                //Sit_USB();

                #region

                contenidoHerramientas.gameObject.SetActive(true);

                contenidoHerramientas.transform.SetParent(hologramas[1].gameObject.transform);
                contenidoHerramientas.transform.SetAsLastSibling();
                contenidoHerramientas.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                contenidoHerramientas.GetComponent<RectTransform>().localScale = Vector3.one;
                contenidoHerramientas.GetComponent<CanvasGroup>().interactable = true;

                for (int i = 0; i < contenidoHerramientas.transform.childCount - 1; i++){
                    print("aparece");
                    contenidoHerramientas.transform.GetChild(i).gameObject.SetActive(false);
                    contenidoHerramientas.transform.GetChild(i).GetComponent<Image>().enabled = true;
                    contenidoHerramientas.transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                }
                #endregion

                //USB_miniatura.SetActive(false);

                for (int i = 0; i < contenidoHerramientas.transform.childCount; i++){
                    contenidoHerramientas.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                    contenidoHerramientas.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
                }
            }

            if (!contenidoHerramientas.gameObject.activeSelf){
                contenidoHerramientas.gameObject.SetActive(true);
                for (int i = 0; i < 12; i++){
                    if (contenidoHerramientas.transform.GetChild(i).gameObject.activeSelf)
                        contenidoHerramientas.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (!protector.enabled){
            protector.enabled = true;
        }else{
            protector.SetTrigger("1");
        }
    }

    public void HoloInicial_Hall(int num){

        // ACCEDES A FICHERO
        if (num == 0){
            panelesHoloInicial.transform.GetChild(0).gameObject.SetActive(false);
            panelesHoloInicial.transform.GetChild(1).gameObject.SetActive(true);
            panelesHoloInicial.transform.GetChild(2).gameObject.SetActive(false);
        }

        // ACCEDES A USB
        if (num == 1){
            panelesHoloInicial.transform.GetChild(0).gameObject.SetActive(false);
            panelesHoloInicial.transform.GetChild(1).gameObject.SetActive(false);
            panelesHoloInicial.transform.GetChild(2).gameObject.SetActive(true);
        }

        // VOLVER A HALL
        if (num == 2){
            panelesHoloInicial.transform.GetChild(0).gameObject.SetActive(true);
            panelesHoloInicial.transform.GetChild(1).gameObject.SetActive(false);
            panelesHoloInicial.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void AccesosDirectosOnOff(int num){

        if (!bandejaAccesos.transform.GetChild(num).gameObject.activeSelf){
            bandejaAccesos.transform.GetChild(num).gameObject.SetActive(true);
            contenido.transform.GetChild(num).GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
        }else{
            bandejaAccesos.transform.GetChild(num).gameObject.SetActive(false);
            contenido.transform.GetChild(num).GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }

    public void ContenidoHerramientasOpenClose (int num){

        // VOLVER...
        if (num == -1){

            for (int i = 0; i < hologramas[1].transform.GetChild(2).transform.childCount; i++){
                if (hologramas[1].transform.GetChild(2).transform.GetChild(i).gameObject.activeSelf){
                    hologramas[1].transform.GetChild(2).transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            hologramas[0].transform.GetChild(1).gameObject.SetActive(true);

            if (teclado.target != null){
                teclado.target = null;
                teclado.retornoInputField.text = "";
            }

            if (teclado.panelAscensor.activeSelf){
                teclado.panelAscensor.SetActive(false);
            }
        }
        else{
            //print("abre");

            if (!contenidoHerramientas.transform.GetChild(num).gameObject.activeSelf){
                contenidoHerramientas.transform.GetChild(num).gameObject.SetActive(true);
                hologramas[0].transform.GetChild(1).gameObject.SetActive(false);

                if (num == 0 || num == 1 || num == 4 || num == 8){
                    if (!teclado.panelAscensor.activeSelf){
                        teclado.panelAscensor.SetActive(true);
                    }
                }

                for (int i = 0; i < 12; i++){
                    if (i != num && contenidoHerramientas.transform.GetChild(i).gameObject.activeSelf){
                        contenidoHerramientas.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }else{
                contenidoHerramientas.transform.GetChild(num).gameObject.SetActive(false);
            }
        }
    }

    public GameObject contenidoHerramientas;
    public bool[] graficaEmparentada = new bool[3];

    // ____________________________________________________

    /* **** FUNCION GESTORA DE LAS GRAFICAS DE BOLSA, PIE **** */
    public void GraficasOnOff(int num){
        for (int i = 0; i < 6; i++){
            if (i == num){

                /* **** ACTIVA LA GRAFICA E ILUMINA EL BOTÓN CORRESPONDIENTE **** */
                if (!padreGraficasSituation.transform.GetChild(i).gameObject.activeSelf) {

                    padreGraficasSituation.gameObject.SetActive(true);
                    padreGraficasSituation.transform.GetChild(i).gameObject.SetActive(true);
                    botonesGraficas[i].color = Color.cyan;

                    if (!protector.enabled){ protector.enabled = true;
                        GetComponent<AudioSource>().clip = hologrAudios[0];
                        GetComponent<AudioSource>().Play();
                        print("lllll");
                    }

                    if (i > 2){
                        if (mapaInicial.transform.GetChild(num - 3).transform.GetChild(1).gameObject.name == ("Grafica")){
                            print("SI");
                            mapaInicial.transform.GetChild(num - 3).transform.GetChild(1).transform.SetParent(padreGraficasSituation.transform.GetChild(num).transform);
                            graficaEmparentada[num - 3] = true;
                        }

                        padreGraficasSituation.transform.GetChild(num).transform.GetChild(0).GetComponent<Text>().text = mapaInicial.transform.GetChild(num - 3).transform.GetChild(0).GetComponent<InputField>().text;
                        padreGraficasSituation.transform.GetChild(num).transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (-100.0f, -55.0f, 0);
                        padreGraficasSituation.transform.GetChild(num).transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 1.5f;
                    }

                // 
                }else { // DESACTIVA LA GRAFICA Y DESILUMINA EL BOTÓN CORRESPONDIENTE
                    botonesGraficas[i].color = Color.white;
                    padreGraficasSituation.gameObject.SetActive(false);
                    padreGraficasSituation.transform.GetChild(i).gameObject.SetActive(false);
                    if (protector.enabled && !panelesHoloInicial.activeSelf) {
                        protector.SetTrigger ("1");
                        GetComponent<AudioSource>().clip = hologrAudios[1];
                        GetComponent<AudioSource>().Play(); }
                }
            }else { //  DESACTIVA LA GRAFICA Y DESILUMINA EL BOTÓN QUE ESTABA ILUMINADO Y QUE NO SE HA PRESIONADO
                if (padreGraficasSituation.transform.GetChild(i).gameObject.activeSelf){
                    botonesGraficas[i].color = Color.white;
                    padreGraficasSituation.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
