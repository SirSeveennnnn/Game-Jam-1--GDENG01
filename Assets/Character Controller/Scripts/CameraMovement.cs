using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float Xsens = 500;
    public float Ysens = 500;

    [Header("Components")]
    [SerializeField] private Transform orientation;

    float xRotation = 0, yRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * Xsens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * Ysens;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
