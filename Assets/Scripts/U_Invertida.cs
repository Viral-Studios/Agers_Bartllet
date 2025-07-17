using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class U_Invertida : MonoBehaviour{

    Root script;

    public Transform[] paneles_UI = new Transform[3];

    public List<GameObject> customPlayer;
    public List<Button> playerButton;
    public List<Button> toolsButton;
    public string modo = "default";
    //Informacion sobre que botones de la U_Invertida estan pulsados
    [HideInInspector] public List<int> hidenPlayer;
    [HideInInspector] public List<int> blockPlayer;
    public int[] atrilPlayer = new int[10];
    [HideInInspector] public bool[] playersNoUsados = new bool[10];

    //Variables para realizar el intercambio de posiciones
    private GameObject firstPlayerSelected;
    public GameObject teclado;
    private Vector3 tecladoPos;

    public int[] int_mapaParticipantes = new int[10];

    private void Awake(){
        script = transform.root.GetComponent<Root>();
    }

    private void Start()
    {
        //Iniciamos la informacion de la UI:    1 para los botones activos; 0 para los bontones inactivos
        for (int i = 0; i < 10; i++) { hidenPlayer.Add(1); } 
        for (int i = 0; i < 10; i++) { blockPlayer.Add(0); }

        //Buscamos el panel del teclado
        tecladoPos = teclado.transform.localPosition;
    }

    private int p1, p1_mapa, p2, p2_mapa;

    public void Ui_Player(int numPlayer){
        
        //Cuando estamos en modo mostrar/ocultar jugadores
        if (modo == "hiden_players"){
            if (hidenPlayer[numPlayer] == 0){
                //print("mimi");
                hidenPlayer[numPlayer] = 1;
                playerButton[numPlayer].GetComponent<Image>().color = Color.cyan;

                if (script.playerInGame[numPlayer].gameObject.activeSelf){
                    script.playerInGame[numPlayer].gameObject.SetActive(false);
                    script.manosPadres_Rooms[0].transform.GetChild(numPlayer).gameObject.SetActive(false);
                    script.playerInGameDescanso[numPlayer].gameObject.SetActive(false);
                }

                customPlayer[numPlayer].SetActive(true);
            }else{
                hidenPlayer[numPlayer] = 0;
                playerButton[numPlayer].GetComponent<Image>().color = Color.white;
                //print("momo");

                customPlayer[numPlayer].transform.GetChild(0).GetComponent<Animator>().Play(0, 0, 0);
                customPlayer[numPlayer].transform.GetChild(1).GetComponent<Animator>().Play(0, 0, 0);

                customPlayer[numPlayer].transform.GetChild(0).GetComponent<Animator>().Rebind();
                customPlayer[numPlayer].transform.GetChild(1).GetComponent<Animator>().Rebind();

                customPlayer[numPlayer].transform.GetChild(0).GetComponent<Animator>().enabled = false;
                customPlayer[numPlayer].transform.GetChild(1).GetComponent<Animator>().enabled = false;

                script.manosPadres_Rooms[0].transform.GetChild(numPlayer).gameObject.SetActive(false);
                customPlayer[numPlayer].GetComponent<Custom_Player>().ResetCustom();

                if (script.playerGo[numPlayer] == 1) { 
                    script.playerInGame[numPlayer].gameObject.SetActive(true);
                    script.manosPadres_Rooms[0].transform.GetChild(numPlayer).gameObject.SetActive(true);
                    script.playerInGameDescanso[numPlayer].gameObject.SetActive(true);
                }

                customPlayer[numPlayer].SetActive(false);
            }
        }
        
        //Cuando estamos en modo bloquear jugadores
        if (modo == "block_players")
        {
            if (blockPlayer[numPlayer] == 0)
            {
                blockPlayer[numPlayer] = 1;
                playerButton[numPlayer].GetComponent<Image>().color = Color.cyan;
                script.playerInGame[numPlayer].GetComponent<Player_Script>().locked = true;
                script.playerInGameDescanso[numPlayer].GetComponent<Player_Script>().locked = true;
                script.playerInGameComunicacion[numPlayer].GetComponent<Player_Script>().locked = true;
                script.playerInGameReuniones[numPlayer].GetComponent<Player_Script>().locked = true;
            }
            else{
                blockPlayer[numPlayer] = 0;
                playerButton[numPlayer].GetComponent<Image>().color = Color.white;
                script.playerInGame[numPlayer].GetComponent<Player_Script>().locked = false;
                script.playerInGameDescanso[numPlayer].GetComponent<Player_Script>().locked = false;
                script.playerInGameComunicacion[numPlayer].GetComponent<Player_Script>().locked = false;
                script.playerInGameReuniones[numPlayer].GetComponent<Player_Script>().locked = false;
            }
        }
        
        //Cuando estamos en modo intercambio de jugadores
        if(modo== "switch_players"){

            if (firstPlayerSelected == null){

                p1 = numPlayer;
                p1_mapa = int_mapaParticipantes[numPlayer];
                print("[1] PLAYER " + p1 + " esta en la sala " + p1_mapa);

                firstPlayerSelected = script.playerInGame[numPlayer];
                playerButton[numPlayer].GetComponent<Image>().color = Color.cyan;
            }else{

                if (firstPlayerSelected == script.playerInGame[numPlayer]){
                    firstPlayerSelected = null;
                    playerButton[numPlayer].GetComponent<Image>().color = Color.white;
                }else{

                    p2 = numPlayer;
                    p2_mapa = int_mapaParticipantes[numPlayer];
                    print("[2] PLAYER " + p2 + " esta en la sala " + p2_mapa);

                    int_mapaParticipantes[p1] = p2_mapa;
                    int_mapaParticipantes[p2] = p1_mapa;

                    #region SE MANDA EL PLAYER 1 SELECC
                    if (int_mapaParticipantes[p1] == 0){
                        print("PLAYER " + p1 + " VA A SALA GENERAL");
                        AutoPlayer_MapaGeneral(p1);
                    }

                    if (int_mapaParticipantes[p1] == 1){
                        print("PLAYER " + p1 + " VA A SALA WORKING");
                        AutoPlayer_MapaSalaWorking(p1);
                    }

                    if (int_mapaParticipantes[p1] == 2){
                        print("PLAYER " + p1 + " VA A SALA DESCANSO");
                        AutoPlayer_MapaSalaDescanso(p1);
                    }

                    if (int_mapaParticipantes[p1] == 3){
                        print("PLAYER " + p1 + " VA A SALA COMUNICACIONES");
                        AutoPlayer_MapaComunicacion(p1);
                    }

                    if (int_mapaParticipantes[p1] == 4){
                        print("PLAYER " + p1 + " VA A SALA REUNIONES");
                        AutoPlayer_MapaReuniones(p1);
                    }
                    #endregion

                    #region SE MANDA EL PLAYER 2 SELECC
                    if (int_mapaParticipantes[p2] == 0)
                    {
                        print("PLAYER " + p2 + " VA A SALA GENERAL");
                        AutoPlayer_MapaGeneral(p2);
                    }

                    if (int_mapaParticipantes[p2] == 1)
                    {
                        print("PLAYER " + p2 + " VA A SALA WORKING");
                        AutoPlayer_MapaSalaWorking(p2);
                    }

                    if (int_mapaParticipantes[p2] == 2)
                    {
                        print("PLAYER " + p2 + " VA A SALA DESCANSO");
                        AutoPlayer_MapaSalaDescanso(p2);
                    }

                    if (int_mapaParticipantes[p2] == 3)
                    {
                        print("PLAYER " + p2 + " VA A SALA COMUNICACIONES");
                        AutoPlayer_MapaComunicacion(p2);
                    }

                    if (int_mapaParticipantes[p2] == 4)
                    {
                        print("PLAYER " + p2 + " VA A SALA REUNIONES");
                        AutoPlayer_MapaReuniones(p2);
                    }
                    #endregion

                    script.playerInGame[numPlayer].GetComponent<Player_Script>().selected = true;
                    script.playerInGame[numPlayer].GetComponent<Player_Script>().targetVector = firstPlayerSelected.transform.position;
                    firstPlayerSelected.GetComponent<Player_Script>().selected = true;
                    firstPlayerSelected.GetComponent<Player_Script>().targetVector = script.playerInGame[numPlayer].transform.position;


                    firstPlayerSelected = null;
                    RetornoUinvertida();
                }
            }
        }
        
        //Cuando queremos mandar al jugador a su punto de inicio
        if (modo == "start_position_players")
        {

            // SALA GENERAL
            if (script.currentScene == 1){
                script.playerInGame[numPlayer].GetComponent<Player_Script>().selected = true;
                script.playerInGame[numPlayer].GetComponent<Player_Script>().targetVector = script.startPlayerPosition[numPlayer];
            }

            // SALA DESCANSO
            if (script.currentScene == 3){
                script.playerInGameDescanso[numPlayer].GetComponent<Player_Script>().selected = true;
                script.playerInGameDescanso[numPlayer].GetComponent<Player_Script>().targetVector = script.startPlayerPositionDescanso[numPlayer];
            }

            // SALA COMUNICACIONES // REUNIONES
            if (script.currentScene == 4){

                if (script.playerInGameReuniones[numPlayer].gameObject.activeSelf){
                    script.playerInGameReuniones[numPlayer].GetComponent<Player_Script>().selected = true;
                    script.playerInGameReuniones[numPlayer].GetComponent<Player_Script>().targetVector = script.startPlayerPositionReuniones[numPlayer];
                }

                if (script.playerInGameComunicacion[numPlayer].gameObject.activeSelf){
                    script.playerInGameComunicacion[numPlayer].GetComponent<Player_Script>().selected = true;
                    script.playerInGameComunicacion[numPlayer].GetComponent<Player_Script>().targetVector = script.startPlayerPositionComunicacion[numPlayer];
                }
            }
        }
        
        //Cuando estamos en modo teclado
        if (modo == "teclado"){
            print("entra en teclado");

            teclado.GetComponent<Teclado>().target = script.playerInGame[numPlayer].transform.GetChild(script.playerInGame[numPlayer].transform.childCount - 1).GetComponent<InputField>();
            teclado.GetComponent<Teclado>().retornoInputField.text = teclado.GetComponent<Teclado>().target.text;
            teclado.GetComponent<Teclado>().caretPosition = teclado.GetComponent<Teclado>().target.caretPosition;
        }

        if (modo == "atril"){
            if (atrilPlayer[numPlayer] == 0){
                atrilPlayer[numPlayer] = 1;
                playerButton[numPlayer].GetComponent<Image>().color = Color.cyan;
                playerButton[3].GetComponent<Image>().color = Color.white;


                script.playerInGameComunicacion[numPlayer].gameObject.SetActive(false);
            }
            else{
                atrilPlayer[numPlayer] = 0;
                playerButton[numPlayer].GetComponent<Image>().color = Color.white;
                script.playerInGameComunicacion[numPlayer].gameObject.SetActive(true);
            }
        }
    }

    public void ToolSelection(int numTool){

        // VISIÓN. Muetra las etiquetas de los jugadores
        if (numTool == 0){
            if (toolsButton[numTool].GetComponent<Image>().color == Color.white){

                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;

                for (int i = 0; i < 10; i++)
                {
                    script.playerInGame[i].transform.GetChild(2).gameObject.SetActive(true);
                    script.playerInGameDescanso[i].transform.GetChild(2).gameObject.SetActive(true);
                    script.playerInGameComunicacion[i].transform.GetChild(2).gameObject.SetActive(true);
                    script.playerInGameReuniones[i].transform.GetChild(2).gameObject.SetActive(true);
                }

            }else{

                toolsButton[numTool].GetComponent<Image>().color = Color.white;

                for (int i = 0; i < 10; i++)
                {
                    script.playerInGame[i].transform.GetChild(2).gameObject.SetActive(false);
                    script.playerInGameDescanso[i].transform.GetChild(2).gameObject.SetActive(false);
                    script.playerInGameComunicacion[i].transform.GetChild(2).gameObject.SetActive(false);
                    script.playerInGameReuniones[i].transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }

        // ATRIL. Herramienta para ON/OFF participantes ¡SÓLO! DEL SUBMAPA COMUNICACIÓN
        if (numTool == 1){
            if (toolsButton[numTool].GetComponent<Image>().color == Color.white){

                modo = "atril";
                paneles_UI[0].gameObject.SetActive(true);
                if (script.currentScene == 1){ paneles_UI[3].gameObject.SetActive(true);}
                paneles_UI[1].gameObject.SetActive(false);
                paneles_UI[2].gameObject.SetActive(false);

                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;
                for (int i = 0; i < 10; i++){

                    if (script.playerGo [i] == 1 && int_mapaParticipantes[i] == 3){
                        print("SE ACTIVAN");
                        playerButton[i].interactable = true;
                    }else{
                        print("SE DESACTIVAN");
                        playerButton[i].interactable = false;
                    }

                    if (atrilPlayer[i] == 0){
                        playerButton[i].GetComponent<Image>().color = Color.white;
                    }
                    else { playerButton[i].GetComponent<Image>().color = Color.cyan; }
                }
            }else{

                modo = "default";
                toolsButton[numTool].GetComponent<Image>().color = Color.white;
                for (int i = 0; i < 10; i++){
                    if (!playerButton[i].interactable){
                        print("SE DESACTIVAN");
                        playerButton[i].interactable = true;
                    }
                }
            }
        } else{
            for (int i = 0; i < 10; i++){
                if (!playerButton[i].interactable){
                    playerButton[i].interactable = true;
                }
            }
        }

        //Herramienta para seleccionar a los jugadores
        if (numTool == 2){
            if (toolsButton[numTool].GetComponent<Image>().color == Color.white){
                modo = "hiden_players";

                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;

                paneles_UI[0].gameObject.SetActive(true);
                if (script.currentScene == 1) { paneles_UI[3].gameObject.SetActive(true); }
                paneles_UI[1].gameObject.SetActive(false);
                paneles_UI[2].gameObject.SetActive(false);

                for (int i = 0; i < 10; i++){
                    if (script.playerGo[i] == 0){
                        playerButton[i].GetComponent<Image>().color = Color.cyan;
                    }else{
                        playerButton[i].GetComponent<Image>().color = Color.white;
                    }
                }
            }
            else{
                modo = "default";

                toolsButton[numTool].GetComponent<Image>().color = Color.white;

                for (int i = 0; i < 10; i++){
                    playerButton[i].GetComponent<Image>().color = Color.white;
                }
            }
        }

        // TECLADO. Herramienta para abrir y cerrar el teclado
        if (numTool == 3){

            if (toolsButton[numTool].GetComponent<Image>().color == Color.white){
                modo = "teclado";
                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;

                teclado.SetActive(true);
                teclado.transform.localPosition = tecladoPos;
                teclado.transform.localEulerAngles = Vector3.zero;                
            }
            else {
                modo = "default";
                toolsButton[numTool].GetComponent<Image>().color = Color.white;
                teclado.SetActive(false);
                teclado.GetComponent<Teclado>().target = null;
                teclado.GetComponent<Teclado>().retornoInputField.text = "";                
            }
        }

        // Herramienta para Customizar la Subdivisión de SALA COMUNICACIÓN - SALA REUNIONES
        if (numTool == 4){

            for (int i = 0; i < 10; i++)
            {
                if (script.playerGo[i] == 1)
                {
                    if (!imageParticipantes[i].gameObject.activeSelf)
                    {
                        imageParticipantes[i].gameObject.SetActive(true);
                        //print("se activan " + i);
                    }
                }
                else
                {
                    if (imageParticipantes[i].gameObject.activeSelf)
                    {
                        imageParticipantes[i].gameObject.SetActive(false);
                        //print("se desactivan " + i);
                    }
                }
            }

            if (toolsButton[numTool].GetComponent<Image>().color == Color.white){
                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;

                paneles_UI[0].gameObject.SetActive(false);
                paneles_UI[3].gameObject.SetActive(false);
                paneles_UI[1].gameObject.SetActive(true);
                paneles_UI[2].gameObject.SetActive(false);
            }
            else
            {
                toolsButton[numTool].GetComponent<Image>().color = Color.white;

                paneles_UI[0].gameObject.SetActive(true);
                if (script.currentScene == 1) { paneles_UI[3].gameObject.SetActive(true); }
                paneles_UI[1].gameObject.SetActive(false);
                paneles_UI[2].gameObject.SetActive(false);
            }
        }

        //Herramienta para Dividir a los participantes en distintos escenarios
        if (numTool == 5)
        {

            if (toolsButton[numTool].GetComponent<Image>().color == Color.white)
            {
                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;

                paneles_UI[0].gameObject.SetActive(false);
                paneles_UI[1].gameObject.SetActive(false);
                paneles_UI[2].gameObject.SetActive(true);
                paneles_UI[3].gameObject.SetActive(false);
            }
            else
            {
                toolsButton[numTool].GetComponent<Image>().color = Color.white;

                paneles_UI[0].gameObject.SetActive(true);
                if (script.currentScene == 1) { paneles_UI[3].gameObject.SetActive(true); }
                paneles_UI[1].gameObject.SetActive(false);
                paneles_UI[2].gameObject.SetActive(false);
            }
        }


        if (toolsButton[7].GetComponent<Image>().color == Color.cyan){
            toolsButton[7].GetComponent<Image>().color = Color.white;
        }

        for (int i = 0; i < 9; i++){

            if (i > 5){
                if (toolsButton[i].GetComponent<Image>().color == Color.cyan){

                    toolsButton[i].GetComponent<Image>().color = Color.white;
                }
            }
            if (i < 6){
                if (numTool != i && numTool != 0 && numTool != 3){
                    if (i != 0 && i != 3){
                        if (toolsButton[i].GetComponent<Image>().color == Color.cyan){
                            toolsButton[i].GetComponent<Image>().color = Color.white;
                        }
                    }
                }
            }
        }

        //Retornamos los botones pulsados de la U_Invertida
        //RetornoUinvertida();
    }

    public void INT_ON(){

        if (script.currentScene == 1){

            if (paneles_UI[0].gameObject.activeSelf){
                paneles_UI[3].gameObject.SetActive(true);
            }
        }
    }

    public void Triada_UI (int numTool){

        for (int i = 0; i < 10; i++){
            if (i < 6 && toolsButton[i].GetComponent<Image>().color == Color.cyan){
                toolsButton[i].GetComponent<Image>().color = Color.white;
            }

            playerButton[i].interactable = true;
            playerButton[i].GetComponent<Image>().color = Color.white;             
        }

        //Herramienta para mover a los jugadores a su posicion inicial
        if (numTool == 6){
            if (toolsButton[numTool].GetComponent<Image>().color == Color.white){
                modo = "start_position_players";
                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;

                paneles_UI[0].gameObject.SetActive(true);
                if (script.currentScene == 1) { paneles_UI[3].gameObject.SetActive(true); }
                paneles_UI[1].gameObject.SetActive(false);
                paneles_UI[2].gameObject.SetActive(false);

            } else{
                toolsButton[numTool].GetComponent<Image>().color = Color.white;
                modo = "default";
            }
        }

        //Herramienta para intercambiar la posicion de los jugadores
        if (numTool == 7){
            if (toolsButton[numTool].GetComponent<Image>().color == Color.white){
                modo = "switch_players";
                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;

                paneles_UI[0].gameObject.SetActive(true);
                if (script.currentScene == 1) { paneles_UI[3].gameObject.SetActive(true); }
                paneles_UI[1].gameObject.SetActive(false);
                paneles_UI[2].gameObject.SetActive(false);

            }else{
                toolsButton[numTool].GetComponent<Image>().color = Color.white;
                modo = "default";
            }
        }

        //Herramienta para el modo bloquear jugadores
        if (numTool == 8){
            if (toolsButton[numTool].GetComponent<Image>().color == Color.white){
                modo = "block_players";
                toolsButton[numTool].GetComponent<Image>().color = Color.cyan;

                paneles_UI[0].gameObject.SetActive(true);
                if (script.currentScene == 1) { paneles_UI[3].gameObject.SetActive(true); }
                paneles_UI[1].gameObject.SetActive(false);
                paneles_UI[2].gameObject.SetActive(false);

                for (int i = 0; i < 10; i++){
                    if (blockPlayer[i] == 1){
                        playerButton[i].GetComponent<Image>().color = Color.cyan;
                    }else{
                        playerButton[i].GetComponent<Image>().color = Color.white;
                    }
                }
            }
            else{
                toolsButton[numTool].GetComponent<Image>().color = Color.white;
                modo = "default";

                for (int i = 0; i < 10; i++){
                    playerButton[i].GetComponent<Image>().color = Color.white;
                }
            }
        }

        for (int i = 6; i < 9; i++){
            if (numTool != i){ toolsButton[i].GetComponent<Image>().color = Color.white; }
        }
    }

    public void UpdateAtril(int player){

        if (modo == "atril")
        {

            for (int i = 0; i < 10; i++)
            {

                if (script.playerGo[i] == 1 && int_mapaParticipantes[i] == 3)
                {
                    print("SE ACTIVAN");
                    playerButton[i].interactable = true;
                }
                else
                {
                    print("SE DESACTIVAN");
                    playerButton[i].interactable = false;
                }
            }
        }
    }

    public void ApplyAtril(int numPlayer){


        if (script.playerGo[numPlayer] == 1 && int_mapaParticipantes[numPlayer] == 3){

            if (atrilPlayer[numPlayer] == 1){
                script.playerInGameComunicacion[numPlayer].gameObject.SetActive(false);
                print("[DESACTIVADO] está en atril player " + numPlayer);
            }else{
                script.playerInGameComunicacion[numPlayer].gameObject.SetActive(true);
                print("[ACTIVADO] está en la sala player " + numPlayer);
            }
        }
    }

    public void ResetAtril (int player){
        if (modo == "atril"){

            atrilPlayer[player] = 0;
            playerButton[player].GetComponent<Image>().color = Color.white;
            playerButton[player].interactable = false;
        }
    }

    public void RetornoUinvertida()
    {
        //Muestra la U_Invertida default
        if (modo == "default" || modo == "start_position_players" || modo == "switch_players" || modo == "teclado")
        {
            for (int i = 0; i < 10; i++) { playerButton[i].GetComponent<Image>().color = Color.white; }
        }
        //Muestra la U_Invertida hiden_players
        if (modo == "hiden_players")
        {
            for (int i = 0; i < 10; i++)
            {
                if (hidenPlayer[i] == 0) { playerButton[i].GetComponent<Image>().color = Color.white; }
                else { playerButton[i].GetComponent<Image>().color = Color.cyan; }
            }
        }
        //Muestra la U_Invertida block_players
        if (modo == "block_players")
        {
            for (int i = 0; i < 10; i++)
            {
                if (blockPlayer[i] == 0) { playerButton[i].GetComponent<Image>().color = Color.white; }
                else { playerButton[i].GetComponent<Image>().color = Color.cyan; }
            }
        }
    }

    [Header("Variables ModoDividirParticipantes")]
    public bool[] boolMapaDivididos = new bool[5];
    public Image[] imageMapas = new Image[5];
    public Image[] imageParticipantes = new Image[10];
    public Color[] colorMapaDivididos = new Color[4];

    public Transform padreBotonesMapas;

    public void DivididosElegirMapa(int num) {
        for (int i = 0; i < 6; i++){
            if (num == i){

                

                if (boolMapaDivididos[i]) {
                    //boolMapaDivididos[i] = false;
                    //imageMapas[i].enabled = false;
                }
                else {
                    boolMapaDivididos[i] = true;
                    imageMapas[i].enabled = true;
                }
            }else{
                if (boolMapaDivididos[i]){
                    boolMapaDivididos[i] = false;
                    imageMapas[i].enabled = false;
                }
            }
        }

        return;
    }

    public void DivididosAsignarParticipanteMapa(int num){
        
        for (int i = 0; i < 6; i++){

                if (boolMapaDivididos[i]){
                    imageParticipantes[num].color = colorMapaDivididos[i];
                    DivididosMandarParticipantes(num);

                /*if (!script.playerInGame[num].activeSelf) { script.playerInGame[num].SetActive(true); }
                if (!script.playerInGameDescanso[num].activeSelf) { script.playerInGameDescanso[num].SetActive(true); }
                if (!script.manosPadres_Rooms[0].transform.GetChild(i).gameObject.activeSelf) { script.manosPadres_Rooms[0].transform.GetChild(i).gameObject.SetActive(true); }*/

                if (script.currentScene != 1){

                    if (boolMapaDivididos[1]){
                        print("UPDATE CHAPA PARTICIPANTE A WORKING" + num);
                        //script.UpdateChapas_SalaDescanso(num);
                        script.UpdateChapas_SalaWorking(num);
                    }

                    if (boolMapaDivididos[2]){
                        print("UPDATE CHAPA PARTICIPANTE A DESCANSO" + num);
                        script.UpdateChapas_SalaDescanso(num);
                    }

                    if (boolMapaDivididos[3]){
                        print("UPDATE CHAPA PARTICIPANTE A COMUNICACION" + num);
                        script.UpdateChapas_SalaComunicacion(num);
                    }

                    if (boolMapaDivididos[4]){
                        print("UPDATE CHAPA PARTICIPANTE A REUNIONES" + num);
                        script.UpdateChapas_SalaReuniones(num);
                    }
                }

                atrilPlayer[num] = 0;
                ResetAtril(num);
                return;
            }        
        }
    }

    public void UpdateMandarParticipante_SubMapa(int num, int color){

            if (color == 0)
            {
                //print("Participante " + num + " va al mapa general");
                script.playerInGameComunicacion[num].SetActive(false);
                script.playerInGameDescanso[num].SetActive(false);
                script.playerInGameReuniones[num].SetActive(false);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
                return;
            }

            if (color == 1)
            {
                //print("Participante " + num + " va al mapa working");
                script.playerInGameComunicacion[num].SetActive(false);
                script.playerInGameDescanso[num].SetActive(false);
                script.playerInGameReuniones[num].SetActive(false);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(true);
                return;
            }

            if (color == 2)
            {
                //print("Participante " + num + " va al mapa descanso");
                script.playerInGameComunicacion[num].SetActive(false);
                script.playerInGameDescanso[num].SetActive(true);
                script.playerInGameReuniones[num].SetActive(false);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
                return;
            }

            if (color == 3)
            {
                //print("Participante " + num + " va al mapa COMUNICACION");
                script.playerInGameComunicacion[num].SetActive(true);
                script.playerInGameDescanso[num].SetActive(false);
                script.playerInGameReuniones[num].SetActive(false);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
                return;
            }

            if (color == 4)
            {
                //print("Participante " + num + " va al mapa REUNIONES");
                script.playerInGameComunicacion[num].SetActive(false);
                script.playerInGameDescanso[num].SetActive(false);
                script.playerInGameReuniones[num].SetActive(true);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
                return;
            }
        
    }

    public void DivididosSeleccTodos(){
        for (int o = 0; o < 5; o++){
            if (boolMapaDivididos[o]){
                for (int i = 0; i < 10; i++){
                    imageParticipantes[i].color = colorMapaDivididos[o];
                    DivididosMandarParticipantes(i);
                }
            }
        }
    }

    public void DivididosDeseleccTodos(){
        for (int i = 0; i < 10; i++){

            if (script.playerGo[i] == 1){
                if (imageParticipantes[i].color != Color.white){
                    imageParticipantes[i].color = Color.white;

                    if (!script.playerInGame[i].activeSelf) { script.playerInGame[i].SetActive(true); }
                    if (!script.playerInGameDescanso[i].activeSelf) { script.playerInGameDescanso[i].SetActive(true); }
                    if (!script.manosPadres_Rooms[0].transform.GetChild(i).gameObject.activeSelf) { script.manosPadres_Rooms[0].transform.GetChild(i).gameObject.SetActive(true); }
                }
            }
        }
    }

    public void DivididosMandarParticipantes(int num){

        if (script.playerGo[num] != 0){

            if (boolMapaDivididos[0]){
                int_mapaParticipantes[num] = 0;
                print("Participante " + num + " va al mapa general");
                script.playerInGameComunicacion[num].SetActive(false);
                script.playerInGameDescanso[num].SetActive(false);
                script.playerInGameReuniones[num].SetActive(false);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
                return;
            }

            if (boolMapaDivididos[1]){
                int_mapaParticipantes[num] = 1;
                print("Participante " + num + " va al mapa working");
                script.playerInGameComunicacion[num].SetActive(false);
                script.playerInGameDescanso[num].SetActive(false);
                script.playerInGameReuniones[num].SetActive(false);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(true);
                return;
            }

            if (boolMapaDivididos[2]){
                int_mapaParticipantes[num] = 2;
                print("Participante " + num + " va al mapa descanso");
                script.playerInGameComunicacion[num].SetActive(false);
                script.playerInGameDescanso[num].SetActive(true);
                script.playerInGameReuniones[num].SetActive(false);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
                return;
            }

            if (boolMapaDivididos[3]){
                int_mapaParticipantes[num] = 3;
                print("Participante " + num + " va al mapa COMUNICACION");
                script.playerInGameComunicacion[num].SetActive(true);
                script.playerInGameDescanso[num].SetActive(false);
                script.playerInGameReuniones[num].SetActive(false);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
                return;
            }

            if (boolMapaDivididos[4]){
                int_mapaParticipantes[num] = 4;
                print("Participante " + num + " va al mapa REUNIONES");
                script.playerInGameComunicacion[num].SetActive(false);
                script.playerInGameDescanso[num].SetActive(false);
                script.playerInGameReuniones[num].SetActive(true);

                script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
                return;
            }

            atrilPlayer[num] = 0;
            ResetAtril(num);
            print("se resetea");
        }
    }

    #region MANDAR AUTOM_CLICK PANTALLA PLAYERS
    public void AutoPlayer_MapaGeneral (int num){
        if (script.playerGo[num] != 0){
            print("Participante " + num + " va al mapa general");
            script.playerInGameComunicacion[num].SetActive(false);
            script.playerInGameDescanso[num].SetActive(false);
            script.playerInGameReuniones[num].SetActive(false);

            script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);
            imageParticipantes[num].color = colorMapaDivididos[0];
            return;
        }
    }

    public void AutoPlayer_MapaSalaWorking(int num){
        
        print("Participante " + num + " va al mapa descanso");

        script.playerInGameComunicacion[num].SetActive(false);
        script.playerInGameDescanso[num].SetActive(false);
        script.playerInGameReuniones[num].SetActive(false);

        script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(true);

        imageParticipantes[num].color = colorMapaDivididos[1];
        return;
    }

    public void AutoPlayer_MapaSalaDescanso(int num){
        print("Participante " + num + " va al mapa descanso");

        script.playerInGameComunicacion[num].SetActive(false);
        script.playerInGameDescanso[num].SetActive(true);
        script.playerInGameReuniones[num].SetActive(false);

        script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);

        imageParticipantes[num].color = colorMapaDivididos[2];
        return;
    }

    public void AutoPlayer_MapaComunicacion (int num){
        print("Participante " + num + " va al mapa Comunicacion");

        script.playerInGameComunicacion[num].SetActive(true);
        script.playerInGameDescanso[num].SetActive(false);
        script.playerInGameReuniones[num].SetActive(false);

        script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);

        imageParticipantes[num].color = colorMapaDivididos[3];
        return;
    }

    public void AutoPlayer_MapaReuniones(int num){
        print("Participante " + num + " va al mapa Reuniones");

        script.playerInGameComunicacion[num].SetActive(false);
        script.playerInGameDescanso[num].SetActive(false);
        script.playerInGameReuniones[num].SetActive(true);

        script.manosPadres_Rooms[0].transform.GetChild(num).gameObject.SetActive(false);

        imageParticipantes[num].color = colorMapaDivididos[4];
        return;
    }
    #endregion
    public void RotarTeclado(){

        teclado.transform.localEulerAngles += Vector3.forward * 90.0f;
    }
}
