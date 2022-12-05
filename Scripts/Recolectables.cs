using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolectables : Interactable
{
    //[SerializeField]
    private int _puntos = 1;
    public override void Interact(PlayerController player)
    {
        player.puntos(_puntos);
        Destroy(gameObject); //Dar puntos al jugador
    }
}
