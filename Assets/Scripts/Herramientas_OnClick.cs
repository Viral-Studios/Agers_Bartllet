using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herramientas_OnClick : MonoBehaviour {

    public void OnClick(){

        if (GetComponent<UnityEngine.UI.Image>().color == Color.white){
            GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
        }else{
            GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }
}
