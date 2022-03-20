using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderInfoKeep : MonoBehaviour
{
    public int orderHouse;
    public int orderEnemy;

    public bool haveOrder;

    public Text houseDisplay;
    public Text enemyDisplay;

    public string houseColor;
    public string enemyType;
    
    public OrderInfoKeep Instance;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(orderHouse == 1)
        {
            houseColor = "White";
        }
        if (orderHouse == 2)
        {
            houseColor = "Red";
        }
        if (orderHouse == 3)
        {
            houseColor = "Brown";
        }

        if (orderEnemy == 1)
        {
            enemyType = "Bread";
        }
        if (orderEnemy == 2)
        {
            enemyType = "Gummy bear";
        }
        if (orderEnemy == 3)
        {
            enemyType = "Cherry";
        }

        houseDisplay.text = (houseColor + " house");
        enemyDisplay.text = ("Food: " + enemyType);
    }
}
