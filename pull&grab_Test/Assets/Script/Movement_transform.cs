using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_transform : MonoBehaviour
{
    [SerializeField] CharacterController CharacterController;
    [SerializeField] Transform Transform;
    [SerializeField] float _speed = 6f;
    [SerializeField] float _MouseSensitivity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _MouseSensitivity * Time.deltaTime;


        Transform.Rotate(Vector3.up * mouseX);
        Transform.Rotate(Vector3.right * mouseY);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Transform.right * x + transform.forward * z;

        CharacterController.Move(move * _speed * Time.deltaTime);

    }
}
