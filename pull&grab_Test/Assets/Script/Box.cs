using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Rigidbody BoxRb;
    public float minSpeed;

    public float maxSpeed;
    public int BoxDmg;

    private GameObject TargetEnemy;

    public float bodyHp;

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

        if (bodyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //private void OnMouseEnter()
    //{
    //    if (!BoxRb.isKinematic)
    //    {
    //        GetComponent<Outlinable>().enabled = true;
    //    }
    //}
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 vel = BoxRb.velocity;

            if (vel.magnitude > minSpeed)
            {
                other.gameObject.GetComponent<SimpleEnemy>().OnDamaged(BoxDmg);

                bodyHp -= 1;
            }

            BoxRb.velocity = Vector3.one;
        }
    }
}
