using System.Collections;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Rigidbody BoxRb;
    public float minSpeed;

    public float maxSpeed;
    public int BoxDmg;

    private GameObject TargetEnemy;

    public Box Instance;

    void Awake()
    {
        BoxRb = GetComponent<Rigidbody>();

        //TargetEnemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);
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

            if (other.gameObject.GetComponent<SimpleEnemy>().CurrentHp <= 0)
            {
                invTest inventory = FindObjectOfType<invTest>();

                other.gameObject.GetComponent<SimpleEnemy>().StoreEnemy();

                if (!inventory.invFull)
                {
                    BoxDmg += 10;
                }
            }

            BoxRb.velocity = Vector3.one;
        }
    }
}
