using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* *****************************************
 * SCRIPT ENCARGADO DE AÑADIR/SELECCIONAR/ELIMINAR CUALQUIER CAMPO DE LAS HERRAMIENTAS
 * 
 * 
 * *****************************************/

public class Herramientas_Add : MonoBehaviour {

    #region VARIABLES (SEPARADAS POR HERRAMIENTA)
    [Header("Analisis Implicados")]
    public GameObject prefab_AnalisisImplicados;
    public Transform contenido_AnalisisImplicados;
    public int TotalAnalisisImplicados;
    [HideInInspector] public List<GameObject> AnalisisImplicados_Lista;

    [Header("Analisis Riesgo")]
    public GameObject prefab_AnalisisRiesgo;
    public Transform contenido_AnalisisRiesgo;

    [Header("Gantt Okapi")]
    public GameObject prefab_Gantt;
    public Transform contenido_Gantt;
    public int TotalGanttOkapi;

    public GameObject prefab_tiempoGantt;
    public Transform contenido_Gantt_tiempo;
    private int aux;

    public List<GameObject> Gantt_campos;
    public List<GameObject> Gantt_camposTiempo;

    [Header("Plan Comunicación")]
    public List<GameObject> planComunicacion_Lista;
    public int TotalPlanComunicacion;
    public GameObject prefab_PlanComun;
    public Transform contenido_PlanComun;

    [Header("Publico Objetivo: EMPRESA I")]
    public List<GameObject> panelesGraficasEmpresa;
    public Transform contenido_PlanEmpresa;
    public List<InputField> CamposI;
    public Text resultadoI;
    public float resultadoFinalI;

    public Transform graficaEmpresaI;

    public List<InputField> CamposII;
    public Text resultadoII;
    public float resultadoFinalII;

    public Transform graficaEmpresaII;

    [Header("*********************************")]
    [Header("Publico Objetivo: POBLACIÓN x1")]
    public List<InputField> porcentajesPoblacionI;
    public List<InputField> camposPoblacionI;
    public List<Text> result_PoblacionI;

    public Transform[] graficasPoblacionI = new Transform[3];
    public List<GameObject> panelesGraficas;

    public List<InputField> conceptosSoluciones;
    public List<Text> conceptosTitutlosTextos;

    [Header("Publico Objetivo: POBLACIÓN x2.1")]
    public List<InputField> porcentajesPoblacionII;
    public List<InputField> camposPoblacionII;
    public List<Text> result_PoblacionII;

    public Transform[] graficasPoblacionII = new Transform[3];
    public List<GameObject> panelesGraficasII;
    public List<Text> conceptosTitutlosTextosII;

    [Header("Publico Objetivo: POBLACIÓN x2.2")]
    public List<InputField> porcentajesPoblacionIII;
    public List<InputField> camposPoblacionIII;
    public List<Text> result_PoblacionIII;

    public Transform[] graficasPoblacionIII = new Transform[3];
    public List<GameObject> panelesGraficasIII;
    public List<Text> conceptosTitutlosTextosIII;
    [Header("*********************************")]

    [Header("Linea Temporal")]
    public GameObject prefab_LineaTemporal;
    public Transform contenido_LineaTemporal;

    public Image comInterna;
    public Image comExterna;
    public GameObject comInterna_gameob;
    public GameObject comExterna_gameob;

    [Space (5)]
    public List<GameObject> listaBloques;
    #endregion

    #region FUNCIONES
    #region ANALISIS RIESGO
    public void AnalisisRiesgo_Add() {
        Instantiate(prefab_AnalisisRiesgo, contenido_AnalisisRiesgo);

        if (contenido_AnalisisRiesgo.transform.childCount > 6) {
            contenido_AnalisisRiesgo.GetComponent<RectTransform>().sizeDelta += Vector2.up * 75.0f;
            print("añades uno ");
        }
    }

    public void AnalisisRiesgo_Remove() {

        for (int i = 0; i < contenido_AnalisisRiesgo.transform.childCount; i++) {

            if (contenido_AnalisisRiesgo.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color == Color.cyan) {
                Destroy(contenido_AnalisisRiesgo.transform.GetChild(i).gameObject);

                if (contenido_AnalisisRiesgo.transform.childCount > 6 && contenido_AnalisisRiesgo.transform.GetComponent<RectTransform>().sizeDelta.y > 475.0f) {
                    contenido_AnalisisRiesgo.GetComponent<RectTransform>().sizeDelta -= Vector2.up * 75.0f;
                }
            }
        }
    }
    #endregion

    #region GANTT
    public void Gantt_Add() {


        GameObject auxI = Instantiate(prefab_Gantt, contenido_Gantt) as GameObject;
        GameObject auxII = Instantiate(prefab_tiempoGantt, contenido_Gantt_tiempo) as GameObject;

        Gantt_campos.Add(auxI.gameObject);
        Gantt_camposTiempo.Add(auxII.gameObject);

        auxI.transform.GetChild(0).GetComponent<Text>().text = contenido_Gantt_tiempo.transform.childCount + "";
        auxII.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = contenido_Gantt_tiempo.transform.childCount + "";

        auxII.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, auxII.transform.GetComponent<RectTransform>().anchoredPosition3D.y + (-70.0f * TotalGanttOkapi), 0);
        TotalGanttOkapi++;

        if (contenido_Gantt.transform.childCount > 6) {
            contenido_Gantt.GetComponent<RectTransform>().sizeDelta += Vector2.up * 80.0f;
            contenido_Gantt_tiempo.GetComponent<RectTransform>().sizeDelta += Vector2.up * 70;
        }
    }
    public void Gantt_Remove() {
        for (int i = 0; i < contenido_Gantt.transform.childCount; i++) {
            if (contenido_Gantt.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color == Color.cyan) {

                Destroy(contenido_Gantt.transform.GetChild(i).gameObject);
                Destroy(contenido_Gantt_tiempo.transform.GetChild(i).gameObject);
                TotalGanttOkapi--;

                Gantt_campos.Remove(contenido_Gantt.transform.GetChild(i).gameObject);
                Gantt_camposTiempo.Remove(contenido_Gantt_tiempo.transform.GetChild(i).gameObject);


                if (contenido_Gantt.transform.childCount > 6 && contenido_Gantt.transform.GetComponent<RectTransform>().sizeDelta.y > 500.0f) {
                    contenido_Gantt.GetComponent<RectTransform>().sizeDelta -= Vector2.up * 80.0f;
                    contenido_Gantt_tiempo.GetComponent<RectTransform>().sizeDelta -= Vector2.up * 70;
                }
            }
        }

        for (int i = 0; i < Gantt_camposTiempo.Count; i++) {
            //Gantt_camposTiempo[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 205.0f - (70.0f * i), 0);
            Gantt_camposTiempo[i].transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + (i + 1);
            Gantt_campos[i].transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
        }
    }
    #endregion

    #region PLAN COMUNICACIÓN IMPLICADOS
    public void PlanComunicacion_Add() {
        GameObject aux = Instantiate(prefab_PlanComun, contenido_PlanComun) as GameObject;
        planComunicacion_Lista.Add(aux);

        aux.transform.GetChild(0).GetComponent<Text>().text = contenido_PlanComun.transform.childCount + "";
        TotalPlanComunicacion++;

        // AJUSTE DE LA LONGITUD DEL CONTENIDO AL AÑADIR UNO
        if (contenido_PlanComun.transform.childCount > 6) {
            contenido_PlanComun.GetComponent<RectTransform>().sizeDelta += Vector2.up * 100.0f;
            contenido_PlanComun.GetComponent<RectTransform>().anchoredPosition += Vector2.up * 100.0f;
        }
    }

    public void PlanComunicacion_Remove() {
        for (int i = 0; i < contenido_PlanComun.transform.childCount; i++) {
            if (contenido_PlanComun.transform.GetChild(i).GetComponent<Image>().color == Color.cyan) {

                planComunicacion_Lista.Remove(contenido_PlanComun.transform.GetChild(i).gameObject);
                Destroy(contenido_PlanComun.transform.GetChild(i).gameObject);
                TotalPlanComunicacion--;

                if (contenido_PlanComun.transform.childCount > 6 && contenido_PlanComun.transform.GetComponent<RectTransform>().sizeDelta.y > 600.0f) {
                    contenido_PlanComun.GetComponent<RectTransform>().sizeDelta -= Vector2.up * 75.0f;
                }
            }
        }

        for (int i = 0; i < TotalPlanComunicacion; i++) {
            planComunicacion_Lista[i].transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
        }
    }
    #endregion

    #region PUBLICO OBJETIVO: EMPRESA
    public void PublicoObjetivoEmpresaI(int num) {

        float aux = 0;
        for (int i = 0; i < 10; i++) {
            if (CamposI[i].text != "") {
                aux += float.Parse(CamposI[i].text);
                resultadoFinalI = aux;
                resultadoI.text = resultadoFinalI + "";
                print(aux);
            }
        }
    }

    public void PublicoObjetivoEmpresaII(int num) {

        float aux = 0;
        for (int i = 0; i < 10; i++) {
            if (CamposII[i].text != "") {
                aux += float.Parse(CamposII[i].text);
                resultadoFinalII = aux;
                resultadoII.text = resultadoFinalII + "";
            }
        }
    }
    #endregion

    #region
    public void GestorPublico_Poblacion(Transform plantillaII){

        if (plantillaII.GetChild(0).gameObject.activeSelf){
            plantillaII.GetChild(0).gameObject.SetActive(false);
            plantillaII.GetChild(1).gameObject.SetActive(true);
        }else{
            plantillaII.GetChild(0).gameObject.SetActive(true);
            plantillaII.GetChild(1).gameObject.SetActive(false);
        }
    }

    #region PUBLICO OBJETIVO: POBLACION
    public void PublicoObjetivoPoblacionI(int num) {

        float aux = 0;

        for (int i = 0; i < 9; i++){

            if (num == i){
                if (num <= 2){
                    print("tocas primera gráfica");

                    for (int u = 0; u < 3; u++){
                        if (porcentajesPoblacionI[u].text != ""){
                            aux += float.Parse(porcentajesPoblacionI[u].text);
                            result_PoblacionI[0].text = aux + "";
                        }
                    }
                }

                if (num >= 3 && num <= 5){
                    print("tocas segunda gráfica");

                    for (int u = 3; u < 6; u++){
                        if (porcentajesPoblacionI[u].text != ""){
                            aux += float.Parse(porcentajesPoblacionI[u].text);
                            result_PoblacionI[1].text = aux + "";
                        }
                    }
                }

                if (num >= 6){
                    print("tocas tercera gráfica");

                    for (int u = 6; u < 9; u++){
                        if (porcentajesPoblacionI[u].text != ""){
                            aux += float.Parse(porcentajesPoblacionI[u].text);
                            result_PoblacionI[2].text = aux + "";
                        }
                    }
                }
            }
        }
    }
    public void PublicoObjetivoPoblacionII(int num)
    {

        float aux = 0;

        for (int i = 0; i < 9; i++)
        {

            if (num == i)
            {
                if (num <= 2)
                {
                    print("tocas primera gráfica");

                    for (int u = 0; u < 3; u++)
                    {
                        if (porcentajesPoblacionII[u].text != "")
                        {
                            aux += float.Parse(porcentajesPoblacionII[u].text);
                            result_PoblacionII[0].text = aux + "";
                        }
                    }
                }

                if (num >= 3 && num <= 5)
                {
                    print("tocas segunda gráfica");

                    for (int u = 3; u < 6; u++)
                    {
                        if (porcentajesPoblacionII[u].text != "")
                        {
                            aux += float.Parse(porcentajesPoblacionII[u].text);
                            result_PoblacionII[1].text = aux + "";
                        }
                    }
                }

                if (num >= 6)
                {
                    print("tocas tercera gráfica");

                    for (int u = 6; u < 9; u++)
                    {
                        if (porcentajesPoblacionII[u].text != "")
                        {
                            aux += float.Parse(porcentajesPoblacionII[u].text);
                            result_PoblacionII[2].text = aux + "";
                        }
                    }
                }
            }
        }
    }
    public void PublicoObjetivoPoblacionIII(int num)
    {

        float aux = 0;

        for (int i = 0; i < 9; i++)
        {

            if (num == i)
            {
                if (num <= 2)
                {
                    print("tocas primera gráfica");

                    for (int u = 0; u < 3; u++)
                    {
                        if (porcentajesPoblacionIII[u].text != "")
                        {
                            aux += float.Parse(porcentajesPoblacionIII[u].text);
                            result_PoblacionIII[0].text = aux + "";
                        }
                    }
                }

                if (num >= 3 && num <= 5)
                {
                    print("tocas segunda gráfica");

                    for (int u = 3; u < 6; u++)
                    {
                        if (porcentajesPoblacionIII[u].text != "")
                        {
                            aux += float.Parse(porcentajesPoblacionIII[u].text);
                            result_PoblacionIII[1].text = aux + "";
                        }
                    }
                }

                if (num >= 6)
                {
                    print("tocas tercera gráfica");

                    for (int u = 6; u < 9; u++)
                    {
                        if (porcentajesPoblacionIII[u].text != "")
                        {
                            aux += float.Parse(porcentajesPoblacionIII[u].text);
                            result_PoblacionIII[2].text = aux + "";
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region PUBLICO OBJETIVO: GRAFICAS
    public void GraficasEmpresa() {

        if (panelesGraficasEmpresa[0].activeSelf) {
            #region EMPRESA I
            for (int i = 0; i < 10; i++)
            {
                if (CamposI[i].text != "")
                    graficaEmpresaI.transform.GetChild(i).GetComponent<Image>().fillAmount = 0.01f * float.Parse(CamposI[i].GetComponent<InputField>().text);
            }

            graficaEmpresaI.transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficaEmpresaI.transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(1).GetComponent<Image>().fillAmount)));
            graficaEmpresaI.transform.GetChild(3).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(2).GetComponent<Image>().fillAmount)));
            graficaEmpresaI.transform.GetChild(4).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(3).GetComponent<Image>().fillAmount)));
            graficaEmpresaI.transform.GetChild(5).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(4).GetComponent<Image>().fillAmount)));
            graficaEmpresaI.transform.GetChild(6).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(4).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(5).GetComponent<Image>().fillAmount)));
            graficaEmpresaI.transform.GetChild(7).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(4).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(5).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(6).GetComponent<Image>().fillAmount)));
            graficaEmpresaI.transform.GetChild(8).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(4).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(5).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(6).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(7).GetComponent<Image>().fillAmount)));
            graficaEmpresaI.transform.GetChild(9).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaI.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(4).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(5).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(6).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(7).GetComponent<Image>().fillAmount + graficaEmpresaI.transform.GetChild(8).GetComponent<Image>().fillAmount)));
            #endregion

            #region EMPRESA II
            for (int i = 0; i < 10; i++)
            {
                if (CamposII[i].text != "")
                    graficaEmpresaII.transform.GetChild(i).GetComponent<Image>().fillAmount = 0.01f * float.Parse(CamposII[i].GetComponent<InputField>().text);
            }

            graficaEmpresaII.transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficaEmpresaII.transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(1).GetComponent<Image>().fillAmount)));
            graficaEmpresaII.transform.GetChild(3).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(2).GetComponent<Image>().fillAmount)));
            graficaEmpresaII.transform.GetChild(4).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(3).GetComponent<Image>().fillAmount)));
            graficaEmpresaII.transform.GetChild(5).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(4).GetComponent<Image>().fillAmount)));
            graficaEmpresaII.transform.GetChild(6).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(4).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(5).GetComponent<Image>().fillAmount)));
            graficaEmpresaII.transform.GetChild(7).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(4).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(5).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(6).GetComponent<Image>().fillAmount)));
            graficaEmpresaII.transform.GetChild(8).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(4).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(5).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(6).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(7).GetComponent<Image>().fillAmount)));
            graficaEmpresaII.transform.GetChild(9).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficaEmpresaII.transform.GetChild(0).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(1).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(2).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(3).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(4).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(5).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(6).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(7).GetComponent<Image>().fillAmount + graficaEmpresaII.transform.GetChild(8).GetComponent<Image>().fillAmount)));

            #endregion

            panelesGraficasEmpresa[0].SetActive(false);
            panelesGraficasEmpresa[1].SetActive(true);
        }
        else {
            panelesGraficasEmpresa[0].SetActive(true);
            panelesGraficasEmpresa[1].SetActive(false);
        }
    }

    public void GraficasPoblacion() {

        if (!panelesGraficas[1].activeSelf) {

            for (int u = 0; u < 3; u++){
                if (porcentajesPoblacionI[u].text != (""))
                    graficasPoblacionI[0].transform.GetChild(u).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionI[u].GetComponent<InputField>().text);
            }

            graficasPoblacionI[0].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionI[0].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionI[0].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionI[0].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionI[0].transform.GetChild(1).GetComponent<Image>().fillAmount)));
                    
            for (int u = 3; u < 6; u++){
                if (porcentajesPoblacionI[u].text != (""))
                graficasPoblacionI[1].transform.GetChild(u-3).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionI[u].GetComponent<InputField>().text);
            }

            graficasPoblacionI[1].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionI[1].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionI[1].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionI[1].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionI[1].transform.GetChild(1).GetComponent<Image>().fillAmount)));

            for (int u = 6; u < 9; u++){
                if (porcentajesPoblacionI[u].text != (""))
                graficasPoblacionI[2].transform.GetChild(u-6).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionI[u].GetComponent<InputField>().text);
            }

            graficasPoblacionI[2].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionI[2].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionI[2].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionI[2].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionI[2].transform.GetChild(1).GetComponent<Image>().fillAmount)));

            conceptosTitutlosTextos[0].text = camposPoblacionI[3].text;
            conceptosTitutlosTextos[1].text = camposPoblacionI[5].text;
            conceptosTitutlosTextos[2].text = camposPoblacionI[7].text;
            conceptosTitutlosTextos[3].text = "GRÁFICA PÚBLICO OBJETIVO: " + camposPoblacionI[1].text;

            panelesGraficas[0].SetActive(false);
            panelesGraficas[1].SetActive(true);
        } else {
            panelesGraficas[0].SetActive(true);
            panelesGraficas[1].SetActive(false);
        }
    }
    public void GraficasPoblacionII(){
        if (!panelesGraficasII[1].activeSelf){

            for (int u = 0; u < 3; u++){
                if (porcentajesPoblacionI[u].text != (""))
                    graficasPoblacionII[0].transform.GetChild(u).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionII[u].GetComponent<InputField>().text);
            }

            graficasPoblacionII[0].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionII[0].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionII[0].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionII[0].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionII[0].transform.GetChild(1).GetComponent<Image>().fillAmount)));

            for (int u = 3; u < 6; u++)
            {
                if (porcentajesPoblacionI[u].text != (""))
                    graficasPoblacionII[1].transform.GetChild(u - 3).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionII[u].GetComponent<InputField>().text);
            }

            graficasPoblacionII[1].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionII[1].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionII[1].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionII[1].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionII[1].transform.GetChild(1).GetComponent<Image>().fillAmount)));

            for (int u = 6; u < 9; u++)
            {
                if (porcentajesPoblacionI[u].text != (""))
                    graficasPoblacionII[2].transform.GetChild(u - 6).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionII[u].GetComponent<InputField>().text);
            }

            graficasPoblacionII[2].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionII[2].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionII[2].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionII[2].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionII[2].transform.GetChild(1).GetComponent<Image>().fillAmount)));

            conceptosTitutlosTextosII[0].text = camposPoblacionII[3].text;
            conceptosTitutlosTextosII[1].text = camposPoblacionII[5].text;
            conceptosTitutlosTextosII[2].text = camposPoblacionII[7].text;
            conceptosTitutlosTextosII[3].text = "GRÁFICA PÚBLICO OBJETIVO: " + camposPoblacionII[1].text;

            panelesGraficasII[0].SetActive(false);
            panelesGraficasII[1].SetActive(true);
        }
        else
        {
            panelesGraficasII[0].SetActive(true);
            panelesGraficasII[1].SetActive(false);
        }
    }
    public void GraficasPoblacionIII()
    {
        if (!panelesGraficasIII[1].activeSelf)
        {

            for (int u = 0; u < 3; u++)
            {
                if (porcentajesPoblacionI[u].text != (""))
                    graficasPoblacionIII[0].transform.GetChild(u).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionIII[u].GetComponent<InputField>().text);
            }

            graficasPoblacionIII[0].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionIII[0].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionIII[0].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionIII[0].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionIII[0].transform.GetChild(1).GetComponent<Image>().fillAmount)));

            for (int u = 3; u < 6; u++)
            {
                if (porcentajesPoblacionI[u].text != (""))
                    graficasPoblacionIII[1].transform.GetChild(u - 3).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionIII[u].GetComponent<InputField>().text);
            }

            graficasPoblacionIII[1].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionIII[1].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionIII[1].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionIII[1].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionIII[1].transform.GetChild(1).GetComponent<Image>().fillAmount)));

            for (int u = 6; u < 9; u++)
            {
                if (porcentajesPoblacionI[u].text != (""))
                    graficasPoblacionIII[2].transform.GetChild(u - 6).GetComponent<Image>().fillAmount = 0.01f * float.Parse(porcentajesPoblacionIII[u].GetComponent<InputField>().text);
            }

            graficasPoblacionIII[2].transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - graficasPoblacionIII[2].transform.GetChild(0).GetComponent<Image>().fillAmount));
            graficasPoblacionIII[2].transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (graficasPoblacionIII[2].transform.GetChild(0).GetComponent<Image>().fillAmount + graficasPoblacionIII[2].transform.GetChild(1).GetComponent<Image>().fillAmount)));

            conceptosTitutlosTextosIII[0].text = camposPoblacionIII[3].text;
            conceptosTitutlosTextosIII[1].text = camposPoblacionIII[5].text;
            conceptosTitutlosTextosIII[2].text = camposPoblacionIII[7].text;
            conceptosTitutlosTextosIII[3].text = "GRÁFICA PÚBLICO OBJETIVO: " + camposPoblacionIII[1].text;

            panelesGraficasIII[0].SetActive(false);
            panelesGraficasIII[1].SetActive(true);
        }
        else
        {
            panelesGraficasIII[0].SetActive(true);
            panelesGraficasIII[1].SetActive(false);
        }
    }
    #endregion
    #endregion

    #region ANALISIS IMPLICADOS
    public void AnalisisImplicados_Add() {
        GameObject aux = Instantiate(prefab_AnalisisImplicados, contenido_AnalisisImplicados) as GameObject;
        AnalisisImplicados_Lista.Add(aux);

        aux.transform.GetChild(0).GetComponent<Text>().text = contenido_AnalisisImplicados.transform.childCount + "";
        TotalAnalisisImplicados++;

        if (contenido_AnalisisImplicados.transform.childCount > 6) {
            contenido_AnalisisImplicados.GetComponent<RectTransform>().sizeDelta += new Vector2 (0, 90);
            //contenido_AnalisisImplicados.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 95);
        } else {
            if (contenido_AnalisisImplicados.transform.GetComponent<RectTransform>().sizeDelta.y != 610)
                contenido_AnalisisImplicados.transform.GetComponent<RectTransform>().sizeDelta = new Vector2 (contenido_AnalisisImplicados.transform.GetComponent<RectTransform>().sizeDelta.x, 610);
        }
    }

    public void AnalisisImplicados_Remove() {

        for (int i = 0; i < contenido_AnalisisImplicados.transform.childCount; i++) {
            if (contenido_AnalisisImplicados.transform.GetChild(i).GetComponent<Image>().color == Color.cyan) {

                AnalisisImplicados_Lista.Remove(contenido_AnalisisImplicados.transform.GetChild(i).gameObject);
                Destroy(contenido_AnalisisImplicados.transform.GetChild(i).gameObject);
                TotalAnalisisImplicados--;

                if (contenido_AnalisisImplicados.transform.childCount > 6 && contenido_AnalisisImplicados.transform.GetComponent<RectTransform>().sizeDelta.y > 610) {
                    contenido_AnalisisImplicados.GetComponent<RectTransform>().sizeDelta -= Vector2.up * 90;

                    if (contenido_AnalisisImplicados.GetComponent<RectTransform>().anchoredPosition.y > 90) {
                        contenido_AnalisisImplicados.GetComponent<RectTransform>().anchoredPosition -= Vector2.up * 90;
                    }
                }
            }
        }

        for (int i = 0; i < TotalAnalisisImplicados; i++) {
            AnalisisImplicados_Lista[i].transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
        }
    }
    #endregion

    public string[] conceptos_AnalisisImplicados = new string[8];

    public void AnalisisImplicados_Conceptos(int num) {
        conceptosTitutlosTextos[num].text = conceptosSoluciones[num].text;
    }

    #region LINEA TEMPORAL

    public int num = 0;
    public void LineaTemporal_Add() {
        GameObject aux = Instantiate(prefab_LineaTemporal, contenido_LineaTemporal) as GameObject;

        listaBloques.Add(aux.gameObject);
        aux.gameObject.transform.GetChild(2).GetComponent<Text>().text = "" + (num + 1);
        aux.gameObject.GetComponent<BloqueLineaTemporal>().miniatura_I.GetComponent<InputField>().text = "" + (num + 1);
        aux.GetComponent<BloqueLineaTemporal>().miniatura_II.GetComponent<InputField>().text = "" + (num + 1);

        aux.GetComponent<BloqueLineaTemporal>().posHijoInicial = num;
        aux.transform.SetSiblingIndex(num);
        aux.gameObject.name = "BloqueLT_" + (num + 1);
        aux.gameObject.GetComponent<BloqueLineaTemporal>().miniatura_I.name = "MiniaturaLT_Izq_" + (num + 1);
        aux.gameObject.GetComponent<BloqueLineaTemporal>().miniatura_II.name = "MiniaturaLT_Der_" + (num + 1);

        num++;

        // SE AUTOAJUSTA LA LONGITUD TENIENDO EN CUENTA EL NUMERO DE HIJOS, SU SEPARACIÓN Y EL OFFSET DESDE ARRIBA
        if (contenido_LineaTemporal.childCount > 12 && (contenido_LineaTemporal.childCount - 1) % 3 == 1) {
            contenido_LineaTemporal.GetComponent<RectTransform>().sizeDelta = Vector2.up * (
            (contenido_LineaTemporal.GetComponent<GridLayoutGroup>().cellSize.y * (contenido_LineaTemporal.childCount - 1) / 3) +
            (contenido_LineaTemporal.GetComponent<GridLayoutGroup>().spacing.y * (contenido_LineaTemporal.childCount - 1)));
        }
    }

    public void LineaTemporal_Update(){
        for (int i = 0; i < listaBloques.Count; i++){
            listaBloques[i].transform.SetSiblingIndex(i);
            listaBloques[i].transform.GetChild(3).GetComponent<Text>().text = "" + (i + 1);
            listaBloques[i].GetComponent<BloqueLineaTemporal>().miniatura_I.GetComponent<InputField>().text = "" + (i + 1);
            listaBloques[i].GetComponent<BloqueLineaTemporal>().miniatura_II.GetComponent<InputField>().text = "" + (i + 1);
        }

        if (listaBloques.Count < 12 && contenido_LineaTemporal.GetComponent<RectTransform>().sizeDelta.y != 400){
            contenido_LineaTemporal.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 400);
        }
    }

    public void ComInternaOnOff(){
        if (!comInterna_gameob.activeSelf){
            comInterna_gameob.SetActive(true);
            comInterna.color = Color.cyan;
        }else{
            comInterna_gameob.SetActive(false);
            comInterna.color = Color.white;
        }
    }
    public void ComExternaOnOff(){
        if (!comExterna_gameob.activeSelf){
            comExterna_gameob.SetActive(true);
            comExterna.color = Color.cyan;
        }else{
            comExterna_gameob.SetActive(false);
            comExterna.color = Color.white;
        }
    }

    public bool modoPapeleraLineaTemporal;
    public Image imagenPapelera;

    public void LineaTemporalPapelera(){
        modoPapeleraLineaTemporal = !modoPapeleraLineaTemporal;

        if (modoPapeleraLineaTemporal) imagenPapelera.color = Color.cyan;
        else imagenPapelera.color = Color.white;
    }
    #endregion

    #endregion
}