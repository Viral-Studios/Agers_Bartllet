using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Grafica : MonoBehaviour {

    private float velocidad = 1;
    public bool adelante;

    [Header ("Variables Calculadora")]
    public InputField retornoCalculadora;
    public List<Text> botonesCalculadora;

    public List<GameObject> SegmentosGraficas;

    [Header("Segmento Socio-Economico")]
    public int segmentoSelecc_SocioEconomico;

    public List<Image> basesGrafica_SocioEconomico;
    public List<Image> barrasSegmento_Socioeconom;

    public List<float> barrasValores_SocioEconomico;
    public List<bool> checkSegmento_SocioEconomico;

    [Header("Segmento Demográfico")]
    public int segmentoSelecc_Demografico;

    public List<Image> basesGrafica_Demografico;
    public List<Image> barrasSegmento_Demografico;

    public List<float> barrasValores_Demografico;
    public List<bool> checkSegmento_Demografico;

    [Header("Segmento Edades")]
    public int segmentoSelecc_Edades;

    public List<Image> basesGrafica_Edades;
    public List<Image> barrasSegmento_Edades;

    public List<float> barrasValores_Edades;
    public List<bool> checkSegmento_Edades;
    #region

    public void BotonesCalcu (int num) {

        for (int i = 0; i < botonesCalculadora.Count; i++){

            if (i == num && i < 10) {
                retornoCalculadora.text += botonesCalculadora[i].text;
            }
        }

        // DELETE...
        if (num == 10 && retornoCalculadora.text.Length > 0){
            retornoCalculadora.text = retornoCalculadora.text.Substring(0, retornoCalculadora.text.Length - 1);
        }
        
        // ENTER...
        if (num == 11){

            float valorfinal = float.Parse(retornoCalculadora.text) / 100;
            adelante = true;

            if (SegmentosGraficas[3].activeSelf){
                barrasValores_SocioEconomico[segmentoSelecc_SocioEconomico] = valorfinal;
                barrasValores_SocioEconomico[segmentoSelecc_SocioEconomico + 3] = 1 - valorfinal;
            }

            if (SegmentosGraficas[4].activeSelf){
                barrasValores_Demografico[segmentoSelecc_Demografico] = valorfinal;
                barrasValores_Demografico[segmentoSelecc_Demografico + 2] = 1 - valorfinal;
            }

            if (SegmentosGraficas[5].activeSelf){
                barrasValores_Edades[segmentoSelecc_Edades] = valorfinal;
                barrasValores_Edades[segmentoSelecc_Edades + 4] = 1 - valorfinal;
            }
        }
    }

    public void SeleccGraficaSeg (int num){

        for (int i = 0; i < 3; i++){
            if (num == i){

                if (SegmentosGraficas[i].GetComponent<Image>().color == Color.white){
                    SegmentosGraficas[i].GetComponent<Image>().color = Color.cyan;
                    SegmentosGraficas[i + 3].SetActive(true);
                }
            }else{
                SegmentosGraficas[i].GetComponent<Image>().color = Color.white;
                SegmentosGraficas[i + 3].SetActive(false);
            }
        }
    }

    public void SeleccCalcu(int num){

        // Limpia el retorno de la calculadora cuando tiene datos
        if (retornoCalculadora.text != "") retornoCalculadora.text = "";

        // FLECHA IZQUIERDA...
        if (num == 0){

            if (SegmentosGraficas[3].activeSelf){
                if (segmentoSelecc_SocioEconomico == 0) segmentoSelecc_SocioEconomico = 2;
                else segmentoSelecc_SocioEconomico--;

                for (int i = 0; i < basesGrafica_SocioEconomico.Count; i++)
                {
                    if (i == segmentoSelecc_SocioEconomico) basesGrafica_SocioEconomico[segmentoSelecc_SocioEconomico].color = Color.cyan;
                    else basesGrafica_SocioEconomico[i].color = Color.white;
                }
            }

            if (SegmentosGraficas[4].activeSelf){
                if (segmentoSelecc_Demografico == 0) segmentoSelecc_Demografico = 1;
                else segmentoSelecc_Demografico--;

                for (int i = 0; i < basesGrafica_Demografico.Count; i++)
                {
                    if (i == segmentoSelecc_Demografico) basesGrafica_Demografico[segmentoSelecc_Demografico].color = Color.cyan;
                    else basesGrafica_Demografico[i].color = Color.white;
                }
            }

            if (SegmentosGraficas[5].activeSelf){
                if (segmentoSelecc_Edades == 0) segmentoSelecc_Edades = 3;
                else segmentoSelecc_Edades--;

                for (int i = 0; i < basesGrafica_Edades.Count; i++){
                    if (i == segmentoSelecc_Edades) basesGrafica_Edades[segmentoSelecc_Edades].color = Color.cyan;
                    else basesGrafica_Edades[i].color = Color.white;
                }
            }
        }

        // FLECHA DERECHA...
        if (num == 1){

            if (SegmentosGraficas[3].activeSelf){
                if (segmentoSelecc_SocioEconomico == 2) segmentoSelecc_SocioEconomico = 0;
                else segmentoSelecc_SocioEconomico++;

                for (int i = 0; i < basesGrafica_SocioEconomico.Count; i++){
                    if (i == segmentoSelecc_SocioEconomico) basesGrafica_SocioEconomico[segmentoSelecc_SocioEconomico].color = Color.cyan;
                    else basesGrafica_SocioEconomico[i].color = Color.white;
                }
            }

            if (SegmentosGraficas[4].activeSelf){
                if (segmentoSelecc_Demografico == 1) segmentoSelecc_Demografico = 0;
                else segmentoSelecc_Demografico++;

                for (int i = 0; i < basesGrafica_Demografico.Count; i++){
                    if (i == segmentoSelecc_Demografico) basesGrafica_Demografico[segmentoSelecc_Demografico].color = Color.cyan;
                    else basesGrafica_Demografico[i].color = Color.white;
                }
            }

            if (SegmentosGraficas[5].activeSelf){
                if (segmentoSelecc_Edades == 3) segmentoSelecc_Edades = 0;
                else segmentoSelecc_Edades++;

                for (int i = 0; i < basesGrafica_Edades.Count; i++){
                    if (i == segmentoSelecc_Edades) basesGrafica_Edades[segmentoSelecc_Edades].color = Color.cyan;
                    else basesGrafica_Edades[i].color = Color.white;
                }
            }
        }
    }

    public void Update(){
        
        if (adelante){
            Go();
        }
    }

    public void Go(){

        if (SegmentosGraficas[3].activeSelf){

            for (int i = 0; i < barrasSegmento_Socioeconom.Count; i++){

                if (barrasSegmento_Socioeconom[i].fillAmount != barrasValores_SocioEconomico[i]){

                    barrasSegmento_Socioeconom[i].fillAmount = Mathf.MoveTowards(barrasSegmento_Socioeconom[i].fillAmount, barrasValores_SocioEconomico[i], Time.deltaTime * velocidad);
                    barrasSegmento_Socioeconom[i].transform.GetChild(0).GetComponent<Text>().text = "" + Mathf.Round(barrasSegmento_Socioeconom[i].fillAmount * 100);
                }else{
                    if (!checkSegmento_SocioEconomico[i])
                    {
                        print("es el mismo " + i);
                        checkSegmento_SocioEconomico[i] = true;
                    }
                }
            }
            if (checkSegmento_SocioEconomico.All(x => x == true))
            {
                print("se acaba la función dichosa");
                adelante = false;

                for (int o = 0; o < barrasSegmento_Socioeconom.Count; o++)
                {
                    checkSegmento_SocioEconomico[o] = false;
                }
            }
        }

        if (SegmentosGraficas[4].activeSelf){

            for (int i = 0; i < barrasSegmento_Demografico.Count; i++){

                if (barrasSegmento_Demografico[i].fillAmount != barrasValores_Demografico[i]){

                    barrasSegmento_Demografico[i].fillAmount = Mathf.MoveTowards(barrasSegmento_Demografico[i].fillAmount, barrasValores_Demografico[i], Time.deltaTime * velocidad);
                    barrasSegmento_Demografico[i].transform.GetChild(0).GetComponent<Text>().text = "" + Mathf.Round(barrasSegmento_Demografico[i].fillAmount * 100);


                }
                else
                {

                    if (!checkSegmento_Demografico[i])
                    {
                        print("es el mismo " + i);
                        checkSegmento_Demografico[i] = true;
                    }
                }
            }
            if (checkSegmento_Demografico.All(x => x == true))
            {
                print("se acaba la función dichosa");
                adelante = false;

                for (int o = 0; o < barrasSegmento_Demografico.Count; o++)
                {
                    checkSegmento_Demografico[o] = false;
                }
            }
        }

        if (SegmentosGraficas[5].activeSelf){
            for (int i = 0; i < barrasSegmento_Edades.Count; i++){

                if (barrasSegmento_Edades[i].fillAmount != barrasValores_Edades[i]){

                    barrasSegmento_Edades[i].fillAmount = Mathf.MoveTowards(barrasSegmento_Edades[i].fillAmount, barrasValores_Edades[i], Time.deltaTime * velocidad);
                    barrasSegmento_Edades[i].transform.GetChild(0).GetComponent<Text>().text = "" + Mathf.Round(barrasSegmento_Edades[i].fillAmount * 100);
                }else{

                    if (!checkSegmento_Edades[i]){
                        print("es el mismo " + i);
                        checkSegmento_Edades[i] = true;
                    }
                }
            }

            if (checkSegmento_Edades.All(x => x == true)){
                print("se acaba la función dichosa");
                adelante = false;

                for (int o = 0; o < barrasSegmento_Edades.Count; o++)
                {
                    checkSegmento_Edades[o] = false;
                }
            }
        }
    }
        #endregion
}







