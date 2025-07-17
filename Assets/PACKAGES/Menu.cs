using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string botonPulsado;
    [HideInInspector] public GameObject menu;
    public List<GameObject> panelMenu;

    private void Start()
    {
        menu = transform.GetChild(0).gameObject;
        //Buscamos los paneles del menu
        int cantidadHijos = menu.transform.GetChild(0).childCount;
        for (int i = 0; i < cantidadHijos; i++)
        {
            panelMenu.Add(menu.transform.GetChild(0).GetChild(i).gameObject);
        }
    }

    public void AbrirCerrarMenu(){
        if (!menu.activeSelf){
            menu.SetActive(true);

            if (transform.root.GetComponent<Root>().panelTeclado.gameObject.activeSelf){
                transform.root.GetComponent<Root>().uInvertida.GetComponent<U_Invertida>().ToolSelection(3);
            }
        }else{
            menu.SetActive(false);
            panelMenu[0].SetActive(true);
            panelMenu[1].SetActive(false);
            panelMenu[2].SetActive(false);
            panelMenu[3].SetActive(false);
            botonPulsado = null;
        }
    }

    public void Reiniciar(){

        botonPulsado = "Reset";
        panelMenu[3].SetActive(true);
        panelMenu[3].transform.GetChild(0).GetComponent<Text>().text = "¿ESTAS SEGURO DE QUE QUIERES REINICIAR? ";
    }

    public void Salir()
    {
        botonPulsado = "Exit";
        panelMenu[3].SetActive(true);
        panelMenu[3].transform.GetChild(0).GetComponent<Text>().text = "¿ESTAS SEGURO DE QUE QUIERES SALIR? ";
    }
    //Pregunta de confirmacion para salir, reiniciar, guardar, cargar...
    public void Confirmacion(int confirm)
    {
        if (confirm == 0){
            if (botonPulsado == "Exit") {
                if (!Application.isEditor) System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            if (botonPulsado == "Reset") { SceneManager.LoadScene(0); }
            if (botonPulsado == "Save") { FindObjectOfType<GenericSave>().SaveData(); panelMenu[3].SetActive(false); botonPulsado = null; }
            if (botonPulsado == "Load") { FindObjectOfType<GenericSave>().LoadData(FindObjectOfType<GenericSave>().slotSelected); }
        }else{
            panelMenu[3].SetActive(false);
        }
    }

    public void SaveLoad(){

        if (panelMenu[0].activeSelf){
            panelMenu[0].SetActive(false);
            panelMenu[1].SetActive(true);
        }else{
            panelMenu[0].SetActive(true);
            panelMenu[1].SetActive(false);
        }
    }

    public void SavePanel(){

        if (!panelMenu[2].activeSelf){
            botonPulsado = "Save";
            panelMenu[2].SetActive(true);
            panelMenu[1].SetActive(false);
        }
        else{
            panelMenu[2].SetActive(false);
            panelMenu[1].SetActive(true);
        }
    }

    public void LoadPanel(){
        if (!panelMenu[2].activeSelf){
            botonPulsado = "Load";
            panelMenu[2].SetActive(true);
            panelMenu[1].SetActive(false);
        }else{
            panelMenu[2].SetActive(false);
            panelMenu[1].SetActive(true);
        }
    }
}
