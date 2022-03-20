using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMe : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyMe[] Instance = FindObjectsOfType<DontDestroyMe>();

        if (Instance.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
