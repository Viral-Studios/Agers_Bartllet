using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoPlayerScript : MonoBehaviour {

    #region // VARIABLES...
    public List<VideoClip> clipsVideo;
    private VideoPlayer reproductor;
    public int videoSelecc;

    #endregion

    #region // FUNCIONES...
    private void Start(){
        reproductor = GetComponent<VideoPlayer>();

        if (Display.displays.Length > 1){
            Display.displays[1].Activate();
        }

        if (Display.displays.Length > 2){
            Display.displays[2].Activate();
        }
    }

    public void PlayPause(){
        if (reproductor.isPlaying) reproductor.Pause();
        else reproductor.Play();
    }

    /*public void ReiniciarSalir(int num){

        if (num == 0){
            Application.LoadLevel(0);
        }

        if (num == 1){
            Application.Quit();
        }
    }*/

    public void SeleccVideos (int num) {
        
        if (num == 0){
            if (videoSelecc == 0){
                videoSelecc = clipsVideo.Count - 1;
            }else{
                videoSelecc--;
            }
        }

        if (num == 1){
            if (videoSelecc == clipsVideo.Count - 1){
                videoSelecc = 0;
            }else{
                videoSelecc++;
            }
        }

        reproductor.clip = clipsVideo[videoSelecc];
	}

    public Camera Monitor;
    [HideInInspector ()] public int videoSelecc_MonitorAux;

    public VideoPlayer reproductor_MonitorAux;

    public void SeleccVideosMonitorAux(int num){

        if (num == 0){
            if (videoSelecc_MonitorAux == 0) videoSelecc_MonitorAux = clipsVideo.Count - 1;
            else videoSelecc_MonitorAux--;
        }

        if (num == 1)
        {
            if (videoSelecc_MonitorAux == clipsVideo.Count - 1) videoSelecc_MonitorAux = 0;
            else videoSelecc_MonitorAux++;
        }

        reproductor_MonitorAux.clip = clipsVideo[videoSelecc_MonitorAux];
    }

    public void PlayPauseMonitorAux(){
        if (reproductor_MonitorAux.isPlaying) reproductor_MonitorAux.Pause();
        else reproductor_MonitorAux.Play();
    }
    #endregion
}
