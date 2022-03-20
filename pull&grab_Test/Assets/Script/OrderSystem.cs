using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    public HouseInfo HouseInfo_1;
    public HouseInfo HouseInfo_2;
    public HouseInfo HouseInfo_3;

    public int houseOrder;
    public int enemyOrder;

    public OrderInfoKeep keeper;
    public bool haveOrder = false;

    private void Start()
    {
        keeper = FindObjectOfType<OrderInfoKeep>();

        if (keeper.haveOrder)
        {
            houseOrder = keeper.orderHouse;
            enemyOrder = keeper.orderEnemy;

            if (houseOrder == 1)
            {
                HouseInfo_1.orderActive = true;
                HouseInfo_1.enemyNeed = enemyOrder;
            }
            if (houseOrder == 2)
            {
                HouseInfo_2.orderActive = true;
                HouseInfo_2.enemyNeed = enemyOrder;
            }
            if (houseOrder == 3)
            {
                HouseInfo_3.orderActive = true;
                HouseInfo_3.enemyNeed = enemyOrder;
            }

            haveOrder = true;
            Debug.Log("order maintain");
        }
    }

    private void Update()
    {
        if (!haveOrder && !keeper.haveOrder)
        {
            houseOrder = Random.Range(1, 4);
            enemyOrder = Random.Range(1, 4);

            keeper.orderHouse = houseOrder;
            keeper.orderEnemy = enemyOrder;

            if (houseOrder == 1)
            {
                HouseInfo_1.orderActive = true;
                HouseInfo_1.enemyNeed = enemyOrder;
            }
            if (houseOrder == 2)
            {
                HouseInfo_2.orderActive = true;
                HouseInfo_2.enemyNeed = enemyOrder;
            }
            if (houseOrder == 3)
            {
                HouseInfo_3.orderActive = true;
                HouseInfo_3.enemyNeed = enemyOrder;
            }

            haveOrder = true;
            keeper.haveOrder = true;

            Debug.Log("new order");
        }

        //if (!HouseInfo_1.orderActive && !HouseInfo_2.orderActive)
        //{
        //    haveOrder = false;
        //    keeper.haveOrder = false;

        //    Debug.Log("delivered");
        //}
    }
}
