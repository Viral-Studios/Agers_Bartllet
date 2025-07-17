using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Visor_Viñetas : MonoBehaviour {

    public Animator myAnimator;
    public Image vignette;

	void Start ()
    {
        //Obtenemos los componentes necesarios para manejar la viñetas
        myAnimator = GetComponent<Animator>();
        vignette = GetComponent<Image>();
	}

    public void MostrarViñeta(Sprite viñeta)
    {
        print("ENTRA");

        //si la viñeta recibida es distinta de la actual
        if (viñeta != vignette.sprite)
        {
            //si la viñeta esta oculta la mostramos
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("None State")){
                vignette.sprite = viñeta;
                myAnimator.Play("FadeIn");
                print("1");
            }
            //si la viñeta esta activa la ocultamos y mostramos la nueva
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
            {
                myAnimator.Play("FadeOff");
                StartCoroutine(NuevaViñeta(viñeta));
                print("2");
            }
        }
        //si la viñeta recibida es la misma que la que tenemos
        else{
            //si esta visible la ocultamos
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn")){
                myAnimator.Play("FadeOff");
                print("3");
            }
            //si esta oculta la mostramos
            else{
                myAnimator.Play("FadeIn");
                print("4");
            }
        }
    }
    //Funcion para cuando tenemos que ocultar la viñeta anterior para mostrar la nueva
    IEnumerator NuevaViñeta(Sprite nuevaViñeta) {
        yield return new WaitForSeconds(1f);
        vignette.sprite = nuevaViñeta;
        myAnimator.Play("FadeIn");
    }
}
