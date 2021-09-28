using System;
using UnityEngine;


public class Heal : MonoBehaviour
{
    public float healAmount = 20f;

    private void Start()
    {
        GameObject player=GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerHealth>().HealPlayer(healAmount);
    }
}