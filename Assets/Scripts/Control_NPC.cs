using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Control_NPC : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler{

    public GameObject character;
    public float targetSpeed = 5;
    //Componentes del joystick
    public GameObject joystick;
    public Image joystickImage;
    public Image joystickBall;
    public GameObject target;
    public Vector3 inputVector;
    public RaycastHit hit;
    //Componentes del minimapa
    public GameObject minimapCamera;
    public GameObject minimap;
    public RaycastHit destination;
    //Botones de tipo de control
    public Button joystickButton;
    public Button minimapButton;

    public bool usandoJoystick;

    public void Start(){
        //Buscamos los componentes del joystick
        joystick = transform.GetChild(1).gameObject;
        joystickImage = joystick.transform.GetChild(0).GetComponent<Image>();
        joystickBall = joystickImage.transform.GetChild(0).GetComponent<Image>();
        target = transform.GetChild(3).gameObject;
        //Buscamos los componentes del minimapa
        minimapCamera = GameObject.Find("Canvas_Escenarios").transform.GetChild(0).gameObject;
        minimap = transform.GetChild(2).gameObject;
        //Buscamos los botones de tipo de control
        joystickButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        minimapButton = transform.GetChild(0).GetChild(1).GetComponent<Button>();
    }

    public void Update(){

        if (usandoJoystick){
            //MOVER EL TARGET CON EL JOYSTICK
            if (target.activeSelf){

                // Retoque Borja
                target.transform.position += inputVector * targetSpeed * Time.deltaTime;
            }

            //CUANDO TENEMOS UN PERSONAJE SELECIONADO
            if (character != null)
            {
                //SI ESTAMOS EN MODO JOYSTICK
                if (joystick.activeSelf)
                {
                    //SI NO ESTAMOS EN MODO NAV_MESH
                    if (character.GetComponent<NavMeshAgent>().enabled == false)
                    {

                        if (character.name != "Donna_X"){
                            // Retoque Borja
                            // EJE Y
                            if (character.transform.localPosition.y <= 485.0f)
                            {
                                if (inputVector.z > 0)
                                {
                                    character.transform.position += new Vector3(0, 0, inputVector.z * targetSpeed * Time.deltaTime);
                                }
                            }
                            if (character.transform.localPosition.y >= -485.0f)
                            {
                                if (inputVector.z < 0)
                                {
                                    character.transform.position += new Vector3(0, 0, inputVector.z * targetSpeed * Time.deltaTime);
                                }
                            }

                            // EJE X
                            if (character.transform.localPosition.x <= 900.0f)
                            {
                                if (inputVector.x > 0)
                                {
                                    character.transform.position += new Vector3(inputVector.x * targetSpeed * Time.deltaTime, 0, 0);
                                }
                            }
                            if (character.transform.localPosition.x >= -710.0f)
                            {

                                if (inputVector.x < 0)
                                {
                                    character.transform.position += new Vector3(inputVector.x * targetSpeed * Time.deltaTime, 0, 0);
                                }
                            }
                        }

                        #region ESPECÍFICO DE DONNA
                        if (character.name == "Donna_X"){
                            if (character.transform.parent.gameObject.name == "Draggeables_DetrasX"){
                                if (character.transform.localPosition.y < 15.0f){
                                    //print("PASA DELANTE");
                                    character.transform.SetParent(character.transform.parent.parent.GetChild(6).transform);
                                }
                            }
                            if (character.transform.parent.gameObject.name == "Draggeables_DelanteX")
                            {
                                //print("Donna está en Reuniones delante a altura " + character.transform.localPosition.y);

                                if (character.transform.localPosition.y >= 10.0f){
                                    //print("PASA DETRAS");
                                    character.transform.SetParent(character.transform.parent.parent.GetChild(2).transform);
                                }
                            }

                            Transform posPadre = character.transform.parent.parent.transform;
                            float Distance_y = Vector3.Distance(Vector3.up * posPadre.localPosition.y, Vector3.up * character.transform.localPosition.y);

                            #region MOVIMIENTO CAPADO VERSION DONNA
                            // EJE Y
                            if (Distance_y <= 400.0f)
                            {
                                if (inputVector.z > 0)
                                {
                                    character.transform.position += new Vector3(0, 0, inputVector.z * targetSpeed * Time.deltaTime);
                                }
                            }

                            if (Distance_y >= 170.0f){
                                if (inputVector.z < 0){
                                    character.transform.position += new Vector3(0, 0, inputVector.z * targetSpeed * Time.deltaTime);
                                }
                            }

                            if (posPadre.localEulerAngles.z == 0){
                                //print("Está abajo");

                                // EJE X
                                if (character.GetComponent<RectTransform>().anchoredPosition.x < 800){
                                    if (inputVector.x > 0){
                                        character.transform.position += new Vector3(inputVector.x * targetSpeed * Time.deltaTime, 0, 0);
                                    }
                                }

                                if (character.GetComponent<RectTransform>().anchoredPosition.x > -800){
                                    if (inputVector.x < 0){
                                        character.transform.position += new Vector3(inputVector.x * targetSpeed * Time.deltaTime, 0, 0);
                                    }
                                }
                            }
                            else{
                                //print("Está arriba");

                                // EJE X
                                if (character.GetComponent<RectTransform>().anchoredPosition.x > -800)
                                {
                                    if (inputVector.x > 0)
                                    {
                                        character.transform.position += new Vector3(inputVector.x * targetSpeed * Time.deltaTime, 0, 0);
                                    }
                                }

                                if (character.GetComponent<RectTransform>().anchoredPosition.x < 800)
                                {
                                    if (inputVector.x < 0)
                                    {
                                        character.transform.position += new Vector3(inputVector.x * targetSpeed * Time.deltaTime, 0, 0);
                                    }
                                }
                            }
                            #endregion
                     }
                        #endregion
                    }
                    //SI ESTAMOS EN MODO NAV_MESH
                    else{
                        if (Vector2.Distance(character.transform.position, hit.point) < 0.09f){
                            target.gameObject.SetActive(false);
                            target.gameObject.transform.localPosition = new Vector3(-432, 200, 0);
                            character.GetComponent<NavMeshAgent>().enabled = false;
                            character = null;
                        }
                    }
                }

                #region//SI ESTAMOS EN MODO MINIMAPA
                /*if (minimap.activeSelf) {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Physics.Raycast(minimapCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out destination);
                    }
                    if (destination.point != Vector3.zero)
                    {
                        //SI NO ESTAMOS EN MODO NAV_MESH
                        if (character.GetComponent<NavMeshAgent>().enabled == false)
                        {
                            character.transform.position = Vector3.MoveTowards(character.transform.position, destination.point, Time.deltaTime * character.GetComponent<NavMeshAgent>().speed);
                            if (Vector3.Distance(character.transform.position, destination.point) < 0.09f)
                            {
                                character = null;
                                destination.point = Vector3.zero;
                            }
                        }
                        //SI ESTAMOS EN MODO NAV_MESH
                        else
                        {
                            if (character.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
                            {
                                character.GetComponent<NavMeshAgent>().SetDestination(new Vector3(destination.point.x, 0, destination.point.z));
                                if (Vector2.Distance(character.transform.position, destination.point) < 0.09f)
                                {
                                    character.GetComponent<NavMeshAgent>().enabled = false;
                                    character = null;
                                    destination.point = Vector3.zero;
                                }
                            }
                            else
                            {
                                character.GetComponent<NavMeshAgent>().enabled = false;
                                character = null;
                                destination.point = Vector3.zero;
                            }
                        }
                    }
                }*/
#endregion
            }
        }
    }
    
    //CUANDO MOVEMOS EL JOYSTICK
    public virtual void OnDrag(PointerEventData eventData){

        if (usandoJoystick){
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            {
                pos.x = (pos.x / joystickImage.rectTransform.sizeDelta.x);
                pos.y = (pos.y / joystickImage.rectTransform.sizeDelta.y);

                inputVector = new Vector3(pos.x * 3, 0, pos.y * 3);
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                joystickBall.rectTransform.anchoredPosition = new Vector3(inputVector.x * (joystickImage.rectTransform.sizeDelta.x / 3), inputVector.z * (joystickImage.rectTransform.sizeDelta.y / 3));

                if (character != null && transform.root.GetComponent<Root>().currentScene == 1)
                {
                    Quaternion newRot = Quaternion.LookRotation(inputVector);
                    character.transform.rotation = newRot;
                }
            }
        }
    }

    //CUANDO PUSAMOS EN CUALQUIER PARTE (ESTA FUNCION ES NECESARIA PARA QUE FUNCIONE LA DE CUANDO SOLTAMOS)
    public void OnPointerDown(PointerEventData eventData){
        if (!usandoJoystick) {
            usandoJoystick = true;
        }
    }

    //CUANDO SOLTAMOS EL JOYSTICK
    public virtual void OnPointerUp(PointerEventData eventData){

        if (usandoJoystick) { usandoJoystick = false; }

        inputVector = Vector3.zero;
        joystickBall.rectTransform.anchoredPosition = Vector3.zero;

    }

    //BOTON GO DEL JOYSTICK PARA HACER QUE SE MUEVAN LOS PERSONAJES
    public void GoButton() {
        Physics.Raycast(target.transform.position + new Vector3(0, 5, 0), Vector3.down * 10, out hit);
        if (character.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
        {
            character.GetComponent<NavMeshAgent>().SetDestination(new Vector3(hit.point.x, 0, hit.point.z));
        }
        else
        {
            target.gameObject.SetActive(false);
            target.gameObject.transform.localPosition = new Vector3(-432, 200, 0);
            character.GetComponent<NavMeshAgent>().enabled = false;
            character = null;
        }
    }

    //FUNCION PARA ACTIVAR EL MODO JOYSTICK O MINIMAP
    public void TipoControlNPC(int tipo) {
        if (tipo == 1)
        {
            minimap.SetActive(false);
            minimapCamera.SetActive(false);
            minimapButton.GetComponent<Image>().color = Color.white;
            joystick.SetActive(true);
            joystickButton.GetComponent<Image>().color = Color.cyan;
        }
        if (tipo == 2)
        {
            joystick.SetActive(false);
            joystickButton.GetComponent<Image>().color = Color.white;
            minimap.SetActive(true);
            minimapCamera.SetActive(true);
            minimapButton.GetComponent<Image>().color = Color.cyan;
        }
    }
}