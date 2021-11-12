using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Switch : MonoBehaviour
{
    public GameObject otherCha;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(this.GetComponent<SFPSC_PlayerMovement>().enabled == true)
                {


                    this.GetComponent<SFPSC_PlayerMovement>().enabled = false;
                    this.GetComponent<PlayerBehavior>().enabled = false;
                    this.GetComponent<Pull>().enabled = false;

                    otherCha.GetComponent<SFPSC_PlayerMovement>().enabled = true;
                    otherCha.GetComponent<PlayerBehavior>().enabled = true;
                    otherCha.GetComponent<Pull>().enabled = true;
                    otherCha.GetComponent<Character_Switch>().enabled = true;

            }



        }
    }
}
