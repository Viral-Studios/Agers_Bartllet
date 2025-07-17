using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drageables_Dual : MonoBehaviour
{

    [Header("- Específico Sala Descanso -")]
    public GameObject panelReproductor;
    [Space(20)]

    public GameObject panelDrags;
    public GameObject viñeta;
    public GameObject[] content;
    public Transform contentDrageables_Ingame;
    public List<Button> funcionDragButton;
    public List<Button> tipoDragButton;
    public List<GameObject> contenedorDrags;
    //public List<GameObject> characterDragsParent;

    private void Awake(){

        
        viñeta.GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;

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
        for (int i = 0; i < cantidadHijos; i++)
        {
            //characterDragsParent.Add(transform.root.GetComponent<Root>().escenario[i].transform.GetChild(2).gameObject);
        }
    }

    public void FuncionDrag(int funcion)
    {

        //Funcion para abrir y cerrar el panel de los drags
        if (funcion == 0)
        {
            if (!this.transform.GetChild(0).gameObject.activeSelf)
            {
                this.transform.GetChild(0).gameObject.SetActive(true);
                funcionDragButton[funcion].GetComponent<Image>().color = Color.cyan;

                if (panelReproductor != null)
                {
                    if (panelReproductor.gameObject.activeSelf)
                    {
                        panelReproductor.gameObject.SetActive(false);
                        panelReproductor.transform.parent.GetComponent<Image>().color = Color.white;
                    }
                }
            }
            else
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
                funcionDragButton[funcion].GetComponent<Image>().color = Color.white;
            }
        }

        // Funcion para abrir y cerrar el panel del listado de los drags
        if (funcion == 1)
        {
            if (!panelDrags.activeSelf)
            {
                panelDrags.SetActive(true);
                funcionDragButton[funcion].GetComponent<Image>().color = Color.cyan;
            }
            else
            {
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

        for (int u = 0; u < 2; u++){

            if (dualSeleccionado[u]){

                if (!transform.root.GetComponent<Root>().personajesSalaDiv[u * (numeroPersonaje + 1)].activeSelf){

                    print("SACAS A DONNA EN SALA " + u + " " + numeroPersonaje);

                    //Le damos al prefab de los personajes todos sus datos
                    GameObject slot = Instantiate(transform.root.GetComponent<Root>().SlotPersonajeSalaDiv, content[u * (numeroPersonaje + 1)].transform);
                    slot.GetComponent<Slot_Personaje_SalaDiv>().character = transform.root.GetComponent<Root>().personajesSalaDiv[u * (numeroPersonaje + 1)];
                    slot.GetComponent<Slot_Personaje_SalaDiv>().vignetteCharacter = transform.root.GetComponent<Root>().viñetasSalasDiv[u * (numeroPersonaje + 1)];
                    slot.GetComponent<Slot_Personaje_SalaDiv>().vignetteViewer = viñeta;
                    slot.transform.GetChild(0).GetComponent<Text>().text = slot.GetComponent<Slot_Personaje_SalaDiv>().character.name;
                    slot.transform.GetChild(2).gameObject.SetActive(false);

                    //mostramos el personaje
                    transform.root.GetComponent<Root>().personajesSalaDiv[u * (numeroPersonaje + 1)].SetActive(true);
                    //adaptamos el tamaño del contenedor de slots a la cantidad de slots
                    content[u * (numeroPersonaje + 1)].GetComponent<RectTransform>().sizeDelta = new Vector2(0, (content[u * (numeroPersonaje + 1)].transform.childCount * 60) + 2);
                }else{

                    print("DEVUELVES A DONNA DE LA SALA " + u + " " + numeroPersonaje);

                    //buscamos el slot del personaje para eliminarlo de la lista
                    int cantidadHijos = content[u * (numeroPersonaje + 1)].transform.childCount;
                    for (int i = 0; i < cantidadHijos; i++)
                    {
                        if (transform.root.GetComponent<Root>().personajesSalaDiv[u * (numeroPersonaje + 1)].name == content[u * (numeroPersonaje + 1)].transform.GetChild(numeroPersonaje).GetChild(0).GetComponent<Text>().text)
                        {
                            transform.root.GetComponent<Root>().script_npc.character = null;
                            Destroy(content[u * (numeroPersonaje + 1)].transform.GetChild(numeroPersonaje).gameObject);
                        }
                    }
                    //ocultamos el personaje
                    transform.root.GetComponent<Root>().personajesSalaDiv[u * (numeroPersonaje + 1)].SetActive(false);
                    //devolvemos el color normal al boton del personaje
                    contenedorDrags[0].transform.GetChild(numeroPersonaje).GetComponent<Image>().color = Color.white;
                    //adaptamos el tamaño del contenedor de slots a la cantidad de slots
                    content[u * (numeroPersonaje + 1)].GetComponent<RectTransform>().sizeDelta = new Vector2(0, ((content[u * (numeroPersonaje + 1)].transform.childCount - 1) * 60) + 2);
                }
            }
        }
    }

    /********************************************************************************************************************************************************/
    public bool[]    dualSeleccionado = new bool[2];
    public Transform padreBotones_MapasDivididos;

    public void SeleccionMapaDividido (int num){
        dualSeleccionado[num-1] = !dualSeleccionado[num-1];

        if (dualSeleccionado[num-1]){
            padreBotones_MapasDivididos.GetChild(num).GetComponent<Image>().color = Color.cyan;
        }else { padreBotones_MapasDivididos.GetChild(num).GetComponent<Image>().color = Color.white; }
    }
}