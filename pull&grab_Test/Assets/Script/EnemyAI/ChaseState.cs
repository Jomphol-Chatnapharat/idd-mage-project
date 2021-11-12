using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public MeleeState attackState;
    public bool isInAttackRange;
    public override State RunCurrentState()
    {
        if (isInAttackRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
