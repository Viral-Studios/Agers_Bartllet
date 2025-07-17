using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class graficaDrag : MonoBehaviour {

    [Header("")]
    public InputField conceptoMaster;
    public InputField conceptoRetorno;
    public InputField[] conceptosArray = new InputField[8];

    [Header ("")]
    public List<int> auxValoresFilas = new List<int> (8);

    public int numFilas = 3;
    private int sumatorio;
    private int restoFinal;

    public Text filasFinal;
    public Text resto;
    public Transform padreFilas;
    public GameObject padreGrafica;
    public GameObject panelLeyenda;
    
    // tres a ocho
    public void Filas (int num) {

        if (num == 0 && numFilas < 8){
            numFilas++;
        }

        if (num == 1 && numFilas > 1){
            numFilas--;
        }

        if (filasFinal.text != "0"){
            filasFinal.text = numFilas + "";
        }

        for (int i = 0; i < padreFilas.transform.childCount; i++){

            if (i < numFilas){
                if (!padreFilas.transform.GetChild(i).gameObject.activeSelf)
                padreFilas.transform.GetChild(i).gameObject.SetActive(true);
                padreGrafica.transform.GetChild(i).gameObject.SetActive(true);
                conceptosArray[i].transform.parent.gameObject.SetActive(true);
                panelLeyenda.transform.GetChild(i).gameObject.SetActive(true);

            }
            else{
                padreFilas.transform.GetChild(i).gameObject.SetActive(false);
                padreGrafica.transform.GetChild(i).gameObject.SetActive(false);
                conceptosArray[i].transform.parent.gameObject.SetActive(false);
                panelLeyenda.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < padreFilas.transform.childCount; i++){
            if (padreFilas.transform.GetChild(i).gameObject.activeSelf){
                ValoresFilas (i);
            }
        }

        padreFilas.transform.GetChild(numFilas-1).GetComponent<AsignarTeclado>().Asigna();
        AsignarConceptoMaster(numFilas - 1);
	}

    // Añade valores a la casilla correspondiente y hace el sumatorio
    public void ValoresFilas (int num){

        for (int i = 0; i < padreFilas.childCount; i++){
            if (padreFilas.GetChild(i).gameObject.activeSelf && padreFilas.GetChild(i).GetComponent<InputField>().text != ""){

                auxValoresFilas[i] = int.Parse(padreFilas.GetChild(i).GetComponent<InputField>().text);
                sumatorio += auxValoresFilas[i];

                padreGrafica.transform.GetChild(num).GetComponent<Image>().fillAmount = 0.01f * auxValoresFilas[num];

                padreGrafica.transform.GetChild(num).GetChild(0).GetChild(0).GetComponent<Text>().text = "" + auxValoresFilas[num];
                conceptosArray[i].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-40 * padreGrafica.transform.GetChild(i).GetComponent<Image>().fillAmount, -100 * padreGrafica.transform.GetChild(i).GetComponent<Image>().fillAmount, 0);

                Pos_RotulosGraficas(num);
            }
        }

        padreGrafica.transform.GetChild(1).transform.localEulerAngles = Vector3.forward * (360 * (1 - padreGrafica.transform.GetChild(0).GetComponent<Image>().fillAmount));
        padreGrafica.transform.GetChild(2).transform.localEulerAngles = Vector3.forward * (360 * (1 - (padreGrafica.transform.GetChild(0).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(1).GetComponent<Image>().fillAmount)));
        padreGrafica.transform.GetChild(3).transform.localEulerAngles = Vector3.forward * (360 * (1 - (padreGrafica.transform.GetChild(0).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(1).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(2).GetComponent<Image>().fillAmount)));
        padreGrafica.transform.GetChild(4).transform.localEulerAngles = Vector3.forward * (360 * (1 - (padreGrafica.transform.GetChild(0).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(1).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(2).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(3).GetComponent<Image>().fillAmount)));
        padreGrafica.transform.GetChild(5).transform.localEulerAngles = Vector3.forward * (360 * (1 - (padreGrafica.transform.GetChild(0).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(1).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(2).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(3).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(4).GetComponent<Image>().fillAmount)));
        padreGrafica.transform.GetChild(6).transform.localEulerAngles = Vector3.forward * (360 * (1 - (padreGrafica.transform.GetChild(0).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(1).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(2).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(3).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(4).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(5).GetComponent<Image>().fillAmount)));
        padreGrafica.transform.GetChild(7).transform.localEulerAngles = Vector3.forward * (360 * (1 - (padreGrafica.transform.GetChild(0).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(1).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(2).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(3).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(4).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(5).GetComponent<Image>().fillAmount + padreGrafica.transform.GetChild(6).GetComponent<Image>().fillAmount)));

        for (int i = padreFilas.childCount - 1; i > -1; i--){
            if (padreFilas.GetChild(i).gameObject.activeSelf){

                float aux = padreGrafica.transform.GetChild(i).GetChild(0).transform.localEulerAngles.z + padreGrafica.transform.GetChild(i).transform.localEulerAngles.z;
                padreGrafica.transform.GetChild(i).GetChild(0).GetChild(0).transform.localEulerAngles = Vector3.forward * (-aux);
            }
        }

        restoFinal = 100 - sumatorio;
        resto.text = restoFinal + "";

        if (sumatorio > 100){
            //print("es mayor");
            //print("sumatorio = " + sumatorio);
            //print("auxValoresFilas = " + auxValoresFilas[num]);

            padreFilas.transform.GetChild(num).GetComponent<InputField>().text = auxValoresFilas[num] + restoFinal + "";
            resto.text = 0 + "";
        }

        sumatorio = 0;
        restoFinal = 0;
    }


    public void AsignarConceptoMaster (int num){
        for (int i = 0; i < 8; i++){

            if (i == num && (conceptoMaster != null || conceptoMaster != conceptosArray[i])){
                conceptoMaster = conceptosArray[i];
                conceptoRetorno.text = conceptoMaster.text;
                //
            }
        }
    }

    public void RefrescarConceptos(){
        if (transform.root.GetComponent<Root>().panelTeclado.GetComponent<Teclado>().target == conceptoRetorno){
            conceptoMaster.text = conceptoRetorno.text;
            print("Está refrescando");
        }
    }

    float poxY;
    float grados;

    void Pos_RotulosGraficas(int num){

        float aux = padreGrafica.transform.GetChild(num).GetComponent<Image>().fillAmount;
        poxY = (aux - 1) + aux;

        // escala 0 a 1 en fillamount pasa a ser 0 a 360 grados
        if (grados != aux * 360){
            grados = aux * 360;

            padreGrafica.transform.GetChild(num).GetChild(0).transform.localEulerAngles = -Vector3.forward * (grados / 2);
            padreGrafica.transform.GetChild(num).GetChild(0).GetChild(0).GetComponent<Text>().text = Mathf.RoundToInt(aux * 100) + "";
        }

        if (aux == 0 && padreGrafica.transform.GetChild(num).GetChild(0).GetChild(0).gameObject.activeSelf){
            padreGrafica.transform.GetChild(num).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }else{
            if (!padreGrafica.transform.GetChild(num).GetChild(0).GetChild(0).gameObject.activeSelf)
            {
                padreGrafica.transform.GetChild(num).GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
