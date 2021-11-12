using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public GameObject currentHitObject;

    public SphereCollider detectCol;
    public LayerMask layerMask;

    private float currentHitDistance;

    public ChaseState chaseState;
    public bool canSeePlayer;

    private void Start()
    {
        detectCol = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            canSeePlayer = true;
        }
    }

    public override State RunCurrentState()
    {
        if (canSeePlayer == true)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
