using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotCamera : MonoBehaviour {

    public bool rotar;
    public Transform[] panelesSalaDiv = new Transform[2];

	public void Rot(){

        rotar = !rotar;

        panelesSalaDiv[0].GetComponent<RectTransform>().localPosition = new Vector3(panelesSalaDiv[0].GetComponent<RectTransform>().localPosition.x,
                                                                        panelesSalaDiv[0].GetComponent<RectTransform>().localPosition.y * -1, 0);

        panelesSalaDiv[1].GetComponent<RectTransform>().localPosition = new Vector3(panelesSalaDiv[1].GetComponent<RectTransform>().localPosition.x,
                                                                        panelesSalaDiv[1].GetComponent<RectTransform>().localPosition.y * -1, 0);

        if (rotar){
            Camera.main.transform.localEulerAngles = new Vector3(90, 0, 180);
            Camera.main.transform.localPosition = new Vector3(7.45f, 0, 0);
            return;
        }else{
            Camera.main.transform.localEulerAngles = new Vector3(90, 0, 0);
            Camera.main.transform.localPosition = new Vector3(3.73f, 0, 0);
            return;
        }



    }
}
