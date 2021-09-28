using System;
using UnityEngine;


public class Shield : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        _playerHealth.Shielded = true;
    }

    private void OnDisable()
    {
        _playerHealth.Shielded = false;
    }
}