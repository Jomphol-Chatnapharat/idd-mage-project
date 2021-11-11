using System;
using EPOOutline;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Rigidbody BoxRb;
    public float minSpeed;
    public int BoxDmg;

    private GameObject TargetEnemy;
   
    void Awake()
    {
        BoxRb = GetComponent<Rigidbody>();
        //TargetEnemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    

    private void OnMouseEnter()
    {
        if (!BoxRb.isKinematic)
        {
            GetComponent<Outlinable>().enabled = true;
        }
       
    }

    private void OnMouseExit()
    {
        GetComponent<Outlinable>().enabled = false;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 vel = BoxRb.velocity;

            if (vel.magnitude > minSpeed)
            {
                other.gameObject.GetComponent<SimpleEnemy>().OnDamaged(BoxDmg);

            }
        }
    }
}
