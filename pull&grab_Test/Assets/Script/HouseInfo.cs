using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInfo : MonoBehaviour
{
    public string houseID;
    public string houseName;

    public Collider deliveryBox;

    public bool orderActive = false;
    public int enemyNeed;

    public PlayerBehavior player;
    public OrderSystem orderSystem;

    private void Start()
    {
        orderSystem = FindObjectOfType<OrderSystem>();
        player = FindObjectOfType<PlayerBehavior>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (orderActive)
        {
            if (other.GetComponent<EnemyType>() != null)
            {
                EnemyType type = other.GetComponent<EnemyType>();

                if (enemyNeed == 1)
                {
                    if (type.type1 == true)
                    {
                        Destroy(other.gameObject);
                        orderActive = false;

                        orderSystem.haveOrder = false;
                        orderSystem.keeper.haveOrder = false;

                        player.currentGold += 30;
                        Debug.Log("delivered");
                    }
                }

                if (enemyNeed == 2)
                {
                    if (type.type2 == true)
                    {
                        Destroy(other.gameObject);
                        orderActive = false;

                        orderSystem.haveOrder = false;
                        orderSystem.keeper.haveOrder = false;

                        player.currentGold += 30;
                        Debug.Log("delivered");
                    }
                }

                if (enemyNeed == 3)
                {
                    if (type.type3 == true)
                    {
                        Destroy(other.gameObject);
                        orderActive = false;

                        orderSystem.haveOrder = false;
                        orderSystem.keeper.haveOrder = false;


                        player.currentGold += 30;
                        Debug.Log("delivered");
                    }
                }
            }
        }
    }
}
