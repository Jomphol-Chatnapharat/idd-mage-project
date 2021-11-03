using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public SFPSC_FPSCamera cam;

    private void Start()
    {
        Player1.SetActive(true);
        Player2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Player1.activeSelf == true)
            {
                Player1.SetActive(false);
                Player2.SetActive(true);

                cam.player = Player2.transform;
                cam.CameraPosition = Player2.transform;
            }
            else
            {
                Player1.SetActive(true);
                Player2.SetActive(false);

                cam.player = Player1.transform;
                cam.CameraPosition = Player1.transform;
            }
        }
    }
}
