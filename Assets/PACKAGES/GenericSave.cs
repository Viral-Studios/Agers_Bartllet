using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Video;

public class GenericSave : MonoBehaviour{

    #region Variables
    [Header("File Info")]
    public string folderName = "Saves";
    public string fileName = "GameSave";
    string fileExtension = ".json";
    string dataContain;
    GameData gameData;

    [Header("Conexiones")]
    public Root root;
    public Eventos_Mapas eventosMapas_WorkingRoom;
    public SalasDiv script_SalasDiv;

    public Drageables drageables_Descanso;
    public Drageables drageables_General;
    public Drageables_Dual drageables_ComunReun;
    public U_Invertida uInvertida;
    public Menu menu;
    public SistemaHologramas scriptHolograma;
    public graficaDrag scriptGraficaI;
    public graficaDrag scriptGraficaII;
    public graficaDrag scriptGraficaIII;
    public Herramientas_Add scriptHerramientas_Add;

    public ReproductorBartler ReproductorSalaDescanso;
    public ReproductorBartler ReproductorSalaWorking;

    public CustomDrag script_customDrag;

    [Header("Slots")]
    public List<Text> Slots;
    public int slotSelected = 0;

    // VARIABLES PARA LOS DRAGGS DE LAS SALAS GENERAL Y DESCANSO
    private bool[] draggs_Descanso = new bool[2];
    private bool[] draggs_General = new bool[10];

    // VARIABLES PARA LOS EVENTOS DE LA SALA WORKING ROOM
    public bool[] eventos_WorkingRoom = new bool[28];
    public string[] eventos_WorkingRoomEstado = new string[28];
    public AudioClip[] eventos_WorkingRoomAudio = new AudioClip[28];
    #endregion

    public bool guardado;

    public void Awake(){

        //Generamos una carpeta para almacenar la informacion de las partidas
        if (!File.Exists(Application.persistentDataPath + "/" + folderName)){
            Directory.CreateDirectory(Application.persistentDataPath + "/" + folderName);
        }

        //PlayerPrefs.DeleteAll(); //Eliminar PlayerPrefs

        for (int i = 0; i < Slots.Count; i++){
            Slots[i].text = PlayerPrefs.GetString("Slot" + i, "EMPTY");
        }
    }
    public void Start()
    {
        //comprueba si se esta cargando una partida
        if (PlayerPrefs.GetInt("slotLoading", -1) != -1){

            if (PlayerPrefs.HasKey("slotLoading")){
                LoadData(PlayerPrefs.GetInt("slotLoading", -1));
            }
        }
    }

    public void CheckSlot(int numSlot){
        slotSelected = numSlot;
        if (menu.botonPulsado == "Save"){
            if (File.Exists(Application.persistentDataPath + "/" + folderName + "/" + fileName + numSlot + fileExtension))
            {
                menu.panelMenu[3].SetActive(true);
                menu.panelMenu[3].transform.GetChild(0).GetComponent<Text>().text = "¿ESTAS SEGURO DE QUE QUIERES SOBRESCRIBIR ESTA CASILLA? ";
            }else{
                SaveData();
            }
        }

        if (menu.botonPulsado == "Load" && PlayerPrefs.HasKey("Slot" + (numSlot - 1))){
            menu.panelMenu[3].SetActive(true);
            menu.panelMenu[3].transform.GetChild(0).GetComponent<Text>().text = "¿ESTAS SEGURO DE QUE QUIERES CARGAR ESTA CASILLA? ";
        }
    }
    //Funcion para GUARDAR la partida
    public void SaveData() {

        gameData = new GameData();
        #region Datos que GUARDAMOS
        /******************************************************************************
        *******************************   I N I C I O   *******************************
        *******************************************************************************/
        gameData.currentScene = root.currentScene;  // Guardado del escenario actual
        gameData.guardado = true;

        // 0) Informacion de la UInvertida V2... SALAS DIV
        for (int i = 0; i < 10; i++){
            for (int o = 0; o < 6; o++){
                if (uInvertida.imageParticipantes[i].gameObject.activeSelf && uInvertida.imageParticipantes[i].color == uInvertida.colorMapaDivididos[o])
                {
                    //print("PLAYER " + i + " guarda el color " + o);
                    gameData.participantes_SalaDiv[i] = o;
                }
            }
        }

        // 1)   Informacion de la UInvertida...
        for (int i = 0; i < 10; i++) {
            gameData.blockPlayer[i] = uInvertida.blockPlayer[i];    //color de los botones de la UInvertida en cada modo
            gameData.hidenPlayer[i] = uInvertida.hidenPlayer[i];    //color de los botones de la UInvertida en cada modo
            gameData.int_mapaParticipantes[i] = uInvertida.int_mapaParticipantes[i];
            gameData.atrilPlayer[i] = uInvertida.atrilPlayer[i];

            // ...Averiguamos si el participante no ha sido usado durante la sesión...
            if (uInvertida.hidenPlayer[i] == 0 && !GetComponent<Root>().playerInGame[i].activeSelf) {
                //print("Participante" + i + " ha estado desactivado en la sesión");
                gameData.playersNoUsados[i] = true;
            }

            // ...Guardamos pos & rot de los participantes no ocultos
            if (!gameData.playersNoUsados[i]) {
                gameData.playersPosGeneral[i] = GetComponent<Root>().playerInGame[i].transform.position;            // Guardamos la POSICIÓN de los participantes en la SALA GENERAL
                gameData.playersRotGeneral[i] = GetComponent<Root>().playerInGame[i].transform.localEulerAngles;    // Guardamos la ROTACIÓN de los participantes en la SALA GENERAL
                gameData.playersPosDescanso[i] = GetComponent<Root>().playerInGameDescanso[i].transform.position;   // Guardamos la POSICIÓN de los participantes en la SALA DESCANSO

                gameData.playersPosReuniones[i] = GetComponent<Root>().playerInGameReuniones[i].transform.localPosition;
                gameData.playersPosComunicacion[i] = GetComponent<Root>().playerInGameComunicacion[i].transform.localPosition;

                gameData.playersNombres[i] = GetComponent<Root>().playerInGame[i].transform.GetChild(2).GetComponent<InputField>().text; // Guardamos el NOMBRE
            }
        }

        // 2)   Datos relativos a la INFO GLOBAL de los participantes
        for (int i = 0; i < 80; i++) {
            if (GetComponent<Root>().infoGlobalPlayers[i] != 0) {

                // GUARDA EN UNA LISTA LAS INFOS DE TODOS LOS PARTICIPANTES
                gameData.infoGlobalPlayers[i] = GetComponent<Root>().infoGlobalPlayers[i];
            }
        }

        // GUARDAMOS EL COLOR DEL PELO
        //gameData.coloresPeloPlayers[0] = GetComponent<Root>().customPanels[0].GetComponent<Custom_Player>().colorPelo;

        // SE CIERRAN DISTINTOS PANELES DEL MENU PAUSE
        if (menu.panelMenu[5].activeSelf) { menu.panelMenu[5].SetActive(false); }
        if (menu.panelMenu[4].activeSelf) { menu.panelMenu[4].SetActive(false); }

        /******************************************** D R A G S ********************************************/
        // Drags usados en sala DESCANSO ********************************************************************
        for (int i = 0; i < 2; i++) {
            gameData.dragsDescanso[i] = draggs_Descanso[i];
            gameData.dragsDescansoPos[i] = drageables_Descanso.contentDrageables_Ingame.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
        }

        for (int i = 0; i < 2; i++){

            if (root.personajesSalaDiv[i].gameObject.activeSelf){
                gameData.dragsDiv[i] = true;
                print(gameData.dragsDiv[i] + " = " + i);
                gameData.dragsDivPos[i] = root.personajesSalaDiv[i].transform.localPosition;
                gameData.dragsDivRot[i] = root.personajesSalaDiv[i].transform.localEulerAngles;
            }else{
                if (!root.personajesSalaDiv[i].gameObject.activeSelf){
                    gameData.dragsDiv[i] = false;
                }
            }
        }

        if (root.personajesSalaDiv[1].gameObject.activeSelf){
            gameData.posPadre = root.personajesSalaDiv[1].transform.parent.GetSiblingIndex();
            print("se guarda en " + gameData.posPadre);
        }

        // Drags usados en sala GENERAL **********************************************************************
        for (int i = 0; i < 10; i++) {
            gameData.dragsGeneral[i] = draggs_General[i];
            gameData.dragsGeneralPos[i] = drageables_General.contentDrageables_Ingame.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
            gameData.dragsGeneralRot[i] = drageables_General.contentDrageables_Ingame.transform.GetChild(i).GetComponent<RectTransform>().localEulerAngles;
        }

        /******************************************** E V E N T O S **********************************************/
        for (int i = 0; i < 28; i++) {
            if (eventos_WorkingRoom[i]) {
                gameData.eventosGeneral[i] = eventos_WorkingRoom[i];
                gameData.eventos_WorkingRoomEstado[i] = eventosMapas_WorkingRoom.animatorEvents[i].GetCurrentAnimatorClipInfo(0)[0].clip.name;
                eventos_WorkingRoomEstado[i] = eventosMapas_WorkingRoom.animatorEvents[i].GetCurrentAnimatorClipInfo(0)[0].clip.name;

                if (eventosMapas_WorkingRoom.animatorEvents[i].GetComponent<AudioSource>() != null) {
                    if (eventosMapas_WorkingRoom.animatorEvents[i].GetComponent<AudioSource>().isPlaying) {
                        gameData.eventos_WorkingRoomAudio[i] = eventosMapas_WorkingRoom.animatorEvents[i].GetComponent<AudioSource>().clip;
                    }
                }
            }
        }

        #region /**************************************** H E R R A M I EN T A S *****************************************/
        #region // Se guardan los accesos directos utilizados al guardar la partida
        for (int i = 0; i < 13; i++) {
            if (scriptHolograma.bandejaAccesos.transform.GetChild(i).gameObject.activeSelf) {
                gameData.toolsUsadas[i] = true;
            } else {
                gameData.toolsUsadas[i] = false;
            }
        }
        #endregion

        #region// CHILD 0) Se guardan los datos de 5ws....
        for (int i = 0; i < 7; i++) {
            gameData.tool_5WS[i] = scriptHolograma.contenidoHerramientas.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<InputField>().text;
        }
        #endregion
        #region// CHILD 1) Se guardan los datos de dafo....
        for (int i = 0; i < 4; i++) {
            gameData.tool_dafo[i] = scriptHolograma.contenidoHerramientas.transform.GetChild(1).GetChild(0).GetChild(i + 4).GetComponent<InputField>().text;
        }
        #endregion
        #region// CHILD 2) Se guardan los datos de Analisis de Implicados...
        // INICIALIZAMOS LA LISTA DE STRINGS QUE VAMOS A GUARDARNOS
        gameData.conceptos_AnalisisImplpicadosLista = new List<string>();

        gameData.numConceptos_AnalisisImplicados = scriptHerramientas_Add.contenido_AnalisisImplicados.transform.childCount;
        gameData.numCampos_AnalisisImplicados = 8 + (8 * (gameData.numConceptos_AnalisisImplicados - 1));
        gameData.numConceptos_AnalisisImplicados = scriptHerramientas_Add.TotalAnalisisImplicados;
        gameData.longitudContenido_AnalisisImplicados = scriptHerramientas_Add.contenido_AnalisisImplicados.GetComponent<RectTransform>().sizeDelta.y;

        for (int i = 0; i < gameData.numCampos_AnalisisImplicados; i++) {
            for (int u = 0; u < gameData.numConceptos_AnalisisImplicados; u++) {
                for (int o = 0; o < 8; o++) {
                    if (gameData.conceptos_AnalisisImplpicadosLista.Count < gameData.numCampos_AnalisisImplicados)
                        gameData.conceptos_AnalisisImplpicadosLista.Add(scriptHerramientas_Add.contenido_AnalisisImplicados.transform.GetChild(u).GetChild(1).GetChild(o).GetComponent<InputField>().text);
                }
            }
        }
        #endregion
        #region// CHILD 3) Se guardan los datos de Gantt Okapi...
        gameData.numConceptos_GanttOkapi = scriptHerramientas_Add.Gantt_campos.Count;
        gameData.longitud_GantOkapi = scriptHerramientas_Add.contenido_Gantt.GetComponent<RectTransform>().sizeDelta.y;
        gameData.longitud_GantOkapiTiempo = scriptHerramientas_Add.contenido_Gantt_tiempo.GetComponent<RectTransform>().sizeDelta.y;

        for (int i = 0; i < gameData.numConceptos_GanttOkapi; i++) {
            for (int o = 0; o < 3; o++) {
                gameData.conceptos_GanttOkapi.Add(scriptHerramientas_Add.contenido_Gantt.transform.GetChild(i).GetChild(1).GetChild(o).GetComponent<InputField>().text);
            }

            gameData.sizePosTiempos_GanttOkapi.Add(new Vector2(scriptHerramientas_Add.contenido_Gantt_tiempo.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().anchoredPosition.x, scriptHerramientas_Add.contenido_Gantt_tiempo.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta.x));
        }
        #endregion
        #region// CHILD 4) Se guardan los datos de storytelling canvas....
        for (int i = 0; i < 9; i++) {
            gameData.tool_storytelling[i] = scriptHolograma.contenidoHerramientas.transform.GetChild(4).GetChild(0).GetChild(i + 9).GetComponent<InputField>().text;
        }
        #endregion
        #region CHILD 5) Se guardan los datos de Analisis de Riesgo
        gameData.numanalisisRiesgo = scriptHerramientas_Add.contenido_AnalisisRiesgo.transform.childCount;
        gameData.longitud_AnalisisRiesgo = scriptHerramientas_Add.contenido_AnalisisRiesgo.GetComponent<RectTransform>().sizeDelta.y;

        for (int i = 0; i < gameData.numanalisisRiesgo; i++) {
            gameData.conceptos_AnalisisRiesgo.Add(scriptHerramientas_Add.contenido_AnalisisRiesgo.transform.GetChild(i).GetChild(0).GetComponent<InputField>().text);
            gameData.numConceptos_AnalisisRiesgo.Add(scriptHerramientas_Add.contenido_AnalisisRiesgo.transform.GetChild(i).GetChild(1).GetComponent<InputField>().text);
        }

        for (int i = 0; i < 50; i++) {
            gameData.numConceptos_AnalisisRiesgo.Add(scriptHerramientas_Add.transform.GetChild(5).GetChild(0).GetChild(3).GetChild(i).GetComponent<InputField>().text);
        }

        #endregion
        #region// CHILD 6) Se guardan los datos de DOC BLANCO I....
        gameData.tool_docBlancoI = scriptHolograma.contenidoHerramientas.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<InputField>().text;
        #endregion
        #region// CHILD 7) Se guardan los datos de DOC BLANCO II....
        gameData.tool_docBlancoII = scriptHolograma.contenidoHerramientas.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<InputField>().text;
        #endregion
        #region// CHILD 8) Se guardan los datos de [C] DAFO....
        for (int i = 0; i < 9; i++) {
            gameData.tool_C_dafo[i] = scriptHolograma.contenidoHerramientas.transform.GetChild(8).GetChild(0).GetChild(i + 9).GetComponent<InputField>().text;
        }
        #endregion
        #region // CHILD 9) Se guardan los datos de [C] PLAN COMUNICACION...
        gameData.numPlanComunicacion = scriptHerramientas_Add.contenido_PlanComun.transform.childCount;
        gameData.longitudPlanComunicacion = scriptHerramientas_Add.contenido_PlanComun.GetComponent<RectTransform>().sizeDelta.y;

        for (int i = 0; i < gameData.numPlanComunicacion; i++) {
            for (int o = 0; o < 6; o++) {
                gameData.conceptos_PlanComunicacion.Add(scriptHerramientas_Add.contenido_PlanComun.transform.GetChild(i).GetChild(1).GetChild(o).GetComponent<InputField>().text);
            }
        }
        #endregion
        #region // CHILD 10) Se guardan los datos de [C] PUBLICO EMPRESA
        gameData.tool_C_publicoEmpresa[0] = scriptHerramientas_Add.contenido_PlanEmpresa.transform.GetChild(0).GetComponent<InputField>().text;
        gameData.tool_C_publicoEmpresa[1] = scriptHerramientas_Add.contenido_PlanEmpresa.transform.GetChild(3).GetComponent<InputField>().text;
        gameData.tool_C_publicoEmpresa[2] = scriptHerramientas_Add.contenido_PlanEmpresa.transform.GetChild(5).GetComponent<InputField>().text;

        for (int i = 0; i < 20; i++) {
            gameData.valores_publicEmpresa[i] = scriptHerramientas_Add.contenido_PlanEmpresa.transform.GetChild(i + 7).GetChild(0).GetComponent<InputField>().text;
        }
        #endregion
        #region // CHILD 11) Se guardan los datos de [C] PUBLICO POBLACION
        // POBLACIÓN I
        for (int i = 0; i < 9; i++){
            gameData.porcentajesPoblacionI[i] = scriptHerramientas_Add.porcentajesPoblacionI[i].text;
            gameData.camposPoblacionI[i] = scriptHerramientas_Add.camposPoblacionI[i].text;

            gameData.porcentajesPoblacionII[i] = scriptHerramientas_Add.porcentajesPoblacionII[i].text;
            gameData.camposPoblacionII[i] = scriptHerramientas_Add.camposPoblacionII[i].text;

            gameData.porcentajesPoblacionIII[i] = scriptHerramientas_Add.porcentajesPoblacionIII[i].text;
            gameData.camposPoblacionIII[i] = scriptHerramientas_Add.camposPoblacionIII[i].text;
        }

        if (scriptHerramientas_Add.panelesGraficasII[0].transform.parent.gameObject.activeSelf){
            gameData.dobleAnalisisPoblacion = true;
        }else{
            gameData.dobleAnalisisPoblacion = false;
        }
        #endregion

        #region // CHILD 12) Se guardan los datos de [C] LINEA TEMPORAL
        gameData.numLineaTemporal = scriptHerramientas_Add.num;
        gameData.longitudLineaTemporal = scriptHerramientas_Add.contenido_LineaTemporal.GetComponent<RectTransform>().sizeDelta.y;

        if (scriptHerramientas_Add.comInterna_gameob.activeSelf) {
            gameData.comAbacos[0] = true;
        }
        if (scriptHerramientas_Add.comExterna_gameob.activeSelf){
            gameData.comAbacos[1] = true;
        }

        for (int i = 0; i < gameData.numLineaTemporal; i++){
            gameData.conceptos_LineaTemporal.Add(scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<InputField>().text);
            gameData.indiceLineaTemporal.Add(scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_I.GetComponent<InputField>().text);

            if (scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().scroll_II){
                gameData.scrollII_min1.Add(true);
                //print("Miniatura izquierda Arriba " + (i + 1));
            }else{
                gameData.scrollII_min1.Add(false);
            }

            if (scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().scroll_III){
                gameData.scrollIII_min1.Add(true);
                //print("Miniatura izquierda Abajo " + (i + 1));
            }else{
                gameData.scrollIII_min1.Add(false);
            }

            if (scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_II.GetComponent<BloqueLineaTemporalMiniatura>().scroll_II){
                gameData.scrollII_min2.Add(true);
                //print("Miniatura derecha Arriba " + (i + 1));
            }else{
                gameData.scrollII_min2.Add(false);
            }

            if (scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_II.GetComponent<BloqueLineaTemporalMiniatura>().scroll_III){
                gameData.scrollIII_min2.Add(true);
                //print("Miniatura izquierda Abajo " + (i + 1));
            }else{
                gameData.scrollIII_min2.Add(false);

            }
        }

        gameData.listaBloquesLT.Clear();
        int auxx = scriptHerramientas_Add.contenido_LineaTemporal.transform.childCount - 1;
        for (int i  = 0; i< auxx; i++){
            if (scriptHerramientas_Add.contenido_LineaTemporal.transform.GetChild(i).name != "fin"){
                gameData.listaBloquesLT.Add(scriptHerramientas_Add.contenido_LineaTemporal.transform.GetChild(i).name);
            }
        }

        gameData.hijos_ScrollII.Clear();
        int auxHijosII = scriptHerramientas_Add.comInterna_gameob.transform.GetChild(0).transform.childCount;
        for (int i = 0; i < auxHijosII; i++){
            gameData.hijos_ScrollII.Add(scriptHerramientas_Add.comInterna_gameob.transform.GetChild(0).GetChild(i).gameObject.name);
        }

        gameData.hijos_ScrollIII.Clear();
        int auxHijosIII = scriptHerramientas_Add.comExterna_gameob.transform.GetChild(0).transform.childCount;
        for (int i = 0; i < auxHijosIII; i++){
            gameData.hijos_ScrollIII.Add(scriptHerramientas_Add.comExterna_gameob.transform.GetChild(0).GetChild(i).gameObject.name);
        }
        #endregion

        #region /******************************* G R A F I C A S  I N I C I A L E S ******************************/
        // GRAFICAS I, II & III
        for (int i = 0; i < 3; i++){
            if (scriptHolograma.graficaEmparentada[i]){
                gameData.graficasInicialesUsadas[i] = scriptHolograma.graficaEmparentada[i];
            }
        }

        // GRAFICA I
        gameData.graficaI_titulo = scriptGraficaI.transform.GetChild(0).GetComponent<InputField>().text;
        gameData.graficaI_numFilas = scriptGraficaI.numFilas;

        for (int i = 0; i < 8; i++){
            gameData.graficaI_auxValoresFilas[i] = scriptGraficaI.auxValoresFilas[i];
            gameData.graficaI_titulosIndiv[i] = scriptGraficaI.conceptosArray[i].text;
        }

        // GRAFICA II
        gameData.graficaII_titulo = scriptGraficaII.transform.GetChild(0).GetComponent<InputField>().text;
        gameData.graficaII_numFilas = scriptGraficaII.numFilas;

        for (int i = 0; i < 8; i++)
        {
            gameData.graficaII_auxValoresFilas[i] = scriptGraficaII.auxValoresFilas[i];
            gameData.graficaII_titulosIndiv[i] = scriptGraficaII.conceptosArray[i].text;
        }

        // GRAFICA III
        gameData.graficaIII_titulo = scriptGraficaIII.transform.GetChild(0).GetComponent<InputField>().text;
        gameData.graficaIII_numFilas = scriptGraficaIII.numFilas;

        for (int i = 0; i < 8; i++){
            gameData.graficaIII_auxValoresFilas[i] = scriptGraficaIII.auxValoresFilas[i];
            gameData.graficaIII_titulosIndiv[i] = scriptGraficaIII.conceptosArray[i].text;
        }

        #endregion
        #region /******************************* R E P R O D U C T O R E S *******************************/
        #region SALA DESCANSO
        for (int i = 0; i < 9; i++){
            gameData.faldonesSalaDescanso[i] = ReproductorSalaDescanso.faldonesLista[i].text;
        }

        gameData.faldonesSelecc[0] = ReproductorSalaDescanso.infoTabletMonitor[0].text;
        gameData.faldonesSelecc[1] = ReproductorSalaDescanso.infoTabletMonitor[1].text;

        gameData.filtroSelecc = ReproductorSalaDescanso.filtrosInfo[0].text;

        gameData.filtrosSelecc[0] = ReproductorSalaDescanso.filtroSelecc[0];
        gameData.filtrosSelecc[1] = ReproductorSalaDescanso.filtroSelecc[1];

        if (ReproductorSalaDescanso.salidaVideoElegida != null){
            gameData.videoSeleccSalaDescanso = ReproductorSalaDescanso.VideosSelecc[0];
            gameData.videoSalaDescanso = ReproductorSalaDescanso.salidaVideoElegida.clip;
        }
        #endregion

        #region SALA WORKING
        // GUARDA LOS TITULOS Y CUERPOS DE LOS FALDONES
        for (int i = 0; i < 9; i++){
            gameData.faldonesSalaWorking[i] = ReproductorSalaWorking.faldonesLista[i].text;
        }

        // GUARDA LOS TEXTOS DEL PANEL DE LOS FALDONES SELECC
        gameData.faldonesSeleccWorking[0] = ReproductorSalaWorking.infoTabletMonitor[0].text;
        gameData.faldonesSeleccWorking[1] = ReproductorSalaWorking.infoTabletMonitor[1].text;

        gameData.faldonesSeleccWorkingII[0] = ReproductorSalaWorking.infoTabletMonitor[3].text;
        gameData.faldonesSeleccWorkingII[1] = ReproductorSalaWorking.infoTabletMonitor[4].text;

        // GUARDA LOS TEXTOS DEL PANEL DE LOS FILTROS SELECC
        gameData.filtroSeleccWorking = ReproductorSalaWorking.filtrosInfo[0].text;
        gameData.filtroSeleccWorkingII = ReproductorSalaWorking.filtrosInfo[1].text;

        gameData.filtrosSeleccWorking[0] = ReproductorSalaWorking.filtroSelecc[0];
        gameData.filtrosSeleccWorkingII[0] = ReproductorSalaWorking.filtroSelecc[1];

        gameData.videoSeleccSalaWorking = ReproductorSalaWorking.VideosSelecc[0];
        gameData.videoSeleccSalaWorkingII = ReproductorSalaWorking.VideosSelecc[1];

        /*if (ReproductorSalaWorking.salidaVideoElegida != null){
            gameData.videoSeleccSalaWorking = ReproductorSalaWorking.VideosSelecc[0];
            //gameData.videoSalaWorking = ReproductorSalaWorking.salidaVideoElegida.clip;

            gameData.videoSeleccSalaWorkingII = ReproductorSalaWorking.VideosSelecc[1];
        }*/

        /*gameData.faldonesSeleccWorking[0] = ReproductorSalaWorking.faldonesAnim[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
        gameData.faldonesSeleccWorking[1] = ReproductorSalaWorking.faldonesAnim[1].transform.GetChild(1).GetChild(0).GetComponent<Text>().text;
        gameData.faldonesSeleccWorkingII[0] = ReproductorSalaWorking.faldonesAnim[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
        gameData.faldonesSeleccWorkingII[1] = ReproductorSalaWorking.faldonesAnim[1].transform.GetChild(1).GetChild(0).GetComponent<Text>().text;*/

        #endregion

        #endregion
        /******************************************************************************
        ********************************   F   I   N   ********************************
        *******************************************************************************/
        #endregion

        /**************************************** C U S T O M  D R A G *******************************************/
        for (int i = 0; i < 90; i++){

            if (script_customDrag.DragsCustom.infoDragsCajaI[i] != 0){
                //print("CAJA I " + script_customDrag.DragsCustom.infoDragsCajaI[i] + " en pos: " + i);
                gameData.infoDragsCajaI[i] = script_customDrag.DragsCustom.infoDragsCajaI[i];
            }

            if (script_customDrag.DragsCustom.infoDragsCajaII[i] != 0){
                //print("CAJA II " + script_customDrag.DragsCustom.infoDragsCajaII[i] + " en pos: " + i);
                gameData.infoDragsCajaII[i] = script_customDrag.DragsCustom.infoDragsCajaII[i];
            }

            if (script_customDrag.DragsCustom.infoDragsCajaIII[i] != 0){
                //print("CAJA III " + script_customDrag.DragsCustom.infoDragsCajaIII[i] + " en pos: " + i);
                gameData.infoDragsCajaIII[i] = script_customDrag.DragsCustom.infoDragsCajaIII[i];
            }
        }

        for (int i = 0; i < 9; i++){
            if (script_customDrag.infoMaster_prev[i] != 0){
                gameData.infoMaster_prev[i] = script_customDrag.infoMaster_prev[i];
            }
        }

        // CAJAS
        for (int i = 0; i < 10; i++){
            if (script_customDrag.DragsCustom.infoDragsVision_cajaI[i] != false){
                gameData.infoDragsVision_cajaI[i] = script_customDrag.DragsCustom.infoDragsVision_cajaI[i];
            }

            if (script_customDrag.DragsCustom.infoDragsVision_cajaII[i] != false){
                gameData.infoDragsVision_cajaII[i] = script_customDrag.DragsCustom.infoDragsVision_cajaII[i];
            }

            if (script_customDrag.DragsCustom.infoDragsVision_cajaIII[i] != false)
            {
                gameData.infoDragsVision_cajaIII[i] = script_customDrag.DragsCustom.infoDragsVision_cajaIII[i];
            }
        }

        for (int i = 0; i < 3; i++) {
            if (eventosMapas_WorkingRoom.imageDragCustom_Ev[i].color == Color.cyan){
                //print("Se guarda " + i);
                gameData.bool_eventosGrupo_SalaReuniones[i] = true;
            }else{
                //print("Se descarta " + i);
                gameData.bool_eventosGrupo_SalaReuniones[i] = false;
            }

            if (eventosMapas_WorkingRoom.animatorEventosDiv_Reuniones[i].enabled){
                print("Se guarda evento reu " + i);
                gameData.bool_ev_SalaReuniones[i] = true;
            }

            if (eventosMapas_WorkingRoom.animatorEventosDiv_Comunicaciones[i].enabled){
                print("Se guarda evento com " + i);
                gameData.bool_ev_SalaComunicaciones[i] = true;
            }
        }

        gameData.num_DragsDelante_Detras[0] = eventosMapas_WorkingRoom.numDragsDelante;
        gameData.num_DragsDelante_Detras[1] = eventosMapas_WorkingRoom.numDragsDetras;

        // ESTADO DE LA DIVISIÓN DE MAPAS
        //script_SalasDiv.EstadoSalasEscogidas();

        for (int i = 0; i < 8; i++){

            if (script_SalasDiv.imageSalas[i].GetComponent<Image>().color == Color.cyan){

                gameData.configSalaDiv[i] = true;
            }else{
                gameData.configSalaDiv[i] = false;
            }
        }

        // GRUPO SALA COMUNICACIONES
        gameData.numCantidadManos = eventosMapas_WorkingRoom.numCantidadManos;
        gameData.numCantidadPublico = eventosMapas_WorkingRoom.numCantidadPublico;

        for (int i = 0; i < 3; i++){

            if (eventosMapas_WorkingRoom.imagePublico[i].color == Color.cyan){
                gameData.tipoPublico[i] = true;
            }else{
                gameData.tipoPublico[i] = false;
            }
        }
        #endregion

        dataContain = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/" + folderName + "/" + fileName + slotSelected + fileExtension, dataContain.ToString());
        Slots[slotSelected - 1].text = System.DateTime.Now.Hour.ToString("D2") + ":" + System.DateTime.Now.Minute.ToString("D2") + " del " + System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year;
        PlayerPrefs.SetString("Slot" + (slotSelected - 1), Slots[slotSelected - 1].text);
        slotSelected = 0;
    }

    //******************************************************************************************************
    //******************************************************************************************************
    //Funcion para CARGAR la partida
    public void LoadData(int slot)
    {
        if (PlayerPrefs.GetInt("slotLoading", -1) == -1)
        {   //Guardamos el slot a cargar para que lo haga despues del reset
            if (Slots[slot - 1].text != "EMPTY")
            {
                PlayerPrefs.SetInt("slotLoading", slot);
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
        else
        {   //Una vez se ha reseteado, carga el slot que se señalo en el PlayerPref
            if (File.Exists(Application.persistentDataPath + "/" + folderName + "/" + fileName + slot + fileExtension))
            {
                slotSelected = slot;
                dataContain = File.ReadAllText(Application.persistentDataPath + "/" + folderName + "/" + fileName + slot + fileExtension);
                gameData = JsonUtility.FromJson<GameData>(dataContain);

                //test = true;
                #region Datos que CARGAMOS
                /******************************************************************************
                *******************************   I N I C I O   *******************************
                *******************************************************************************/
                root.currentScene = gameData.currentScene;  // Cargado del escenario actual
                eventosMapas_WorkingRoom.CambiarMapa(root.currentScene);

                // 0)   Información de la UInvertida V2
                for (int i = 0; i < 10; i++){

                    if (!gameData.playersNoUsados[i]){

                        uInvertida.imageParticipantes[i].color = uInvertida.colorMapaDivididos[gameData.participantes_SalaDiv[i]];
                        uInvertida.UpdateMandarParticipante_SubMapa(i, gameData.participantes_SalaDiv[i]);
                    }
                }

                // 1)   Informacion de la UInvertida
                for (int i = 0; i < 10; i++) {
                    uInvertida.blockPlayer[i] = gameData.blockPlayer[i];    //color de los botones de la UInvertida en cada modo
                    uInvertida.hidenPlayer[i] = gameData.hidenPlayer[i];    //color de los botones de la UInvertida en cada modo
                    uInvertida.playersNoUsados[i] = gameData.playersNoUsados[i];
                    uInvertida.int_mapaParticipantes[i] = gameData.int_mapaParticipantes[i];
                    uInvertida.atrilPlayer[i] = gameData.atrilPlayer[i];
                }

                // DATOS RELATIVOS A LA INFO GLOBL DE LOS PARTICIPANTES
                for (int i = 0; i < 80; i++) {

                    if (gameData.infoGlobalPlayers[i] != 0)
                    {
                        GetComponent<Root>().infoGlobalPlayers[i] = gameData.infoGlobalPlayers[i];

                        #region Subdivisión individual de los participantes
                        // P1
                        if (i < 8) {

                            GetComponent<Root>().customPanels[0].GetComponent<Custom_Player>().infoJugador0[i] = gameData.infoGlobalPlayers[i];
                        }

                        // P2
                        if (i > 7 && i < 16)
                        {
                            GetComponent<Root>().customPanels[1].GetComponent<Custom_Player>().infoJugador0[i - 8] = gameData.infoGlobalPlayers[i];
                        }

                        // P3
                        if (i > 15 && i < 24)
                        {
                            GetComponent<Root>().customPanels[2].GetComponent<Custom_Player>().infoJugador0[i - 16] = gameData.infoGlobalPlayers[i];
                        }

                        // P4
                        if (i > 23 && i < 32)
                        {
                            GetComponent<Root>().customPanels[3].GetComponent<Custom_Player>().infoJugador0[i - 24] = gameData.infoGlobalPlayers[i];
                        }

                        // P5
                        if (i > 31 && i < 40)
                        {
                            GetComponent<Root>().customPanels[4].GetComponent<Custom_Player>().infoJugador0[i - 32] = gameData.infoGlobalPlayers[i];
                        }

                        // P6
                        if (i > 39 && i < 48)
                        {
                            GetComponent<Root>().customPanels[5].GetComponent<Custom_Player>().infoJugador0[i - 40] = gameData.infoGlobalPlayers[i];
                        }

                        // P7
                        if (i > 47 && i < 56)
                        {
                            GetComponent<Root>().customPanels[6].GetComponent<Custom_Player>().infoJugador0[i - 48] = gameData.infoGlobalPlayers[i];
                        }

                        // P8
                        if (i > 55 && i < 64)
                        {
                            GetComponent<Root>().customPanels[7].GetComponent<Custom_Player>().infoJugador0[i - 56] = gameData.infoGlobalPlayers[i];
                        }

                        // P9
                        if (i > 63 && i < 72)
                        {
                            print("P9: " + i);
                            GetComponent<Root>().customPanels[8].GetComponent<Custom_Player>().infoJugador0[i - 64] = gameData.infoGlobalPlayers[i];
                        }

                        // P10
                        if (i > 71)
                        {
                            GetComponent<Root>().customPanels[9].GetComponent<Custom_Player>().infoJugador0[i - 72] = gameData.infoGlobalPlayers[i];
                        }
                        #endregion
                    }
                }

                // Cargamos la pos & rot de los participantes si fueron usados
                for (int i = 0; i < 10; i++) {

                    GetComponent<Root>().customPanels[i].SetActive(false);
                    uInvertida.hidenPlayer[i] = 0;

                    uInvertida.playersNoUsados[i] = gameData.playersNoUsados[i];

                    if (uInvertida.playersNoUsados[i] == false){
                        root.playerGo[i] = 1;
                        uInvertida.ApplyAtril(i);
                    }

                    // Si los participantes no han sido desactivados los hacemos aparecer....
                    if (!gameData.playersNoUsados[i]) {
                        GetComponent<Root>().playerInGame[i].SetActive(true);
                        //GetComponent<Root>().playerInGameDescanso[i].SetActive(true);
                        //GetComponent<Root>().manosPadres_Rooms[i].SetActive(true);
                        //GetComponent<Root>().manosPadres_Rooms[0].transform.GetChild(i).gameObject.SetActive(true);

                        GetComponent<Root>().playerInGame[i].transform.position = gameData.playersPosGeneral[i];            // Cargamos la POSICIÓN de los participantes en la SALA GENERAL
                        GetComponent<Root>().playerInGame[i].transform.localEulerAngles = gameData.playersRotGeneral[i];    // Cargamos la ROTACIÓN de los participantes en la SALA GENERAL
                        GetComponent<Root>().playerInGameDescanso[i].transform.position = gameData.playersPosDescanso[i];   // Cargamos la POSICIÓN de los participantes en la SALA DESCANSO

                        GetComponent<Root>().playerInGameComunicacion[i].transform.localPosition = gameData.playersPosComunicacion[i];   // Cargamos la POSICIÓN de los participantes en la SALA COMUNICACION
                        GetComponent<Root>().playerInGameReuniones[i].transform.localPosition = gameData.playersPosReuniones[i];   // Cargamos la POSICIÓN de los participantes en la SALA REUNIONES


                        // Cargamos el nombre de los participantes en las etiquetas de la sala general & descanso
                        GetComponent<Root>().playerInGame[i].transform.GetChild(2).GetComponent<InputField>().text = gameData.playersNombres[i];
                        GetComponent<Root>().playerInGameDescanso[i].transform.GetChild(2).GetComponent<InputField>().text = gameData.playersNombres[i];
                        GetComponent<Root>().playerInGameComunicacion[i].transform.GetChild(2).GetComponent<InputField>().text = gameData.playersNombres[i];
                        GetComponent<Root>().playerInGameReuniones[i].transform.GetChild(2).GetComponent<InputField>().text = gameData.playersNombres[i];

                        root.playerInGameReuniones[i].GetComponent<Player_Script>().Update_CambioPadre();
                    }
                }

                // Actualizamos la apariencia de los participantes
                GetComponent<Root>().UpdateCargaParticipantes();

                #region /************ D R A G S ************/
                // Drags usados en sala DESCANSO ***********************************************************************************************************************
                for (int i = 0; i < 2; i++) {
                    draggs_Descanso[i] = gameData.dragsDescanso[i];

                    if (gameData.dragsDescanso[i]) {
                        draggs_Descanso[i] = gameData.dragsDescanso[i];
                        drageables_Descanso.PersonajeDrag(i + 10);
                        drageables_Descanso.contentDrageables_Ingame.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = gameData.dragsDescansoPos[i];
                    }
                }


                for (int i = 0; i < 2; i++){

                    if (gameData.dragsDiv[i]){



                        print("Se cargan " + i);
                        drageables_ComunReun.SeleccionMapaDividido(i + 1);
                        root.personajesSalaDiv[i].transform.localPosition = gameData.dragsDivPos[i];
                        root.personajesSalaDiv[i].transform.localEulerAngles = gameData.dragsDivRot[i];
                    }
                }

                if (gameData.posPadre != 2){
                    Transform newPadre = root.personajesSalaDiv[1].transform.parent.parent.GetChild(6).transform;
                    root.personajesSalaDiv[1].transform.SetParent(newPadre);
                    print("Se pasa delante");
                }

                drageables_ComunReun.PersonajeDrag(0);

                // Drags usados en sala GENERAL ************************************************************************************************************************
                for (int i = 0; i < 11; i++) {

                    if (gameData.dragsGeneral[i]) {
                        print("carga drag" + i);
                        draggs_General[i] = gameData.dragsGeneral[i];
                        drageables_General.PersonajeDrag(i);
                        drageables_General.contentDrageables_Ingame.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = gameData.dragsGeneralPos[i];
                        drageables_General.contentDrageables_Ingame.transform.GetChild(i).GetComponent<RectTransform>().localEulerAngles = gameData.dragsGeneralRot[i];
                    }
                }
                #endregion
                #region /******************************************** E V E N T O S **********************************************/
                for (int i = 0; i < 28; i++) {
                    eventos_WorkingRoom[i] = gameData.eventosGeneral[i];

                    if (gameData.eventos_WorkingRoomAudio[i] != null) {
                        eventosMapas_WorkingRoom.animatorEvents[i].GetComponent<AudioSource>().clip = gameData.eventos_WorkingRoomAudio[i];
                        eventosMapas_WorkingRoom.animatorEvents[i].GetComponent<AudioSource>().Play();
                    }

                    if (eventos_WorkingRoom[i]) {

                        // Dividimos los animators del terremoto -que afecta globalmente a todos los mapas-
                        if (i != 7 && i != 8 && i != 9) {
                            print("se activa el evento " + i);
                            eventos_WorkingRoomEstado[i] = gameData.eventos_WorkingRoomEstado[i];
                            eventosMapas_WorkingRoom.animatorEvents[i].enabled = true;
                            eventosMapas_WorkingRoom.animatorEvents[i].Play(eventos_WorkingRoomEstado[i], 0, 1);
                        } else {
                            eventos_WorkingRoomEstado[7] = gameData.eventos_WorkingRoomEstado[7];
                            eventos_WorkingRoomEstado[8] = gameData.eventos_WorkingRoomEstado[8];
                            eventos_WorkingRoomEstado[9] = gameData.eventos_WorkingRoomEstado[9];

                            eventosMapas_WorkingRoom.animatorEvents[7].enabled = true;
                            eventosMapas_WorkingRoom.animatorEvents[8].enabled = true;
                            eventosMapas_WorkingRoom.animatorEvents[9].enabled = true;

                            eventosMapas_WorkingRoom.animatorEvents[7].Play(eventos_WorkingRoomEstado[7], 0, 1);
                            eventosMapas_WorkingRoom.animatorEvents[8].Play(eventos_WorkingRoomEstado[8], 0, 1);
                            eventosMapas_WorkingRoom.animatorEvents[9].Play(eventos_WorkingRoomEstado[9], 0, 1);

                            eventosMapas_WorkingRoom.grietaFxWorking.enabled = true;
                            eventosMapas_WorkingRoom.grietaFxDescanso.enabled = true;
                        }

                        // SE ILUMINAN LOS BOTONES SEGÚN EL EVENTO GUARDADO ACTIVO
                        if (i != 2 && i != 3) {

                            if (i != 16 && i != 11){
                                eventosMapas_WorkingRoom.image_eventButton_Evento[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                            }else{

                                if (i == 11){

                                    print(gameData.eventos_WorkingRoomEstado[i]);

                                    if (gameData.eventos_WorkingRoomEstado[i] == "PersonajeTopSecret2")
                                    {
                                        print("UNITO");
                                        eventosMapas_WorkingRoom.image_eventButton_Evento[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1.0f;
                                    }

                                    if (gameData.eventos_WorkingRoomEstado[i] == "PersonajeTopSecret02"){
                                        print("DOSITO");
                                        eventosMapas_WorkingRoom.image_eventButton_Evento[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
                                    }
                                }

                                if (i == 16){

                                    if (gameData.eventos_WorkingRoomEstado[i] == "PapeleraQuemand1" || gameData.eventos_WorkingRoomEstado[i] == "PapeleraQuemand0"){
                                        print("TRESITO");
                                        eventosMapas_WorkingRoom.image_eventButton_Evento[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
                                    }else{
                                        print("CUATRITO");
                                        eventosMapas_WorkingRoom.image_eventButton_Evento[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1.0f;
                                    }
                                }
                            }
                        }
                        else {
                            eventosMapas_WorkingRoom.image_eventButton_EventoMultiple[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                        }
                    }
                }
                #endregion
                #region /**************************************** H E R R A M I EN T A S *****************************************/
                #region // Se cargan los accesos directos utilizados al guardar la partida
                for (int i = 0; i < 13; i++) {
                    if (gameData.toolsUsadas[i]) {
                        scriptHolograma.bandejaAccesos.transform.GetChild(i).gameObject.SetActive(true);
                        scriptHolograma.contenido.transform.GetChild(i).GetComponent<Image>().color = Color.cyan;
                    }
                }
                #endregion

                #region // CHILD 0) Se cargan los datos de 5ws....
                for (int i = 0; i < 7; i++) {
                    if (gameData.tool_5WS[i] != null) {
                        scriptHolograma.contenidoHerramientas.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<InputField>().text = gameData.tool_5WS[i];
                    }
                }
                #endregion
                #region // CHILD 1) Se cargan los datos de dafo....
                for (int i = 0; i < 4; i++) {
                    if (gameData.tool_dafo[i] != null) {
                        scriptHolograma.contenidoHerramientas.transform.GetChild(1).GetChild(0).GetChild(i + 4).GetComponent<InputField>().text = gameData.tool_dafo[i];
                    }
                }
                #endregion
                #region // CHILD 2) Se guardan los datos de Analisis Implicados
                scriptHerramientas_Add.TotalAnalisisImplicados = gameData.numConceptos_AnalisisImplicados;
                scriptHerramientas_Add.contenido_AnalisisImplicados.GetComponent<RectTransform>().sizeDelta = new Vector2 (scriptHerramientas_Add.contenido_AnalisisImplicados.GetComponent<RectTransform>().sizeDelta.x, gameData.longitudContenido_AnalisisImplicados);

                int numAux = -1;
                for (int i = 0; i < gameData.numConceptos_AnalisisImplicados; i++) {
                    GameObject aux = Instantiate(scriptHerramientas_Add.prefab_AnalisisImplicados, scriptHerramientas_Add.contenido_AnalisisImplicados);
                    scriptHerramientas_Add.AnalisisImplicados_Lista.Add(aux);

                    for (int o = 0; o < 8; o++){

                        numAux++;
                        aux.transform.GetChild(1).GetChild(o).GetComponent<InputField>().text = gameData.conceptos_AnalisisImplpicadosLista[numAux];
                        aux.transform.GetChild(0).GetComponent<Text>().text = "" + aux.transform.parent.childCount;
                    }
                }
                #endregion
                #region // CHILD 3) Se cargan los datos de Gantt Okapi
                int numAuxGantt = -1;
                int numAuxGanttII = -1;

                scriptHerramientas_Add.contenido_Gantt.GetComponent<RectTransform>().sizeDelta = Vector2.up * gameData.longitud_GantOkapi;
                scriptHerramientas_Add.contenido_Gantt_tiempo.GetComponent<RectTransform>().sizeDelta = Vector2.up * gameData.longitud_GantOkapiTiempo;


                for (int i = 0; i < gameData.numConceptos_GanttOkapi; i++){
                    GameObject auxI = Instantiate(scriptHerramientas_Add.prefab_Gantt, scriptHerramientas_Add.contenido_Gantt);
                    GameObject auxII = Instantiate(scriptHerramientas_Add.prefab_tiempoGantt, scriptHerramientas_Add.contenido_Gantt_tiempo);

                    numAuxGantt++;

                    auxI.transform.GetChild(0).GetComponent<Text>().text = "" + (numAuxGantt+1);
                    auxII.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + (numAuxGantt + 1);

                    scriptHerramientas_Add.Gantt_campos.Add(auxI);
                    scriptHerramientas_Add.Gantt_camposTiempo.Add(auxII);

                    for (int o = 0; o < 3; o++){
                        numAuxGanttII++;
                        scriptHerramientas_Add.contenido_Gantt.transform.GetChild(i).GetChild(1).GetChild(o).GetComponent<InputField>().text = gameData.conceptos_GanttOkapi[numAuxGanttII];
                    }

                    auxII.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = Vector2.right * gameData.sizePosTiempos_GanttOkapi[numAuxGantt].x;
                    auxII.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2 (gameData.sizePosTiempos_GanttOkapi[numAuxGantt].y, 60.0f);
                }

                #endregion
                #region// CHILD 4) Se cargan los datos de storytelling canvas....
                for (int i = 0; i < 9; i++){
                    if (gameData.tool_storytelling[i] != null){
                        scriptHolograma.contenidoHerramientas.transform.GetChild(4).GetChild(0).GetChild(i + 9).GetComponent<InputField>().text = gameData.tool_storytelling[i];
                    }
                }
                #endregion
                #region// CHILD 5) Se cargan los datos de Analisis de Riesgo...
                scriptHerramientas_Add.contenido_AnalisisRiesgo.GetComponent<RectTransform>().sizeDelta = Vector2.up * gameData.longitud_AnalisisRiesgo;
                int auxAnalRiesgo = gameData.numanalisisRiesgo;
                for (int i = 0; i < auxAnalRiesgo; i++){
                    GameObject aux = Instantiate(scriptHerramientas_Add.prefab_AnalisisRiesgo, scriptHerramientas_Add.contenido_AnalisisRiesgo);
                    aux.transform.GetChild(0).GetComponent<InputField>().text = gameData.conceptos_AnalisisRiesgo[i];
                    aux.transform.GetChild(1).GetComponent<InputField>().text = gameData.numConceptos_AnalisisRiesgo[i];
                }
                for (int i = 0; i < 50; i++){
                    scriptHerramientas_Add.transform.GetChild(5).GetChild(0).GetChild(3).GetChild(i).GetComponent<InputField>().text = gameData.numConceptos_AnalisisRiesgo[i];
                }
                #endregion
                #region// CHILD 6) Se cargan los datos de storytelling canvas....
                if (gameData.tool_docBlancoI != null){
                    scriptHolograma.contenidoHerramientas.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<InputField>().text = gameData.tool_docBlancoI;
                }
                #endregion
                #region// CHILD 7) Se cargan los datos de storytelling canvas....
                if (gameData.tool_docBlancoII != null){
                    scriptHolograma.contenidoHerramientas.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<InputField>().text = gameData.tool_docBlancoII;
                }
                #endregion
                #region// CHILD 8) Se cargan los datos de [C] DAFO....
                for (int i = 0; i < 9; i++){
                    if (gameData.tool_C_dafo[i] != null){
                        scriptHolograma.contenidoHerramientas.transform.GetChild(8).GetChild(0).GetChild(i + 9).GetComponent<InputField>().text = gameData.tool_C_dafo[i];
                    }
                }
                #endregion
                #region// CHILD 9) Se cargan los datos de PLAN COMUNICACION
                scriptHerramientas_Add.contenido_PlanComun.GetComponent<RectTransform>().sizeDelta = Vector2.up * gameData.longitudPlanComunicacion;

                int auxPlanComunicacion = gameData.numPlanComunicacion;
                int auxPlanComunicacionI = -1;
                for (int i = 0; i < auxPlanComunicacion; i++){

                    GameObject aux = Instantiate(scriptHerramientas_Add.prefab_PlanComun, scriptHerramientas_Add.contenido_PlanComun);
                    scriptHerramientas_Add.planComunicacion_Lista.Add(aux);
                    aux.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);

                    for (int o = 0; o < 6; o++){
                        auxPlanComunicacionI++;
                        aux.transform.GetChild(1).GetChild(o).GetComponent<InputField>().text = gameData.conceptos_PlanComunicacion[auxPlanComunicacionI];
                    }
                }
                #endregion
                #region// CHILD 10) Se cargan los datos de [C] PUBLICO EMPRESA
                scriptHerramientas_Add.contenido_PlanEmpresa.transform.GetChild(0).GetComponent<InputField>().text = gameData.tool_C_publicoEmpresa[0];
                scriptHerramientas_Add.contenido_PlanEmpresa.transform.GetChild(3).GetComponent<InputField>().text = gameData.tool_C_publicoEmpresa[1];
                scriptHerramientas_Add.contenido_PlanEmpresa.transform.GetChild(5).GetComponent<InputField>().text = gameData.tool_C_publicoEmpresa[2];

                for (int i = 0; i < 20; i++){
                    scriptHerramientas_Add.contenido_PlanEmpresa.transform.GetChild(i + 7).GetChild(0).GetComponent<InputField>().text = gameData.valores_publicEmpresa[i];
                }
                #endregion
                #region// CHILD 11) Se cargan los datos de [C] PUBLICO POBLACION
                // POBLACIÓN I
                for (int i = 0; i < 9; i++){
                    scriptHerramientas_Add.porcentajesPoblacionI[i].text = gameData.porcentajesPoblacionI[i];
                    scriptHerramientas_Add.camposPoblacionI[i].text = gameData.camposPoblacionI[i];

                    scriptHerramientas_Add.porcentajesPoblacionII[i].text = gameData.porcentajesPoblacionII[i];
                    scriptHerramientas_Add.camposPoblacionII[i].text = gameData.camposPoblacionII[i];

                    scriptHerramientas_Add.porcentajesPoblacionIII[i].text = gameData.porcentajesPoblacionIII[i];
                    scriptHerramientas_Add.camposPoblacionIII[i].text = gameData.camposPoblacionIII[i];
                }

                /*if (gameData.dobleAnalisisPoblacion){
                    GameObject gameObjI = scriptHerramientas_Add.panelesGraficasII[0].transform.parent.gameObject;
                    scriptHerramientas_Add.GestorPublico_Poblacion(gameObjI.transform);

                    GameObject gameObjII = scriptHerramientas_Add.panelesGraficasIII[0].transform.parent.gameObject;
                    scriptHerramientas_Add.GestorPublico_Poblacion(gameObjII.transform);
                }*/
                #endregion

                #region // CHILD 12) Se cargan los datos de [C] LINEA TEMPORAL
                // SE CARGA LA LONGITUD
                scriptHerramientas_Add.contenido_LineaTemporal.GetComponent<RectTransform>().sizeDelta = Vector2.up * gameData.longitudLineaTemporal;

                // SE ABREN LAS LINEAS-ABACOS SI SE DEJARON ABIERTAS ANTES DE GUARDAR
                if (gameData.comAbacos[0]) { scriptHerramientas_Add.ComInternaOnOff(); }
                if (gameData.comAbacos[1]){ scriptHerramientas_Add.ComExternaOnOff(); }

                int auxLineaTemporal = gameData.numLineaTemporal;
                scriptHerramientas_Add.num = auxLineaTemporal;

                int auxLineaTemporalII = -1;
                for (int i = 0; i < auxLineaTemporal; i++){
                    auxLineaTemporalII++;
                    GameObject aux = Instantiate(scriptHerramientas_Add.prefab_LineaTemporal, scriptHerramientas_Add.contenido_LineaTemporal);
                    aux.transform.SetSiblingIndex(auxLineaTemporalII);

                    aux.GetComponent<BloqueLineaTemporal>().miniatura_I.GetComponent<InputField>().text = gameData.indiceLineaTemporal[auxLineaTemporalII];
                    aux.GetComponent<BloqueLineaTemporal>().miniatura_II.GetComponent<InputField>().text = gameData.indiceLineaTemporal[auxLineaTemporalII];
                    aux.GetComponent<BloqueLineaTemporal>().numerito.GetComponent<Text>().text = gameData.indiceLineaTemporal[auxLineaTemporalII];

                    aux.GetComponent<InputField>().text = "" + gameData.conceptos_LineaTemporal[auxLineaTemporalII];
                }

                int auxx = gameData.listaBloquesLT.Count;
                for (int i = 0; i < auxx; i++){
                    scriptHerramientas_Add.contenido_LineaTemporal.transform.GetChild(i).name = gameData.listaBloquesLT[i];
                    scriptHerramientas_Add.contenido_LineaTemporal.transform.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_I.name = "MiniaturaLT_Izq_" + scriptHerramientas_Add.contenido_LineaTemporal.transform.GetChild(i).GetComponent<BloqueLineaTemporal>().numerito.text;
                    scriptHerramientas_Add.contenido_LineaTemporal.transform.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_II.name = "MiniaturaLT_Der_" + scriptHerramientas_Add.contenido_LineaTemporal.transform.GetChild(i).GetComponent<BloqueLineaTemporal>().numerito.text;
                }

                //Invoke("Refresh", 0.15f);
                #endregion
                #endregion
                #region /******************************* G R A F I C A S  I N I C I A L E S ******************************/
                // GRAFICAS I, II & III, se emparentan de haber sido usadas en el holograma de la sala working room
                for (int i = 0; i < 3; i++){
                    scriptHolograma.graficaEmparentada[i] = gameData.graficasInicialesUsadas[i];

                    if (scriptHolograma.graficaEmparentada[i]){
                        print("se emparenta la grafica " + i);
                        scriptHolograma.mapaInicial.transform.GetChild(i).transform.GetChild(1).transform.SetParent(scriptHolograma.padreGraficasSituation.transform.GetChild(i + 3).transform);
                    }
                }

                // *********************************** GRAFICA I
                scriptGraficaI.transform.GetChild(0).GetComponent<InputField>().text = gameData.graficaI_titulo;
                scriptGraficaI.numFilas = gameData.graficaI_numFilas;

                for (int i = 0; i < 8; i++){
                    scriptGraficaI.auxValoresFilas[i] = gameData.graficaI_auxValoresFilas[i];
                    scriptGraficaI.conceptosArray[i].text = gameData.graficaI_titulosIndiv[i];
                    scriptGraficaI.padreFilas.transform.GetChild(i).GetComponent<InputField>().text = gameData.graficaI_auxValoresFilas[i] + "";
                }
                scriptGraficaI.Filas(gameData.graficaI_numFilas - 1);

                // *********************************** GRAFICA II
                scriptGraficaII.transform.GetChild(0).GetComponent<InputField>().text = gameData.graficaII_titulo;
                scriptGraficaII.numFilas = gameData.graficaII_numFilas;

                for (int i = 0; i < 8; i++)
                {
                    scriptGraficaII.auxValoresFilas[i] = gameData.graficaII_auxValoresFilas[i];
                    scriptGraficaII.conceptosArray[i].text = gameData.graficaII_titulosIndiv[i];
                    scriptGraficaII.padreFilas.transform.GetChild(i).GetComponent<InputField>().text = gameData.graficaII_auxValoresFilas[i] + "";
                }
                scriptGraficaII.Filas(gameData.graficaII_numFilas - 1);

                // *********************************** GRAFICA III
                scriptGraficaIII.transform.GetChild(0).GetComponent<InputField>().text = gameData.graficaIII_titulo;
                scriptGraficaIII.numFilas = gameData.graficaIII_numFilas;

                for (int i = 0; i < 8; i++)
                {
                    scriptGraficaIII.auxValoresFilas[i] = gameData.graficaIII_auxValoresFilas[i];
                    scriptGraficaIII.conceptosArray[i].text = gameData.graficaIII_titulosIndiv[i];
                    scriptGraficaIII.padreFilas.transform.GetChild(i).GetComponent<InputField>().text = gameData.graficaIII_auxValoresFilas[i] + "";
                }
                scriptGraficaIII.Filas(gameData.graficaIII_numFilas - 1);
                #endregion
                #region /******************************* R E P R O D U C T O R E S ******************************/
                #region// SALA DESCANSO
                for (int i = 0; i < 9; i++){
                    ReproductorSalaDescanso.faldonesLista[i].text = gameData.faldonesSalaDescanso[i];
                }

                ReproductorSalaDescanso.VideosSelecc[0] = gameData.videoSeleccSalaDescanso;
                ReproductorSalaDescanso.VideosSelecc[1] = gameData.videoSeleccSalaDescanso;

                if (gameData.videoSalaDescanso != null){
                    ReproductorSalaDescanso.salidaVideoElegida = ReproductorSalaDescanso.salidasVideo[0];
                    ReproductorSalaDescanso.salidaVideoElegida.clip = gameData.videoSalaDescanso;

                    ReproductorSalaDescanso.textos[0].text = ReproductorSalaDescanso.salidaVideoElegida.clip.name;
                }

                ReproductorSalaDescanso.infoTabletMonitor[0].text = gameData.faldonesSelecc[0];
                ReproductorSalaDescanso.infoTabletMonitor[1].text = gameData.faldonesSelecc[1];

                ReproductorSalaDescanso.filtrosInfo[0].text = gameData.filtroSelecc;

                ReproductorSalaDescanso.filtroSelecc[0] = gameData.filtrosSelecc[0];
                ReproductorSalaDescanso.filtroSelecc[1] = gameData.filtrosSelecc[1];

                ReproductorSalaDescanso.faldonesImag[3].transform.GetChild(0).GetComponent<UnityEngine.UI.InputField>().text = gameData.faldonesSelecc[0];
                ReproductorSalaDescanso.faldonesImag[3].transform.GetChild(1).GetComponent<UnityEngine.UI.InputField>().text = gameData.faldonesSelecc[1];

                ReproductorSalaDescanso.faldonesAnim[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = gameData.faldonesSelecc[0];
                ReproductorSalaDescanso.faldonesAnim[0].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = gameData.faldonesSelecc[1];
                #endregion

                #region// SALA WORKING
                // CARGA LOS TITULOS Y CUERPOS DE LOS FALDONES
                for (int i = 0; i < 9; i++){
                    ReproductorSalaWorking.faldonesLista[i].text = gameData.faldonesSalaWorking[i];
                }

                // GUARDA LOS TEXTOS DEL PANEL DE LOS FALDONES SELECC
                ReproductorSalaWorking.infoTabletMonitor[0].text = gameData.faldonesSeleccWorking[0];
                ReproductorSalaWorking.infoTabletMonitor[1].text = gameData.faldonesSeleccWorking[1];

                ReproductorSalaWorking.infoTabletMonitor[3].text = gameData.faldonesSeleccWorkingII[0];
                ReproductorSalaWorking.infoTabletMonitor[4].text = gameData.faldonesSeleccWorkingII[1];

                // GUARDA LOS TEXTOS DEL PANEL DE LOS FILTROS SELECC
                ReproductorSalaWorking.filtrosInfo[0].text = gameData.filtroSeleccWorking;
                ReproductorSalaWorking.filtrosInfo[1].text = gameData.filtroSeleccWorkingII;

                ReproductorSalaWorking.filtroSelecc[0] = gameData.filtrosSeleccWorking[0];
                ReproductorSalaWorking.filtroSelecc[1] = gameData.filtrosSeleccWorkingII[0];

                ReproductorSalaWorking.VideosSelecc[0] = gameData.videoSeleccSalaWorking;
                ReproductorSalaWorking.VideosSelecc[1] = gameData.videoSeleccSalaWorkingII;

                ReproductorSalaWorking.salidasVideo[0].clip = ReproductorSalaWorking.ListadoclipsVideo[gameData.videoSeleccSalaWorking];
                ReproductorSalaWorking.salidasVideo[1].clip = ReproductorSalaWorking.ListadoclipsVideo[gameData.videoSeleccSalaWorkingII];

                ReproductorSalaWorking.textos[0].text = ReproductorSalaDescanso.ListadoclipsVideo[gameData.videoSeleccSalaWorking].name;
                ReproductorSalaWorking.textos[1].text = ReproductorSalaDescanso.ListadoclipsVideo[gameData.videoSeleccSalaWorkingII].name;

                ReproductorSalaWorking.faldonesImag[2].transform.GetChild(0).GetComponent<UnityEngine.UI.InputField>().text = gameData.faldonesSeleccWorking[0];
                ReproductorSalaWorking.faldonesImag[3].transform.GetChild(1).GetComponent<UnityEngine.UI.InputField>().text = gameData.faldonesSeleccWorkingII[0];

                // Texto de los faldones de los TITULOS [0] TABLET [1] MONITOR EXTERNO
                ReproductorSalaWorking.faldonesAnim[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = gameData.faldonesSeleccWorking[0];
                ReproductorSalaWorking.faldonesAnim[0].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = gameData.faldonesSeleccWorking[1];

                ReproductorSalaWorking.faldonesAnim[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = gameData.faldonesSeleccWorkingII[0];
                ReproductorSalaWorking.faldonesAnim[1].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = gameData.faldonesSeleccWorkingII[1];
                #endregion
                #endregion

                /******************************* C U S T O M  D R A G *******************************************************/
                for (int i = 0; i < 90; i++){

                    script_customDrag.DragsCustom.infoDragsCajaI[i] = gameData.infoDragsCajaI[i];
                    

                    //if (gameData.infoDragsCajaII[i] != 0){
                        script_customDrag.DragsCustom.infoDragsCajaII[i] = gameData.infoDragsCajaII[i];
                    //}

                    //if (gameData.infoDragsCajaIII[i] != 0){
                    script_customDrag.DragsCustom.infoDragsCajaIII[i] = gameData.infoDragsCajaIII[i];
                    //}
                }

                for (int i = 0; i < 9; i++)
                {
                    if (gameData.infoMaster_prev[i] != 0){
                        script_customDrag.infoMaster_prev[i] = gameData.infoMaster_prev[i];
                    }
                }

                if (gameData.infoMaster_prev[3] != 0){
                    script_customDrag.padreMiniaturasLuz[0].GetChild(0).GetComponent<Image>().color = Color.white;
                }

                if (gameData.infoMaster_prev[4] != 0){
                    script_customDrag.padreMiniaturasLuz[1].GetChild(0).GetComponent<Image>().color = Color.white;
                }

                if (gameData.infoMaster_prev[6] != 0){
                    script_customDrag.padreMiniaturasLuz[2].GetChild(0).GetComponent<Image>().color = Color.white;
                }

                if (gameData.infoMaster_prev[7] != 0){
                    script_customDrag.padreMiniaturasLuz[3].GetChild(0).GetComponent<Image>().color = Color.white;
                }

                script_customDrag.Aplicar_Arte();

                // CAJAS
                for (int i = 0; i < 10; i++){
                    if (gameData.infoDragsVision_cajaI[i] == true){
                        script_customDrag.DragsCustom.infoDragsVision_cajaI[i] = true;
                        script_customDrag.padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).gameObject.SetActive(true);
                    }

                    if (gameData.infoDragsVision_cajaII[i] == true){
                        script_customDrag.DragsCustom.infoDragsVision_cajaII[i] = true;
                        script_customDrag.padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).gameObject.SetActive(true);
                    }

                    if (gameData.infoDragsVision_cajaIII[i] == true)
                    {
                        script_customDrag.DragsCustom.infoDragsVision_cajaIII[i] = true;
                        script_customDrag.padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).gameObject.SetActive(true);
                    }

                    script_customDrag.PersonajesSelecc[i] = true;
                    script_customDrag.padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan;

                }

                script_customDrag.Aplicar_DesdeGuardado();

                eventosMapas_WorkingRoom.numDragsDelante = gameData.num_DragsDelante_Detras[0];
                eventosMapas_WorkingRoom.numDragsDetras = gameData.num_DragsDelante_Detras[1];

                for (int i = 0; i < 3; i++){
                    if (gameData.bool_eventosGrupo_SalaReuniones[i] == true){

                        //print("Se carga " + i);
                        //eventosMapas_WorkingRoom.imageDragCustom_Ev[i].color = Color.cyan;
                        eventosMapas_WorkingRoom.CustomDragGrupos_Ev(i);

                        //eventosMapas_WorkingRoom.dragsCustomDelante.gameObject.SetActive(true);
                        //eventosMapas_WorkingRoom.dragsCustomDetras.gameObject.SetActive(true);
                    }else{

                        //print("Se pasa a blanco " + i);
                        eventosMapas_WorkingRoom.imageDragCustom_Ev[i].color = Color.white;
                    }

                    if (gameData.bool_ev_SalaReuniones[i]){
                        print("Se carga evento " + i);
                        eventosMapas_WorkingRoom.EventoDiv_Reuniones(i);
                    }

                    if (gameData.bool_ev_SalaComunicaciones[i])
                    {
                        print("Se carga evento " + i);
                        eventosMapas_WorkingRoom.EventoDiv_Comunicaciones(i);
                    }
                }

                // GRUPO SALA COMUNICACIONES
                eventosMapas_WorkingRoom.numCantidadPublico = gameData.numCantidadPublico;
                eventosMapas_WorkingRoom.image_publico_manos[0].fillAmount += 0.5f * gameData.numCantidadPublico;

                eventosMapas_WorkingRoom.numCantidadManos = gameData.numCantidadManos;
                eventosMapas_WorkingRoom.image_publico_manos[1].fillAmount += 0.5f * gameData.numCantidadManos;


                eventosMapas_WorkingRoom.Publico_Apply();
                // APLICAR

                for (int i = 0; i < 3; i++){
                    if (gameData.tipoPublico[i]){
                        print("Se carga " + i);                        
                        eventosMapas_WorkingRoom.PublicoSelecc(i);
                    }
                }

                for (int i = 0; i < 8; i++)
                {
                    if (gameData.configSalaDiv[i])
                    {
                        // adadfasf

                        // S.COMUN PRINCIPAL IZQ
                        if (i == 0)
                        {
                            print("S.COMUN PRINCIPAL IZQ");
                            script_SalasDiv.ElegirSala_PanelIzq(0);
                        }

                        // S.REUN PRINCIPAL  IZQ
                        if (i == 1)
                        {
                            print("S.REUN PRINCIPAL  IZQ");
                            script_SalasDiv.ElegirSala_PanelIzq(1);

                        }

                        // S. COMUN PRINCIPAL DER
                        if (i == 2){
                            print("S. COMUN PRINCIPAL DER");
                            script_SalasDiv.ElegirSala_PanelDer(0);

                        }

                        // S. REUN PRINCIPAL DER
                        if (i == 3){
                            print("S. REUN PRINCIPAL DER");
                            script_SalasDiv.ElegirSala_PanelDer(1);
                        }

                        // S.COMUN EXTENDIDA IZQ
                        if (i == 4){
                            print("S.COMUN EXTENDIDA IZQ");
                            script_SalasDiv.ElegirSala_PanelIzq(4);
                        }

                        // S.REUN EXTENDIDA IZQ
                        if (i == 5){
                            print("S.REUN EXTENDIDA IZQ");
                            script_SalasDiv.ElegirSala_PanelIzq(5);
                        }

                        // S.COMUN EXTENDIDA DER
                        if (i == 6)
                        {
                            print("S.COMUN EXTENDIDA DER");
                            script_SalasDiv.ElegirSala_PanelDer(6);
                        }

                        // S.REUN EXTENDIDA DER
                        if (i == 7)
                        {
                            print("S.REUN EXTENDIDA DER");
                            script_SalasDiv.ElegirSala_PanelDer(7);
                        }
                    }
                }

                guardado = gameData.guardado;
                /******************************************************************************
                ********************************   F   I   N   ********************************
                *******************************************************************************/
                #endregion
            }
            else
            {
                Debug.Log("No hay partidas guardadas");
            }
            //Reseteamos el PlayerPref
            PlayerPrefs.SetInt("slotLoading", -1);
            slotSelected = -1;
        }
    }

    #region
    /*
    public bool test;

    public void Refresh_MiniaturasLineaTemporal(){

        if (test){
            Invoke("Refresh", 0.25f);
        }
    }

    void Refresh(){

        test = false;

        // CHILD 12) Se cargan los datos de [C] LINEA TEMPORAL
        scriptHerramientas_Add.contenido_LineaTemporal.GetComponent<RectTransform>().sizeDelta = Vector2.up * gameData.longitudLineaTemporal;

        int auxLineaTemporal = gameData.numLineaTemporal;
        scriptHerramientas_Add.num = auxLineaTemporal;

        print("scrollII_min1" + gameData.scrollII_min1.Count);

        int aux = 0;
        for (int i = 0; i < auxLineaTemporal; i++){

            if (gameData.scrollII_min1.Count > 0 && gameData.scrollII_min1.Count > i && gameData.scrollII_min1[i]){

                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().padreVirtual = GetComponent<Root>().padreVirtual;
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().script = GetComponent<GenericSave>().scriptHerramientas_Add;
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_I.transform.SetParent(scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().scrollII);

                print("i = " + i);
            }

            if (gameData.scrollIII_min1.Count > 0 && gameData.scrollIII_min1.Count > i && gameData.scrollIII_min1[i]){

                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().padreVirtual = GetComponent<Root>().padreVirtual;
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().script = GetComponent<GenericSave>().scriptHerramientas_Add;
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_I.transform.SetParent(scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_I.GetComponent<BloqueLineaTemporalMiniatura>().scrollIII);

                print("i = " + i);
            }

            if (gameData.scrollII_min2.Count > 0 && gameData.scrollII_min2.Count > i &&  gameData.scrollII_min2[i]){
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().padreVirtual = GetComponent<Root>().padreVirtual;
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().script = GetComponent<GenericSave>().scriptHerramientas_Add;
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_II.transform.SetParent(scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_II.GetComponent<BloqueLineaTemporalMiniatura>().scrollII);
                print("i = " + i);
            }

            if (gameData.scrollIII_min2.Count > 0 && gameData.scrollIII_min2.Count > i && gameData.scrollIII_min2[i]){
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().padreVirtual = GetComponent<Root>().padreVirtual;
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().script = GetComponent<GenericSave>().scriptHerramientas_Add;
                scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_II.transform.SetParent(scriptHerramientas_Add.contenido_LineaTemporal.GetChild(i).GetComponent<BloqueLineaTemporal>().miniatura_II.GetComponent<BloqueLineaTemporalMiniatura>().scrollIII);
                print("i = " + i);
            }

            aux++;
        }

        int auxII = scriptHerramientas_Add.comInterna_gameob.transform.GetChild(0).childCount;
        for (int i = 0; i < auxII; i++){

            if (i != gameData.posMiniaturas_ScollII[i]){

                scriptHerramientas_Add.comInterna_gameob.transform.GetChild(0).GetChild(i).transform.SetSiblingIndex(i + gameData.posMiniaturas_ScollII[i]);
                print("la miniatura instanciada " + i + " acaba en la pos " + gameData.posMiniaturas_ScollII[i]);
            }
        }

        int auxIII = scriptHerramientas_Add.comExterna_gameob.transform.GetChild(0).childCount;
        for (int i = 0; i < auxIII; i++){

            if (i != gameData.posMiniaturas_ScollIII[i])
            {
                scriptHerramientas_Add.comExterna_gameob.transform.GetChild(0).GetChild(i).transform.SetSiblingIndex(gameData.posMiniaturas_ScollIII[i]);
                print("la miniatura instanciada " + i + " acaba en la pos " + gameData.posMiniaturas_ScollIII[i]);
            }
        }
    }*/
    #endregion

    private int recorridoInterno = 0;

    public void Refresh(){

        print("************  METODO REFRESH  ************");

        if (guardado){
            // RECORRIDO DE LAS MINIATURAS QUE HAN SIDO GUARDADAS EN LA COMUNICACIÓN INTERNA
                int oCount_II = gameData.hijos_ScrollII.Count;
                for (int o = 0; o < oCount_II; o++)
                {
                    GameObject.Find(gameData.hijos_ScrollII[o]).gameObject.transform.SetParent(scriptHerramientas_Add.comInterna_gameob.transform.GetChild(0).transform);
                }

            // RECORRIDO DE LAS MINIATURAS QUE HAN SIDO GUARDADAS EN LA COMUNICACIÓN EXTERNA

                int iCount_III = gameData.hijos_ScrollIII.Count;
                for (int o = 0; o < gameData.hijos_ScrollIII.Count; o++)
                {
                    GameObject.Find(gameData.hijos_ScrollIII[o]).gameObject.transform.SetParent(scriptHerramientas_Add.comExterna_gameob.transform.GetChild(0).transform);
                }
            guardado = false;
            gameData.guardado = false;
        }
    }

    public void DraggsSaveLoad_Descanso (int num){
        for (int i = 0; i < 2; i++){
            if (i == num){
                if (!draggs_Descanso[i]){
                    draggs_Descanso[i] = true;
                }else{
                    draggs_Descanso[i] = false;
                }
            }
        }
    }
    public void DraggsSaveLoad_General (int num){
        for (int i = 0; i < 10; i++){
            if (i == num){
                if (!draggs_General[i]){
                    draggs_General[i] = true;
                }else{
                    draggs_General[i] = false;
                }
            }
        }
    }
}

#region [System.Serializable] //Clase con la informacion que vamos a guardar
public class GameData {

    public int currentScene; // Escena usada al guardar partida

    // Info de los participantes
    public int[] infoGlobalPlayers = new int[80];           // Información global de los 10 participantes sobre su customizacion
    public bool[] playersNoUsados = new bool[10];       // Participantes que no se han usado durante la sesión
    public int[] hidenPlayer = new int[10];            // Participantes activados durante la sesión
    public int[] blockPlayer = new int[10];
    public int[] int_mapaParticipantes = new int[10];

    public string[] playersNombres = new string[10];        // Nombres de los participantes
    public Vector3[] playersPosDescanso = new Vector3[10];  // Posición de los participantes en el mapa Descanso
    public Vector3[] playersPosGeneral = new Vector3[10];   // Posición de los participantes en el mapa General
    public Vector3[] playersRotGeneral = new Vector3[10];   // Rotación de los participantes en el mapa General

    public Vector3[] playersPosReuniones = new Vector3[10];     // Posición de los participantes en el mapa REUNIONES
    public Vector3[] playersPosComunicacion = new Vector3[10];  // Posición de los participantes en el mapa COMUNICACION


    // Drags SALA DESCANSO
    public bool[] dragsDescanso = new bool[2];
    public Vector3[] dragsDescansoPos = new Vector3[2];

    // Drags SALA GENERAL
    public bool[] dragsGeneral = new bool[11];
    public Vector3[] dragsGeneralPos = new Vector3[11];
    public Vector3[] dragsGeneralRot = new Vector3[11];

    public bool[] dragsDiv = new bool[2];
    public Vector3[] dragsDivPos = new Vector3[2];
    public Vector3[] dragsDivRot = new Vector3[2];

    public int posPadre;

    // Eventos
    public bool[] eventosGeneral = new bool[28];
    public float[] eventos_WorkingRoomTiempo = new float[28];
    public string[] eventos_WorkingRoomEstado = new string[28];
    public AudioClip[] eventos_WorkingRoomAudio = new AudioClip[28];

    #region /****************** C A M P O S  D E  L A S  H E R R A M I E N T A S ******************/
    public bool[] toolsUsadas = new bool[13];

    public string[] tool_5WS = new string[7];                  // ** RELLENAR STRINGS
    public string[] tool_dafo = new string[4];                  // ** RELLENAR STRINGS

    /**************************************************************/// ANALISIS IMPLICADOS
    public int numCampos_AnalisisImplicados = 0;
    public int numConceptos_AnalisisImplicados = 0;
    public float longitudContenido_AnalisisImplicados = 0.0f;

    public List<string> conceptos_AnalisisImplpicadosLista = new List<string>();
    /**************************************************************/// GANTT OKAPI
    public int numConceptos_GanttOkapi = 0;
    public float longitud_GantOkapi = 0.0f;
    public float longitud_GantOkapiTiempo = 0.0f;

    public List<string> conceptos_GanttOkapi = new List<string>();
    public List<Vector2> sizePosTiempos_GanttOkapi = new List<Vector2>();
    /************************************************************************************/
    public string[] tool_storytelling = new string[9];          // ** RELLENAR STRINGS
    /**************************************************************/// ANALISIS DE RIESGO
    public int numanalisisRiesgo = 0;
    public float longitud_AnalisisRiesgo = 0.0f;

    public List<string> conceptos_AnalisisRiesgo = new List<string>();
    public List<string> numConceptos_AnalisisRiesgo = new List<string>();
    public List<string> tablaConceptos_AnalisisRiesgo = new List<string>();

    /************************************************************************************/
    public string tool_docBlancoI = null;                       // ** RELLENAR STRINGS
    public string tool_docBlancoII = null;                      // ** RELLENAR STRINGS
    public string[] tool_C_dafo = new string[9];               // ** RELLENAR STRINGS
    /**************************************************************/// ANALISIS [C] PLAN COMUNICACION
    public int numPlanComunicacion = 0;
    public float longitudPlanComunicacion = 0.0f;
    public List<string> conceptos_PlanComunicacion = new List<string>();

    /**************************************************************/// ANALISIS [C] PUBLICO EMPRESA
    public string[] tool_C_publicoEmpresa = new string[3]; //[0] -> situacion, [1] solucion A, [2] -> solucion B
    public string[] valores_publicEmpresa = new string[20];

    /************************************************************************************/
    /**************************************************************/// ANALISIS [C] PUBLICO POBLACION
    // POBLACION I
    public string[] porcentajesPoblacionI = new string[9];
    public string[] camposPoblacionI = new string[9];

    // POBLACION II
    public string[] porcentajesPoblacionII = new string[9];
    public string[] camposPoblacionII = new string[9];

    // POBLACION III
    public string[] porcentajesPoblacionIII = new string[9];
    public string[] camposPoblacionIII = new string[9];

    public bool dobleAnalisisPoblacion;

    /**************************************************************/// ANALISIS [C] LINEA TEMPORAL
    public int numLineaTemporal = 0;
    public float longitudLineaTemporal = 0.0f;
    public List<string> indiceLineaTemporal = new List<string>();
    public List<string> conceptos_LineaTemporal = new List<string>();

    // MINIATURA 1
    public List<bool> scrollII_min1 = new List<bool>();
    public List<bool> scrollIII_min1 = new List<bool>();

    // MINIATURA 2
    public List<bool> scrollII_min2 = new List<bool>();
    public List<bool> scrollIII_min2 = new List<bool>();


    public List<string> hijos_ScrollII = new List<string>();
    public List<string> hijos_ScrollIII = new List<string>();

    public List <string> listaBloquesLT = new List<string>();

    public bool[] comAbacos = new bool[2];
    #endregion

    #region /******************************************************************/ REPRODUCTORES
    // SALA DESCANSO
    public string[] faldonesSalaDescanso = new string[9];
    public string[] faldonesSelecc = new string[2];
    public string filtroSelecc = null;
    public int[] filtrosSelecc = new int[2];
    public int videoSeleccSalaDescanso = 0;

    public VideoClip videoSalaDescanso;

    // SALA WORKING ROOM
    public string[] faldonesSalaWorking = new string[9];
    public string[] faldonesSalaWorkingII = new string[9];

    public string[] faldonesSeleccWorking = new string[2];
    public string[] faldonesSeleccWorkingII = new string[2];

    public string filtroSeleccWorking = null;
    public string filtroSeleccWorkingII = null;

    public int[] filtrosSeleccWorking = new int[2];
    public int[] filtrosSeleccWorkingII = new int[2];

    public int videoSeleccSalaWorking = 0;
    public int videoSeleccSalaWorkingII = 0;

    public VideoClip videoSalaWorking;
    public VideoClip videoSalaWorkingII;
    #endregion

    #region /************************* G R A F I C A S  I N I C I A L E S *************************/
    // GRAFICAS EMPARENTADAS
    public bool[] graficasInicialesUsadas = new bool[3];

    // GRAFICA I
    public string graficaI_titulo = null;
    public float[] graficaI_valores = new float[8];
    public string[] graficaI_titulosIndiv = new string[8];
    public int[] graficaI_auxValoresFilas = new int[8];
    public int graficaI_numFilas = 0;
    public string graficaI_Resto = null;

    // GRAFICA II
    public string graficaII_titulo = null;
    public float[] graficaII_valores = new float[8];
    public string[] graficaII_titulosIndiv = new string[8];
    public int[] graficaII_auxValoresFilas = new int[8];
    public int graficaII_numFilas = 0;
    public string graficaII_Resto = null;

    // GRAFICA III
    public string graficaIII_titulo = null;
    public float[] graficaIII_valores = new float[8];
    public string[] graficaIII_titulosIndiv = new string[8];
    public int[] graficaIII_auxValoresFilas = new int[8];
    public int graficaIII_numFilas = 0;
    public string graficaIII_Resto = null;
    #endregion

    public bool guardado;

    /************************ S I S T E M A  D R A G **************************************/
    public int[] infoDragsCajaI = new int[90];
    public int[] infoDragsCajaII = new int[90];
    public int[] infoDragsCajaIII = new int[90];

    public int[] infoMaster_prev = new int[9];

    // CAJAS
    public bool[] infoDragsVision_cajaI = new bool[10];
    public bool[] infoDragsVision_cajaII = new bool[10];
    public bool[] infoDragsVision_cajaIII = new bool[10];

    // EVENTOS GRUPOS SALA REUNIONES
    public bool[] bool_eventosGrupo_SalaReuniones = new bool[3];

    public bool[] bool_ev_SalaReuniones = new bool[3];
    public bool[] bool_ev_SalaComunicaciones = new bool[3];
    
    // [0] Delante [1] Detras
    public int[] num_DragsDelante_Detras = new int[2];

    // 
    public bool[] configSalaDiv = new bool[8];

    // GRUPOS COMUNICACION
    public int numCantidadManos;
    public int numCantidadPublico;

    public bool[] tipoPublico = new bool[3];

    // UINVERTIDA_v2
    public int[] participantes_SalaDiv = new int[10];
    public int[] atrilPlayer = new int[10];
}
#endregion