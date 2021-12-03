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
    public Text potionIndicator;
    public Text aetherIndicator;


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

        potionIndicator.text = "Potion: " + potionLeft;
        aetherIndicator.text = "Aether: " + aetherLeft;
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

                        if (grabObj.GetComponent<SimpleEnemy>() != null)
                        {
                            grabObj.GetComponent<SimpleEnemy>().unlease = true;
                            Debug.Log("let go");
                        }

                        //if (grabObj.GetComponent<EnemyAI>() != null)
                        //{
                        //        grabObj.GetComponent<EnemyAI>().enabled = true;
                        //        grabObj.GetComponent<NavMeshAgent>().enabled = true;
                        //}

                        currentMana -= useMana;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (grabbedRB)
                {
                    if (grabObj.GetComponent<SimpleEnemy>() != null)
                    {
                        grabObj.GetComponent<SimpleEnemy>().unlease = true;
                    }

                    grabbedRB.isKinematic = false;
                    grabbedRB = null;
                    //if (grabObj.GetComponent<EnemyAI>() != null)
                    //{
                    //    grabObj.GetComponent<EnemyAI>().enabled = true;
                    //    grabObj.GetComponent<NavMeshAgent>().enabled = true;
                    //}
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

                            if (grabObj.GetComponent<SimpleEnemy>() != null)
                            {
                                grabObj.GetComponent<SimpleEnemy>().unlease = false;
                            }

                            if (grabObj.GetComponent<EnemyAI>() != null) 
                            {
                                grabObj.GetComponent<EnemyAI>().enabled = false;
                                grabObj.GetComponent<NavMeshAgent>().enabled = false;
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (currentHP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }

        SetHealthImageAmount(currentHP / maxHP);
        SetManaImageAmount(currentMana / maxMana);

<<<<<<< Updated upstream
=======
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (potionLeft > 0)
            {
                currentHP += potionHeal;
                potionLeft -= 1;
                potionIndicator.text = "Potion: " + potionLeft;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (aetherLeft > 0)
            {
                currentMana += aetherHeal;
                aetherLeft -= 1;
                aetherIndicator.text = "Aether: " + aetherLeft;
            }
        }

>>>>>>> Stashed changes
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
