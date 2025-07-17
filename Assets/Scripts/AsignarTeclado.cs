using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AsignarTeclado : MonoBehaviour {
    
    public Teclado keyboard;
    OkapiBot okapi;

    void Start() {
        keyboard = transform.root.GetComponent<Root>().panelTeclado;
        //okapi = GameObject.Find("OkapiBot").gameObject.GetComponent<OkapiBot>();
    }

    public void Asigna () {

        //okapi.m_Recognitions = this.gameObject.GetComponent<InputField>();
        if (keyboard.gameObject.activeSelf){
            keyboard.target = this.gameObject.GetComponent<InputField>();
            keyboard.retornoInputField.text = this.gameObject.GetComponent<InputField>().text;
            keyboard.retornoInputField.caretPosition = this.gameObject.GetComponent<InputField>().text.Length;
            keyboard.caretPosition = this.gameObject.GetComponent<InputField>().text.Length;
            keyboard.content.GetComponent<RectTransform>().sizeDelta = Vector2.up * this.GetComponent<RectTransform>().sizeDelta.y;
            keyboard.content.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

        }
    }

    public void CambioTitulo(){
        if (GetComponent<InputField>().text.Length < 0){
            keyboard.GetComponent<Teclado>().caretPosition = 0;
        }
    }
    
}
