using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CustomDrag : MonoBehaviour{

    #region DECLARACIÓN VARIABLES
    public Transform botonPannuelo;
    public Transform padreBotonesSelecc;
    public Transform padreCajasSelecc;

    public int cajaSelecc;
    public bool[] PersonajesSelecc = new bool[10];

    public bool vision;
    public Image imageVision;
    public GameObject go_aplicar;

    [Header("ARTE CUSTOM - DRAGS")]
    public ArteDrags ArteDrags_ = new ArteDrags();

    [System.Serializable]
    public class ArteDrags{

        [Header ("FRONTAL")]
        public Sprite[] frontal_Pieles = new Sprite[4];
        public Sprite[] frontal_Pelo = new Sprite[8];
        public Sprite[] frontal_Ropa = new Sprite[10];
        public Color[] frontal_PeloColor = new Color[5];
        public Sprite[] frontal_Pannuelo = new Sprite[1];
        public Sprite[] frontal_Calzado = new Sprite[2];

        [Header ("TRASERO")]
        public Sprite[] trasero_Pieles = new Sprite[4];
        public Sprite[] trasero_Pelo = new Sprite[8];
        public Sprite[] trasero_Ropa = new Sprite[10];
        public Color[] trasero_PeloColor = new Color[5];
        public Sprite[] trasero_Pannuelo = new Sprite[1];
        public Sprite[] trasero_Calzado = new Sprite[2];
    }

    #region INFO CAJAS DRAGS CUSTOMIZADOS
    [Header("Info CAJA")]
    public CajaI_info DragsCustom = new CajaI_info();

    [System.Serializable]
    public class CajaI_info{
        public int[] infoDragsCajaI = new int[70];
        public int[] infoDragsCajaII = new int[70];
        public int[] infoDragsCajaIII = new int[70];

        public bool[] infoDragsVision_cajaI = new bool[10];
        public bool[] infoDragsVision_cajaII = new bool[10];
        public bool[] infoDragsVision_cajaIII = new bool[10];
    }

    #endregion

    #region INFO PREVISUALIZACION
    [Header("GENERO [1] FRENTE/ESPALDA [2] RAZA [3] PELO [4] PELO_COLOR [5] FRONTAL_INFORMAL [6] ROPA [7] ROPACOLOR [8] PANNUELO ")]
    public int[] infoMaster_prev = new int[8];
    public Image[] capas_prev = new Image[5];
    public Sprite[] capas_prevDef = new Sprite[5];
    #endregion

    #region
    [Header("[0] GENERO [1] FRENTE/PERFIL [2] FORMAL/INFORMAL [3] RAZA")]
    public Transform[] botonesCategorias = new Transform[4];

    public Color[] razaColores = new Color[2];
    public Sprite[] generoSimbolos = new Sprite[2];
    public Sprite[] simboloPostura;

    public Transform[] padreMiniaturasLuz = new Transform[3];
    private char[] letraEstiloRopa = { 'I', 'F' };

    #endregion
    #endregion

    #region DECLARACIÓN DE FUNCIONES
    public void Awake(){
        padreBotonesSelecc = transform.GetChild(0).GetChild(2).transform;
        padreCajasSelecc = transform.GetChild(1).transform;
    }

    #region Seleccionando Drags...
    public void SeleccPlayers(int num){

        if (!vision){

            PersonajesSelecc[num] = !PersonajesSelecc[num];


            if (PersonajesSelecc[num]){
                padreBotonesSelecc.transform.GetChild(num).GetComponent<Image>().color = Color.cyan;
            }
            else{
                padreBotonesSelecc.transform.GetChild(num).GetComponent<Image>().color = Color.white;
            }
        }else{

            if (!padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(num).gameObject.activeSelf) {
                print("SE VISIBILIZA PERSONAJE " + num + " DE CAJA " + cajaSelecc);
                padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(num).gameObject.SetActive(true);
                padreBotonesSelecc.transform.GetChild(num).GetComponent<Image>().color = Color.cyan;


                if (cajaSelecc == 0) { DragsCustom.infoDragsVision_cajaI[num] = true; }
                if (cajaSelecc == 1) { DragsCustom.infoDragsVision_cajaII[num] = true; }
                if (cajaSelecc == 2) { DragsCustom.infoDragsVision_cajaIII[num] = true; }
            }
            else{
                print("SE OCULTA PERSONAJE " + num + " DE CAJA " + cajaSelecc);
                padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(num).gameObject.SetActive(false);
                padreBotonesSelecc.transform.GetChild(num).GetComponent<Image>().color = Color.white;


                if (cajaSelecc == 0) { DragsCustom.infoDragsVision_cajaI[num] = false; }
                if (cajaSelecc == 1) { DragsCustom.infoDragsVision_cajaII[num] = false; }
                if (cajaSelecc == 2) { DragsCustom.infoDragsVision_cajaIII[num] = false; }
            }
        }
    }

    public void TodosSelecc (){

        for (int i = 0; i < 10; i++){

            if (!PersonajesSelecc[i]){

                PersonajesSelecc[i] = true;
                padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan;
            }
        }
    }

    public void TodosDeselecc (){
        for (int i = 0; i < 10; i++)
        {

            if (PersonajesSelecc[i]){

                PersonajesSelecc[i] = false;

                if (cajaSelecc == 0){
                    if (!DragsCustom.infoDragsVision_cajaI[i]){
                        padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.grey;
                    }else{
                        padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                    }
                }

                if (cajaSelecc == 1){
                    if (!DragsCustom.infoDragsVision_cajaII[i]){
                        padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.grey;
                    }else{
                        padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                    }
                }
            }
        }
    }
    #endregion

    #region Seleccionando Cajas...
    public void SeleccCajas(int num){

        if (num != cajaSelecc){
            padreCajasSelecc.GetChild(num).GetChild(0).gameObject.SetActive(true);
            padreCajasSelecc.GetChild(cajaSelecc).GetChild(0).gameObject.SetActive(false);

            cajaSelecc = num;

            if (!vision){

                for (int i = 0; i < 10; i++){
                    if (PersonajesSelecc[i]){
                        padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan;
                    }else{
                        padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                    }
                }
            }else{

            

                for (int i = 0; i < 10; i++){

                    if (cajaSelecc == 0){
                        if (DragsCustom.infoDragsVision_cajaI[i]) { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan; print("Pasan a azul: " + i); }
                        else { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white; print("Pasan a blanco: " + i); }
                    }

                    if (cajaSelecc == 1)
                    {
                        if (DragsCustom.infoDragsVision_cajaII[i]) { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan; print("Pasan a azul: " + i); }
                        else { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white; print("Pasan a blanco: " + i); }
                    }

                    if (cajaSelecc == 2)
                    {
                        if (DragsCustom.infoDragsVision_cajaIII[i]) { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan; print("Pasan a azul: " + i); }
                        else { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white; print("Pasan a blanco: " + i); }
                    }
                }
            }
        }
    }
    #endregion

    public void VisionDrags(){

        vision = !vision;

        if (vision) {
            print("Pones vision");
            go_aplicar.SetActive(false);

            imageVision.color = Color.cyan;

            if (cajaSelecc == 0){
                for (int i = 0; i < 10; i++){
                    if (DragsCustom.infoDragsVision_cajaI[i]){ padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan; }
                    else { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white; }
                }
            }

            if (cajaSelecc == 1){
                for (int i = 0; i < 10; i++){
                    if (DragsCustom.infoDragsVision_cajaII[i]) { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan; }
                    else { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white; }
                }
            }

            if (cajaSelecc == 2){
                for (int i = 0; i < 10; i++)
                {
                    if (DragsCustom.infoDragsVision_cajaIII[i]) { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan; }
                    else { padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white; }
                }
            }
        }
        else {
            print("Quitas vision");
            go_aplicar.SetActive(true);
            imageVision.color = Color.white;

            for (int i = 0; i < 10; i++){

                if (PersonajesSelecc[i]){
                    padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.cyan;
                }else{
                    padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    #region PASAMOS LA INFORMACION DE LA PREVISUALIZACION A LOS DRAGS SELECCIONADOS (0 A 10) SEGÚN LA CAJA
    public void Aplicar(){

        for (int i = 0; i < 5; i++){
            capas_prevDef[i] = capas_prev[i].sprite;
        }

        // UPDATE INFORMACION infomaster_prev SEGÚN ESTADO DE PANNUELO: SI HOMBRE SIEMPRE -OFF-, SI MUJER -ON- O -OFF-
        if (infoMaster_prev[0] == 0){ // -> SI ES HOMBRE
            infoMaster_prev[8] = 0;
        }else{ // -> SI ES MUJER

            if (capas_prev[3].gameObject.activeSelf){ infoMaster_prev[8] = 1; }
            else { infoMaster_prev[8] = 0; }
        }

        #region
        if (cajaSelecc == 0){

            for (int i = 0; i < 10; i++){
                if (PersonajesSelecc[i]) { 
                    int player = i;

                    //
                    for (int o = 0; o < 9; o++){ DragsCustom.infoDragsCajaI[(o + (player * 9))] = infoMaster_prev[o]; }

                    //
                    for (int o = 0; o < 5; o++){ padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(o).GetComponent<Image>().sprite = capas_prevDef[o]; }

                    // SI LA CUSTOMIZACION MASTER ES MUJER Y TIENE PAÑUELO ACTIVADO HAY QUE QUITAR EL PELO
                    //print("Player: " + player + " Posicion accesorio del personaje: " + (9 + (player * 9)));
                    //print("Player: " + player + " Posicion genero del personaje: " + (player * 9));

                    // SI ES MUJER Y TIENE EL VELO PUESTO
                    if (DragsCustom.infoDragsCajaI[player * 9] == 1){
                        // print("ES MUJER Y LLEVA PAÑUELO");


                        if (DragsCustom.infoDragsCajaI[(8 + (player * 9))] == 1){
                            // CAPA PELO SE DESACTIVA Y SE ACTIVA CAPA PAÑUELO
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(false);
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(true);
                        }else{
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                        }
                    }
                    else
                    { // SI NO ES ASÍ...
                        // print("NO SE CUMPLE QUE SEA MUJER Y LLEVA PAÑUELO");

                        padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                        padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                    }

                    // LE APLICAS EL COLOR DEL PELO
                    padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = capas_prev[2].color;
                    
                }
            }
        }

        if (cajaSelecc == 1){

            for (int i = 0; i < 10; i++)
            {
                if (PersonajesSelecc[i])
                {
                    int player = i;

                    //
                    for (int o = 0; o < 9; o++) { DragsCustom.infoDragsCajaII[(o + (player * 9))] = infoMaster_prev[o]; }

                    //
                    for (int o = 0; o < 5; o++) { padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(o).GetComponent<Image>().sprite = capas_prevDef[o]; }

                    // SI LA CUSTOMIZACION MASTER ES MUJER Y TIENE PAÑUELO ACTIVADO HAY QUE QUITAR EL PELO
                    print("Player: " + player + " Posicion accesorio del personaje: " + (9 + (player * 9)));
                    print("Player: " + player + " Posicion genero del personaje: " + (player * 9));

                    // SI ES MUJER Y TIENE EL VELO PUESTO
                    if (DragsCustom.infoDragsCajaII[player * 9] == 1)
                    {
                        // print("ES MUJER Y LLEVA PAÑUELO");


                        if (DragsCustom.infoDragsCajaII[(8 + (player * 9))] == 1)
                        {
                            // CAPA PELO SE DESACTIVA Y SE ACTIVA CAPA PAÑUELO
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(false);
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(true);
                        }
                        else
                        {
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                        }
                    }
                    else
                    { // SI NO ES ASÍ...
                        // print("NO SE CUMPLE QUE SEA MUJER Y LLEVA PAÑUELO");

                        padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                        padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                    }

                    // LE APLICAS EL COLOR DEL PELO
                    padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = capas_prev[2].color;

                }
            }

        }

        if (cajaSelecc == 2){

            for (int i = 0; i < 10; i++)
            {
                if (PersonajesSelecc[i])
                {
                    int player = i;

                    //
                    for (int o = 0; o < 9; o++) { DragsCustom.infoDragsCajaIII[(o + (player * 9))] = infoMaster_prev[o]; }

                    //
                    for (int o = 0; o < 5; o++) { padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(o).GetComponent<Image>().sprite = capas_prevDef[o]; }

                    // SI LA CUSTOMIZACION MASTER ES MUJER Y TIENE PAÑUELO ACTIVADO HAY QUE QUITAR EL PELO
                    print("Player: " + player + " Posicion accesorio del personaje: " + (9 + (player * 9)));
                    print("Player: " + player + " Posicion genero del personaje: " + (player * 9));

                    // SI ES MUJER Y TIENE EL VELO PUESTO
                    if (DragsCustom.infoDragsCajaIII[player * 9] == 1)
                    {
                        // print("ES MUJER Y LLEVA PAÑUELO");


                        if (DragsCustom.infoDragsCajaIII[(8 + (player * 9))] == 1)
                        {
                            // CAPA PELO SE DESACTIVA Y SE ACTIVA CAPA PAÑUELO
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(false);
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(true);
                        }
                        else
                        {
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                            padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                        }
                    }
                    else
                    { // SI NO ES ASÍ...
                        // print("NO SE CUMPLE QUE SEA MUJER Y LLEVA PAÑUELO");

                        padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                        padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                    }

                    // LE APLICAS EL COLOR DEL PELO
                    padreCajasSelecc.GetChild(cajaSelecc).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = capas_prev[2].color;

                }
            }
        }

        for (int i = 0; i < 10; i++){
            if (PersonajesSelecc[i]){
                PersonajesSelecc[i] = false;
                padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
        #endregion  
    }

    #endregion

    public void Aplicar_DesdeGuardado(){

        for (int i = 0; i < 5; i++)
        {
            capas_prevDef[i] = capas_prev[i].sprite;
        }

        #region
        // CONTENIDO CAJA I
        for (int i = 0; i < 10; i++){
            if (DragsCustom.infoDragsVision_cajaI[i]){

                // GENERO
                if (DragsCustom.infoDragsCajaI[i * 9] == 0){

                    if (DragsCustom.infoDragsCajaI[1 + (i * 9)] == 0){
                        print("HOMBRE Player" + i + " frente");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.frontal_Pieles[DragsCustom.infoDragsCajaI[i * 9] + DragsCustom.infoDragsCajaI[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.frontal_Ropa[(6 * (DragsCustom.infoDragsCajaI[5 + (i * 9)])) + DragsCustom.infoDragsCajaI[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaI[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.frontal_Pelo [DragsCustom.infoDragsCajaI[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaI[4 + (i * 9)]];

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.frontal_Calzado[0];
                    }else{
                        print("HOMBRE Player " + i + " espalda");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.trasero_Pieles[DragsCustom.infoDragsCajaI[i * 9] + DragsCustom.infoDragsCajaI[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.trasero_Ropa[(6 * (DragsCustom.infoDragsCajaI[5 + (i * 9)])) + DragsCustom.infoDragsCajaI[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaI[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.trasero_Pelo[DragsCustom.infoDragsCajaI[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaI[4 + (i * 9)]];

                        // [3] ACCESORIO


                        // [4] CALZADO
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.trasero_Calzado[0];
                    }
                }
                else{

                    if (DragsCustom.infoDragsCajaI[8 + (i * 9)] == 1){
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(false);
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(true);
                    }else{
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                    }

                    if (DragsCustom.infoDragsCajaI[1 + (i * 9)] == 0){
                        print("MUJER Player + " + i + " frente");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.frontal_Pieles[1 + DragsCustom.infoDragsCajaI[i * 9] + DragsCustom.infoDragsCajaI[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.frontal_Ropa[12+(6 * (DragsCustom.infoDragsCajaI[5 + (i * 9)])) + DragsCustom.infoDragsCajaI[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaI[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.frontal_Pelo[2+DragsCustom.infoDragsCajaI[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaI[4 + (i * 9)]];

                        // [3] PANNUELO
                        if (DragsCustom.infoDragsCajaI[8 + (i * 9)] == 1){
                            padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(3).GetComponent<Image>().sprite = ArteDrags_.frontal_Pannuelo[0];
                        }

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.frontal_Calzado[1];

                    }else{
                        print("MUJER Player + " + i + " espalda");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.trasero_Pieles[1 + DragsCustom.infoDragsCajaI[i * 9] + DragsCustom.infoDragsCajaI[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.trasero_Ropa[12 + (6 * (DragsCustom.infoDragsCajaI[5 + (i * 9)])) + DragsCustom.infoDragsCajaI[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaI[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.trasero_Pelo[2 + DragsCustom.infoDragsCajaI[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaI[4 + (i * 9)]];

                        // [3] PANNUELO
                        if (DragsCustom.infoDragsCajaI[8 + (i * 9)] == 1){
                            padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(3).GetComponent<Image>().sprite = ArteDrags_.trasero_Pannuelo[0];
                        }

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(0).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.trasero_Calzado[1];

                    }
                }
            }

        }

        // CONTENIDO CAJA II
        for (int i = 0; i < 10; i++)
        {
            if (DragsCustom.infoDragsVision_cajaII[i])
            {

                // GENERO
                if (DragsCustom.infoDragsCajaII[i * 9] == 0)
                {

                    if (DragsCustom.infoDragsCajaII[1 + (i * 9)] == 0)
                    {
                        print("HOMBRE Player" + i + " frente");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.frontal_Pieles[DragsCustom.infoDragsCajaII[i * 9] + DragsCustom.infoDragsCajaII[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.frontal_Ropa[(6 * (DragsCustom.infoDragsCajaII[5 + (i * 9)])) + DragsCustom.infoDragsCajaII[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaII[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.frontal_Pelo[DragsCustom.infoDragsCajaII[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaII[4 + (i * 9)]];

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.frontal_Calzado[0];
                    }
                    else
                    {
                        print("HOMBRE Player " + i + " espalda");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.trasero_Pieles[DragsCustom.infoDragsCajaII[i * 9] + DragsCustom.infoDragsCajaII[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.trasero_Ropa[(6 * (DragsCustom.infoDragsCajaII[5 + (i * 9)])) + DragsCustom.infoDragsCajaII[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaII[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.trasero_Pelo[DragsCustom.infoDragsCajaII[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaII[4 + (i * 9)]];

                        // [3] ACCESORIO


                        // [4] CALZADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.trasero_Calzado[0];
                    }
                }
                else
                {

                    if (DragsCustom.infoDragsCajaII[8 + (i * 9)] == 1)
                    {
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(false);
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(true);
                    }
                    else
                    {
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                    }

                    if (DragsCustom.infoDragsCajaII[1 + (i * 9)] == 0)
                    {
                        print("MUJER Player + " + i + " frente");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.frontal_Pieles[1 + DragsCustom.infoDragsCajaII[i * 9] + DragsCustom.infoDragsCajaII[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.frontal_Ropa[12 + (6 * (DragsCustom.infoDragsCajaII[5 + (i * 9)])) + DragsCustom.infoDragsCajaII[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaII[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.frontal_Pelo[2 + DragsCustom.infoDragsCajaII[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaII[4 + (i * 9)]];

                        // [3] PANNUELO
                        if (DragsCustom.infoDragsCajaII[8 + (i * 9)] == 1)
                        {
                            padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(3).GetComponent<Image>().sprite = ArteDrags_.frontal_Pannuelo[0];
                        }

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.frontal_Calzado[1];

                    }
                    else
                    {
                        print("MUJER Player + " + i + " espalda");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.trasero_Pieles[1 + DragsCustom.infoDragsCajaII[i * 9] + DragsCustom.infoDragsCajaII[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.trasero_Ropa[12 + (6 * (DragsCustom.infoDragsCajaII[5 + (i * 9)])) + DragsCustom.infoDragsCajaII[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaII[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.trasero_Pelo[2 + DragsCustom.infoDragsCajaII[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaII[4 + (i * 9)]];

                        // [3] PANNUELO
                        if (DragsCustom.infoDragsCajaII[8 + (i * 9)] == 1)
                        {
                            padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(3).GetComponent<Image>().sprite = ArteDrags_.trasero_Pannuelo[0];
                        }

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.trasero_Calzado[1];

                    }
                }
            }

        }

        // CONTENIDO CAJA III
        for (int i = 0; i < 10; i++)
        {
            if (DragsCustom.infoDragsVision_cajaIII[i])
            {

                // GENERO
                if (DragsCustom.infoDragsCajaIII[i * 9] == 0)
                {

                    if (DragsCustom.infoDragsCajaIII[1 + (i * 9)] == 0)
                    {
                        print("HOMBRE Player" + i + " frente");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.frontal_Pieles[DragsCustom.infoDragsCajaIII[i * 9] + DragsCustom.infoDragsCajaIII[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.frontal_Ropa[(6 * (DragsCustom.infoDragsCajaIII[5 + (i * 9)])) + DragsCustom.infoDragsCajaIII[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaIII[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.frontal_Pelo[DragsCustom.infoDragsCajaIII[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaIII[4 + (i * 9)]];

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.frontal_Calzado[0];
                    }
                    else
                    {
                        print("HOMBRE Player " + i + " espalda");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.trasero_Pieles[DragsCustom.infoDragsCajaIII[i * 9] + DragsCustom.infoDragsCajaIII[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.trasero_Ropa[(6 * (DragsCustom.infoDragsCajaIII[5 + (i * 9)])) + DragsCustom.infoDragsCajaIII[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaIII[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.trasero_Pelo[DragsCustom.infoDragsCajaIII[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaIII[4 + (i * 9)]];

                        // [3] ACCESORIO


                        // [4] CALZADO
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.trasero_Calzado[0];
                    }
                }
                else
                {

                    if (DragsCustom.infoDragsCajaIII[8 + (i * 9)] == 1)
                    {
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(false);
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(true);
                    }
                    else
                    {
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(2).gameObject.SetActive(true);
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(3).gameObject.SetActive(false);
                    }

                    if (DragsCustom.infoDragsCajaIII[1 + (i * 9)] == 0)
                    {
                        print("MUJER Player + " + i + " frente");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.frontal_Pieles[1 + DragsCustom.infoDragsCajaIII[i * 9] + DragsCustom.infoDragsCajaIII[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.frontal_Ropa[12 + (6 * (DragsCustom.infoDragsCajaIII[5 + (i * 9)])) + DragsCustom.infoDragsCajaIII[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaIII[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.frontal_Pelo[2 + DragsCustom.infoDragsCajaIII[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaIII[4 + (i * 9)]];

                        // [3] PANNUELO
                        if (DragsCustom.infoDragsCajaIII[8 + (i * 9)] == 1)
                        {
                            padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(3).GetComponent<Image>().sprite = ArteDrags_.frontal_Pannuelo[0];
                        }

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(2).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.frontal_Calzado[1];

                    }
                    else
                    {
                        print("MUJER Player + " + i + " espalda");

                        // [0] PIEL
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(0).GetComponent<Image>().sprite = ArteDrags_.trasero_Pieles[1 + DragsCustom.infoDragsCajaII[i * 9] + DragsCustom.infoDragsCajaII[2 + (i * 9)]];

                        // [1] ROPA
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(1).GetComponent<Image>().sprite = ArteDrags_.trasero_Ropa[12 + (6 * (DragsCustom.infoDragsCajaII[5 + (i * 9)])) + DragsCustom.infoDragsCajaII[7 + (i * 9)] + (3 * DragsCustom.infoDragsCajaII[6 + (i * 9)])];

                        // [2] PEINADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().sprite = ArteDrags_.trasero_Pelo[2 + DragsCustom.infoDragsCajaII[3 + (i * 9)]];
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(2).GetComponent<Image>().color = ArteDrags_.frontal_PeloColor[DragsCustom.infoDragsCajaII[4 + (i * 9)]];

                        // [3] PANNUELO
                        if (DragsCustom.infoDragsCajaII[8 + (i * 9)] == 1)
                        {
                            padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(3).GetComponent<Image>().sprite = ArteDrags_.trasero_Pannuelo[0];
                        }

                        // [4] CALZADO
                        padreCajasSelecc.GetChild(1).GetChild(2).GetChild(i).GetChild(4).GetComponent<Image>().sprite = ArteDrags_.trasero_Calzado[1];

                    }
                }
            }

        }
        #endregion

        for (int i = 0; i < 10; i++){
            if (PersonajesSelecc[i]){
                PersonajesSelecc[i] = false;
                padreBotonesSelecc.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
    }


    // ****************************

    #region [0] GENERO [1] FRENTE/ESPALDA [2] RAZA [3] PELO [4] PELO_COLOR [5] FRONTAL_INFORMAL [6] ROPACOLOR
    //infoMaster_prev[0]
    public void Cambiar_Genero (){
        int num = infoMaster_prev[0];

        if (num == 0) {
            num = 1;
        } else {
            if (num == 1) {
                num = 0;
            }
        }

        infoMaster_prev[0] = num;
        //botonesCategorias[0].GetComponent<Image>().sprite = generoSimbolos[num];
    }

    //infoMaster_prev[1]
    public void Cambiar_Postura()
    {
        int num = infoMaster_prev[1];

        if (num == 0)
        {
            num = 1;
        }
        else
        {
            if (num == 1)
            {
                num = 0;
            }
        }

        infoMaster_prev[1] = num;
        //botonesCategorias[1].GetComponent<Image>().sprite = simboloPostura[num];
    }

    //infoMaster_prev[2]
    public void Cambiar_Raza()
    {
        int num = infoMaster_prev[2];

        if (num == 0)
        {
            num = 1;
        }
        else
        {
            if (num == 1)
            {
                num = 0;
            }
        }

        infoMaster_prev[2] = num;
        //botonesCategorias[3].GetComponent<Image>().color = razaColores[num];
    }

    //infoMaster_prev[2]
    public void Cambiar_EstiloVestimenta()
    {
        int num = infoMaster_prev[2];

        if (num == 0)
        {
            num = 1;
        }
        else
        {
            if (num == 1)
            {
                num = 0;
            }
        }

        infoMaster_prev[2] = num;
    }

    //infoMaster_prev[3]
    public void Cambiar_Pelo()
    {
        int num = infoMaster_prev[3];
        padreMiniaturasLuz[0].GetChild(num).GetComponent<Image>().color = Color.white;

        if (num == 0)
        {
            num = 1;
        }else{
           if (num == 1){
                num = 0;
            }
        }

        infoMaster_prev[3] = num;
        //padreMiniaturasLuz[0].GetChild(num).GetComponent<Image>().color = Color.cyan;
    }

    //infoMaster_prev[4]
    public void Cambiar_PeloColor()
    {
        int num = infoMaster_prev[4];
        padreMiniaturasLuz[1].GetChild(num).GetComponent<Image>().color = Color.white;

        if (num <= 3){
            num++;
        }else{
            num = 0;
        }

        infoMaster_prev[4] = num;
        //padreMiniaturasLuz[1].GetChild(num).GetComponent<Image>().color = Color.cyan;
        //capas_prev[2].color = ArteDrags_.frontal_PeloColor[num];
    }

    //infoMaster_prev[5]
    public void Cambiar_RopaTipo(){
        int num = infoMaster_prev[5];

        if (num == 0)
        {
            num = 1;
        }
        else
        {
            if (num == 1)
            {
                num = 0;
            }
        }

        infoMaster_prev[5] = num;
        //botonesCategorias[2].GetComponent<Text>().text = "" + letraEstiloRopa[num];
    }

    //infoMaster_prev[6]
    public void Cambiar_Ropa(){

        int num = infoMaster_prev[6];
        padreMiniaturasLuz[2].GetChild(num).GetComponent<Image>().color = Color.white;

        if (num == 1){
            num = 0;
        }
        else{
            num = 1;
        }

        infoMaster_prev[6] = num;
        //padreMiniaturasLuz[2].GetChild(num).GetComponent<Image>().color = Color.cyan;
    }

    //infoMaster_prev[7]
    public void Cambiar_RopaColor()
    {
        int num = infoMaster_prev[7];
        padreMiniaturasLuz[3].GetChild(num).GetComponent<Image>().color = Color.white;

        if (num <= 1){
            num ++;
        }else{
            num = 0;
        }

        infoMaster_prev[7] = num;
        //padreMiniaturasLuz[3].GetChild(num).GetComponent<Image>().color = Color.cyan;
    }

    public void Cambiar_Pannuel_OnOff(){

        int num = infoMaster_prev[8];

        if (num == 0) { num = 1; } else { if (num == 1) { num = 0; } }

        infoMaster_prev[8] = num;
        
        if (infoMaster_prev[8] == 0){
            capas_prev[3].gameObject.SetActive(false);
            capas_prev[2].gameObject.SetActive(true);
        }else{
            capas_prev[3].gameObject.SetActive(true);
            capas_prev[2].gameObject.SetActive(false);
        }
    }
    #endregion
    // ****************************

    #endregion

    #region CAMBIOS DE ARTE
    public void Aplicar_Arte (){

        #region
        // GENERO
        botonesCategorias[0].GetComponent<Image>().sprite = generoSimbolos[infoMaster_prev[0]];

        // POSTURA
        botonesCategorias[1].GetComponent<Image>().sprite = simboloPostura[infoMaster_prev[1]];

        // RAZA
        botonesCategorias[3].GetComponent<Image>().color = razaColores[infoMaster_prev[2]];

        // PELO
        padreMiniaturasLuz[0].GetChild(infoMaster_prev[3]).GetComponent<Image>().color = Color.cyan;

        // PELO COLOR
        padreMiniaturasLuz[1].GetChild(infoMaster_prev[4]).GetComponent<Image>().color = Color.cyan;
        capas_prev[2].color = ArteDrags_.frontal_PeloColor[infoMaster_prev[4]];

        // ROPA TIPO
        botonesCategorias[2].GetComponent<Text>().text = "" + letraEstiloRopa[infoMaster_prev[5]];

        //
        padreMiniaturasLuz[2].GetChild(infoMaster_prev[6]).GetComponent<Image>().color = Color.cyan;

        //
        padreMiniaturasLuz[3].GetChild(infoMaster_prev[7]).GetComponent<Image>().color = Color.cyan;

        // PANNUELO
        if (infoMaster_prev[8] == 0){
            capas_prev[3].gameObject.SetActive(false);
            capas_prev[2].gameObject.SetActive(true);
        }else{
            capas_prev[3].gameObject.SetActive(true);
            capas_prev[2].gameObject.SetActive(false);
        }

        #endregion

        int num = infoMaster_prev[0];
        int numRopa = infoMaster_prev[1];

        // SI ES HOMBRE...
        if (infoMaster_prev[0] == 0){

            // SE OCULTA EL PANNUELO
            if (botonPannuelo.gameObject.activeSelf){ botonPannuelo.gameObject.SetActive(false); 
            capas_prev[2].gameObject.SetActive(true);
            capas_prev[3].gameObject.SetActive(false);
            }

            // SI ESTÁ EN POSICION FRONTAL...
            if (infoMaster_prev[1] == 0){
            
                // APLICAS PIEL
                capas_prev[0].sprite = ArteDrags_.frontal_Pieles[num + infoMaster_prev[2]];

                // ROPA INFORMAL...
                if (infoMaster_prev[5] == 0){
                    //print("ROPA INFORMAL FRONTAL");

                    int final = numRopa + (infoMaster_prev[6] * 3) + infoMaster_prev[7];
                    capas_prev[1].sprite = ArteDrags_.frontal_Ropa[final];
                    //print(final);
                }else{ // ROPA FORMA...
                    //print("ROPA FORMAL FRONTAL");

                    int final = 6 + (numRopa + (infoMaster_prev[6] * 3) + infoMaster_prev[7]);
                    capas_prev[1].sprite = ArteDrags_.frontal_Ropa[final];
                    print(final);
                }

                // APLICAS PELO
                capas_prev[2].sprite = ArteDrags_.frontal_Pelo[num + infoMaster_prev[3]];

                // APLICAS CALZADO
                capas_prev[4].sprite = ArteDrags_.frontal_Calzado[num];
            }
            else
            { // SI ESTÁ EN POSICION ESPALDAS...

                // APLICAS PIEL...
                capas_prev[0].sprite = ArteDrags_.trasero_Pieles[num + infoMaster_prev[2]];

                // ROPA INFORMAL...
                if (infoMaster_prev[5] == 0)
                {
                    //print("ROPA INFORMAL ESPALDA");

                    int final = (infoMaster_prev[6] * 3) + infoMaster_prev[7];
                    capas_prev[1].sprite = ArteDrags_.trasero_Ropa[final];
                    print(final);
                }
                else
                { // ROPA FORMA...
                    //print("ROPA FORMAL ESPALDA");

                    int final = 5 + (numRopa + (infoMaster_prev[6] * 3) + infoMaster_prev[7]);
                    capas_prev[1].sprite = ArteDrags_.trasero_Ropa[final];
                    print(final);
                }

                // APLICAS PELO
                capas_prev[2].sprite = ArteDrags_.trasero_Pelo[num];

                // APLICAS COMPLEMENTO

                // APLICAS CALZADO
                capas_prev[4].sprite = ArteDrags_.trasero_Calzado[num];

            }
        }
        else{ // SI ES MUJER...

            // SE VISIBILIZA EL PANNUELO
            if (!botonPannuelo.gameObject.activeSelf){ botonPannuelo.gameObject.SetActive(true); }

            if (infoMaster_prev[8] == 1){
                capas_prev[2].gameObject.SetActive(false);
                capas_prev[3].gameObject.SetActive(true);
            }

            // SI ESTÁ EN POSICION FRONTAL...
            if (infoMaster_prev[1] == 0){

                // APLICAS PIEL...
                capas_prev[0].sprite = ArteDrags_.frontal_Pieles[num + infoMaster_prev[2] + 1];

                // ROPA INFORMAL...
                if (infoMaster_prev[5] == 0)
                {
                    print("ROPA INFORMAL");

                    int final = 12 + (numRopa + (infoMaster_prev[6] * 3) + infoMaster_prev[7]);
                    capas_prev[1].sprite = ArteDrags_.frontal_Ropa[final];
                    print(final);
                }
                else
                { // ROPA FORMA...
                    print("ROPA FORMAL");

                    int final = 18 + (numRopa + (infoMaster_prev[6] * 3) + infoMaster_prev[7]);
                    capas_prev[1].sprite = ArteDrags_.frontal_Ropa[final];
                    print(final);
                }

                // APLICAS PELO
                capas_prev[2].sprite = ArteDrags_.frontal_Pelo[num + infoMaster_prev[3] + 1];

                // APLICAS COMPLEMENTO
                capas_prev[3].sprite = ArteDrags_.frontal_Pannuelo[0];

                // APLICAS CALZADO
                capas_prev[4].sprite = ArteDrags_.frontal_Calzado[num];
            }
            else
            { // SI ESTÁ EN POSICION ESPALDAS...

                // APLICAS PIEL...
                capas_prev[0].sprite = ArteDrags_.trasero_Pieles[num + infoMaster_prev[2] + 1];

                // ROPA INFORMAL...
                if (infoMaster_prev[5] == 0)
                {
                    print("ROPA INFORMAL");

                    int final = 11 + (numRopa + (infoMaster_prev[6] * 3) + infoMaster_prev[7]);
                    capas_prev[1].sprite = ArteDrags_.trasero_Ropa[final];
                    print(final);
                }
                else
                { // ROPA FORMA...
                    print("ROPA FORMAL");

                    int final = 17 + (numRopa + (infoMaster_prev[6] * 3) + infoMaster_prev[7]);
                    capas_prev[1].sprite = ArteDrags_.trasero_Ropa[final];
                    print(final);
                }

                // APLICAS PELO
                capas_prev[2].sprite = ArteDrags_.trasero_Pelo[num + infoMaster_prev[3] + 1];

                // APLICAS COMPLEMENTO
                capas_prev[3].sprite = ArteDrags_.trasero_Pannuelo[0];

                // APLICAS CALZADO
                capas_prev[4].sprite = ArteDrags_.trasero_Calzado[num];
            }
        }
    }
    #endregion
}