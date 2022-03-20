using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class potionCan : MonoBehaviour
{
    public Rigidbody BoxRb;
    public float minSpeed;

    public float maxSpeed;
    public int BoxDmg;

    private GameObject TargetEnemy;

    void Awake()
    {
        BoxRb = GetComponent<Rigidbody>();

        //TargetEnemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        if (BoxRb.velocity.magnitude > maxSpeed)
        {
            BoxRb.velocity = Vector3.ClampMagnitude(BoxRb.velocity, maxSpeed);
        }
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

            if (other.gameObject.GetComponent<SimpleEnemy>().CurrentHp > 0)
            {
                if (vel.magnitude > minSpeed)
                {
                    other.gameObject.GetComponent<SimpleEnemy>().OnDamaged(BoxDmg);
                }
            }

            BoxRb.velocity = Vector3.one;
        }
    }
}
