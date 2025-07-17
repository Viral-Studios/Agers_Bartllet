using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltavocesTest : MonoBehaviour {

    public List<AudioSource> Audios;
    public List<string> Textos;

	public void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            Audios[0].Play();
            print(Audios[0].gameObject.name);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            Audios[1].Play();
            print(Audios[1].gameObject.name);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            Audios[2].Play();
            print(Audios[2].gameObject.name);
        }
    }
}
