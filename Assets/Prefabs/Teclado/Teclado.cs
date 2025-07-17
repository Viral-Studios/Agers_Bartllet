using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Nombre: Teclado.cs
 * Descripcion: Script encargado de gestionar la impresión de letras por tap en los botones del teclado.
 * 
 * -- QUEDA PENDIENTE --
 * Con el mayAuto activado no se desactiva al pulsar vocal 
 * Arreglar la e y el €
 */

public class Teclado : MonoBehaviour {

    public GameObject panelAscensor;
    public bool test;

    #region DECLARACION VARIABLES
    public OkapiBot okapiBot;
    Root script;
    public InputField target;
    public InputField retornoInputField;
    public int caretPosition;

    [Header("Bools")]
    public bool mayAuto = true;
    public bool mayBloq;
    public bool modoShift;
    public bool tilde;

    [Header("Images Botones & Teclas")]
    public Image ImageMay;
    public Image imageShift;
    public Image imageTilde;

    public List<Button> TeclasNumeros;
    public List<Button> TeclasLetras;
    public List<Button> TeclasVocales;
    public List<Button> TeclasConsonantes;
    public List<Button> TeclasSimbolos;

    [Header ("Chars")]
    [Header ("0 - 9: Nums, 10++ Letras")]
    public char[] letrasMin;
    public char[] letrasMay;

    public char[] letrasVocalesMin;
    public char[] letrasVocalesMay;
    #endregion
 
    #region PARTE DECLARATIVA
    public void Start(){

        //print("Entra en Start");
        retornoInputField = transform.GetChild(1).GetComponent<InputField>();

        // AÑADE LAS TECLAS AL ARRAY
        for (int i = 0; i < TeclasNumeros.Count; i++){
            TeclasNumeros[i] = transform.GetChild(0).GetChild(0).GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    public void Limpiar(){
        if (target != null){
            target.text = "";
            transform.GetChild(1).GetComponent<InputField>().text = "";
            content.anchoredPosition3D = Vector3.zero;

            caretPosition = 0;

            if (!mayBloq){
                mayAuto = true;
                MayAutoOn();
                LetrasMay();
            }

            #region
            parrafo = retornoInputField.text.Split("\n"[0]);
            #endregion

            return;
        }
    }
    
    // AÑADE LETRA NORMAL (CONSONANTE)
    public void AddLetra(int num){

        if (target != null){
            if (tilde)
            {
                retornoInputField.text = retornoInputField.text.Insert(caretPosition, "´");
                target.text = retornoInputField.text;
                caretPosition++;
                //print("priimO");
                TildeOff();
            }

            for (int i = 0; i < 27; i++)
            {
                if (i == num)
                {

                    // Se desecha la letra E por el símbolo €
                    if (num != 2)
                    {
                        if (!mayAuto)
                        {
                            retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasLetras[num].transform.GetChild(0).GetComponent<Text>().text);
                            target.text = retornoInputField.text;

                            caretPosition++;
                            return;
                        }
                        else
                        {
                            retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasLetras[num].transform.GetChild(0).GetComponent<Text>().text);
                            target.text = retornoInputField.text;

                            caretPosition++;
                            LetrasMin();
                            MayOff();
                            return;
                        }
                    }
                }

                // Caso especial para la E por el símbolo €
                if (num == 2 && !modoShift)
                {
                    //print("si debería entrar");
                    if (!mayAuto)
                    {
                        retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasLetras[num].transform.GetChild(0).GetComponent<Text>().text);
                        target.text = retornoInputField.text;
                        caretPosition++;
                        return;
                    }
                    else
                    {
                        retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasLetras[num].transform.GetChild(0).GetComponent<Text>().text);
                        target.text = retornoInputField.text;
                        caretPosition++;
                        LetrasMin();
                        MayOff();
                        return;
                    }
                }
            }
        }
    }

    // PROC. AÑADE LETRA VOCAL
    public void AddLetraVocal(int num){

        if (target != null)
        {
            for (int i = 0; i < 27; i++)
            {
                if (i == num)
                {
                    if (i != 2)
                    {
                        if (tilde)
                        {
                            if (!mayAuto && !mayBloq)
                            {
                                retornoInputField.text = retornoInputField.text.Insert(caretPosition, "" + letrasVocalesMin[i]);
                                target.text = retornoInputField.text;
                                caretPosition++;
                            }
                            else
                            {
                                retornoInputField.text = retornoInputField.text.Insert(caretPosition, "" + letrasVocalesMay[i]);
                                target.text = retornoInputField.text;
                                caretPosition++;

                                if (mayAuto)
                                {
                                    MayOff();
                                    LetrasMin();
                                }
                            }

                            TildeOff();
                        }
                        else
                        {
                            retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasLetras[num].transform.GetChild(0).GetComponent<Text>().text);
                            target.text = retornoInputField.text;
                            caretPosition++;

                            if (mayAuto)
                            {
                                MayOff();
                                LetrasMin();
                                return;
                            }
                        }
                    }
                    else
                    {

                        // SI PULSAS LA E...
                        if (tilde)
                        {

                            // Y EL MODO SHIFT ESTÁ PUESTO...
                            if (modoShift)
                            {
                                retornoInputField.text = retornoInputField.text.Insert(caretPosition, "´€");
                                target.text = retornoInputField.text;
                                caretPosition += 2;
                            }
                            else
                            {
                                if (mayAuto || mayBloq)
                                {
                                    retornoInputField.text = retornoInputField.text.Insert(caretPosition, "É");
                                    target.text = retornoInputField.text;
                                    caretPosition++;

                                    if (mayAuto)
                                    {
                                        MayOff();
                                        LetrasMin();
                                    }
                                }
                                else
                                {
                                    retornoInputField.text = retornoInputField.text.Insert(caretPosition, "é");
                                    target.text = retornoInputField.text;
                                    caretPosition++;
                                }
                            }
                        }
                        else
                        {
                            if (!modoShift)
                            {
                                if (mayAuto || mayBloq)
                                {
                                    retornoInputField.text = retornoInputField.text.Insert(caretPosition, "E");
                                    target.text = retornoInputField.text;
                                    caretPosition++;

                                    if (mayAuto)
                                    {
                                        MayOff();
                                        LetrasMin();
                                    }
                                }
                                else
                                {
                                    retornoInputField.text = retornoInputField.text.Insert(caretPosition, "e");
                                    target.text = retornoInputField.text;
                                    caretPosition++;
                                }
                            }
                        }
                    }
                }
            }
            TildeOff();
        }
        return;
    }

    // AÑADE NUMEROS
    public void AddNumero(int num){

        if (target != null)
        {
            if (tilde)
            {
                retornoInputField.text = retornoInputField.text.Insert(caretPosition, "´");
                target.text = retornoInputField.text;
                caretPosition++;
                TildeOff();
            }

            for (int i = 0; i < TeclasNumeros.Count; i++)
            {
                if (i == num && target != null)
                {

                    if (!modoShift)
                    {
                        retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasNumeros[i].transform.GetChild(0).GetComponent<Text>().text);
                        target.text = retornoInputField.text;
                        caretPosition++;
                        return;
                    }
                    else
                    {
                        retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasNumeros[i].transform.GetChild(1).GetComponent<Text>().text);
                        target.text = retornoInputField.text;
                        caretPosition++;
                        ShiftModos();

                        // Añadirle un espacio y activar modo mayAuto "automáticamente"
                        if (num == 1)
                        {
                            #region " " + mayAuto = true;
                            retornoInputField.text = retornoInputField.text.Insert(caretPosition, " ");
                            target.text = retornoInputField.text;
                            caretPosition++;

                            if (!mayAuto && !mayBloq)
                            {
                                mayAuto = true;
                                MayAutoOn();
                                LetrasMay();
                            }
                            #endregion
                        }

                        return;
                    }
                }
            }
        }
    }

    // AÑADE SIMBOLOS
    public void AddSimbolos(int num){

        if (target != null){
            if (tilde)
            {
                retornoInputField.text = retornoInputField.text.Insert(caretPosition, "´");
                target.text = target.text.Insert(caretPosition, "´");
                caretPosition++;

                if (num != 6)
                {
                    TildeOff();
                }
            }

            for (int i = 0; i < TeclasSimbolos.Count; i++)
            {
                if (i == num && target != null)
                {

                    if (num != 3 && num != 6)
                    {

                        if (!modoShift)
                        {
                            retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasSimbolos[i].transform.GetChild(0).GetComponent<Text>().text);
                            target.text = retornoInputField.text;
                            caretPosition++;

                            // Añadirle un espacio y activar modo mayAuto "automáticamente" al pulsar "punto" 
                            if (num == 10 || num == 1){

                                retornoInputField.text = retornoInputField.text.Insert(caretPosition, " ");
                                target.text = retornoInputField.text;
                                caretPosition++;

                                if (!mayAuto && !mayBloq)
                                {
                                    #region " " + mayAuto = true;
                                    mayAuto = true;
                                    MayAutoOn();
                                    LetrasMay();
                                }
                                #endregion
                            }
                            return;
                        }else{
                            retornoInputField.text = retornoInputField.text.Insert(caretPosition, TeclasSimbolos[i].transform.GetChild(1).GetComponent<Text>().text);
                            target.text = retornoInputField.text;
                            caretPosition++;
                            ShiftModos();
                            return;
                        }
                    }
                    else
                    {
                        if (num == 3)
                        { // PULSAMOS LA E
                            if (modoShift)
                            {
                                retornoInputField.text = retornoInputField.text.Insert(caretPosition, "€");
                                target.text = retornoInputField.text;
                                caretPosition++;
                                //print("entra");
                            }
                        }

                        if (num == 6)
                        { // PULSAMOS LA TILDE
                            if (tilde) { imageTilde.color = Color.cyan; } else { imageTilde.color = Color.white; }

                            if (!modoShift)
                            {
                                tilde = !tilde;
                            }
                            else
                            {
                                retornoInputField.text = retornoInputField.text.Insert(caretPosition, "{");
                                target.text = retornoInputField.text;
                                caretPosition++;
                            }
                        }

                        if (modoShift)
                        {
                            ShiftModos();
                        }
                        return;
                    }
                }
            }
        }
    }

    // CAMBIO MAY/MIN
    public void LetrasMay(){
        for (int i = 0; i < 27; i++){
            TeclasLetras[i].transform.GetChild(0).GetComponent<Text>().text = "" + letrasMay[i];
        }
    }
    public void LetrasMin(){
        for (int i = 0; i < 27; i++){
            TeclasLetras[i].transform.GetChild(0).GetComponent<Text>().text = "" + letrasMin[i];
        }
    }

    #region CAMBIOS MODO MAY -AUTO- & -FIJA-
    public void MayusModos(){
        // Se activa el modoCambioAuto          auto | bloq | nada
        if (mayAuto && !mayBloq){
            mayAuto = false;
            mayBloq = true;
            MayBloqOn();
            return;
        }

        if (!mayAuto && !mayBloq){
            mayAuto = true;
            MayAutoOn();
            LetrasMay();
            return;
        }else{
            MayOff();
            LetrasMin();
            return;
        }
    }
    public void MayAutoOn(){
        ImageMay.color = Color.cyan;
        ImageMay.transform.GetChild(0).GetComponent<Text>().color = Color.black;
        ImageMay.transform.GetChild(0).GetComponent<Text>().text = "May";
    }
    public void MayBloqOn(){
        ImageMay.color = Color.blue;
        ImageMay.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        ImageMay.transform.GetChild(0).GetComponent<Text>().text = "MAY";
    }
    public void MayOff(){

        mayBloq = false;
        mayAuto = false;

        ImageMay.color = Color.white;
        ImageMay.transform.GetChild(0).GetComponent<Text>().color = Color.black;
        ImageMay.transform.GetChild(0).GetComponent<Text>().text = "May";
    }

    public void TildeOff(){
        if (tilde){
            tilde = false;
            imageTilde.color = Color.white;
        }
    }

    #endregion

    /* CAMBIOS MODO SHIFT */
    public void ShiftModos(){

        if (target != null)
        {
            modoShift = !modoShift;
            ShiftTeclasBrillo();

            if (modoShift)
            {
                imageShift.color = Color.cyan;
            }
            else
            {
                imageShift.color = Color.white;
            }
        }
    }

    public Color colorBrillo;

    public void ShiftTeclasBrillo(){

        if (target != null) {
            if (modoShift)
            {

                #region SE ILUMINAN LAS TECLAS EN MODO SHIFT ON
                // teclas de los números
                for (int i = 0; i < 10; i++)
                {
                    TeclasNumeros[i].GetComponent<Image>().color = colorBrillo;
                }

                // teclas de los símbolos
                for (int i = 0; i < 12; i++)
                {
                    TeclasSimbolos[i].GetComponent<Image>().color = colorBrillo;
                }

                // tecla "e"
                TeclasLetras[2].GetComponent<Image>().color = colorBrillo;
                #endregion
            }
            else
            {
                #region SE DESILUMINAN LAS TECLAS EN MODO SHIFT OFF
                // teclas de los números
                for (int i = 0; i < 10; i++)
                {
                    TeclasNumeros[i].GetComponent<Image>().color = Color.white;
                }

                // teclas de los símbolos
                for (int i = 0; i < 12; i++)
                {
                    TeclasSimbolos[i].GetComponent<Image>().color = Color.white;
                }

                // tecla "e"
                TeclasLetras[2].GetComponent<Image>().color = Color.white;
                #endregion
            }
        }
    }

    public void Espacios(int num){

        if (target != null){
            // DELETE
            if (num == 0 && target.text.Length >= 1)
            {

                if (caretPosition == 0)
                {
                    caretPosition++;
                }

                retornoInputField.text = retornoInputField.text.Substring(0, caretPosition - 1) + retornoInputField.text.Substring(caretPosition, retornoInputField.text.Length - caretPosition);
                target.text = retornoInputField.text;
                caretPosition--;

                if (caretPosition > 1 && (retornoInputField.text[caretPosition - 2].ToString() == "." || retornoInputField.text[caretPosition - 2].ToString() == "?" || retornoInputField.text[caretPosition - 2].ToString() == "!"))
                {
                    //print("ENTRA");
                    if (!mayBloq && !mayAuto)
                    {

                        mayAuto = true;
                        MayAutoOn();
                        LetrasMay();
                    }
                }

                if (caretPosition < 0)
                {
                    caretPosition = 0;
                }
            }

            // ENTER
            if (num == 1)
            {

                if (target.lineType != InputField.LineType.SingleLine)
                {

                    retornoInputField.text = retornoInputField.text.Insert(caretPosition, System.Environment.NewLine);
                    retornoInputField.text = target.text;

                    retornoInputField.textComponent.text = retornoInputField.text.Substring(retornoInputField.text.Length - 5);

                    caretPosition += 2;
                    retornoInputField.caretPosition = caretPosition;
                    content.anchoredPosition += Vector2.up * 30;

                    if (!mayAuto && !mayBloq)
                    {
                        mayAuto = true;
                        MayAutoOn();
                        LetrasMay();
                    }


                    //ContarSaltos();
                }
            }

            // SPACE
            if (num == 2)
            {
                retornoInputField.text = retornoInputField.text.Insert(caretPosition, " ");
                target.text = retornoInputField.text;
                caretPosition++;
            }
        }

        return;
    }
    public void Test(){

        if (target != null && target.text.Length >= 1){
            caretPosition = retornoInputField.caretPosition;
        }
    }

    private float dist;
    public bool dragging = true;
    private Vector3 offset;
    public Transform toDrag;
    public Transform localizador;

    public bool bloqEjeX;
    public bool bloqEjeY;
    private Vector3 v3;

    public float limiteIzqX;
    public float limiteDerX;

    public float posIzqX;
    public float posDerX;

    public float limiteIzqY;
    public float limiteDerY;

    public float posIzqY;
    public float posDerY;

    public void FixedUpdate(){

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 v3;
                Vector3 pos = Input.mousePosition;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(pos);
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.name == "MoverTeclado")
                    {
                        Debug.Log("Here");
                        toDrag = hit.transform.parent.transform;
                        dist = hit.transform.position.z - Camera.main.transform.position.z;
                        v3 = new Vector3(pos.x, pos.y, dist);
                        v3 = Camera.main.ScreenToWorldPoint(v3);
                        offset = toDrag.position - v3;

                        dragging = true;
                    }
                }
            }

            if (Input.GetMouseButton(0))
            {

                if (dragging)
                {
                    Vector3 pos = Input.mousePosition;
                    v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                }

                // EL PANEL DEL TECLADO SE MUEVE SEGÚN LOS LÍMITES QUE ALCANCE EN LA PANTALLA
                if (dragging)
                {

                    if (!bloqEjeX)
                    {

                        if (!bloqEjeY)
                        {
                            toDrag.position = v3 + offset;
                            //print("DESBLOQUEADOS AMBOS EJES");
                        }
                        else
                        {
                            toDrag.position = new Vector3(v3.x + offset.x, toDrag.position.y, toDrag.position.z); ;
                            //print("BLOQUEADO EJE Y");
                        }
                    }
                    else
                    {
                        if (!bloqEjeY)
                        {
                            toDrag.position = new Vector3(toDrag.position.x, toDrag.position.y, v3.z + offset.z); ;
                            //print("BLOQUEADO EJE X");
                        }
                        else
                        {
                            //print("BLOQUEADOS AMBOS EJES");
                        }
                    }
                }
            }

            #region LIMITES TECLADO
            if (toDrag != null && dragging)
            {
                if (Input.mousePosition.x > limiteIzqX && Input.mousePosition.x < limiteDerX)
                {
                    if (bloqEjeX)
                    {
                        bloqEjeX = false;
                    }
                }
                else
                {
                    if (!bloqEjeX)
                    {
                        bloqEjeX = true;

                        if (!bloqEjeY)
                        {
                            if (Input.mousePosition.x > 810)
                            {
                                toDrag.transform.localPosition = new Vector3(toDrag.transform.localPosition.x, posDerX, toDrag.transform.localPosition.z);
                                toDrag.transform.localEulerAngles = Vector3.forward * 180.0f;

                            }
                            else
                            {

                                if (toDrag != null)
                                {
                                    toDrag.transform.localPosition = new Vector3(toDrag.transform.localPosition.x, posIzqX, toDrag.transform.localPosition.z);
                                    toDrag.transform.localEulerAngles = Vector3.zero;
                                    //print(Input.mousePosition.x);
                                }
                            }
                        }
                    }
                }

                if (Input.mousePosition.y > limiteDerY && Input.mousePosition.y < limiteIzqY)
                {
                    if (bloqEjeY)
                    {
                        bloqEjeY = false;
                    }
                }
                else
                {
                    if (!bloqEjeY)
                    {
                        bloqEjeY = true;

                        if (!bloqEjeX)
                        {
                            if (Input.mousePosition.y < 625)
                            {
                                toDrag.transform.localPosition = new Vector3(posDerY, toDrag.transform.localPosition.y, toDrag.transform.localPosition.z);
                                toDrag.transform.localEulerAngles = Vector3.forward * 90.0f;

                            }
                            else
                            {
                                toDrag.transform.localPosition = new Vector3(posIzqY, toDrag.transform.localPosition.y, toDrag.transform.localPosition.z);
                                toDrag.transform.localEulerAngles = Vector3.forward * -90.0f;
                            }
                        }
                    }
                }
            }
            #endregion

            if (dragging && Input.GetMouseButtonUp(0))
            {
                Vector3 pos = Input.mousePosition;
                dragging = false;
                return;
            }
        
    }
    #endregion

    public void CheckRotacion()
    {
        if (Input.mousePosition.x > limiteIzqX && Input.mousePosition.x < limiteDerX)
        {
            if (bloqEjeX)
            {
                bloqEjeX = false;
            }
        }
        else
        {
            if (!bloqEjeX)
            {
                bloqEjeX = true;

                if (!bloqEjeY)
                {
                    if (Input.mousePosition.x > 810)
                    {
                        toDrag.transform.localPosition = new Vector3(toDrag.transform.localPosition.x, posDerX, toDrag.transform.localPosition.z);
                        toDrag.transform.localEulerAngles = Vector3.forward * 180.0f;

                    }
                    else
                    {
                        toDrag.transform.localPosition = new Vector3(toDrag.transform.localPosition.x, posIzqX, toDrag.transform.localPosition.z);
                        toDrag.transform.localEulerAngles = Vector3.zero;
                        print(Input.mousePosition.x);
                    }
                }
            }
        }

        if (Input.mousePosition.y > limiteDerY && Input.mousePosition.y < limiteIzqY)
        {
            if (bloqEjeY)
            {
                bloqEjeY = false;
            }
        }
        else
        {
            if (!bloqEjeY)
            {
                bloqEjeY = true;

                if (!bloqEjeX)
                {
                    if (Input.mousePosition.y < 625)
                    {
                        toDrag.transform.localPosition = new Vector3(posDerY, toDrag.transform.localPosition.y, toDrag.transform.localPosition.z);
                        toDrag.transform.localEulerAngles = Vector3.forward * 90.0f;

                    }
                    else
                    {
                        toDrag.transform.localPosition = new Vector3(posIzqY, toDrag.transform.localPosition.y, toDrag.transform.localPosition.z);
                        toDrag.transform.localEulerAngles = Vector3.forward * -90.0f;
                    }
                }
            }
        }
    }

    
    public void Activar(){

        if (target != null){
            target.text = retornoInputField.text;
        }
    }

    public RectTransform content;
    public string[] parrafo;

    public void ContarSaltos(){
        #region
        parrafo = retornoInputField.text.Split("\n"[0]);
        content.sizeDelta = new Vector2(content.sizeDelta.x, parrafo.Length * 19 + 8);
        content.anchoredPosition += Vector2.up * 20;
        #endregion
    }

    public void OnTriggerEnter(Collider other){
        
        if (other.gameObject.name == "bloqueoTeclado_EJEX")
        {
            bloqEjeX = true;
        }

        if (other.gameObject.name == "bloqueoTeclado_EJEY")
        {
            bloqEjeY = true;
        }
    }

    #region SELECCIONA CON FLECHA ARRIBA/ABAJO EL SIGUIENTE O ANTERIOR CAMPO A RELLENAR
    public void UI_Siguiente(){

        if (target.navigation.mode == Navigation.Mode.Explicit){
            target.navigation.selectOnUp.gameObject.GetComponent<AsignarTeclado>().Asigna();
        }

        //okapiBot.m_Recognitions = target;
        return;
    }

    public void UI_Anterior(){

        if (target.navigation.mode == Navigation.Mode.Explicit){
            target.navigation.selectOnDown.gameObject.GetComponent<AsignarTeclado>().Asigna();
        }
        //okapiBot.m_Recognitions = target;
        return;
    }
    #endregion

    /*public void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "bloqueoTeclado_EJEX" && !bloqEjeX){
            print("Entra");
            bloqEjeX = true;
        }

        if (other.gameObject.name == "bloqueoTeclado_EJEY" && !bloqEjeY){
            print("Entra");
            bloqEjeY = true;
        }
    }*/

    /***********************************************************************************************/
    /*#region Variables ANTIGUO SISTEMA REMODELADO TECLADO INTEGRO 
    //public OkapiBot okapiBot;

    Root script;
    public Image ImagemodoNegrita, imageModoShift;
    public List<GameObject> Teclas;
    public List<string> letrasMin;
    public List<string> letrasMay;
    public InputField target;
    public bool modoMayBloq = false, modoCambioAuto = true, modoTilde, modoShift = false;
    bool modoNegrita;
    public int caretPosition;
    #endregion

    private void Awake(){
        script = transform.root.GetComponent<Root>();
    }

    private void Update(){
        if (target != null && transform.GetChild(0).GetComponent<InputField>().text != target.text){
            transform.GetChild(0).GetComponent<InputField>().text = target.text;
        }
    }

    public void EditarSeccion(int num){

        if (target != null){
            for (int i = 0; i < Teclas.Count - 3; i++){
                if (i == num){
                    if (num <= 41){

                        // CON MODOTILDE -OFF- SE AÑADE DIRECTAMENTE LA LETAR DIRECTAMENTE
                        if (!modoTilde){

                            if (!modoShift){
                                target.text = target.text.Insert(caretPosition, Teclas[i].transform.GetChild(0).GetComponent<Text>().text);
                                caretPosition++;
                            }else{
                                if (Teclas[i].transform.childCount == 2){
                                    target.text = target.text.Insert(caretPosition, Teclas[i].transform.GetChild(1).GetComponent<Text>().text);
                                    caretPosition++;

                                    modoShift = false;
                                    imageModoShift.color = Color.white;
                                }
                                else{
                                    target.text = target.text.Insert(caretPosition, Teclas[i].transform.GetChild(0).GetComponent<Text>().text);
                                    caretPosition++;

                                    modoShift = false;
                                    imageModoShift.color = Color.white;
                                }
                            }

                        }else{
                            if (num != 12 && num != 16 && num != 17 && num != 18 && num != 20){

                                target.text = target.text.Insert(caretPosition, Teclas[i].transform.GetChild(0).GetComponent<Text>().text);
                                caretPosition++;
                            }else{
                                if (modoMayBloq){
                                    #region CONJUNTO VOCALES MAY CON TILDE
                                    // Letra e
                                    if (num == 12){ target.text = target.text.Insert(caretPosition, "É"); caretPosition++; }

                                    // Letra u
                                    if (num == 16){ target.text = target.text.Insert(caretPosition, "Ú"); caretPosition++; }

                                    // Letra i
                                    if (num == 17){ target.text = target.text.Insert(caretPosition, "Í"); caretPosition++; }

                                    // Letra o
                                    if (num == 18){ target.text = target.text.Insert(caretPosition, "Ó"); caretPosition++; }

                                    // Letra a
                                    if (num == 20){ target.text = target.text.Insert(caretPosition, "Á"); caretPosition++; }
                                    #endregion
                                }else{
                                    #region CONJUNTO VOCALES MIN CON TILDE
                                    // Letra e
                                    if (num == 12) { target.text = target.text.Insert(caretPosition, "é"); caretPosition++; }

                                    // Letra u
                                    if (num == 16) { target.text = target.text.Insert(caretPosition, "ú"); caretPosition++; }

                                    // Letra i
                                    if (num == 17) { target.text = target.text.Insert(caretPosition, "í"); caretPosition++; }

                                    // Letra o
                                    if (num == 18) { target.text = target.text.Insert(caretPosition, "ó"); caretPosition++; }

                                    // Letra a
                                    if (num == 20) { target.text = target.text.Insert(caretPosition, "á"); caretPosition++; }
                                    #endregion
                                }
                            }
                        }

                        if (!modoMayBloq && modoCambioAuto){
                            print("entra especial");
                            modoCambioAuto = false;

                            for (int o = 0; o < letrasMin.Count; o++){
                                Teclas[o + 10].transform.GetChild(0).GetComponent<Text>().text = letrasMin[o];
                            }

                            ImagemodoNegrita.color = Color.white;
                            ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "May";
                            ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                        }
                    }
                    modoTilde = false;
                }
            }
            
            // DELETE
            if (num == 42 && target.text.Length >= 1){

                if (caretPosition == 0){
                    caretPosition++;
                }

                target.text = target.text.Substring(0, caretPosition - 1) + target.text.Substring(caretPosition, target.text.Length - caretPosition);
                caretPosition--;

                if (caretPosition > 1 && (target.text[target.text.Length - 2].ToString() == "." || target.text[target.text.Length - 2].ToString() == "?" || target.text[target.text.Length - 2].ToString() == "!")){
                    if (!modoMayBloq && !modoCambioAuto){

                        modoCambioAuto = true;

                        for (int i = 0; i < letrasMin.Count; i++){
                            Teclas[i + 10].transform.GetChild(0).GetComponent<Text>().text = letrasMay[i];
                        }

                        ImagemodoNegrita.color = Color.cyan;
                        ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "May";
                        ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.black;

                        //print("ultima letra: " + target.text[target.text.Length - 2]);
                    }
                }

                if (caretPosition < 0) {
                    caretPosition = 0;

                    if (!modoCambioAuto && !modoMayBloq){
                        modoCambioAuto = true;

                        for (int i = 0; i < letrasMin.Count; i++)
                        {
                            Teclas[i + 10].transform.GetChild(0).GetComponent<Text>().text = letrasMay[i];
                        }

                        ImagemodoNegrita.color = Color.cyan;
                        ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "May";
                        ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                    }
                }
            }

            // ENTER
            if (num == 43){

                if (target.lineType != InputField.LineType.SingleLine){
                    target.text = target.text.Insert(caretPosition, System.Environment.NewLine);
                    caretPosition += 2;

                    if (!modoCambioAuto){
                        modoCambioAuto = true;

                        for (int i = 0; i < letrasMin.Count; i++){
                            Teclas[i + 10].transform.GetChild(0).GetComponent<Text>().text = letrasMay[i];
                        }

                        ImagemodoNegrita.color = Color.cyan;
                        ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "May";
                        ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                    }
                }
            }

            // SPACE
            if (num == 44){
                //target.text += " ";
                target.text = target.text.Insert(caretPosition, " ");
                caretPosition++;
            }

            // SE ASOCIA LA ETIQUETA DEL PERSONAJE INGAME GENERAL AL DE LA SALA DE DESCANSO
            for (int i = 0; i < 10; i++)
            {
                if (target.gameObject == script.personajesInGame[i].transform.GetChild(2).gameObject)
                {
                    print("coinciden");
                    script.playerInGameDescanso[i].transform.GetChild(2).GetComponent<InputField>().text = target.GetComponent<InputField>().text;
                }
            }
        }

        // MODO MAYUS / MINUS
        if (num == 50){

            // Se activa el modoCambioAuto
            if (!modoCambioAuto && !modoMayBloq){
                modoCambioAuto = true;

                for (int i = 0; i < letrasMin.Count; i++){
                    Teclas[i + 10].transform.GetChild(0).GetComponent<Text>().text = letrasMay[i];
                }

                ImagemodoNegrita.color = Color.cyan;
                ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "May";
                ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            }
            else{
                // Se activa el modoMayBloq
                if (modoCambioAuto){
                    modoCambioAuto = false;
                    modoMayBloq = true;

                    ImagemodoNegrita.color = Color.blue;
                    ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "MAY";
                    ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.white;
                }
                else {
                    // Se desactiva todo
                    if (modoMayBloq){
                        modoMayBloq = false;

                        for (int i = 0; i < letrasMin.Count; i++){
                            Teclas[i + 10].transform.GetChild(0).GetComponent<Text>().text = letrasMin[i];
                        }

                        ImagemodoNegrita.color = Color.white;
                        ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "May";
                        ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                    }
                }
            }
        }

        // MODO TILDE
        if (num == 51){
            if (!modoTilde) { modoTilde = true; }
        }

        // MODO ALT
        if (num == 52){
            modoShift = !modoShift;

            if (modoShift) { imageModoShift.color = Color.cyan; }
            else { imageModoShift.color = Color.white; }
        }

            #region MODO NEGRITA

        if (num == 51){
            modoNegrita = !modoNegrita;

            if (modoNegrita){
                botones[1].color = Color.cyan;

                for (int i = 0; i < 42; i++){
                    Teclas[i].transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Bold;
                }
            }
            else{
                botones[1].color = Color.white;

                for (int i = 0; i < 42; i++){
                    Teclas[i].transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Normal;
                }
            }
        }
#endregion
}

    public void EditarSeccionEspecial (int num){

        for (int i = 0; i < Teclas.Count - 3; i++){

            if (i == num){
                target.text = target.text.Insert(caretPosition, Teclas[i].transform.GetChild(0).GetComponent<Text>().text);
                caretPosition++;
            }
        }

        if (!modoCambioAuto){
            modoCambioAuto = true;

            for (int i = 0; i < letrasMin.Count; i++)
            {
                Teclas[i + 10].transform.GetChild(0).GetComponent<Text>().text = letrasMay[i];
            }

            ImagemodoNegrita.color = Color.cyan;
            ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "May";
            ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.black;
        }
    }

    public void Limpiar(){

        if (target != null){
            target.text = "";
            caretPosition = 0;

            if (!modoMayBloq){
                modoCambioAuto = true;

                for (int i = 0; i < letrasMin.Count; i++){
                    Teclas[i + 10].transform.GetChild(0).GetComponent<Text>().text = letrasMay[i];
                }

                ImagemodoNegrita.color = Color.cyan;
                ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().text = "May";
                ImagemodoNegrita.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            }

            // SE ASOCIA LA ETIQUETA DEL PERSONAJE INGAME GENERAL AL DE LA SALA DE DESCANSO
            for (int i = 0; i < 10; i++){
                if (target.gameObject == script.personajesInGame[i].transform.GetChild(2).gameObject){
                    print("coinciden");
                    script.playerInGameDescanso[i].transform.GetChild(2).GetComponent<InputField>().text = target.GetComponent<InputField>().text;
                }
            }
        }
    }
*/
}