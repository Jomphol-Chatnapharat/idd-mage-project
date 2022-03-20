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
    [SerializeField] LayerMask LayerMask;
    [SerializeField] GameObject grabObj;
    public Transform objectHolder;

    public Rigidbody grabbedRB;
    public bool holdingObj = false;

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

    public float potionLeft;
    public float potionHeal;

    public float aetherLeft;
    public float aetherHeal;

    public Canvas invCanvas;
    public bool onInv = false;

    public float currentGold;
    public Text goldCount;

    public static PlayerBehavior Instance;

    public GameObject can;

    void Start()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);

        currentHP = maxHP;
        currentMana = maxMana;

        potionIndicator.text = "Potion: " + potionLeft;
        aetherIndicator.text = "Energy Drink: " + aetherLeft;
    }


    private void Awake()
    {
        invCanvas.gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (onInv)
            {
                invCanvas.gameObject.SetActive(false);
                onInv = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance, LayerMask))
                {
                    if (hit.collider.gameObject.GetComponent<Box>())
                    {
                        invCanvas.gameObject.SetActive(true);
                        onInv = true;

                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                    }
                }
            }
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
                            holdingObj = false;
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
                        holdingObj = false;
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
                            HoldingObj();
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (potionLeft > 0)
            {
                currentHP += potionHeal;
                potionLeft -= 1;
                potionIndicator.text = "Potion: " + potionLeft;

                Vector3 canPos = objectHolder.transform.position;
                Quaternion rotation = objectHolder.transform.rotation;
                Instantiate(can, canPos, rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (aetherLeft > 0)
            {
                currentMana += aetherHeal;
                aetherLeft -= 1;
                aetherIndicator.text = "Energy Drink: " + aetherLeft;

                Vector3 canPos = objectHolder.transform.position;
                Quaternion rotation = objectHolder.transform.rotation;
                Instantiate(can, canPos, rotation);
            }
        }
        goldCount.text = "Gold: " + currentGold;
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

    public void HoldingObj()
    {
        holdingObj = true;

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

    public void GetGold()
    {
        currentGold += 10;

    }
}
