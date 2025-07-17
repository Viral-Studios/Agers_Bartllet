using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reproductor : MonoBehaviour
{
    public AudioSource audioPlayer;
    private Text viewport;
    public Slider volume;
    private Image playPause;
    private int currentTrack;
    public List<Sprite> buttonIcon;
    public List<AudioClip> track;
    public List<bool> loopTrack;

    private void Start()
    {
        //Buscamos los componentes del reproductor
        audioPlayer = GetComponent<AudioSource>();
        viewport = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        //volume = transform.GetChild(1).GetComponent<Slider>();
        playPause = transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Image>();
    }

    public void Update()
    {
        //Mostramos el icono de play si el track actual finaliza
        /*if (audioPlayer.clip != null)
        {
            if (!loopTrack[currentTrack])
            {
                if (audioPlayer.clip.length == audioPlayer.time)
                {
                    playPause.sprite = buttonIcon[0];
                }
            }
        }*/
    }

    public void SelectorPistas(int num)
    {
        //ANTERIOR PISTA
        if (num == 0)
        {
            if (currentTrack > 0)
            {
                currentTrack--;
            }
            else
            {
                currentTrack = track.Count - 1;
            }
            UpdateAdioPlayer();
        }
        //PLAY/PAUSE
        if (num == 1)
        {
            if (audioPlayer.clip != null)
            {
                if (audioPlayer.isPlaying)
                {
                    audioPlayer.Pause();
                    playPause.sprite = buttonIcon[0];
                }
                else
                {
                    audioPlayer.UnPause();
                    playPause.sprite = buttonIcon[1];
                }
            }
            else {
                UpdateAdioPlayer();
            }
        }
        //SIGUIENTE PISTA
        if (num == 2)
        {
            if (currentTrack < track.Count - 1)
            {
                currentTrack++;
            }
            else
            {
                currentTrack = 0;
            }
            UpdateAdioPlayer();
        }
    }
    //ACTUALIZAR EL REPRODUCTOR
    public void UpdateAdioPlayer()
    {
        if (loopTrack[currentTrack]) { audioPlayer.loop = true; }
        else { audioPlayer.loop = false; }
        audioPlayer.clip = track[currentTrack];
        viewport.text = (currentTrack + 1).ToString() + ") " + track[currentTrack].name;
        audioPlayer.Play();
        playPause.sprite = buttonIcon[1];
    }
    //SI SE PULSA EL SLIDER EL VOLUMEN DEL AUDIOSOURCE SUBE O BAJA
    public void CambiarVolumen()
    {
        audioPlayer.volume = volume.value;
    }
}
