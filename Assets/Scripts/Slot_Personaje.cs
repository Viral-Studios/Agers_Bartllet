using UnityEngine;
using UnityEngine.UI;

public class Slot_Personaje : MonoBehaviour
{
    public GameObject character;
    public GameObject vignetteViewer;
    public Sprite vignetteCharacter;
    private Control_NPC controler;
    private Button buttonSelectCharacter;
    private Button buttonStripsCharacter;

    private void Start(){
        buttonSelectCharacter = transform.GetChild(1).GetComponent<Button>();
        buttonStripsCharacter = transform.GetChild(2).GetComponent<Button>();
        controler = FindObjectOfType<Control_NPC>();
        if (vignetteCharacter == null) { buttonStripsCharacter.interactable = false; }
    }

    public void Seleccionar()
    {
        //Comprobamos si esta conectado con el personaje correspondiente
        if (character != null)
        {
            //Seleccionamos el personaje
            if (controler.character == null)
            {
                controler.character = character;
                buttonSelectCharacter.GetComponent<Image>().color = Color.cyan;
            }
            else
            {
                //Se ha vuelto a seleccionar el mismo personaje
                if (controler.character == character)
                {
                    controler.character = null;
                    buttonSelectCharacter.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    //Ponemos todos los botones en blanco
                    for (int i = 0; i < transform.parent.childCount; i++)
                    {
                        transform.parent.GetChild(i).GetComponent<Slot_Personaje>().buttonSelectCharacter.GetComponent<Image>().color = Color.white;
                    }
                    //Seleccionamos el nuevo personaje
                    controler.character = character;
                    buttonSelectCharacter.GetComponent<Image>().color = Color.cyan;
                }
            }
        }
    }

    public void Viñeta()
    {
        //print(vignetteViewer.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("None State"));

        //Comprobamos si el personaje tiene viñeta
        if (vignetteCharacter != null)
        {
            //Si esta oculta la viñeta la mostramos
            if (buttonStripsCharacter.GetComponent<Image>().color == Color.white)
            {
                //Ocultamos todas las viñetas
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    transform.parent.GetChild(i).GetComponent<Slot_Personaje>().buttonStripsCharacter.GetComponent<Image>().color = Color.white;
                }
                //Mostramos solo la que seleccionamos
                buttonStripsCharacter.GetComponent<Image>().color = Color.cyan;
            }
            //Si esta activa la viñeta la ocultamos
            else
            {
                buttonStripsCharacter.GetComponent<Image>().color = Color.white;
            }
            vignetteViewer.GetComponent<Visor_Viñetas>().MostrarViñeta(vignetteCharacter);
        }
    }
}
