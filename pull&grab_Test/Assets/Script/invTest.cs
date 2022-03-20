using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invTest : MonoBehaviour
{
    public PlayerBehavior player;
    public InventoryData[] Inventory;
    public int count;
    public bool invFull = false;

    public void AddToInv(InventoryData itemData)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = itemData;

                count += 1;

                break;
            }
        }
    }

    public void CheckInv()
    {
        //for (int j = 0; j < Inventory.Length; j++)
        //{
        //    if (Inventory[j] != null)
        //    {
        //        count += 1;
        //    }
        //}

        if (count >= Inventory.Length)
        {
            invFull = true;
        }
        else
        {
            invFull = false;
        }
    }

    public void PickEnemy0()
    {
            if (player.grabbedRB == null && Inventory[0] != null)
            {
                Debug.Log("condition pass");

                    Vector3 holderPos = new Vector3(player.objectHolder.position.x, player.objectHolder.position.y, player.objectHolder.position.z);

                    
                    Instantiate(Inventory[0].prefab, holderPos, Quaternion.identity);

                Inventory[0] = null;

                count -= 1;

            //player.HoldingObj();
            }
    }

    public void PickEnemy1()
    {
        if (player.grabbedRB == null && Inventory[1] != null)
        {
            Debug.Log("condition pass");

            Vector3 holderPos = new Vector3(player.objectHolder.position.x, player.objectHolder.position.y, player.objectHolder.position.z);


            Instantiate(Inventory[1].prefab, holderPos, Quaternion.identity);

            Inventory[1] = null;

            count -= 1;

            //player.HoldingObj();
        }
    }

    public void PickEnemy2()
    {
        if (player.grabbedRB == null && Inventory[2] != null)
        {
            Debug.Log("condition pass");

            Vector3 holderPos = new Vector3(player.objectHolder.position.x, player.objectHolder.position.y, player.objectHolder.position.z);


            Instantiate(Inventory[2].prefab, holderPos, Quaternion.identity);

            Inventory[2] = null;

            count -= 1;

            //player.HoldingObj();
        }
    }

    public void PickEnemy3()
    {
        if (player.grabbedRB == null && Inventory[3] != null)
        {
            Debug.Log("condition pass");

            Vector3 holderPos = new Vector3(player.objectHolder.position.x, player.objectHolder.position.y, player.objectHolder.position.z);


            Instantiate(Inventory[3].prefab, holderPos, Quaternion.identity);

            Inventory[3] = null;

            count -= 1;

            //player.HoldingObj();
        }
    }
}
