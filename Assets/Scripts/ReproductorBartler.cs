using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ReproductorBartler : MonoBehaviour {

    public List<VideoClip> ListadoclipsVideo;
    public List<UnityEngine.UI.Text> textos;

    public GameObject contenidos;

    public Animator cortinilla;
    public Animator cortinillaTablet;

    public bool cortinillaBool;
    public bool cortinillaTabletBool;

    public List<VideoPlayer> salidasVideo;
    public List<Sprite> iconosReprod;
    public List<UnityEngine.UI.Image> botonesPlay;
    public VideoPlayer salidaVideoElegida;

    public bool animarVideo1;
    public bool animarVideo2;

    [Header("1º F Tablet, 2º F. Monitor")]
    // Terremoto, Interferencias, Ruido, No Signal, Hackers.
    public List<Animator> filtrosAnim;
    public List<int> filtroSelecc;
    public List<UnityEngine.UI.InputField> filtrosInfo;
    [Header("Faldones")]
    public List<UnityEngine.UI.InputField> faldonesLista;
    public List<Animator> faldonesAnim;
    public List<GameObject> faldonesImag;

    public GameObject CamAux;
    public List<UnityEngine.UI.InputField> infoTabletMonitor;

    // Use this for initialization
    void Start(){

        for (int i = 0; i < ListadoclipsVideo.Count; i++){
            contenidos.transform.GetChild(i).name = ListadoclipsVideo[i].name;
            contenidos.transform.GetChild(i).transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "" + ListadoclipsVideo[i].name;
        }
    }

    public void AsignarSalidaVideo(int num){

        salidaVideoElegida = salidasVideo[num];
    }

    public bool audioOn = false;

    public void AsignarClipVideo (int num){
 
        salidaVideoElegida.clip = ListadoclipsVideo[num];
        //salidaVideoElegida.SetTargetAudioSource (0, salidaVideoElegida.GetComponent<AudioSource>());

        salidaVideoElegida.GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(0, 0, 0, 0);
        salidaVideoElegida.Stop();

        if (num == 1){
            cortinillaBool = false;
        }else{
            cortinillaTabletBool = false;
        }

        for (int i = 0; i < 2; i++) {
            if (salidaVideoElegida == salidasVideo[i]){
                botonesPlay[i].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[1];
                VideosSelecc[i] = num;
            }
        }

        for (int i = 0; i < 2; i++){
           if (salidaVideoElegida == salidasVideo[i]){
                textos[i].text = ListadoclipsVideo[num].name;
            }
        }
    }

    private string aux;

    public void Reproductores(int num){

        if (!recup)
        {
            if (!salidasVideo[num].isPlaying){

                salidasVideo[num].enabled = true;
                salidasVideo[num].Play();
                salidasVideo[num].GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(255, 255, 255, 255);
                botonesPlay[num].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[0];

                // Salta la cortinilla bienvenida
                if (num == 1 && salidasVideo[num].clip != null){
                    if (!cortinilla.enabled && !cortinillaBool){
                        cortinilla.enabled = true;
                        cortinilla.GetComponent<AudioSource>().enabled = true;
                        cortinillaBool = true;
                    }

                    aux = salidasVideo[num].clip.name;
                }

                if (num == 0 && salidasVideo[num].clip != null){
                    if (!cortinillaTablet.enabled && !cortinillaTabletBool){
                        cortinillaTablet.enabled = true;
                        cortinillaTablet.GetComponent<AudioSource>().enabled = true;
                        cortinillaTabletBool = true;
                    }

                    aux = salidasVideo[num].clip.name;
                }

            }else{
                salidasVideo[num].Pause();
                botonesPlay[num].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[1];
            }
        }else{

            for (int i = 0; i < 2; i++) {
                if (i == num) {
                    salidasVideo[0].clip = ListadoclipsVideo[VideosSelecc[num]];
                }
            }

            if (!salidasVideo[0].isPlaying){

                salidasVideo[0].enabled = true;
                salidasVideo[0].Play();
                salidasVideo[0].GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(255, 255, 255, 255);
                botonesPlay[0].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[0];
                botonesPlay[1].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[0];
            }
            else{
                salidasVideo[0].Pause();
                botonesPlay[0].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[1];
                botonesPlay[1].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[1];
            }
        }
    }

    public void ReproductoresStop(int num) {

        if (num == 1){
            cortinillaBool = false;
        }
        else{
            cortinillaTabletBool = false;

        }

        salidasVideo[num].GetComponent<UnityEngine.UI.RawImage>().color = Vector4.zero;
        salidasVideo[num].Stop();
        salidasVideo[num].enabled = false;

        if (faldonesAnim[num].GetCurrentAnimatorStateInfo(0).IsName ("Faldon0")){
            faldonesAnim[num].Play("Faldon1", 0, 0.975f);
            print("jejej");
        }

        if (faldonesAnim[num].GetCurrentAnimatorStateInfo(0).IsName("Faldon1_1")){
            faldonesAnim[num].Play("Faldon1_2", 0, 0.975f);
            print("jejej");
        }

        if (faldonesAnim[num].GetCurrentAnimatorStateInfo(0).IsName("Faldon2_1")){
            faldonesAnim[num].Play("Faldon2_2", 0, 0.975f);
            print("jejej");
        }

        Vector4 Color = faldonesImag[num+2].gameObject.GetComponent<UnityEngine.UI.Image>().color;
        faldonesImag[num+2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 0.0f);

        if (!recup){
            botonesPlay[num].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[1];
        }else{
            botonesPlay[0].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[1];
            botonesPlay[1].GetComponent<UnityEngine.UI.Image>().sprite = iconosReprod[1];
        }
    }

    /* *********** */
    public void FaldonesTituloSelecc(int num){
        for (int i = 0; i < 2; i++){
            if (salidaVideoElegida == salidasVideo[i]){
                faldonesAnim[i].transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = faldonesLista[num].text;
                infoTabletMonitor[i * 3].text = faldonesLista[num].text;
                //print("la salida de video " + i + " tiene el titulo " + num + ": " + faldonesLista[num].text);
            }
        }
    }

    public void FaldonesCuerpoSelecc(int num){
        for (int i = 0; i < 2; i++){
            if (salidaVideoElegida == salidasVideo[i]){
                faldonesAnim[i].transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = faldonesLista[num].text;
                infoTabletMonitor[(i * 3) + 1].text = faldonesLista[num].text;
                //print("la salida de video " + i + " tiene el cuerpo " + num + ": " + faldonesLista[num].text);
            }
        }
    }

    // ANIMATORS DE FALDONES DE TABLET Y PANTALLA AUXILIAR [0] [1]
    public void Faldones(int num){

            for (int i = 0; i < 2; i++){
                if (num == i){

                    if (!recup){
                        if (!faldonesAnim[i].enabled){
                            faldonesAnim[i].enabled = true;
                            faldonesImag[i].gameObject.SetActive(true);

                            Vector4 Color = faldonesImag[i + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color;
                            faldonesImag[i + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 255.0f);

                            if (faldonesAnim[num].transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text == ""){
                                faldonesAnim[num].transform.GetChild(0).gameObject.SetActive(false);
                            }else{
                                faldonesAnim[num].transform.GetChild(0).gameObject.SetActive(true);
                            }
                        }else{
                            faldonesAnim[i].SetTrigger("1");
                            faldonesImag[i].gameObject.SetActive(false);
                            Vector4 Color = faldonesImag[i + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color;
                            faldonesImag[i + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 0.0f);
                        }
                    }else {

                        if (num == 0){
                            faldonesAnim[0].transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = faldonesImag[2].transform.GetChild(0).GetComponent<UnityEngine.UI.InputField>().text;
                            faldonesAnim[0].transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = faldonesImag[2].transform.GetChild(1).GetComponent<UnityEngine.UI.InputField>().text;
                        }else{
                            faldonesAnim[0].transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = faldonesImag[3].transform.GetChild(0).GetComponent<UnityEngine.UI.InputField>().text;
                            faldonesAnim[0].transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = faldonesImag[3].transform.GetChild(1).GetComponent<UnityEngine.UI.InputField>().text;
                        }

                        if (!faldonesAnim[0].enabled){
                            faldonesAnim[0].enabled = true;
                            faldonesImag[0].gameObject.SetActive(true);

                            Vector4 Color = faldonesImag[2].gameObject.GetComponent<UnityEngine.UI.Image>().color;
                            faldonesImag[2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 255.0f);
                            faldonesImag[3].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 255.0f);

                            if (faldonesAnim[num].transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text == ""){
                                faldonesAnim[num].transform.GetChild(0).gameObject.SetActive(false);
                            }else{
                                faldonesAnim[num].transform.GetChild(0).gameObject.SetActive(true);
                            }
                        }else{
                            faldonesAnim[0].SetTrigger("1");
                            faldonesImag[0].gameObject.SetActive(false);
                            Vector4 Color = faldonesImag[2].gameObject.GetComponent<UnityEngine.UI.Image>().color;
                            faldonesImag[2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 0.0f);
                            faldonesImag[3].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 0.0f);
                        }

                    }
                }
            }
    }

    /*********************/
    public void AsignarFiltros(int num){

        if (salidaVideoElegida == salidasVideo[0]){
            filtroSelecc[0] = num;
            filtrosInfo[0].text = filtrosAnim[num].gameObject.name;
            Vector4 Color = filtrosInfo[0].transform.parent.GetComponent<UnityEngine.UI.Image>().color;
            filtrosInfo[0].transform.parent.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 0);
        }

        if (salidaVideoElegida == salidasVideo[1]){
            filtroSelecc[1] = num + 5;
            filtrosInfo[1].text = filtrosAnim[num + 5].gameObject.name;

            Vector4 Color = filtrosInfo[1].transform.parent.GetComponent<UnityEngine.UI.Image>().color;
            filtrosInfo[1].transform.parent.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 0);

            for (int i = 5; i < 10; i++){
                if (filtroSelecc[1] != num+5 && filtrosAnim[i].enabled){
                    print("entra en " + i);
                }
            }
        }
    }
    
    public void FiltrosBrillo (int num){

        for (int i = 0; i < 2; i++){
            if (num == i && salidasVideo[num].clip != null){

                if (filtrosInfo[num].transform.parent.GetComponent<UnityEngine.UI.Image>().color.a == 0){
                    Vector4 Color = filtrosInfo[num].transform.parent.GetComponent<UnityEngine.UI.Image>().color;
                    filtrosInfo[num].transform.parent.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 255);
                }else{
                    Vector4 Color = filtrosInfo[num].transform.parent.GetComponent<UnityEngine.UI.Image>().color;
                    filtrosInfo[num].transform.parent.GetComponent<UnityEngine.UI.Image>().color = new Vector4(Color.x, Color.y, Color.z, 0);
                }
            }
        }
    }

    public void Filtros (int num){

        if (!recup){
            for (int i = 0; i < 2; i++)
            {
                if (num == i && salidasVideo[i].clip != null)
                {

                    if (!filtrosAnim[filtroSelecc[num]].enabled)
                    {
                        filtrosAnim[filtroSelecc[num]].enabled = true;

                        if (filtrosAnim[filtroSelecc[num]].GetComponent<AudioSource>())
                        {
                            filtrosAnim[filtroSelecc[num]].GetComponent<AudioSource>().Play();
                        }
                    }
                    else
                    {

                        if (filtrosAnim[filtroSelecc[num]].GetComponent<AudioSource>())
                        {
                            filtrosAnim[filtroSelecc[num]].GetComponent<AudioSource>().Stop();
                        }

                        // - Terremotos -
                        if (filtroSelecc[num] == 0 || filtroSelecc[num] == 5)
                        {
                            filtrosAnim[filtroSelecc[num]].SetTrigger("1");
                        }

                        // - Interferencias -
                        if (filtroSelecc[num] == 1 || filtroSelecc[num] == 6)
                        {
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().enabled = false;
                        }

                        // - Ruido -
                        if (filtroSelecc[num] == 2 || filtroSelecc[num] == 7)
                        {
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().enabled = false;
                        }

                        // - No Signal -
                        if (filtroSelecc[num] == 3 || filtroSelecc[num] == 8)
                        {
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().enabled = false;
                        }

                        // - Hacker -
                        if (filtroSelecc[num] == 4 || filtroSelecc[num] == 9)
                        {
                            filtrosAnim[filtroSelecc[num]].SetTrigger("1");
                        }
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (filtroSelecc[0] != i && filtrosAnim[i].enabled)
                {
                    print("desactivas el filtro1 activado: " + i);
                    filtrosAnim[i].Rebind();
                    filtrosAnim[i].enabled = false;

                    if (filtrosAnim[i].GetComponent<AudioSource>())
                    {
                        filtrosAnim[i].GetComponent<AudioSource>().Stop();
                    }
                }
            }

            for (int i = 5; i < 10; i++)
            {
                if (filtroSelecc[1] != i && filtrosAnim[i].enabled)
                {
                    print("desactivas el filtro2 activado: " + (i - 5));
                    filtrosAnim[i].Rebind();
                    filtrosAnim[i].enabled = false;

                    if (filtrosAnim[i].GetComponent<AudioSource>())
                    {
                        filtrosAnim[i].GetComponent<AudioSource>().Stop();
                    }
                }
            }
        }else{

            if (salidasVideo[0].clip != null){

                #region
                if (num == 0){
                    print("LLamas a los animators originales de la tablet");
                    filtrosInfo[1].transform.parent.GetComponent<UnityEngine.UI.Image>().color = new Vector4(160, 160, 160, 0);

                    if (!filtrosAnim[filtroSelecc[0]].enabled){
                        filtrosAnim[filtroSelecc[0]].enabled = true;
                    }else{
                        if (filtrosAnim[filtroSelecc[0]].GetComponent<AudioSource>()){
                            filtrosAnim[filtroSelecc[num]].GetComponent<AudioSource>().Stop();
                        }

                        // - Terremotos -
                        if (filtroSelecc[num] == 0){filtrosAnim[filtroSelecc[num]].SetTrigger("1");}

                        // - Interferencias -
                        if (filtroSelecc[num] == 1){
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().enabled = false;
                        }

                        // - Ruido -
                        if (filtroSelecc[num] == 2){
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().enabled = false;
                        }

                        // - No Signal -
                        if (filtroSelecc[num] == 3){
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num]].GetComponent<Animator>().enabled = false;
                        }

                        // - Hacker -
                        if (filtroSelecc[num] == 4){
                            filtrosAnim[filtroSelecc[num]].SetTrigger("1");
                        }
                    }
                }

                if (num == 1) {
                    print("LLamas a los animators originales de la tv");
                    filtrosInfo[0].transform.parent.GetComponent<UnityEngine.UI.Image>().color = new Vector4(160, 160, 160, 0);

                    if (filtrosInfo[0].transform.parent.GetComponent<UnityEngine.UI.Image>().color.a == 1){
                        FiltrosBrillo(0);
                        print("andaa andaa");
                    }

                    if (!filtrosAnim[filtroSelecc[1]-5].enabled){
                        filtrosAnim[filtroSelecc[1]-5].enabled = true;
                    }else{
                        if (filtrosAnim[filtroSelecc[num] - 5].GetComponent<AudioSource>()){
                            filtrosAnim[filtroSelecc[num] - 5].GetComponent<AudioSource>().Stop();
                        }

                        // - Terremotos -
                        if (filtroSelecc[num] == 5){filtrosAnim[filtroSelecc[num] - 5].SetTrigger("1");}

                        // - Interferencias -
                        if (filtroSelecc[num] == 6){
                            filtrosAnim[filtroSelecc[num] - 5].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num] - 5].GetComponent<Animator>().enabled = false;
                        }

                        // - Ruido -
                        if (filtroSelecc[num] == 7){
                            filtrosAnim[filtroSelecc[num] - 5].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num] - 5].GetComponent<Animator>().enabled = false;
                        }

                        // - No Signal -
                        if (filtroSelecc[num] == 8){
                            filtrosAnim[filtroSelecc[num] - 5].GetComponent<Animator>().GetComponent<Animator>().Rebind();
                            filtrosAnim[filtroSelecc[num] - 5].GetComponent<Animator>().enabled = false;
                        }

                        // - Hacker -
                        if (filtroSelecc[num] == 9){
                            filtrosAnim[filtroSelecc[num] - 5].SetTrigger("1");
                        }
                    }
                }
                #endregion

                #region
                for (int i = 0; i < 5; i++){
                    if (num == 0 && filtroSelecc[0] != i && filtrosAnim[i].enabled){
                        print("desactivas el filtro1 activado: " + i);
                        filtrosAnim[i].Rebind();
                        filtrosAnim[i].enabled = false;

                        if (filtrosAnim[i].GetComponent<AudioSource>()){
                            filtrosAnim[i].GetComponent<AudioSource>().Stop();
                        }
                    }
                }

                for (int i = 5; i < 10; i++){
                    if (num == 1 && filtroSelecc[1] != i && filtrosAnim[i-5].enabled){
                        print("desactivas el filtro2 activado: " + (i - 5));
                        filtrosAnim[i - 5].Rebind();
                        filtrosAnim[i - 5].enabled = false;
                        if (filtrosAnim[i - 5].GetComponent<AudioSource>()){filtrosAnim[i - 5].GetComponent<AudioSource>().Stop();}
                    }
                }
                #endregion
            }
        }
    }

    public bool recup;
    public UnityEngine.UI.Image imagen;

    [Header("Recuperado")]
    public List<string> textosFaldonesFaldonRecup;
    public List<int> VideosSelecc;

    public void Recover(){
        recup = !recup;
        
        if (recup){
            imagen.color = Color.cyan;
            ReproductoresStop(0);
            ReproductoresStop(1);
        }else{
            imagen.color = Color.white;
        }
    }
}