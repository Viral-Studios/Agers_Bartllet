
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalasDiv : MonoBehaviour {

    public Transform separador;
    public GameObject[] salas = new GameObject[4];
    public Button[] imageSalas;

    public Vector3 posIzqPrincipal;
    public Vector3 posDerPrincipal;

    public Vector3 rotIzq;
    public Vector3 rotDer;

    public string salaComun;
    public string salaReun;

    public bool[] configSalasIzq = new bool[4];
    public bool[] configSalasDer = new bool[4];

    public void ElegirSala_PanelIzq(int num){

        for (int i = 0; i < imageSalas.Length; i++)
        {

            // ACTIVA/DESACTIVA SALA PRINCIPAL DE PANEL DERECHA
            if (i == num && num < 4)
            {

                if (!salas[num].activeSelf || (salas[num].activeSelf && salas[num].GetComponent<RectTransform>().localPosition == posDerPrincipal)){
                    //print("Activamos " + salas[num].name + "a la izquierda");

                    // COMUNICACIONES
                    if (num == 0){
                        if (salaReun == "izquierda") {
                            if (salas[3].activeSelf){
                                SalaReunionesCortina_Off();
                            }

                            salaReun = "";
                            SalaReunionesPrincipal_Off();
                        }

                        salaComun = "izquierda";
                        SalaComunicacionesPrincipal_On();
                    }

                    // REUNIONES
                    if (num == 1){
                        if (salaComun == "izquierda"){
                            if (salas[2].activeSelf){
                                SalaComunicacionesCortina_Off();
                            }

                            salaComun = "";
                            SalaComunicacionesPrincipal_Off();
                        }

                        salaReun = "izquierda";
                        SalaReunionesPrincipal_On();
                    }
                }
                else{

                    // DESACTIVAMOS LA SALA DE COMUNICACIONES PRINCIPAL PANEL IZQUIERDO
                    if (num == 0){
                        //print("Desactivamos la sala de comunicaciones principal del panel derecho");
                        salaComun = "";
                        SalaComunicacionesPrincipal_Off();
                        SalaComunicacionesCortina_Off();
                    }

                    // DESACTIVAMOS LA SALA DE REUNIONES PRINCIPAL PANEL IZQUIERDO
                    if (num == 1){
                        //print("Desactivamos la sala de reuniones principal del panel derecho");
                        salaReun = "";
                        SalaReunionesPrincipal_Off();
                        SalaReunionesCortina_Off();
                    }

                    SeparadorCheck();
                    return;
                }
            }

            // ACTIVA/DESACTIVA SALA EXTENDIDA DE PANEL DERECHA
            if (i == num && num > 3){
                print("Extendida panel Der");

                if (!salas[i - 2].activeSelf){

                    if (num == 4){
                        SalaComunicacionesCortina_On();
                    }

                    if (num == 5){
                        SalaReunionesCortina_On();
                    }

                    SeparadorCheck();
                    return;
                }else{
                    if (num == 4){
                        SalaComunicacionesCortina_Off();
                    }

                    if (num == 5){
                        SalaReunionesCortina_Off();
                    }

                    SeparadorCheck();
                    return;
                }
            }
        }

    }

    public void ElegirSala_PanelDer(int num){

        for (int i = 0; i < imageSalas.Length; i++){

            // ACTIVA/DESACTIVA SALA PRINCIPAL DE PANEL DERECHA
            if (i == num && num < 4){

                if (!salas[num].activeSelf || (salas[num].activeSelf && salas[num].GetComponent<RectTransform>().localPosition == posIzqPrincipal)){


                    // ACTIVAMOS SALA COMUNICACIONES PANEL DERECHO
                    if (num == 0){
                        if (salaReun == "derecha") {
                            salaReun = "";
                            SalaReunionesPrincipal_Off();

                            if (salas[3].activeSelf){
                                SalaReunionesCortina_Off();
                            }
                        }

                        salaComun = "derecha";
                        SalaComunicacionesPrincipal_On();
                    }

                    // ACTIVAMOS SALA REUNIONES PANEL DERECHO
                    if (num == 1){
                        if (salaComun == "derecha") {
                            salaComun = "";
                            SalaComunicacionesPrincipal_Off();

                            if (salas[2].activeSelf){
                                SalaComunicacionesCortina_Off();
                            }
                        }

                        salaReun = "derecha";
                        SalaReunionesPrincipal_On();
                    }

                    SeparadorCheck();
                    return;
                }
                else{

                    // Desactivamos la sala de comunicaciones principal del panel derecho
                    if (num == 0){
                        salaComun = "";
                        SalaComunicacionesPrincipal_Off();
                        SalaComunicacionesCortina_Off();
                    }

                    // Desactivamos la sala de reuniones principal del panel derecho
                    if (num == 1){
                        salaReun = "";
                        SalaReunionesPrincipal_Off();
                        SalaReunionesCortina_Off();
                    }

                    SeparadorCheck();
                    return;
                }
            }

            // ACTIVA/DESACTIVA SALA EXTENDIDA DE PANEL DERECHA
            if (i == num && num > 3){
                print("Extendida panel Der");

                if (!salas[i - 4].activeSelf){

                    if (num == 6){ SalaComunicacionesCortina_On(); }
                    if (num == 7){ SalaReunionesCortina_On(); }

                    SeparadorCheck();
                    return;
                }
                else{

                    if (num == 6){ SalaComunicacionesCortina_Off(); }
                    if (num == 7){ SalaReunionesCortina_Off(); }

                    SeparadorCheck();
                    return;
                }
            }
        }
    }

    public void SeparadorCheck(){

        if (salas[0].gameObject.activeSelf && salas[1].gameObject.activeSelf){
            separador.gameObject.SetActive(true);
            //print("separador ON");
        }else{
            separador.gameObject.SetActive(false);
            //print("separador OFF");
        }
    }

    #region FUNCIONES PARA CERRAR LAS SALAS
    void SalaComunicacionesPrincipal_Off(){
        //print("Ocultas Comunicaciones: PRINCIPAL");
        salas[0].SetActive(false);
        salas[2].SetActive(false);

        imageSalas[0].GetComponent<Image>().color = Color.white;
        imageSalas[2].GetComponent<Image>().color = Color.white;

        imageSalas[4].GetComponent<Image>().color = Color.white;
        imageSalas[6].GetComponent<Image>().color = Color.white;

        imageSalas[4].interactable = false;
        imageSalas[6].interactable = false;

        imageSalas[4].GetComponent<Image>().color = Color.white;
        imageSalas[6].GetComponent<Image>().color = Color.white;

        if (salas[1].activeSelf){
            if (salaReun == "izquierda"){
                imageSalas[5].interactable = true;
            }else{
                imageSalas[7].interactable = true;
            }
        }
    }

    void SalaComunicacionesCortina_Off()
    {
        //print("Ocultas Comunicaciones: CORTINA");
        salas[2].SetActive(false);
        imageSalas[4].GetComponent<Image>().color = Color.white;
        imageSalas[6].GetComponent<Image>().color = Color.white;
    }

    void SalaReunionesPrincipal_Off(){
        //print("Ocultas Reuniones: PRINCIPAL");
        salas[1].SetActive(false);
        salas[3].SetActive(false);

        imageSalas[1].GetComponent<Image>().color = Color.white;
        imageSalas[3].GetComponent<Image>().color = Color.white;

        imageSalas[5].GetComponent<Image>().color = Color.white;
        imageSalas[7].GetComponent<Image>().color = Color.white;

        imageSalas[5].interactable = false;
        imageSalas[7].interactable = false;

        imageSalas[5].GetComponent<Image>().color = Color.white;
        imageSalas[7].GetComponent<Image>().color = Color.white;

        if (salas[0].activeSelf)
        {
            if (salaComun == "izquierda"){
                imageSalas[4].interactable = true;
            }
            else
            {
                imageSalas[6].interactable = true;
            }
        }
    }

    void SalaReunionesCortina_Off(){
        //print("Ocultas Reuniones: CORTINA");
        salas[3].SetActive(false);
        imageSalas[5].GetComponent<Image>().color = Color.white;
        imageSalas[7].GetComponent<Image>().color = Color.white;
    }
    #endregion

    #region FUNCIONES PARA ABRIR LAS SALAS
    void SalaComunicacionesPrincipal_On(){
        //print("Aparece Comunicaciones: PRINCIPAL");
        salas[0].SetActive(true);

        if (salaComun == "izquierda"){
            salas[0].GetComponent<RectTransform>().localPosition = posIzqPrincipal;
            salas[0].GetComponent<RectTransform>().localEulerAngles = rotIzq;

            imageSalas[5].interactable = false;
            imageSalas[6].interactable = false;
            imageSalas[7].interactable = false;

            imageSalas[5].GetComponent<Image>().color = Color.white;
            imageSalas[6].GetComponent<Image>().color = Color.white;
            imageSalas[7].GetComponent<Image>().color = Color.white;

            if (salaReun == "") { imageSalas[4].interactable = true; }
            imageSalas[0].GetComponent<Image>().color = Color.cyan;
            imageSalas[2].GetComponent<Image>().color = Color.white;
        }

        if (salaComun == "derecha"){
            salas[0].GetComponent<RectTransform>().localPosition = posDerPrincipal;
            salas[0].GetComponent<RectTransform>().localEulerAngles = rotDer;
            imageSalas[4].interactable = false;
            imageSalas[5].interactable = false;
            imageSalas[7].interactable = false;

            imageSalas[4].GetComponent<Image>().color = Color.white;
            imageSalas[5].GetComponent<Image>().color = Color.white;
            imageSalas[7].GetComponent<Image>().color = Color.white;

            if (salaReun == ""){ imageSalas[6].interactable = true; }
            imageSalas[2].GetComponent<Image>().color = Color.cyan;
            imageSalas[0].GetComponent<Image>().color = Color.white;
        }

        if (salas[1].activeSelf){
            if (salaReun == ""){
                imageSalas[1].GetComponent<Image>().color = Color.white;
                imageSalas[3].GetComponent<Image>().color = Color.white;
            }
        }

        if (salas[2].activeSelf){
            SalaComunicacionesCortina_On();
        }

        if (salas[3].activeSelf){
            salas[3].SetActive(false);
            imageSalas[5].GetComponent<Image>().color = Color.white;
            imageSalas[7].GetComponent<Image>().color = Color.white;
            print("entra");
        }

        SeparadorCheck();
    }

    void SalaComunicacionesCortina_On(){
        //print("Aparece Comunicaciones: CORTINA");
        salas[2].SetActive(true);

        if (salaComun == "izquierda"){
            salas[2].GetComponent<RectTransform>().localEulerAngles = rotIzq;
            salas[2].GetComponent<RectTransform>().localPosition = posIzqPrincipal - (2 * posIzqPrincipal);
            imageSalas[4].GetComponent<Image>().color = Color.cyan;
        }

        if (salaComun == "derecha"){
            salas[2].GetComponent<RectTransform>().localEulerAngles = rotDer;
            salas[2].GetComponent<RectTransform>().localPosition = posDerPrincipal - (2 * posDerPrincipal);
            imageSalas[6].GetComponent<Image>().color = Color.cyan;
        }
    }

    void SalaReunionesPrincipal_On(){

        salas[1].SetActive(true);

        if (salaReun == "izquierda"){
            salas[1].GetComponent<RectTransform>().localPosition = posIzqPrincipal;
            salas[1].GetComponent<RectTransform>().localEulerAngles = rotIzq;
            imageSalas[4].interactable = false;
            imageSalas[6].interactable = false;
            imageSalas[7].interactable = false;

            imageSalas[4].GetComponent<Image>().color = Color.white;
            imageSalas[6].GetComponent<Image>().color = Color.white;
            imageSalas[7].GetComponent<Image>().color = Color.white;

            if (salaComun == "") { imageSalas[5].interactable = true; }
            imageSalas[1].GetComponent<Image>().color = Color.cyan;
            imageSalas[3].GetComponent<Image>().color = Color.white;
        }

        if (salaReun == "derecha"){
            salas[1].GetComponent<RectTransform>().localPosition = posDerPrincipal;
            salas[1].GetComponent<RectTransform>().localEulerAngles = rotDer;
            imageSalas[4].interactable = false;
            imageSalas[5].interactable = false;
            imageSalas[6].interactable = false;

            imageSalas[4].GetComponent<Image>().color = Color.white;
            imageSalas[5].GetComponent<Image>().color = Color.white;
            imageSalas[6].GetComponent<Image>().color = Color.white;

            if (salaComun == "") { imageSalas[7].interactable = true; }
            imageSalas[3].GetComponent<Image>().color = Color.cyan;
            imageSalas[1].GetComponent<Image>().color = Color.white;
        }

        if (salas[0].activeSelf){

            if (salaComun == ""){
                imageSalas[0].GetComponent<Image>().color = Color.white;
                imageSalas[2].GetComponent<Image>().color = Color.white;
            }
        }

        if (salas[2].activeSelf){
            salas[2].SetActive(false);
            imageSalas[4].GetComponent<Image>().color = Color.white;
            imageSalas[6].GetComponent<Image>().color = Color.white;
            print("entra");
        }

        if (salas[3].activeSelf)
        {
            SalaReunionesCortina_On();
        }

        SeparadorCheck();
    }

    void SalaReunionesCortina_On(){
        print("Aparece Reuniones: CORTINA");
        salas[3].SetActive(true);

        if (salaReun == "izquierda")
        {
            salas[3].GetComponent<RectTransform>().localEulerAngles = rotIzq;
            salas[3].GetComponent<RectTransform>().localPosition = posIzqPrincipal - (2 * posIzqPrincipal);
            imageSalas[5].GetComponent<Image>().color = Color.cyan;
        }

        if (salaReun == "derecha")
        {
            salas[3].GetComponent<RectTransform>().localEulerAngles = rotDer;
            salas[3].GetComponent<RectTransform>().localPosition = posDerPrincipal - (2 * posDerPrincipal);
            imageSalas[7].GetComponent<Image>().color = Color.cyan;
        }
    }
    #endregion

    public void Reiniciar(){
        Application.LoadLevel(0);
    }

    public void Salir(){
        Application.Quit();
    }

    public void EstadoSalasEscogidas(){

        for (int i = 0; i < 4; i++){

            // PARTE IZQUIERDA
            if (imageSalas[i].GetComponent<Image>().color == Color.cyan) {
                configSalasIzq[i] = true;
                //print("SALA IZQUIERDA " + i + " pasa a TRUE");
            }else{
                if (configSalasIzq[i]){
                    configSalasIzq[i] = false;
                    //print("SALA IZQUIERDA " + i + " pasa a FALSE");
                }
            }

            // PARTE DERECHA
            if (imageSalas[i+4].GetComponent<Image>().color == Color.cyan){
                configSalasDer[i] = true;
                //print("SALA DERECHA" + i + " pasa a TRUE");
            }
            else{
                if (configSalasDer[i]){
                    configSalasDer[i] = false;
                    //print("SALA DERECHA " + i + " pasa a FALSE");
                }
            }
        }
    }
}
