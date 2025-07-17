using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drageables : MonoBehaviour{

    [Header ("- Específico Sala Descanso -")]
    public GameObject panelReproductor;
    [Space (20)]

    public GameObject panelDrags;
    public GameObject viñeta;
    public GameObject content;
    public Transform contentDrageables_Ingame;
    public List<Button> funcionDragButton;
    public List<Button> tipoDragButton;
    public List<GameObject> contenedorDrags;
    //public List<GameObject> characterDragsParent;

    private void Awake(){

        //Buscamos el panel de drag que mostraremos y ocultaremos
        panelDrags = transform.GetChild(2).gameObject;
        //Buscamos la viñeta que mostraremos y ocultaremos
        viñeta = transform.GetChild(3).GetChild(0).gameObject;
        viñeta.GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
        //Buscamos el contenedor de los slots de personajes
        content = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //Buscamos los botones que activan las funciones (retornar drags, bloquear drags y mostrar/ocultar drags)
        int cantidadHijos = transform.GetChild(1).childCount;
        for (int i = 0; i < cantidadHijos; i++){
            funcionDragButton.Add(transform.GetChild(1).GetChild(i).GetComponent<Button>());
        }
        //Buscamos los botones de los submenus de los drageables (drag normal, drag personaje y drag evento)
        cantidadHijos = panelDrags.transform.GetChild(0).childCount;
        for (int i = 0; i < cantidadHijos; i++)
        {
            tipoDragButton.Add(panelDrags.transform.GetChild(0).GetChild(i).GetComponent<Button>());
        }

        //Buscamos los contenedores de drag para poder activarlos con los botones de submenu
        cantidadHijos = panelDrags.transform.GetChild(1).childCount;
        for (int i = 0; i < cantidadHijos; i++)
        {
            contenedorDrags.Add(panelDrags.transform.GetChild(1).GetChild(i).gameObject);
        }
        //Buscamos el padre de los drags ingame de los personajes
        cantidadHijos = transform.root.GetComponent<Root>().escenario.Count;
        for (int i = 0; i < cantidadHijos; i++){
            //characterDragsParent.Add(transform.root.GetComponent<Root>().escenario[i].transform.GetChild(2).gameObject);
        }
    }

    public void FuncionDrag(int funcion){

        #region Añadido específico Sala Descanso
        if (funcion == -1){

            if (!panelReproductor.activeSelf){

                if (transform.GetChild(0).gameObject.activeSelf)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    funcionDragButton[0].GetComponent<Image>().color = Color.white;
                    print("Entra en el reproductor");
                }

                panelReproductor.gameObject.SetActive(true);
                panelReproductor.transform.parent.GetComponent<Image>().color = Color.cyan;
            }else{
                panelReproductor.gameObject.SetActive(false);
                panelReproductor.transform.parent.GetComponent<Image>().color = Color.white;
            }

        }
        #endregion

        //Funcion para abrir y cerrar el panel de los drags
        if (funcion == 0){
            if (!this.transform.GetChild(0).gameObject.activeSelf){
                this.transform.GetChild(0).gameObject.SetActive(true);
                funcionDragButton[funcion].GetComponent<Image>().color = Color.cyan;

                if (panelReproductor != null){
                    if (panelReproductor.gameObject.activeSelf){
                        panelReproductor.gameObject.SetActive(false);
                        panelReproductor.transform.parent.GetComponent<Image>().color = Color.white;
                    }
                }
            }else{
                this.transform.GetChild(0).gameObject.SetActive(false);
                funcionDragButton[funcion].GetComponent<Image>().color = Color.white;
            }
        }

        // Funcion para abrir y cerrar el panel del listado de los drags
        if (funcion == 1){
            if (!panelDrags.activeSelf){
                panelDrags.SetActive(true);
                funcionDragButton[funcion].GetComponent<Image>().color = Color.cyan;
            }else{
                panelDrags.SetActive(false);
                funcionDragButton[funcion].GetComponent<Image>().color = Color.white;

            }
        }

        //Funcion para bloquear los drageables
        if (funcion == 2) { }
        //Funcion para devolver los drageables
        if (funcion == 3) { }
    }

    public void TipoDrag(int tipo)
    {
        //Funcion para cambiar entre las pestañas con los diferentes tipos de drags
        for (int i = 0; i < tipoDragButton.Count; i++) { tipoDragButton[i].GetComponent<Image>().color = Color.white; }
        tipoDragButton[tipo].GetComponent<Image>().color = Color.cyan;
        for (int i = 0; i < contenedorDrags.Count; i++) { contenedorDrags[i].SetActive(false); }
        contenedorDrags[tipo].SetActive(true);
    }

    //Funcion para hacer aparecer los personajes drags
    public void PersonajeDrag(int numeroPersonaje){
        if (!transform.root.GetComponent<Root>().personajes[numeroPersonaje].activeSelf){
            //Le damos al prefab de los personajes todos sus datos
            GameObject slot = Instantiate(transform.root.GetComponent<Root>().SlotPersonaje, content.transform);
            slot.GetComponent<Slot_Personaje>().character = transform.root.GetComponent<Root>().personajes[numeroPersonaje];
            slot.GetComponent<Slot_Personaje>().vignetteCharacter = transform.root.GetComponent<Root>().viñetas[numeroPersonaje];
            slot.GetComponent<Slot_Personaje>().vignetteViewer = transform.GetChild(3).GetChild(0).gameObject;
            slot.transform.GetChild(0).GetComponent<Text>().text = slot.GetComponent<Slot_Personaje>().character.name;

            if (slot.GetComponent<Slot_Personaje>().vignetteCharacter.name == "Visibility" && slot.transform.GetChild(2).gameObject.activeSelf){
                slot.transform.GetChild(2).gameObject.SetActive(false);
            }

            //mostramos el personaje
            transform.root.GetComponent<Root>().personajes[numeroPersonaje].SetActive(true);
            //oscurecemos el boton del personaje que sacamos
            contenedorDrags[0].transform.GetChild(numeroPersonaje).GetComponent<Image>().color = Color.gray;
            //adaptamos el tamaño del contenedor de slots a la cantidad de slots
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (content.transform.childCount * 60) + 2);
        }else{

            //buscamos el slot del personaje para eliminarlo de la lista
            int cantidadHijos = content.transform.childCount;
            for (int i = 0; i < cantidadHijos; i++){
                if (transform.root.GetComponent<Root>().personajes[numeroPersonaje].name == content.transform.GetChild(i).GetChild(0).GetComponent<Text>().text){

                    transform.root.GetComponent<Root>().script_npc.character = null;
                    Destroy(content.transform.GetChild(i).gameObject);
                }
            }
            //ocultamos el personaje
            transform.root.GetComponent<Root>().personajes[numeroPersonaje].SetActive(false);
            //devolvemos el color normal al boton del personaje
            contenedorDrags[0].transform.GetChild(numeroPersonaje).GetComponent<Image>().color = Color.white;
            //adaptamos el tamaño del contenedor de slots a la cantidad de slots
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ((content.transform.childCount - 1) * 60) + 2);

            if (numeroPersonaje == 0){
                transform.root.GetComponent<Root>().personajes[numeroPersonaje].transform.localPosition = new Vector3 (859.9f, -327.5f, 0);
                transform.root.GetComponent<Root>().personajes[numeroPersonaje].transform.localEulerAngles = new Vector3(225.0f, 90.0f, -90.0f);
            }
        }
    }
}