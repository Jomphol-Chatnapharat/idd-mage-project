using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("Attacked");
        return this;
    }
}
