using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cauldron : MonoBehaviour
{
    public GameObject canvas;
    public GameObject endCanvas;

    public float monster1Need;
    public float monster2Need;
    public float monster3Need;
    public float monster4Need;

    private void Start()
    {
        canvas.SetActive(true);
        endCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyType>() != null)
        {
            EnemyType type = other.GetComponent<EnemyType>();

            if (type.type1 == true)
            {
                if (monster1Need > 0)
                {
                    monster1Need -= 1;
                    Destroy(other.gameObject);
                }
            }

            if (type.type2 == true)
            {
                if (monster2Need > 0)
                {
                    monster2Need -= 1;
                    Destroy(other.gameObject);
                }
            }

            if (type.type3 == true)
            {
                if (monster3Need > 0)
                {
                    monster3Need -= 1;
                    Destroy(other.gameObject);
                }
            }

            if (type.type4 == true)
            {
                if (monster4Need > 0)
                {
                    monster4Need -= 1;
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        if (monster1Need <= 0 && monster2Need <= 0 && monster3Need <= 0 && monster4Need <= 0)
        {
            canvas.SetActive(false);
            endCanvas.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
