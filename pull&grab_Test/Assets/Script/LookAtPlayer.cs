using System;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
   [SerializeField] private float damping = 5f;
   public Transform target;
   
   
   private void Start()
   {
      if (target == null)
      {
         target = FindObjectOfType<SFPSC_PlayerMovement>().transform;
      }
      
      var lookPos = target.position - transform.position;
      lookPos.y = 0;
      var rotation = Quaternion.LookRotation(lookPos);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
   }

   private void Update()
   {
      var lookPos = target.position - transform.position;
      lookPos.y = 0;
      var rotation = Quaternion.LookRotation(lookPos);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
   }
}
