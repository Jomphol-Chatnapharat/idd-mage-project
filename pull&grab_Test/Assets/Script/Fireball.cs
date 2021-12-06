using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerBehavior player))
        {
            player.currentHP -= damage;
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);

    }
}
