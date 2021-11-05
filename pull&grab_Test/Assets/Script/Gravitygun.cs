using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitygun : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;
    [SerializeField] LayerMask LayerMask;

    Rigidbody grabbedRB;

    public float maxMana;
    public float currentMana;

    public float useMana;
    public float regenMana;

    void Start()
    {
        currentMana = maxMana;
    }

    void Update()
    {
        if (currentMana < maxMana)
        {
            currentMana += regenMana * Time.deltaTime;
        }

        if (grabbedRB)
        {
            grabbedRB.MovePosition(objectHolder.transform.position);

            if (Input.GetMouseButtonDown(0))
            {
                if (currentMana >= useMana)
                {
                    grabbedRB.isKinematic = false;
                    grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                    grabbedRB = null;

                    currentMana -= useMana;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedRB)
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if(Physics.Raycast(ray, out hit, maxGrabDistance, LayerMask))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if (grabbedRB)
                    {
                        grabbedRB.isKinematic = true;
                    }
                }
            }
        }
    }

    void ManaRegen()
    {
        currentMana += regenMana;
    }
}
