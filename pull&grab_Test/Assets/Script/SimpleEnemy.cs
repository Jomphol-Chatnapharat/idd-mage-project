using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float minFall = 2f;

    public float MaxHp;
    public float CurrentHp;
    public Rigidbody enemyRb;
    public int bodyDmg;

    public float minSpeed;

    bool wasGrounded;
    bool wasFalling;
    float startOfFall;

    bool _grounded = false;
    public float FallDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentHp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHp <= 60)
        {
            this.gameObject.layer = 0;
        }

        if (CurrentHp <= 0)
        {
            enemyRb.freezeRotation = false;
        }
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (!wasFalling && isFalling) 
        {
            startOfFall = transform.position.y;
        }

        if(!wasGrounded && _grounded)
        {
            float fallDistance = startOfFall - transform.position.y;

            if (fallDistance > minFall)
            {
                CurrentHp -= FallDamage;

                //enemyRb.freezeRotation = false;
            }
        }

        wasGrounded = _grounded;
        wasFalling = isFalling;
    }

    void CheckGround()
    {
        _grounded = Physics.Raycast(transform.position + Vector3.up, -Vector3.up, 1.01f);
    }

    bool isFalling { get { return (!_grounded && enemyRb.velocity.y < 0); } }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 vel = enemyRb.velocity;

        if (enemyRb.freezeRotation == false)
        {
            if (other.gameObject.tag == "Enemy")
            {
                
                //Debug.Log(vel.magnitude);
                if (vel.magnitude > minSpeed)
                {
                    OnDamaged(bodyDmg);
                }
            }
        }
    }

    public void OnDamaged(int Damage)
    {
        CurrentHp -= Damage;
    }
}
