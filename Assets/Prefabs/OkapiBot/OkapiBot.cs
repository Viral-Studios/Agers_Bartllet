using System;
using UnityEditor;
using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Windows.Speech;

public class OkapiBot : MonoBehaviour{
    
    /*#region Variables
    public Teclado teclado;
    Image escucha_Imagen;

    public bool listening;
    float tiempo = 2.0f;

    [HideInInspector] public InputField m_Recognitions;
    public DictationRecognizer m_DictationRecognizer;
    #endregion

    private void Awake(){escucha_Imagen = GetComponent<Image>();}

    public void DictarTexto(){

        if (m_Recognitions != null){

            m_DictationRecognizer = new DictationRecognizer();

            // EMPIEZA EL RECONOCIMIENTO
            if (m_DictationRecognizer.Status == SpeechSystemStatus.Stopped && !listening)
            {
                m_DictationRecognizer.InitialSilenceTimeoutSeconds = tiempo;
                m_DictationRecognizer.Start();

                //GetComponent<Button>().interactable = false;
                escucha_Imagen.color = Color.cyan;
                listening = true;
            }else{
                print("Apagas el sistema");
                listening = false;
                escucha_Imagen.color = Color.white;
                m_DictationRecognizer.Dispose();
                m_DictationRecognizer.Stop();
            }

            // SI ADQUIERE UN RESULTADO CORRECTO...
            m_DictationRecognizer.DictationResult += (text, confidence) => {

                if (listening){
                    //Debug.LogFormat("Dictation result: {0}", text);
                    m_Recognitions.text = m_Recognitions.text.Insert(teclado.caretPosition, text + "\n");
                    teclado.retornoInputField.text = m_Recognitions.text;
                    teclado.caretPosition += text.Length;

                    //GetComponent<Button>().interactable = true;
                    listening = false;
                    escucha_Imagen.color = Color.white;
                    m_DictationRecognizer.Dispose();
                    m_DictationRecognizer.Stop();
                }
            };
            
            // SI PASA EL TIEMPO LÍMITE SE QUEDA EN AMARILLO...
            m_DictationRecognizer.DictationComplete += (completionCause) => {
                if (completionCause.ToString() == "TimeoutExceeded" && listening){
                    print("se acabó el tiempo");
                    //GetComponent<Button>().interactable = true;
                    escucha_Imagen.color = Color.yellow;
                    listening = false;
                    m_DictationRecognizer.Dispose();
                    m_DictationRecognizer.Stop();
                }
            };
        }
    }*/
}