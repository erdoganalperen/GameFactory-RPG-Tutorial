using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMaterial : MonoBehaviour
{
    [SerializeField] private Material disolve;
    void Start()
    {
        GetComponent<Renderer>().material = disolve;
        GetComponent<SpawnEffect>().enabled = true;
    }
    
}
