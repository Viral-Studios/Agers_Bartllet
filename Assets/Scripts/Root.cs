using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Root : MonoBehaviour{

    #region VARIABLES
    public Teclado panelTeclado;
    private OkapiBot okapi;
    public U_Invertida uInvertida;
    public Control_NPC script_npc;

    public List<int> infoGlobalPlayers;

    //Informacion necesaria del escenario
    public int currentScene = 0;
    public List<GameObject> escenario;
    public List<GameObject> playerInGame;
    public List<GameObject> playerInGameDescanso;
    public List<GameObject> playerInGameComunicacion;
    public List<GameObject> playerInGameReuniones;

    public int[] playerGo = new int[10];
    [HideInInspector] public List<Vector3> startPlayerPosition;
    [HideInInspector] public List<Vector3> startPlayerPositionDescanso;
    [HideInInspector] public List<Vector3> startPlayerPositionComunicacion;
    [HideInInspector] public List<Vector3> startPlayerPositionReuniones;

    [HideInInspector] public bool gamePaused;

    //Modo Navigation Mesh
    [Space(5)]
    [HideInInspector] public bool navMeshMode = true;

    //Sprite_Data_Base
    public List<Sprite> pieles, vestimenta, accesorios, peinados, calzado, manos;

    [Space(5)]
    public List<GameObject> personajesInGame;
    public List<GameObject> customPanels;
    [HideInInspector] public List<Sprite> peinadosInGame;
    [HideInInspector] public List<Sprite> vestimentaInGame;

    [Space(5)]
    [Header("Drageables")]
    [Space(5)]
    public GameObject SlotPersonaje;
    public List<GameObject> personajes;
    public List<Sprite> viñetas;

    [Space(5)]
    public GameObject SlotPersonajeSalaDiv;
    public List<GameObject> personajesSalaDiv;
    public List<Sprite> viñetasSalasDiv;

    [Space (5)]
    public List<GameObject> manosPadres_Rooms;

    [Space(5)]
    public Transform padreVirtual;
    public Transform posChapasGeneral;
    public Transform posChapasReuniones;
    public Transform posChapasComunicacion;

    #endregion

    #region FUNCIONES
    private void Awake () {

        // Buscamos el okapiBot y lo asignamos
        //okapi = GameObject.Find("OkapiBot").gameObject.GetComponent<OkapiBot>();

        //Buscamos los escenarios del proyecto
        GameObject escenarios = transform.GetChild(0).GetChild(0).gameObject;
        for (int i = 1; i < 6; i++) {
            escenario.Add(escenarios.transform.GetChild(i).gameObject);
        }

        //Buscamos los players in game
        for (int i = 0; i < 10; i++){
            playerInGame.Add(escenario[1].transform.GetChild(1).GetChild(i).gameObject);
            startPlayerPosition.Add(escenario[1].transform.GetChild(1).GetChild(i).transform.position);
            startPlayerPositionDescanso.Add(escenario[3].transform.GetChild(3).GetChild(i).transform.position);
            startPlayerPositionComunicacion.Add(playerInGameComunicacion[i].gameObject.transform.position);
            startPlayerPositionReuniones.Add(playerInGameReuniones[i].gameObject.transform.position);
        }
    }

    private void Start(){
        panelTeclado.gameObject.SetActive(false);
    }

    public void Go(){
        for (int i = 0; i < 10; i++){

            if (uInvertida.hidenPlayer[i] == 1 && customPanels[i].GetComponent<Custom_Player>().capasChapa[2].gameObject.activeSelf){

                playerGo[i] = 1;

                personajesInGame[i].transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = customPanels[i].GetComponent<Custom_Player>().colorChapaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[6]).GetComponent<UnityEngine.UI.Image>().color;
                personajesInGame[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = customPanels[i].GetComponent<Custom_Player>().colorChapaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[6]).GetComponent<UnityEngine.UI.Image>().color;

                manosPadres_Rooms[0].transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite = manos[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1]];

                customPanels[i].transform.GetChild(0).GetComponent<Animator>().Play(0, 0, 0);
                customPanels[i].transform.GetChild(1).GetComponent<Animator>().Play(0, 0, 0);

                customPanels[i].transform.GetChild(0).GetComponent<Animator>().Rebind();
                customPanels[i].transform.GetChild(1).GetComponent<Animator>().Rebind();

                customPanels[i].transform.GetChild(0).GetComponent<Animator>().enabled = false;
                customPanels[i].transform.GetChild(1).GetComponent<Animator>().enabled = false;

                customPanels[i].gameObject.SetActive(false);
                playerInGame[i].gameObject.SetActive(true);

                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(true);
                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(3).gameObject.SetActive(true);

                uInvertida.hidenPlayer[i] = 0;
                uInvertida.playerButton[i].GetComponent<UnityEngine.UI.Image>().color = Color.white;
            }
        }
    }

    public void UpdateCargaParticipantes(){

        for (int i = 0; i < 10; i++){

            if (!uInvertida.playersNoUsados[i]){
                //print("se actualizan los participantes " + i);

                #region Actualiza COLOR CHAPAS
                // CHAPA DE SALA GENERAL
                personajesInGame[i].transform.GetChild(1).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorChapaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[6]).GetComponent<Image>().color;
                personajesInGame[i].transform.GetChild(0).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorChapaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[6]).GetComponent<Image>().color;

                // CHAPA DE SALA DESCANSO
                playerInGameDescanso[i].transform.GetChild(0).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorChapaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[6]).GetComponent<Image>().color;
                playerInGameReuniones[i].transform.GetChild(0).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorChapaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[6]).GetComponent<Image>().color;
                playerInGameComunicacion[i].transform.GetChild(0).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorChapaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[6]).GetComponent<Image>().color;

                // CHAPA DE PANEL CUSTOM
                customPanels[i].GetComponent<Custom_Player>().ColorChapa(customPanels[i].GetComponent<Custom_Player>().infoJugador0[6]);

                // SE ACTUALIZA EL ASPECTO DEL PERSONAJE DE LA SALA DE DESCANSO
                playerInGameDescanso[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = pieles[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1]];
                playerInGameDescanso[i].transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = peinados[customPanels[i].GetComponent<Custom_Player>().infoJugador0[2]];
                playerInGameDescanso[i].transform.GetChild(1).GetChild(1).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorPeinadoPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[7]).GetChild(0).GetComponent<Image>().color;
                playerInGameDescanso[i].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = vestimenta[customPanels[i].GetComponent<Custom_Player>().infoJugador0[3]];
                playerInGameDescanso[i].transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = accesorios[customPanels[i].GetComponent<Custom_Player>().infoJugador0[4]];
                playerInGameDescanso[i].transform.GetChild(1).GetChild(4).GetComponent<Image>().sprite = calzado[customPanels[i].GetComponent<Custom_Player>().infoJugador0[5]];

                // SE ACTUALIZA EL ASPECTO DEL PERSONAJE DE LA SALA DE REUNIONES
                playerInGameReuniones[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = pieles[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1]];
                playerInGameReuniones[i].transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = peinados[customPanels[i].GetComponent<Custom_Player>().infoJugador0[2]];
                playerInGameReuniones[i].transform.GetChild(1).GetChild(1).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorPeinadoPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[7]).GetChild(0).GetComponent<Image>().color;
                playerInGameReuniones[i].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = vestimenta[customPanels[i].GetComponent<Custom_Player>().infoJugador0[3]];
                playerInGameReuniones[i].transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = accesorios[customPanels[i].GetComponent<Custom_Player>().infoJugador0[4]];
                playerInGameReuniones[i].transform.GetChild(1).GetChild(4).GetComponent<Image>().sprite = calzado[customPanels[i].GetComponent<Custom_Player>().infoJugador0[5]];

                // SE ACTUALIZA EL ASPECTO DEL PERSONAJE DE LA SALA DE COMUNICACIONES
                playerInGameComunicacion[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = pieles[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1]];
                playerInGameComunicacion[i].transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = peinados[customPanels[i].GetComponent<Custom_Player>().infoJugador0[2]];
                playerInGameComunicacion[i].transform.GetChild(1).GetChild(1).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorPeinadoPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[7]).GetChild(0).GetComponent<Image>().color;
                playerInGameComunicacion[i].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = vestimenta[customPanels[i].GetComponent<Custom_Player>().infoJugador0[3]];
                playerInGameComunicacion[i].transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = accesorios[customPanels[i].GetComponent<Custom_Player>().infoJugador0[4]];
                playerInGameComunicacion[i].transform.GetChild(1).GetChild(4).GetComponent<Image>().sprite = calzado[customPanels[i].GetComponent<Custom_Player>().infoJugador0[5]];

                // SE ACTUALIZA EL ASPECTO DEL PERSONAJE DE LA SALA GENERAL
                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = pieles[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1]];
                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().sprite = peinados[customPanels[i].GetComponent<Custom_Player>().infoJugador0[2]];
                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorPeinadoPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[7]).GetChild(0).GetComponent<Image>().color;
                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().sprite = vestimenta[customPanels[i].GetComponent<Custom_Player>().infoJugador0[3]];
                personajesInGame[i].transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Image>().sprite = accesorios[customPanels[i].GetComponent<Custom_Player>().infoJugador0[4]];

                // SE ACTUALIZAN LAS MANOS DE LA SALA WORKING ROOM
                manosPadres_Rooms[0].transform.GetChild(i).GetComponent<Image>().sprite = manos[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1]];

                // SE ACTUALIZA LA APARIENCIA DEL PANEL DE CUSTOMIZACIÓN
                customPanels[i].GetComponent<Custom_Player>().capasPersonaje[0].sprite = pieles[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1]];
                customPanels[i].GetComponent<Custom_Player>().capasPersonaje[1].sprite = vestimenta[customPanels[i].GetComponent<Custom_Player>().infoJugador0[3]];
                customPanels[i].GetComponent<Custom_Player>().capasPersonaje[2].sprite = peinados[customPanels[i].GetComponent<Custom_Player>().infoJugador0[2]];
                customPanels[i].GetComponent<Custom_Player>().capasPersonaje[2].color = playerInGameDescanso[i].transform.GetChild(1).GetChild(1).GetComponent<Image>().color = customPanels[i].GetComponent<Custom_Player>().colorPeinadoPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[7]).GetChild(0).GetComponent<Image>().color;
                customPanels[i].GetComponent<Custom_Player>().capasPersonaje[3].sprite = accesorios[customPanels[i].GetComponent<Custom_Player>().infoJugador0[4]];
                customPanels[i].GetComponent<Custom_Player>().capasPersonaje[4].sprite = calzado[customPanels[i].GetComponent<Custom_Player>().infoJugador0[5]];

                // SE ACTUALIZA EL SISTEMA DE BRILLOS DEL GÉNERO
                if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[0] == 1){
                    customPanels[i].GetComponent<Custom_Player>().RanuraBrilla[0].GetComponent<Button>().interactable = true;
                    customPanels[i].GetComponent<Custom_Player>().RanuraBrilla[1].GetComponent<Button>().interactable = false;
                }

                // SE ACTUALIZA EL SISTEMA DE BRILLOS DE LA PIEL
                if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[1] != 0){
                    customPanels[i].GetComponent<Custom_Player>().RanuraBrilla[2].color = Color.grey;

                    // SI ES HOMBRE...
                    if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[0] == 0){
                        customPanels[i].GetComponent<Custom_Player>().RanuraBrilla[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1] + 2].color = Color.white;
                    }else{
                        customPanels[i].GetComponent<Custom_Player>().RanuraBrilla[customPanels[i].GetComponent<Custom_Player>().infoJugador0[1] - 2].color = Color.white;
                    }
                }

                // SE ACTUALIZA EL SISTEMA DE BRILLOS DEL PEINADO
                if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[2] != 0 || customPanels[i].GetComponent<Custom_Player>().infoJugador0[2] != 4){
                    customPanels[i].GetComponent<Custom_Player>().peinadoPadre.transform.GetChild(0).GetComponent<Image>().color = Color.black;

                    // SI ES HOMBRE...
                    if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[0] == 0){
                        customPanels[i].GetComponent<Custom_Player>().peinadoPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[2]).GetComponent<Image>().color = Color.cyan;
                    }else{
                        customPanels[i].GetComponent<Custom_Player>().peinadoPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[2] - 4).GetComponent<Image>().color = Color.cyan;
                    }
                }

                // SE ACTUALIZA EL SISTEMA DE BRILLOS DE LA ROPA
                if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[3] != 0 || customPanels[i].GetComponent<Custom_Player>().infoJugador0[3] != 12)
                {
                    customPanels[i].GetComponent<Custom_Player>().ropaPadre.transform.GetChild(0).GetComponent<Image>().color = Color.black;

                    // SI ES HOMBRE...
                    if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[0] == 0)
                    {
                        customPanels[i].GetComponent<Custom_Player>().ropaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[3]).GetComponent<Image>().color = Color.cyan;
                    }
                    else
                    {
                        customPanels[i].GetComponent<Custom_Player>().ropaPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[3] - 12).GetComponent<Image>().color = Color.cyan;
                    }
                }

                // SE ACTUALIZA EL SISTEMA DE BRILLOS DE ACCESORIOS
                if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[4] != 0 || customPanels[i].GetComponent<Custom_Player>().infoJugador0[4] != 12)
                {
                    customPanels[i].GetComponent<Custom_Player>().accesorioPadre.transform.GetChild(0).GetComponent<Image>().color = Color.black;

                    // SI ES HOMBRE...
                    if (customPanels[i].GetComponent<Custom_Player>().infoJugador0[0] == 0)
                    {
                        customPanels[i].GetComponent<Custom_Player>().accesorioPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[4]).GetComponent<Image>().color = Color.cyan;
                    }
                    else
                    {
                        customPanels[i].GetComponent<Custom_Player>().accesorioPadre.transform.GetChild(customPanels[i].GetComponent<Custom_Player>().infoJugador0[4] - 12).GetComponent<Image>().color = Color.cyan;
                    }
                }
                #endregion
            }
            }
    }

    public void Full(){
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
    }
    #endregion

    public void RefrescarNombresEtiquetas(int num){

        if (playerInGameDescanso[num].transform.GetChild(2).GetComponent<InputField>().text != playerInGame[num].transform.GetChild(2).GetComponent<InputField>().text){
            playerInGameDescanso[num].transform.GetChild(2).GetComponent<InputField>().text = playerInGame[num].transform.GetChild(2).GetComponent<InputField>().text;
            playerInGameComunicacion[num].transform.GetChild(2).GetComponent<InputField>().text = playerInGame[num].transform.GetChild(2).GetComponent<InputField>().text;
            playerInGameReuniones[num].transform.GetChild(2).GetComponent<InputField>().text = playerInGame[num].transform.GetChild(2).GetComponent<InputField>().text;
        }
    }

    public void UpdateChapas_SalaDescanso (int num){
        playerInGame[num].transform.position = posChapasGeneral.GetChild(num).gameObject.transform.position;
    }

    public void UpdateChapas_SalaReuniones(int num){
        playerInGame[num].transform.position = posChapasReuniones.GetChild(num).gameObject.transform.position;
    }

    public void UpdateChapas_SalaComunicacion(int num){
        playerInGame[num].transform.position = posChapasComunicacion.GetChild(num).gameObject.transform.position;
    }

    public void UpdateChapas_SalaWorking (int num){
        playerInGame[num].transform.position = playerInGame[num].GetComponent<Player_Script>().Vector3MesaMapaGeneral;

        float rotX = playerInGame[num].GetComponent<Player_Script>().rotX;
        Vector3 rot = new Vector3(rotX, -90, 90);
        playerInGame[num].gameObject.transform.localEulerAngles = rot;


    }
}
