using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaletinUSB_Manager : MonoBehaviour {

    public GameObject holoPeque_Contenido;
    public GameObject botonera_Contenido;

    public GameObject holoPeque_Panel;
    public GameObject holoGrande_Panel;

    public GameObject USB_Panel;
    public GameObject USB_Contenido;

    [Header("Padres..")]
    public GameObject HerramientasPaneles;
    public GameObject padreArchivador;
    public GameObject padreUSB;

    #region USB
    public void GuardadoUSB(int num){
        if (holoPeque_Contenido.transform.GetChild(num).gameObject.GetComponent<Image>().color == Color.white){
            holoPeque_Contenido.transform.GetChild(num).gameObject.GetComponent<Image>().color = Color.cyan;
            USB_Contenido.transform.GetChild(num).gameObject.SetActive(true);
        }else{
            holoPeque_Contenido.transform.GetChild(num).gameObject.GetComponent<Image>().color = Color.white;
            USB_Contenido.transform.GetChild(num).gameObject.SetActive(false);
        }
    }

    public void USB_AbrirCerrar(){

        // LOS DOSSIERES PASAN SE EMPARENTAN AL PANEL DEL USB
        if (!USB_Panel.gameObject.activeSelf){
            USB_Panel.gameObject.SetActive(true);
        }else {
            USB_Panel.gameObject.SetActive(false);
        }
    }
    #endregion

    // ________________________________________________________________________

    #region ARCHIVADOR

    // EN EL SCROLL DE LA BOTONERA DEL MASTER SE ELIGEN LOS DOCUMENTOS QUE APARECERÁN EN EL ARCHIVADOR...
    public void HerramientasBotonera (int num){

        if (!holoPeque_Contenido.transform.GetChild(num).gameObject.activeSelf){
            holoPeque_Contenido.transform.GetChild(num).gameObject.SetActive(true);
            botonera_Contenido.transform.GetChild(num).gameObject.GetComponent<Image>().color = Color.cyan;
        }
        else{
            holoPeque_Contenido.transform.GetChild(num).gameObject.SetActive(false);
            botonera_Contenido.transform.GetChild(num).gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    // ABRE Y CIERRA EL HOLOGRAMA PEQUEÑO
    public void GestorPaneles (int num){

        // Abre/Cierra Panel pequeño
        if (num == 0){

            if (!holoPeque_Panel.activeSelf){
                holoPeque_Panel.SetActive(true);
                USB_Panel.SetActive(false);
            }else{
                holoPeque_Panel.SetActive(false);
                holoGrande_Panel.SetActive(false);
            }
        }
    }

    // ABRE Y CIERRA LOS CONTENIDOS EN EL HOLOGRAMA GRANDE      --> SE PUEDEN EDITAR <--
    public void AbrirCerrarPaneles (int num){

        if (HerramientasPaneles.transform.GetChild(num).gameObject.activeSelf == false){
            HerramientasPaneles.transform.GetChild(num).gameObject.SetActive (true);
            holoGrande_Panel.transform.GetChild(0).gameObject.SetActive(true);

            for (int i = 0; i < HerramientasPaneles.transform.childCount; i++){
                if (num != i && HerramientasPaneles.transform.GetChild(i).gameObject.activeSelf == true){
                    HerramientasPaneles.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }else{
            HerramientasPaneles.transform.GetChild(num).gameObject.SetActive(false);
            holoGrande_Panel.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Seleccion(int num){

        if (holoPeque_Contenido.transform.GetChild(num).gameObject.GetComponent<Image>().color == Color.white){
            holoPeque_Contenido.transform.GetChild(num).gameObject.GetComponent<Image>().color = Color.cyan;
        }else{
            holoPeque_Contenido.transform.GetChild(num).gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    #endregion

    #region EMPARENTAR A...
    public void Emparentar_USB(){

        HerramientasPaneles.transform.SetParent(padreUSB.transform);
        HerramientasPaneles.transform.localPosition = Vector3.zero;

        for (int i = 0; i < 7; i++){
            HerramientasPaneles.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Emparentar_ARCHIVADOR(int num){

        HerramientasPaneles.transform.SetParent(padreArchivador.transform);
        HerramientasPaneles.transform.localPosition = Vector3.zero;

        for (int i = 0; i < 7; i++){
            HerramientasPaneles.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    #endregion
}
