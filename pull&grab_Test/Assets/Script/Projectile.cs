using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    public float force;

    public void Shoot()
    {
        Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        rb.AddForce(transform.up * (force / 8), ForceMode.Impulse);
    }
}
