using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
   public float time;
   private void Start()
   {
      Destroy(this.gameObject,time);
   }
}
