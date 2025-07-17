using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Eventos_Mapas : MonoBehaviour {

    private Root root;
    public CustomDrag scriptCustomDrag;

    public Control_NPC script_Control;
    public SistemaHologramas script;

    public Drageables script_DragsGeneral;
    public Drageables script_DragsDescanso;

    /*[HideInInspector]*/
    public GameObject canvasMain;
    /*[HideInInspector]*/
    public List<Button> tabButton;
    /*[HideInInspector]*/
    public List<GameObject> tabContainer;
    /*[HideInInspector]*/
    public List<Image> image_eventButton_Evento;
    public List<Image> image_eventButton_EventoMultiple;

    /*[HideInInspector]*/
    public List<Button> mapsButton;
    public List<Animator> animatorEvents;
    public List<Animator> animatorEventosDiv_Reuniones;
    public List<Animator> animatorEventosDiv_Comunicaciones;

    public List<Button> botonesEv;

    public Animator grietaFxWorking;
    public Animator grietaFxDescanso;

    [Header("Custom. Ev Publico Sala Comun")]

    public Animator public_Ev;

    public string[] textoTipoPublico = new string[3];
    public string[] textoCantidadPublico = new string[3];
    public string[] textoCantidadManos = new string[3];

    public Image[] imagePublico = new Image[3];
    public int publicSelecc = 0;

    public Image[] image_publico_manos = new Image[2];
    public int numCantidadPublico = 0;
    public int numCantidadManos = 0;

    [Header("Sprites Filas")]
    public Image[] imageFilas = new Image[5];
    public Image[] imageFilas_Ev = new Image[5];

    public Sprite[] arte_publico_Fila1;
    public Sprite[] arte_publico_Fila2;
    public Sprite[] arte_publico_Fila3;
    public Sprite[] arte_publico_Fila4;
    public Sprite[] arte_publico_Fila5;

    [Header("Drags Custom ")]
    public Transform[] dragsSalaCom = new Transform[10];
    public Vector3[] posDrags = new Vector3[10];

    public Transform dragsCustomDelante;
    public Transform dragsCustomDetras;

    public int numDragsDelante = 0;
    public int numDragsDetras = 0;

    public Image[] imageDragCustom_Ev = new Image[3];

    #region
    void Awake() {

        root = transform.root.GetComponent<Root>();

        //Buscamos el canvasMain
        canvasMain = transform.parent.parent.parent.gameObject;
        //Buscamos los botones pestañas de evnetos y mapas
        int cantidadHijos = transform.GetChild(0).childCount;
        for (int i = 0; i < cantidadHijos; i++)
        {
            tabButton.Add(transform.GetChild(0).GetChild(i).GetComponent<Button>());
        }
        //Buscamos las pestañas de evnetos y mapas
        cantidadHijos = transform.childCount;
        for (int i = 1; i < cantidadHijos; i++)
        {
            tabContainer.Add(transform.GetChild(i).gameObject);
        }

        //Buscamos los botones de eventos
        cantidadHijos = transform.GetChild(1).childCount; //esto es la cantidad de grupos de eventos
        /*for (int i = 0; i < cantidadHijos; i++)
        {
            int hijosDelGrupo = transform.GetChild(1).GetChild(i).childCount;
            for (int j = 0; j < hijosDelGrupo; j++)
                eventsButton.Add(transform.GetChild(1).GetChild(i).GetChild(j).GetComponent<Button>());
        }*/
        //Buscamos l0s botones de mapas
        cantidadHijos = transform.GetChild(2).childCount;
        for (int i = 0; i < cantidadHijos; i++)
        {
            mapsButton.Add(transform.GetChild(2).GetChild(i).GetComponent<Button>());
        }
        //Ponemos todos los animators con "keepAnimatorControllerStateOnDisable = true"
        for (int i = 0; i < animatorEvents.Count; i++)
        {
            animatorEvents[i].keepAnimatorControllerStateOnDisable = true;
        }

        for (int i = 0; i < animatorEventosDiv_Comunicaciones.Count; i++){
            animatorEventosDiv_Comunicaciones[i].keepAnimatorControllerStateOnDisable = true;
        }

        for (int i = 0; i < animatorEventosDiv_Reuniones.Count; i++){
            animatorEventosDiv_Reuniones[i].keepAnimatorControllerStateOnDisable = true;
        }

        public_Ev.keepAnimatorControllerStateOnDisable = true;
    }

    public void Start(){

        if (transform.root.GetComponent<Root>().currentScene != 0){
            CambiarMapa(transform.root.GetComponent<Root>().currentScene);
        }
    }

    private int mapaActual = 0;

    //Funcion para cambiar de modo evento a modo mapa y viceversa
    public void CambioDeModo(int modo)
    {

        for (int i = 0; i < tabButton.Count; i++)
        {
            tabButton[i].GetComponent<Image>().color = Color.white;
            tabContainer[i].SetActive(false);
        }
        tabButton[modo].GetComponent<Image>().color = Color.cyan;

        if (modo == 0)
        {
            tabContainer[0].transform.GetChild(mapaActual).gameObject.SetActive(true);
            tabContainer[0].SetActive(true);

            for (int i = 0; i < 5; i++)
            {

                if (i != mapaActual && tabContainer[0].transform.GetChild(i).gameObject.activeSelf)
                {
                    tabContainer[0].transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else tabContainer[modo].SetActive(true);
    }

    //Funcion para cambiar de mapa
    public void CambiarMapa(int numeroMapa){

        if (root.currentScene != numeroMapa){
            if (script_Control.character != null){
                print("Cambias de mapa");
                script_Control.character = null;
            }
        }

        //Movemos la camara sobre el mapa seleccionado
        canvasMain.transform.position = root.escenario[numeroMapa].transform.position;

        //Cambiamos de color los botones de los mapas
        for (int i = 0; i < mapsButton.Count; i++){
            mapsButton[i].GetComponent<Image>().color = Color.white;
        }
        mapsButton[numeroMapa].GetComponent<Image>().color = Color.cyan;

        #region
        if (numeroMapa == 1){
            for (int i = 0; i < script_DragsGeneral.content.transform.childCount; i++) {
                if (script_DragsGeneral.content.transform.GetChild(i).GetChild(1).GetComponent<Image>().color == Color.cyan){
                    script_Control.character = script_DragsGeneral.contentDrageables_Ingame.transform.GetChild(i).gameObject;
                }
            }
        }

        if (numeroMapa == 3){
            for (int i = 0; i < script_DragsDescanso.content.transform.childCount; i++){
                if (script_DragsDescanso.content.transform.GetChild(i).GetChild(1).GetComponent<Image>().color == Color.cyan){
                    script_Control.character = script_DragsDescanso.contentDrageables_Ingame.transform.GetChild(i).gameObject;
                }
            }
        }

        if (numeroMapa == 4){
            /*for (int i = 0; i < script_DragsDescanso.content.transform.childCount; i++){
                if (script_DragsDescanso.content.transform.GetChild(i).GetChild(1).GetComponent<Image>().color == Color.cyan){
                    script_Control.character = script_DragsDescanso.contentDrageables_Ingame.transform.GetChild(i).gameObject;
                }
            }*/
        }
        #endregion

        root.currentScene = numeroMapa;
        mapaActual = numeroMapa;

        return;
    }
    
    //Funcion para activar los eventos
    public void Evento(int numeroEvento) {

        // **** AQUI HAY QUE INTRODUCIR EL CODIGO PARA ACTIVAR EL ANIMATOR DEL EVENTO CORRESPONDIENTE ****
        // ***********************************************************************************************
        // **** EJEMPLO: **** animatorEvents[numeroEvento].Play("Nombre de la animación"); ***************

        // SI LOS EVENTOS SON DE ON / OFF...
        if (!animatorEvents[numeroEvento].enabled){
            root.GetComponent<GenericSave>().eventos_WorkingRoom[numeroEvento] = true;
            if (numeroEvento == 7 || numeroEvento == 8 || numeroEvento == 9)
            {
                print("ALIEN");
                animatorEvents[7].enabled = true;
                animatorEvents[8].enabled = true;
                animatorEvents[9].enabled = true;

                grietaFxWorking.enabled = true;
                grietaFxDescanso.enabled = true;

                Camera.main.GetComponent<Animator>().enabled = true;
                Camera.main.GetComponent<AudioSource>().Play();

                root.GetComponent<GenericSave>().eventos_WorkingRoom[7] = true;
                root.GetComponent<GenericSave>().eventos_WorkingRoom[8] = true;
                root.GetComponent<GenericSave>().eventos_WorkingRoom[9] = true;

                image_eventButton_Evento[7].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                image_eventButton_Evento[8].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                image_eventButton_Evento[9].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;

            }else{

                if (numeroEvento == 22){

                }
                else {

                    animatorEvents[numeroEvento].enabled = true;

                    if (numeroEvento != 16 && numeroEvento != 11)
                    {
                        image_eventButton_Evento[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                        image_eventButton_Evento[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                        image_eventButton_Evento[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                    }
                    else
                    {
                        print("DOOOOOS");
                        image_eventButton_Evento[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
                    }

                    if (animatorEvents[numeroEvento].GetComponent<AudioSource>() != null)
                    {
                        animatorEvents[numeroEvento].GetComponent<AudioSource>().enabled = true;
                    }
                }
            }
        }else{
            if (numeroEvento == 7 || numeroEvento == 8 || numeroEvento == 9)
            {
                animatorEvents[7].SetTrigger("1");
                animatorEvents[8].SetTrigger("1");
                animatorEvents[9].SetTrigger("1");

                grietaFxWorking.SetTrigger("1");
                grietaFxDescanso.SetTrigger("1");

                image_eventButton_Evento[7].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
                image_eventButton_Evento[8].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
                image_eventButton_Evento[9].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;

                root.GetComponent<GenericSave>().eventos_WorkingRoom[7] = false;
                root.GetComponent<GenericSave>().eventos_WorkingRoom[8] = false;
                root.GetComponent<GenericSave>().eventos_WorkingRoom[9] = false;
            }
            else
            {

                if (numeroEvento == 22){
                    print("JIJIJIJI");
                }else{

                    animatorEvents[numeroEvento].SetTrigger("1");

                    if (numeroEvento != 16 && numeroEvento != 11){
                        image_eventButton_Evento[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
                        root.GetComponent<GenericSave>().eventos_WorkingRoom[numeroEvento] = false;
                    }else{

                        if (numeroEvento == 16 || numeroEvento == 11){
                            if (image_eventButton_Evento[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount != 1){
                                image_eventButton_Evento[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 1.0f;
                            }else{
                                image_eventButton_Evento[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 0.0f;
                                root.GetComponent<GenericSave>().eventos_WorkingRoom[numeroEvento] = false;
                            }
                        }
                    } if (animatorEvents[numeroEvento].GetComponent<AudioSource>() != null)
                    {
                        animatorEvents[numeroEvento].GetComponent<AudioSource>().enabled = false;
                    }
                }
            }
        }


        if (numeroEvento == 22){
            if (!animatorEvents[numeroEvento].gameObject.activeSelf){
                animatorEvents[numeroEvento].gameObject.SetActive(true);
                image_eventButton_Evento[22].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                print("JIJI X2");
            }else{
                animatorEvents[numeroEvento].gameObject.SetActive(false);
                image_eventButton_Evento[22].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
                print("JIJI X3");
            }
        }
    }

    public void EventoMultiple(int numeroEvento) {

        if (!animatorEvents[numeroEvento].enabled) {
            animatorEvents[numeroEvento].enabled = true;
            //root.GetComponent<GenericSave>().eventos_WorkingRoom[numeroEvento] = true;

            int aux = animatorEvents[numeroEvento].GetCurrentAnimatorClipInfoCount(0);
            image_eventButton_EventoMultiple[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;

            if (animatorEvents[numeroEvento].GetComponent<AudioSource>()){
                animatorEvents[numeroEvento].GetComponent<AudioSource>().Play();
            }
        } else {
            animatorEvents[numeroEvento].Rebind();
            animatorEvents[numeroEvento].enabled = false;
            //root.GetComponent<GenericSave>().eventos_WorkingRoom[numeroEvento] = false;


            int aux = animatorEvents[numeroEvento].GetCurrentAnimatorClipInfoCount(0);
            image_eventButton_EventoMultiple[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;

            if (animatorEvents[numeroEvento].GetComponent<AudioSource>()){
                animatorEvents[numeroEvento].GetComponent<AudioSource>().Stop();
            }
        }
    }

    public void EventoMultipleRespuesta(int numeroEvento) {
        animatorEvents[numeroEvento].SetTrigger("1");

        if (animatorEvents[numeroEvento].GetComponent<AudioSource>().isPlaying){
            animatorEvents[numeroEvento].GetComponent<AudioSource>().Stop();
        }else{
            //root.GetComponent<GenericSave>().eventos_WorkingRoom[numeroEvento] = false;
            image_eventButton_EventoMultiple[numeroEvento].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        }
    }

    // Función para activar eventos en sala div
    public void EventoDiv_Reuniones (int numeroEvento)
    {

        for (int i = 0; i < animatorEventosDiv_Reuniones.Count; i++){
            if (i == numeroEvento){

                if (!animatorEventosDiv_Reuniones[numeroEvento].enabled){
                    animatorEventosDiv_Reuniones[numeroEvento].enabled = true;
                }else{
                    animatorEventosDiv_Reuniones[numeroEvento].SetTrigger("1");

                    if (!animatorEventosDiv_Reuniones[numeroEvento].transform.parent.parent.gameObject.activeSelf){
                        animatorEventosDiv_Reuniones[numeroEvento].Rebind();
                        animatorEventosDiv_Reuniones[numeroEvento].enabled = false;
                        animatorEventosDiv_Reuniones[numeroEvento].transform.GetChild(0).GetComponent<Image>().color = new Vector4 (1,1,1,0);
                    }
                }
            }
        }
    }

    public void EventoDiv_Comunicaciones(int numeroEvento)
    {

        if (numeroEvento >= 1)
        {

            for (int i = 1; i < animatorEventosDiv_Comunicaciones.Count; i++)
            {
                if (i == numeroEvento)
                {
                    if (!animatorEventosDiv_Comunicaciones[numeroEvento].enabled){
                        animatorEventosDiv_Comunicaciones[numeroEvento].enabled = true;
                    }else{
                        animatorEventosDiv_Comunicaciones[numeroEvento].SetTrigger("1");
                    }
                }
            }
        }else{

            print("CERCA DE LA ALARMA");

            if (!animatorEventosDiv_Comunicaciones[numeroEvento].gameObject.activeSelf){
                print("SE ACTIVA");
                animatorEventosDiv_Comunicaciones[numeroEvento].gameObject.SetActive(true);
            }else{
                print("SE DESACTIVA");
                animatorEventosDiv_Comunicaciones[numeroEvento].gameObject.SetActive(false);
            }

            if (!animatorEventosDiv_Comunicaciones[numeroEvento].transform.parent.parent.gameObject.activeSelf)
            {
                animatorEventosDiv_Comunicaciones[numeroEvento].Rebind();
                animatorEventosDiv_Comunicaciones[numeroEvento].enabled = false;
                animatorEventosDiv_Comunicaciones[numeroEvento].transform.GetChild(0).GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
            }
        }
    }
    #endregion

    public void CantidadPublico_Mas(){
        if (numCantidadPublico < 2){ numCantidadPublico++; image_publico_manos[0].fillAmount += 0.5f; }
        else { numCantidadPublico = 0; image_publico_manos[0].fillAmount = 0.0f; }

        Publico_Apply();
    }

    public void CantidadManos_Mas(){
        if (numCantidadManos < 2){ numCantidadManos++; image_publico_manos[1].fillAmount += 0.5f; }
        else { numCantidadManos = 0; image_publico_manos[1].fillAmount = 0.0f; }

        Publico_Apply();
    }

    public void CantidadPublico_Menos()
    {
        if (numCantidadPublico > 0) { numCantidadPublico--; image_publico_manos[0].fillAmount -= 0.5f; }
        else { numCantidadPublico = 2; image_publico_manos[0].fillAmount = 1.0f; }

        Publico_Apply();
    }

    public void CantidadManos_Menos()
    {
        if (numCantidadManos > 0) { numCantidadManos--; image_publico_manos[1].fillAmount -= 0.5f; }
        else { numCantidadManos = 2; image_publico_manos[1].fillAmount = 1.0f; }

        Publico_Apply();
    }

    public void PublicoSelecc(int num){

        for (int i = 0; i < 3; i++){
            if (num == i){

                if (imagePublico[num].color != Color.cyan){
                    imagePublico[num].color = Color.cyan;
                    publicSelecc = num;
                    //print("SE ACTIVA");

                    Publico_Apply();
                    public_Ev.Rebind();
                    public_Ev.enabled = true;
                    public_Ev.gameObject.GetComponent<Collider>().enabled = true;
                }
                else{
                    imagePublico[num].color = Color.white;
                    //print("SE DESACTIVA");

                    public_Ev.SetTrigger("1");
                    public_Ev.gameObject.GetComponent<Collider>().enabled = false;
                }
            }else{
                imagePublico[i].color = Color.white;
            }
        }
    }

    public void Publico_Apply(){

        int resultado = (publicSelecc * 3) + (numCantidadPublico * 3) + numCantidadManos;
        //print("RESULTADO -> PUBLICO: " + textoTipoPublico[publicSelecc] + " " + textoCantidadPublico[numCantidadPublico] + " " + textoCantidadManos[numCantidadManos]);

        imageFilas_Ev[0].sprite = arte_publico_Fila1[resultado];
        imageFilas_Ev[1].sprite = arte_publico_Fila2[resultado];
        imageFilas_Ev[2].sprite = arte_publico_Fila3[resultado];
        imageFilas_Ev[3].sprite = arte_publico_Fila4[resultado];
        imageFilas_Ev[4].sprite = arte_publico_Fila5[resultado];

        //for (int i = 0; i < 5; i++){ imageFilas_Ev[i].SetNativeSize(); }
    }

    public void Update(){

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            CustomDrags_Ev(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            CustomDrags_Ev(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            CustomDrags_Ev(2);
        }
    }

    // EV_CUSTOM DRAGS
    public void CustomDrags_Ev (int num){

        numDragsDelante = 0;
        numDragsDetras = 0;

        for (int i = 0; i < 10; i++){
            if (dragsSalaCom[i].gameObject.activeSelf){
                dragsSalaCom[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 10; i++){
            int player = i;

            if (num == 0){

                if (scriptCustomDrag.DragsCustom.infoDragsCajaI[1 + (9 * player)] == 0){
                    if (numDragsDelante < 5 && scriptCustomDrag.DragsCustom.infoDragsVision_cajaI[i]){
                        print("Jugador " + player + " está FRONTAL ");

                        dragsSalaCom[player].gameObject.SetActive(true);
                        dragsSalaCom[player].SetParent(dragsCustomDetras.transform);

                        dragsSalaCom[player].GetComponent<RectTransform>().anchoredPosition = posDrags[numDragsDelante];
                        numDragsDelante++;

                        for (int o = 0; o < 5; o++){
                            dragsSalaCom[player].GetChild(o).GetComponent<Image>().sprite = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(o).GetComponent<Image>().sprite;
                        }

                        dragsSalaCom[player].GetChild(2).GetComponent<Image>().color = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(2).GetComponent<Image>().color;

                        // NO LLEVA PAÑUELO
                        if (scriptCustomDrag.DragsCustom.infoDragsCajaI[8 + (9 * player)] == 0){
                            print("PLAYER " + player + " no lleva pañuelo " + (8 + (9 * player)));
                            
                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(true);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(false);
                        }
                        else{ // SI LLEVA PAÑUELO
                            print("PLAYER " + player + " si lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(false);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(true);
                        }
                    }
                }else{
                    if (numDragsDetras < 5 && scriptCustomDrag.DragsCustom.infoDragsVision_cajaI[i])
                    {
                        print("Jugador " + player + " está ESPALDAS ");

                        dragsSalaCom[player].gameObject.SetActive(true);
                        dragsSalaCom[player].SetParent(dragsCustomDelante.transform);

                        dragsSalaCom[player].GetComponent<RectTransform>().anchoredPosition = posDrags[numDragsDetras + 5];
                        numDragsDetras++;

                        for (int o = 0; o < 5; o++)
                        {
                            dragsSalaCom[player].GetChild(o).GetComponent<Image>().sprite = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(o).GetComponent<Image>().sprite;
                        }

                        dragsSalaCom[player].GetChild(2).GetComponent<Image>().color = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(2).GetComponent<Image>().color;

                        // NO LLEVA PAÑUELO
                        if (scriptCustomDrag.DragsCustom.infoDragsCajaI[8 + (9 * player)] == 0)
                        {
                            print("PLAYER " + player + " no lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(true);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(false);
                        }
                        else
                        { // SI LLEVA PAÑUELO
                            print("PLAYER " + player + " si lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(false);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(true);
                        }
                    }
                }
            }

            if (num == 1)
            {

                if (scriptCustomDrag.DragsCustom.infoDragsCajaII[1 + (9 * player)] == 0)
                {
                    if (numDragsDelante < 5 && scriptCustomDrag.DragsCustom.infoDragsVision_cajaII[i])
                    {
                        print("Jugador " + player + " está FRONTAL ");

                        dragsSalaCom[player].gameObject.SetActive(true);
                        dragsSalaCom[player].SetParent(dragsCustomDetras.transform);

                        dragsSalaCom[player].GetComponent<RectTransform>().anchoredPosition = posDrags[numDragsDelante];
                        numDragsDelante++;

                        for (int o = 0; o < 5; o++)
                        {
                            dragsSalaCom[player].GetChild(o).GetComponent<Image>().sprite = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(o).GetComponent<Image>().sprite;
                        }

                        dragsSalaCom[player].GetChild(2).GetComponent<Image>().color = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(2).GetComponent<Image>().color;

                        // NO LLEVA PAÑUELO
                        if (scriptCustomDrag.DragsCustom.infoDragsCajaII[8 + (9 * player)] == 0)
                        {
                            print("PLAYER " + player + " no lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(true);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(false);
                        }
                        else
                        { // SI LLEVA PAÑUELO
                            print("PLAYER " + player + " si lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(false);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (numDragsDetras < 5 && scriptCustomDrag.DragsCustom.infoDragsVision_cajaII[i])
                    {
                        print("Jugador " + player + " está ESPALDAS ");

                        dragsSalaCom[player].gameObject.SetActive(true);
                        dragsSalaCom[player].SetParent(dragsCustomDelante.transform);

                        dragsSalaCom[player].GetComponent<RectTransform>().anchoredPosition = posDrags[numDragsDetras + 5];
                        numDragsDetras++;

                        for (int o = 0; o < 5; o++)
                        {
                            dragsSalaCom[player].GetChild(o).GetComponent<Image>().sprite = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(o).GetComponent<Image>().sprite;
                        }

                        dragsSalaCom[player].GetChild(2).GetComponent<Image>().color = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(2).GetComponent<Image>().color;

                        // NO LLEVA PAÑUELO
                        if (scriptCustomDrag.DragsCustom.infoDragsCajaII[8 + (9 * player)] == 0)
                        {
                            print("PLAYER " + player + " no lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(true);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(false);
                        }
                        else
                        { // SI LLEVA PAÑUELO
                            print("PLAYER " + player + " si lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(false);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(true);
                        }
                    }
                }
            }

            if (num == 2)
            {

                if (scriptCustomDrag.DragsCustom.infoDragsCajaIII[1 + (9 * player)] == 0)
                {
                    if (numDragsDelante < 5 && scriptCustomDrag.DragsCustom.infoDragsVision_cajaIII[i])
                    {
                        print("Jugador " + player + " está FRONTAL ");

                        dragsSalaCom[player].gameObject.SetActive(true);
                        dragsSalaCom[player].SetParent(dragsCustomDetras.transform);

                        dragsSalaCom[player].GetComponent<RectTransform>().anchoredPosition = posDrags[numDragsDelante];
                        numDragsDelante++;

                        for (int o = 0; o < 5; o++)
                        {
                            dragsSalaCom[player].GetChild(o).GetComponent<Image>().sprite = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(o).GetComponent<Image>().sprite;
                        }

                        dragsSalaCom[player].GetChild(2).GetComponent<Image>().color = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(2).GetComponent<Image>().color;

                        // NO LLEVA PAÑUELO
                        if (scriptCustomDrag.DragsCustom.infoDragsCajaIII[8 + (9 * player)] == 0)
                        {
                            print("PLAYER " + player + " no lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(true);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(false);
                        }
                        else
                        { // SI LLEVA PAÑUELO
                            print("PLAYER " + player + " si lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(false);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (numDragsDetras < 5 && scriptCustomDrag.DragsCustom.infoDragsVision_cajaIII[i])
                    {
                        print("Jugador " + player + " está ESPALDAS ");

                        dragsSalaCom[player].gameObject.SetActive(true);
                        dragsSalaCom[player].SetParent(dragsCustomDelante.transform);

                        dragsSalaCom[player].GetComponent<RectTransform>().anchoredPosition = posDrags[numDragsDetras + 5];
                        numDragsDetras++;

                        for (int o = 0; o < 5; o++)
                        {
                            dragsSalaCom[player].GetChild(o).GetComponent<Image>().sprite = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(o).GetComponent<Image>().sprite;
                        }

                        dragsSalaCom[player].GetChild(2).GetComponent<Image>().color = scriptCustomDrag.padreCajasSelecc.GetChild(num).GetChild(2).GetChild(player).GetChild(2).GetComponent<Image>().color;

                        // NO LLEVA PAÑUELO
                        if (scriptCustomDrag.DragsCustom.infoDragsCajaIII[8 + (9 * player)] == 0)
                        {
                            print("PLAYER " + player + " no lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(true);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(false);
                        }
                        else
                        { // SI LLEVA PAÑUELO
                            print("PLAYER " + player + " si lleva pañuelo " + (8 + (9 * player)));

                            dragsSalaCom[player].GetChild(2).gameObject.SetActive(false);
                            dragsSalaCom[player].GetChild(3).gameObject.SetActive(true);
                        }
                    }
                }
            }

        }
    }

    public void CustomDragGrupos_Ev (int num){

        for (int i = 0; i < 3; i++){
            if (i == num){

                if (imageDragCustom_Ev[num].color == Color.white){
                    imageDragCustom_Ev[num].color = Color.cyan;
                    dragsCustomDelante.gameObject.SetActive(true);
                    dragsCustomDetras.gameObject.SetActive(true);
                    CustomDrags_Ev(num);

                }else{
                    imageDragCustom_Ev[num].color = Color.white;
                    dragsCustomDelante.gameObject.SetActive(false);
                    dragsCustomDetras.gameObject.SetActive(false);
                }
            }else{
                if (imageDragCustom_Ev[i].color == Color.cyan) { imageDragCustom_Ev[i].color = Color.white; }
            }
        }
    }
}