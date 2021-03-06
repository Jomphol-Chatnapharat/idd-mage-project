using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    public float minFall = 2f;

    public float MaxHp;
    public float CurrentHp;
    public float MaxArmor;
    public float CurArmor;

    public Rigidbody enemyRb;
    public int bodyDmg;

    public float minSpeed;

    bool wasGrounded;
    bool wasFalling;
    float startOfFall;

    bool _grounded = false;
    public float FallDamage;
    
    [SerializeField] private Image fillImage;

    private EnemyAI AI;
    private NavMeshAgent navMeshAgent;

    
    // Start is called before the first frame update
    void Start()
    {
        CurrentHp = MaxHp;
        CurArmor = MaxArmor;

        AI = GetComponent<EnemyAI>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if(CurArmor == 0)
        {
            this.gameObject.layer = 0;
        }

        if (CurrentHp <= 0)
        {
            enemyRb.freezeRotation = false;
            AI.enabled = false;
            navMeshAgent.enabled = false;
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

        if (this.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Enemy")
            {
                
                //Debug.Log(vel.magnitude);
                if (vel.magnitude > minSpeed)
                {
                    other.gameObject.GetComponent<SimpleEnemy>().OnDamaged(bodyDmg);
                    OnDamaged(bodyDmg);
                }
            }
        }
    }

    public void OnDamaged(int Damage)
    {
        CurArmor -= Damage;
        if (CurArmor <= 0)
        {
            CurrentHp += CurArmor;
            CurArmor = 0;
        }

        UIManager.instance.SetDamagePopupText("-" + Damage, transform.position);
        SetHealthImageAmount(CurrentHp / MaxHp);
    }
    
    public void SetHealthImageAmount(float newAmount)
    {
        fillImage.fillAmount = newAmount;
    }

}
