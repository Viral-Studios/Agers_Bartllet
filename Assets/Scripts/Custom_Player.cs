using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Custom_Player : MonoBehaviour {

    U_Invertida script;
    Root Root;

    public GameObject startCustom;
    public GameObject customPanel;

    public int jugador;
    public GameObject panelPadre;
    public GameObject panelSimbolos;

    [Header("Sprites ARTE")]
    public List<Image> RanuraBrilla;
    public List<Image> capasPersonaje;
    [Header ("0 género | 1 piel | 2 peinado | 3 ropa | 4 Accesorio | 5 Calzado | 6 Chapa | 7 colorPelo")]
    public List<int> infoJugador0;

    public List<GameObject> capasChapa;

    private void Awake(){
        Root = GameObject.Find("RootBartler").gameObject.GetComponent<Root>();
        script = GameObject.Find("U_Invertida").gameObject.GetComponent<U_Invertida>();
        
        //transform.GetChild(0).GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
        //transform.GetChild(1).GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;

        startCustom = transform.GetChild(0).gameObject;
        customPanel = transform.GetChild(1).gameObject;

        panelPadre = customPanel.transform.GetChild(2).gameObject;
        panelSimbolos = startCustom.transform.GetChild(5).gameObject;

        RanuraBrilla[0] = panelPadre.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        RanuraBrilla[1] = panelPadre.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();

        RanuraBrilla[2] = panelPadre.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
        RanuraBrilla[3] = panelPadre.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>();
        RanuraBrilla[4] = panelPadre.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>();
        RanuraBrilla[5] = panelPadre.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Image>();

        for (int i = 0; i < 5; i++){
            capasPersonaje[i] = customPanel.transform.GetChild(1).GetChild(i).GetComponent<Image>();
        }

        capasChapa[0] = panelPadre.transform.GetChild(3).GetChild(1).gameObject;
        capasChapa[1] = panelPadre.transform.GetChild(3).GetChild(0).GetChild(10).gameObject;

        ropaPadre = panelPadre.transform.GetChild(2).GetChild(0).GetChild(4).gameObject;
        peinadoPadre = panelPadre.transform.GetChild(1).GetChild(0).GetChild(3).gameObject;
        colorPeinadoPadre = panelPadre.transform.GetChild(1).GetChild(1).gameObject;
        accesorioPadre = panelPadre.transform.GetChild(2).GetChild(1).GetChild(3).gameObject;
        colorChapaPadre = panelPadre.transform.GetChild(3).GetChild(0).gameObject;
    }

    void Start (){
        //Buscamos el panel de custom y el boton de comenzar
        startCustom = transform.GetChild(0).gameObject;
        customPanel = transform.GetChild(1).gameObject;
    }

    //Funcion para abrir el panel de custom
    public void ComenzarCustom(){
        customPanel.GetComponent<Animator>().enabled = true;
        startCustom.GetComponent<Animator>().enabled = true;
    }

    //Funcion para reiniciar la custom
    public void ResetCustom(){
        //startCustom.GetComponent<Animator>().enabled = false;
        //customPanel.GetComponent<Animator>().enabled = false;

        //startCustom.SetActive(true);
        //customPanel.SetActive(false);
    }

    public GameObject ropaPadre;
    public GameObject peinadoPadre;
    public GameObject colorPeinadoPadre;
    public GameObject accesorioPadre;
    public GameObject colorChapaPadre;

    public Color colorPelo;

    public void Peinado (int num){

        // BOTON ATRÁS...
        if (num == 0){

            // HOMBRE...
            if (infoJugador0[0] == 0)
            {
                if (infoJugador0[2] > 0) infoJugador0[2]--; else infoJugador0[2] = 3;

                peinadoPadre.transform.GetChild(infoJugador0[2]).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < peinadoPadre.transform.childCount; i++){
                    if (i != infoJugador0[2] && peinadoPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan){
                        peinadoPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                    }
                }
            }
            // MUJER...
            else {
                if (infoJugador0[2] > 4) infoJugador0[2]--; else infoJugador0[2] = 7;

                peinadoPadre.transform.GetChild(infoJugador0[2] - 4).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < peinadoPadre.transform.childCount; i++){
                    if (i != infoJugador0[2] - 4 && peinadoPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan){
                        peinadoPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                    }
                }
            }

            capasPersonaje[2].sprite = Root.peinados[infoJugador0[2]];
        }

        // BOTÓN ALANTE...
        if (num == 1){

            // HOMBRE...
            if (infoJugador0[0] == 0){
                if (infoJugador0[2] < 3) infoJugador0[2]++; else infoJugador0[2] = 0;

                peinadoPadre.transform.GetChild(infoJugador0[2]).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < peinadoPadre.transform.childCount; i++){
                    if (i != infoJugador0[2] && peinadoPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan){
                        peinadoPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                    }
                }
            // MUJER...
            }else{
                if (infoJugador0[2] < 7) infoJugador0[2]++; else infoJugador0[2] = 4;

                peinadoPadre.transform.GetChild(infoJugador0[2] - 4).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < peinadoPadre.transform.childCount; i++){
                    if (i != infoJugador0[2] - 4 && peinadoPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan){
                        peinadoPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                    }
                }
            }

            capasPersonaje[2].sprite = Root.peinados[infoJugador0[2]];
        }
    }

    public void ColorPeinado (int num){

        for (int i = 0; i < 5; i++){
            if(i == num){
                capasPersonaje[2].color = colorPeinadoPadre.transform.GetChild(num).transform.GetChild(0).GetComponent<Image>().color;
                infoJugador0[7] = num;
            }
        }
    }

    public void Vestuario(int num){

        #region ROPA
        if (num == 0){

            // HOMBRE...
            if (infoJugador0[0] == 0){
                if (infoJugador0[3] > 0) infoJugador0[3]--; else infoJugador0[3] = 11;

                ropaPadre.transform.GetChild(infoJugador0[3]).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < ropaPadre.transform.childCount; i++){
                    if (i != infoJugador0[3] && ropaPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan){
                        ropaPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                    }
                }
            }
            // MUJER...
            else{
                if (infoJugador0[3] > 12) infoJugador0[3]--; else infoJugador0[3] = 23;

                ropaPadre.transform.GetChild(infoJugador0[3] - 12).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < ropaPadre.transform.childCount; i++){
                    if (i != infoJugador0[3] - 12 && ropaPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan){
                        ropaPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                    }
                }
            }

            capasPersonaje[1].sprite = Root.vestimenta[infoJugador0[3]];
        }

        if (num == 1){

            // HOMBRE...
            if (infoJugador0[0] == 0){
                if (infoJugador0[3] < 11) infoJugador0[3]++; else infoJugador0[3] = 0;
                ropaPadre.transform.GetChild(infoJugador0[3]).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < ropaPadre.transform.childCount; i++){
                    if (i != infoJugador0[3] && ropaPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan){
                        ropaPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                    }
                }
            // MUJER...
            }else{
                if (infoJugador0[3] < 23) infoJugador0[3]++; else infoJugador0[3] = 12;
                ropaPadre.transform.GetChild(infoJugador0[3] - 12).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < ropaPadre.transform.childCount; i++){
                    if (i != infoJugador0[3] - 12 && ropaPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan){
                        ropaPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                    }
                }
            }
                
            capasPersonaje[1].sprite = Root.vestimenta[infoJugador0[3]];
        }
        #endregion
    
        #region ACCESORIO
        if (num == 2){

            // HOMBRE...
            if (infoJugador0[0] == 0){
                if (infoJugador0[4] > 0) infoJugador0[4]--; else infoJugador0[4] = 11;

                accesorioPadre.transform.GetChild(infoJugador0[4]).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < accesorioPadre.transform.childCount; i++){
                    if (i != infoJugador0[4] && accesorioPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan)
                        accesorioPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                }
            }
            // MUJER...
            else {

                if (infoJugador0[4] > 12) infoJugador0[4]--; else infoJugador0[4] = 23;

                accesorioPadre.transform.GetChild(infoJugador0[4] - 12).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < accesorioPadre.transform.childCount; i++){
                    if (i != infoJugador0[4] - 12 && accesorioPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan)
                        accesorioPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                }
            }

            capasPersonaje[3].sprite = Root.accesorios[infoJugador0[4]];
        }

        if (num == 3){

            // HOMBRE...
            if (infoJugador0[0] == 0){
                if (infoJugador0[4] < 11) infoJugador0[4]++; else infoJugador0[4] = 0;

                accesorioPadre.transform.GetChild(infoJugador0[4]).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < accesorioPadre.transform.childCount; i++)
                {
                    if (i != infoJugador0[4] && accesorioPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan)
                        accesorioPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                }
            }
            // MUJER...
            else {

                if (infoJugador0[4] < 23) infoJugador0[4]++; else infoJugador0[4] = 12;

                accesorioPadre.transform.GetChild(infoJugador0[4] - 12).GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < accesorioPadre.transform.childCount; i++)
                {
                    if (i != infoJugador0[4] - 12 && accesorioPadre.transform.GetChild(i).GetComponent<Image>().color == Color.cyan)
                        accesorioPadre.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                }
            }


            capasPersonaje[3].sprite = Root.accesorios[infoJugador0[4]];
        }
        #endregion
    }

    public void ColorChapa (int num){
        for (int i = 0; i < 10; i++){
            if (i == num)
            capasChapa[1].GetComponent<Image>().color = colorChapaPadre.transform.GetChild(num).GetComponent<Image>().color;
            capasChapa[2].GetComponent<Image>().color = colorChapaPadre.transform.GetChild(num).GetComponent<Image>().color;
            infoJugador0[6] = num;

            if (num == 0 || num == 3 || num == 4 || num == 1 || num == 6 || num == 7){
                Root.personajesInGame[jugador].transform.GetChild(2).transform.GetChild(Root.personajesInGame[jugador].transform.GetChild(2).transform.childCount - 1).GetComponent<Text>().color = Color.white;
            }else{
                Root.personajesInGame[jugador].transform.GetChild(2).transform.GetChild(Root.personajesInGame[jugador].transform.GetChild(2).transform.childCount - 1).GetComponent<Text>().color = Color.black;

            }

        }
    }

    public void Aceptar(int num){

        customPanel.GetComponent<Animator>().SetTrigger("2");
        startCustom.GetComponent<Animator>().SetTrigger("2");

        if (infoJugador0[6] == 0 || infoJugador0[6] == 3 || infoJugador0[6] == 4 || infoJugador0[6] == 6 || infoJugador0[6] == 7){
            Root.personajesInGame[jugador].transform.GetChild(2).transform.GetChild(Root.personajesInGame[jugador].transform.GetChild(2).transform.childCount - 1).GetComponent<Text>().color = Color.white;
        }else{
            Root.personajesInGame[jugador].transform.GetChild(2).transform.GetChild(Root.personajesInGame[jugador].transform.GetChild(2).transform.childCount - 1).GetComponent<Text>().color = Color.black;
        }

        #region MINIATURA FICHA..

        Root.personajesInGame[jugador].transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Root.pieles[infoJugador0[1]];
        Root.personajesInGame[jugador].transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().sprite = Root.peinados[infoJugador0[2]];
        Root.personajesInGame[jugador].transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().color = capasPersonaje[2].color;
        Root.personajesInGame[jugador].transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().sprite = Root.vestimenta[infoJugador0[3]];
        Root.personajesInGame[jugador].transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Image>().sprite = Root.accesorios[infoJugador0[4]];

        // MINIATURA FICHA VIRTUAL
        capasChapa[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Root.pieles[infoJugador0[1]];
        capasChapa[2].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Root.peinados[infoJugador0[2]];
        capasChapa[2].transform.GetChild(0).GetChild(1).GetComponent<Image>().color = capasPersonaje[2].color;
        capasChapa[2].transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Root.vestimenta[infoJugador0[3]];
        capasChapa[2].transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = Root.accesorios[infoJugador0[4]];

        // MINIATURA PERSONAJE DESCANSO
        if (Root.playerInGameDescanso[jugador] != null){
            Root.playerInGameDescanso[jugador].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Root.pieles[infoJugador0[1]];
            Root.playerInGameDescanso[jugador].transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Root.peinados[infoJugador0[2]];
            Root.playerInGameDescanso[jugador].transform.GetChild(1).GetChild(1).GetComponent<Image>().color = capasPersonaje[2].color;
            Root.playerInGameDescanso[jugador].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = Root.vestimenta[infoJugador0[3]];
            Root.playerInGameDescanso[jugador].transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = Root.accesorios[infoJugador0[4]];
            Root.playerInGameDescanso[jugador].transform.GetChild(1).GetChild(4).GetComponent<Image>().sprite = Root.calzado[infoJugador0[5]];

            Root.playerInGameDescanso[jugador].transform.GetChild(0).GetComponent<Image>().color = capasChapa[1].GetComponent<Image>().color;
        }

        // MINIATURA PERSONAJE COMUNICACIONES
        if (Root.playerInGameComunicacion[jugador] != null)
        {
            Root.playerInGameComunicacion[jugador].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Root.pieles[infoJugador0[1]];
            Root.playerInGameComunicacion[jugador].transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Root.peinados[infoJugador0[2]];
            Root.playerInGameComunicacion[jugador].transform.GetChild(1).GetChild(1).GetComponent<Image>().color = capasPersonaje[2].color;
            Root.playerInGameComunicacion[jugador].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = Root.vestimenta[infoJugador0[3]];
            Root.playerInGameComunicacion[jugador].transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = Root.accesorios[infoJugador0[4]];
            Root.playerInGameComunicacion[jugador].transform.GetChild(1).GetChild(4).GetComponent<Image>().sprite = Root.calzado[infoJugador0[5]];

            Root.playerInGameComunicacion[jugador].transform.GetChild(0).GetComponent<Image>().color = capasChapa[1].GetComponent<Image>().color;
        }

        // MINIATURA PERSONAJE REUNIONES
        if (Root.playerInGameReuniones[jugador] != null)
        {
            Root.playerInGameReuniones[jugador].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Root.pieles[infoJugador0[1]];
            Root.playerInGameReuniones[jugador].transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Root.peinados[infoJugador0[2]];
            Root.playerInGameReuniones[jugador].transform.GetChild(1).GetChild(1).GetComponent<Image>().color = capasPersonaje[2].color;
            Root.playerInGameReuniones[jugador].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = Root.vestimenta[infoJugador0[3]];
            Root.playerInGameReuniones[jugador].transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = Root.accesorios[infoJugador0[4]];
            Root.playerInGameReuniones[jugador].transform.GetChild(1).GetChild(4).GetComponent<Image>().sprite = Root.calzado[infoJugador0[5]];

            Root.playerInGameReuniones[jugador].transform.GetChild(0).GetComponent<Image>().color = capasChapa[1].GetComponent<Image>().color;
        }
        #endregion

        for (int i = 0; i < 8; i++){
            Root.infoGlobalPlayers[i + (jugador * 8)] = infoJugador0[i];
        }
    }

    public void BrilloRanuras(int num){

        // BRILLO GÉNERO
        if (num >= 0 && num <= 1){

            for (int i = 0; i < 2; i++){
                if (i == num){
                    //RanuraBrilla[i].color = Color.white;
                    RanuraBrilla[i].GetComponent<Button>().interactable = false;
                }
                else{
                    //RanuraBrilla[i].color = Color.grey;
                    RanuraBrilla[i].GetComponent<Button>().interactable = true;
                }
            }

            // Seleccionas HOMBRE
            if (num == 0) { infoJugador0[0] = 0; infoJugador0[1] -= 4; infoJugador0[2] -= 4; infoJugador0[3] -= 12; infoJugador0[4] -= 12; infoJugador0[5] -= 1; }
            // Seleccionas MUJER
            else { infoJugador0[0] = 1; infoJugador0[1] += 4; infoJugador0[2] += 4; infoJugador0[3] += 12; infoJugador0[4] += 12; infoJugador0[5] += 1; }

            #region 
            // LA PIEL DEL JUGADOR SERÁ SEGÚN LA INFO [1]
            capasPersonaje[0].sprite = Root.pieles[infoJugador0[1]];

            // LA ROPA DEL JUGADOR SERÁ SEGÚN LA INFO [1]
            capasPersonaje[1].sprite = Root.vestimenta[infoJugador0[3]];

            // EL PEINADO DEL JUGADOR SERÁ SEGÚN LA INFO [2]
            capasPersonaje[2].sprite = Root.peinados[infoJugador0[2]];

            // EL ACCESORIO DEL JUGADOR SERÁ SEGÚN LA INFO [3]
            capasPersonaje[3].sprite = Root.accesorios[infoJugador0[4]];

            // EL CALZADO DEL JUGADOR SERÁ SEGÚN LA INFO [4]
            capasPersonaje[4].sprite = Root.calzado[infoJugador0[5]];
            #endregion
        }

        // BRILLO Y CAMBIO -> PIEL
        if (num >= 2 && num <= 5){

            for (int i = 2; i < 6; i++){

                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }

            // HOMBRE
            if (infoJugador0[0] == 0){
                infoJugador0[1] = num - 2;
            }else{
                infoJugador0[1] = num + 2;
            }

            capasPersonaje[0].sprite = Root.pieles[infoJugador0[1]];
        }

        // BRILLO Y CAMBIO -> PELO
        if (num >= 6 && num <= 7){

            for (int i = 6; i < 8; i++)
            {
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }

        // BRILLO Y CAMBIO -> COLOR PELO
        if (num >= 8 && num <= 12){

            for (int i = 8; i < 13; i++)
            {
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }

        //BRILLO ROPA
        if (num >= 13 && num <= 17){

            for (int i = 13; i < 18; i++)
            {
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }

        // BRILLO ACCESORIOS
        if (num >= 18 && num <= 23)
        {

            for (int i = 18; i < 24; i++)
            {
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }
    }

    public void CambiarPaneles (int num){

        panelPadre.transform.GetChild(num).gameObject.SetActive(true);
        panelSimbolos.transform.GetChild(num).GetComponent<Image>().color = Color.black;

        for (int i = 0; i < panelPadre.transform.childCount; i++){

            if (num != i && panelPadre.transform.GetChild(i).gameObject.activeSelf){
                panelPadre.transform.GetChild(i).gameObject.SetActive(false);
                panelSimbolos.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
    }

   /* public void CargarVestimenta(){


        capasPersonaje[4].sprite = Root.calzado[infoJugador0[5]];
    }*/
}
