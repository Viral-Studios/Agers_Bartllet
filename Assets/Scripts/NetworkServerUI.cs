using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Networking;
//using UnityEngine.Networking.NetworkSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class NetworkServerUI : MonoBehaviour {

   /* public InputField campoPropio;
    public NetworkClient cliente;

    [Header("Campos Según Herramientas")]
    public List<InputField> DocBlanco_campos = new List<InputField>(5);
    public List<InputField> DAFO = new List<InputField>(4);

    private void OnGUI(){

        string ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connect:" + NetworkServer.connections.Count);
    }

    void Start () {
        NetworkServer.Listen(25000);
        NetworkServer.RegisterHandler(888, ServerReceiveMessage);
    }

    public void ServerReceiveMessage(NetworkMessage message){

        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        //string[] deltas = msg.value.Split('|');
        campoPropio.text = msg.value;
        print("Refresca");
    }

    public void Refrescar(){
        NetworkServer.RegisterHandler(888, ServerReceiveMessage);
    }

    public void Test(){
        print("hello");

        StringMessage msg = new StringMessage();
        msg.value = campoPropio.text;
        NetworkServer.SendToClient(0, 889, msg);
    }

    #region REINICIAR Y SALIR
    public void Reiniciar(){
        NetworkServer.ResetConnectionStats();
        NetworkServer.Reset();
        SceneManager.LoadScene("Host");
    }

    public void Salir(){
        NetworkServer.ResetConnectionStats();
        NetworkServer.Reset();
        Application.Quit();
    }
    #endregion

    */
}
