using System;
using UnityEngine;

public class Spell : PlayerSkillDamage
{
    public GameObject explosion;
    public float speed=10;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    internal override void Update()
    {
        base.Update();
     transform.Translate(Vector3.forward*(speed*Time.deltaTime));   
        if (collided)
        {
            Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}