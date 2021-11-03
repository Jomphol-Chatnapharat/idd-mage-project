using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 vel = BoxRb.velocity;

            Debug.Log(vel.magnitude);
            if (vel.magnitude > minSpeed)
            {
                other.gameObject.GetComponent<SimpleEnemy>().OnDamaged(BoxDmg);

            }
        }
    }
}
