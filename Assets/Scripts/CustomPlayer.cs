using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayer : MonoBehaviour {

    public int jugador;

    [Header("Sprites ARTE")]
    public List<Image> RanuraBrilla;
    public List<Image> capasPersonaje;
    public List<Image> capasPersonajeIngame;

    public void BrilloRanuras(int num){

        // BRILLO GÉNERO
        if (num >= 0 && num <= 1){

            for (int i = 0; i < 2; i++){
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }

        // BRILLO PIEL
        if (num >= 2 && num <= 5){

            for (int i = 2; i < 6; i++){

                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }

        // BRILLO PELO
        if (num >= 6 && num <= 7)
        {

            for (int i = 6; i < 8; i++)
            {
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }

        // BRILLO COLOR PELO
        if (num >= 8 && num <= 12)
        {

            for (int i = 8; i < 13; i++)
            {
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }

        // BRILLO ROPA
        if (num >= 13 && num <= 17)
        {

            for (int i = 13; i < 18; i++)
            {
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }

        // BRILLO ACCESORIOS
        if (num >= 18 && num <= 23)
        {

            for (int i = 18; i < 24; i++)
            {
                if (i == num) RanuraBrilla[i].color = Color.white;
                else RanuraBrilla[i].color = Color.grey;
            }
        }
    }
}
