using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootBatler : MonoBehaviour {

    #region
    [Header("Sprites ARTE")]
    public SpritesCustom ArteSprites = new SpritesCustom();

    [System.Serializable]
    public class SpritesCustom{
        public List<Sprite> Pieles;
        public List<Sprite> Pelo;
        public List<Sprite> Ropa;
        public List<Sprite> Complemento;
    }
    #endregion

    [Header("0: gén, 1: piel, 2: pelo, 3: c. pelo, 4: ropa, 5: acces")]
    public List<int> infoGlobalPlayers;

    [Header("Sprites ARTE")]
    public List<GameObject> CrearPlayers;
    public List<GameObject> CustomPlayers;

    public List<GameObject> HojasPlayers;
    public List<GameObject> ContenidoPlayers;

    public void Start(){

        // Carga Automática de Listas

        for (int o = 0; o < CrearPlayers.Count; o++){
            //ContenidoPlayers[o + 4] = CrearPlayers[o].transform.GetChild(2).transform.GetChild(CrearPlayers[o].transform.GetChild(2).GetSiblingIndex()).gameObject;
        }
    }

    public void CreaPlayer (int num) {
		
        for (int i = 0; i < CrearPlayers.Count; i++){
            if (i == num){

                if (!CrearPlayers[i].GetComponent<Animator>().enabled){

                    CrearPlayers[i].GetComponent<Animator>().enabled = true;
                    CustomPlayers[i].GetComponent<Animator>().enabled = true;
                }
            }            
        }
	}

    public void MoverHojas (int num){

        for (int i = 0; i < 5; i++){

            if (i == num){

                HojasPlayers[i].transform.SetAsLastSibling();
                HojasPlayers[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = false;
                ContenidoPlayers[i].SetActive(true);
            }else{
                if (HojasPlayers[i].activeSelf){
                    HojasPlayers[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().interactable = true;
                    ContenidoPlayers[i].SetActive(false);
                }
            }
        }
    }

    [Header("Gestor")]
    public GameObject mesa;
    public GameObject panelInicial;
    public List<GameObject> contenidos;

    public void Manager(int num){

        if (num == -1) {

            for (int i = 0; i < contenidos.Count; i++) {
                if (contenidos[i].activeSelf) contenidos[i].SetActive(false);
            }

            if (!mesa.activeSelf) {
                mesa.SetActive(true);
                panelInicial.SetActive(false);
            } else {
                if (mesa.activeSelf){
                    mesa.SetActive(false);
                    panelInicial.SetActive(true);
                }
            }
        }

        for (int i = 0; i < contenidos.Count; i++){

            if (i == num){
                contenidos[i].SetActive(true);
                if (!mesa.activeSelf) mesa.SetActive(true); panelInicial.SetActive(false);
            }else contenidos[i].SetActive(false); 
        }
    }
}
