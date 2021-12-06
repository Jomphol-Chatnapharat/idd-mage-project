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

    public bool isDead = false;

    public Rigidbody enemyRb;
    public int bodyDmg;

    public float minSpeed;
    public float maxSpeed = 30f;

    bool wasGrounded;
    bool wasFalling;
    float startOfFall;

    public float distToGround = 1f;
    bool _grounded = false;
    public float FallDamage;
    
    [SerializeField] private Image fillImage;

    private EnemyAI AI;
    private NavMeshAgent navMeshAgent;

    public bool isShieldOn;
    public GameObject shield;

    public float refundMana;

    public bool unlease = true;

    bool isStun = false;
    public float stunTime;

    public float bodyHp;

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

            shield.SetActive(false);
        }

        if (CurrentHp <= 0)
        {
            enemyRb.freezeRotation = false;
            AI.enabled = false;
            navMeshAgent.enabled = false;

            isDead = true;
        }

        if (enemyRb.velocity.magnitude > maxSpeed)
        {
            enemyRb.velocity = Vector3.ClampMagnitude(enemyRb.velocity, maxSpeed);
        }

        if (bodyHp <= 0)
        {
            Destroy(this.gameObject);
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

        if (_grounded && unlease && AI.enabled == false && !isDead)
        {
            isStun = true;
            Invoke(nameof(AIEnable), stunTime);
        }

        //if (_grounded)
        //{
        //    AI.enabled = true;
        //    navMeshAgent.enabled = true;
        //}

        wasGrounded = _grounded;
        wasFalling = isFalling;
    }

    void AIEnable()
    {
        AI.enabled = true;
        navMeshAgent.enabled = true;

        isStun = false;
    }

    void CheckGround()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    bool isFalling { get { return (!_grounded && enemyRb.velocity.y < 0); } }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 vel = enemyRb.velocity;

            if (other.gameObject.tag == "Enemy")
            {
                
                //Debug.Log(vel.magnitude);
                if (vel.magnitude > minSpeed)
                {
                    other.gameObject.GetComponent<SimpleEnemy>().OnDamaged(bodyDmg);

                    OnDamaged(bodyDmg);

                enemyRb.velocity = Vector3.one;
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

        if (CurrentHp <= 0)
        {
            bodyHp -= 1;
        }
        UIManager.instance.SetDamagePopupText("-" + Damage, transform.position);
        SetHealthImageAmount(CurrentHp / MaxHp);
    }
    
    public void SetHealthImageAmount(float newAmount)
    {
        fillImage.fillAmount = newAmount;
    }
}
