using UnityEngine;
using UnityEngine.AI;

public class Player_Script : MonoBehaviour{

    public U_Invertida script;
    public int player;

    public Transform referencaScale;

    private NavMeshAgent myNavMesh;
    private GameObject reticula;

    [Header ("VARIABLES GIRO")]
    public Vector3 Vector3MesaMapaGeneral;
    public float rotX;
    public bool rotMesa;

    [HideInInspector] public GameObject label;
    public bool selected;
    public bool locked;

    private RaycastHit hit;

    public Transform detras;
    public Transform delante;

    public bool masterBlock;

    float offset = -155.0f;

    /*[HideInInspector]*/ public Vector3 targetVector;
	// Use this for initialization
	private void Start () {
        myNavMesh = GetComponent<NavMeshAgent>();
        reticula = transform.GetChild(0).gameObject;
        label = transform.GetChild(2).gameObject;

        if (detras == null && delante == null){
            masterBlock = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!locked && !transform.root.GetComponent<Root>().gamePaused){
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    //seleccion y deseleccion
                    if (hit.collider.name == gameObject.name){
                        if (!selected){
                            selected = true;
                            if (transform.root.GetComponent<Root>().navMeshMode)
                                myNavMesh.enabled = true;
                            targetVector = Vector3.zero;
                            reticula.SetActive(true);
                        }else{
                            selected = false;
                            rotMesa = false;

                            if (transform.root.GetComponent<Root>().navMeshMode)
                                myNavMesh.enabled = false;
                            targetVector = Vector3.zero;
                            reticula.SetActive(false);
                        }
                    }
                    if (hit.collider.name != "Pared" && hit.collider.tag != "Player"){
                        if (selected && targetVector == Vector3.zero){

                            if (hit.collider.name == "Suelo"){
                                targetVector = new Vector3(hit.point.x, 0, hit.point.z);
                            }

                            #region DIVISIÓN POR MAPAS
                            if (hit.collider.name == "Mapa General"){

                                script.int_mapaParticipantes[player] = 0;
                                script.UpdateAtril(player);
                                script.ResetAtril(player);

                                print("Va a Mapa General");
                                targetVector = new Vector3(hit.point.x, 0, hit.point.z);

                                script.AutoPlayer_MapaGeneral(player);
                            }

                            if (hit.collider.name == "Sala Descanso"){

                                script.int_mapaParticipantes[player] = 2;
                                script.UpdateAtril(player);
                                script.ResetAtril(player);

                                print("Sala Descanso");
                                targetVector = new Vector3(hit.point.x, 0, hit.point.z);
                                script.AutoPlayer_MapaSalaDescanso(player);
                            }

                            if (hit.collider.name == "Sala Working"){

                                script.int_mapaParticipantes[player] = 1;
                                script.UpdateAtril(player);
                                script.ResetAtril(player);

                                print("Sala Working");
                                targetVector = new Vector3(hit.point.x, 0, hit.point.z);
                                script.AutoPlayer_MapaSalaWorking(player);
                            }

                            if (hit.collider.name == "Sala Working Mesa"){

                                script.int_mapaParticipantes[player] = 1;
                                script.UpdateAtril(player);
                                script.ResetAtril(player);

                                print("Sala Working MESA");
                                targetVector = Vector3MesaMapaGeneral;
                                rotMesa = true;
                                script.AutoPlayer_MapaSalaWorking(player);
                            }

                            if (hit.collider.name == "Sala Comunicaciones"){

                                script.int_mapaParticipantes[player] = 3;
                                script.UpdateAtril(player);
                                print("Sala Comunicaciones");
                                targetVector = new Vector3(hit.point.x, 0, hit.point.z);
                                script.AutoPlayer_MapaComunicacion(player);
                            }

                            if (hit.collider.name == "Sala Reuniones"){

                                script.int_mapaParticipantes[player] = 4;
                                script.UpdateAtril(player);
                                script.ResetAtril(player);

                                print("Sala Reuniones");
                                targetVector = new Vector3(hit.point.x, 0, hit.point.z);
                                script.AutoPlayer_MapaReuniones(player);
                            }
                            #endregion
                        }
                    }

                    if (hit.collider.name == "ParedFix" && selected && targetVector == Vector3.zero){
                        print("entra en la peculiaridad");

                        if (transform.parent.parent.gameObject.transform.localEulerAngles.z == 180.0f){
                            print("está en panel izq " + transform.parent.parent.gameObject.transform.localEulerAngles.z + " " + transform.parent.parent.gameObject.name);
                            targetVector = new Vector3(hit.point.x, 0, hit.collider.gameObject.transform.position.z + hit.collider.gameObject.transform.forward.z * offset);
                        }
                        else
                        {
                            print("está en panel der " + transform.parent.parent.gameObject.transform.localEulerAngles.z + " " + transform.parent.parent.gameObject.name);
                            targetVector = new Vector3(hit.point.x, 0, hit.collider.gameObject.transform.position.z + hit.collider.gameObject.transform.forward.z * offset);
                        }
                    }
                }
            }

            if (selected){
                //Esta linea es para activar el navMesh cuando se mueve al jugador desde la opcion intercambio de posicion
                if (transform.root.GetComponent<Root>().navMeshMode) myNavMesh.enabled = true;

                if (!masterBlock){

                    print (transform.localPosition.y);

                    if (transform.parent.gameObject == delante.gameObject)
                    {
                        if (transform.localPosition.y > 15.0f)
                        {
                            //print("CAMBIO");
                            transform.SetParent(detras);
                        }
                    }

                    if (transform.parent.gameObject == detras.gameObject)
                    {
                        if (transform.localPosition.y <= 10.0f)
                        {
                            //print("CAMBIO a alante");
                            transform.SetParent(delante);
                        }
                    }
                }

                if (targetVector != Vector3.zero){
                    if (myNavMesh.enabled){
                        if (myNavMesh.pathStatus == NavMeshPathStatus.PathComplete){
                            myNavMesh.SetDestination(targetVector);
                            // Va aquúi
                        }else{
                            Desseleccion();
                        }
                    }else {
                        transform.position = Vector3.MoveTowards(transform.position, targetVector, Time.deltaTime * myNavMesh.speed);
                    }
                }
                if (Vector3.Distance(targetVector, transform.position) < 1f){

                    if (rotMesa){
                        this.gameObject.transform.localEulerAngles = new Vector3(rotX, -90, 90);
                        rotMesa = false;
                    }

                    selected = false;
                    myNavMesh.enabled = false;
                    targetVector = Vector3.zero;
                    reticula.SetActive(false);
                }
            }
        }else{
            selected = false;
            myNavMesh.enabled = false;
            targetVector = Vector3.zero;
            reticula.SetActive(false);
        }

        /*if (rotMesa && targetVector == Vector3.zero){

            float newRot = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, rotX, 100.0f * Time.deltaTime);
            transform.localEulerAngles = new Vector3(newRot, -90, 90);

            if (newRot == rotX || selected){
                rotMesa = false;
                targetVector = Vector3.zero;
                return;
            }
        }*/
    }

    public void Desseleccion(){
        selected = false;
        rotMesa = false;
        myNavMesh.enabled = false;
        targetVector = Vector3.zero;
        reticula.SetActive(false);
    }
    
    public void Update_CambioPadre(){
        if (transform.parent.gameObject == delante.gameObject)
        {
            if (transform.localPosition.y > 70.0f)
            {
                print("CAMBIO");
                transform.SetParent(detras);
            }
        }

        if (transform.parent.gameObject == detras.gameObject)
        {
            if (transform.localPosition.y <= 60.0f)
            {
                print("CAMBIO a alante");
                transform.SetParent(delante);
            }
        }
    }
}
