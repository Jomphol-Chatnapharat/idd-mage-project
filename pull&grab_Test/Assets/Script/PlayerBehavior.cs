using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;
    [SerializeField] LayerMask LayerMask;
    [SerializeField] GameObject grabObj;

    Rigidbody grabbedRB;

    public Image hpBar;
    public Image manaBar;


    public float maxHP;
    public float currentHP;
    public float regenHP;

    public float maxMana;
    public float currentMana;

    public float useMana;
    public float regenMana;

    public bool isCharging = false;

    void Start()
    {
        currentHP = maxHP;
        currentMana = maxMana;
    }

    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            isCharging = true;
            ManaRegen();
        }
        if (Input.GetMouseButtonUp(1))
        {
            isCharging = false;
        }

        if (isCharging == false)
        {
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

                        if (grabObj.GetComponent<EnemyAI>() != null)
                        {
                            grabObj.GetComponent<EnemyAI>().enabled = true;
                            grabObj.GetComponent<NavMeshAgent>().enabled = true;
                        }

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
                    if (grabObj.GetComponent<EnemyAI>() != null)
                    {
                        grabObj.GetComponent<EnemyAI>().enabled = true;
                        grabObj.GetComponent<NavMeshAgent>().enabled = true;
                    }
                }
                else
                {
                    RaycastHit hit;
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                    if (Physics.Raycast(ray, out hit, maxGrabDistance, LayerMask))
                    {
                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                        if (grabbedRB)
                        {
                            grabbedRB.isKinematic = true;
                            grabObj = grabbedRB.gameObject;

                            if (grabObj.GetComponent<EnemyAI>() != null) 
                            {
                                grabObj.GetComponent<EnemyAI>().enabled = false;
                                grabObj.GetComponent<NavMeshAgent>().enabled = false;

                            }
                        }
                    }
                }
            }

            if (currentHP <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        SetHealthImageAmount(currentHP / maxHP);
        SetManaImageAmount(currentMana / maxMana);

    }

    void ManaRegen()
    {
        if (currentMana < maxMana || currentHP < maxHP)
        {
            currentMana += regenMana * Time.deltaTime;
            currentHP += regenHP * Time.deltaTime;


        }
    }

    public void SetHealthImageAmount(float newAmount)
    {
        hpBar.fillAmount = newAmount;
    }

    public void SetManaImageAmount(float newAmount)
    {
        manaBar.fillAmount = newAmount;
    }
}
