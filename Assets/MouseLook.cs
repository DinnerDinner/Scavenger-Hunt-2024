using System.Collections;
using System.Collections.Generic;
// using UnityEngine;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float standingSensitivity = 75f;
    public float crouchingSensitivity = 25f; // Adjust this value as needed
    public Transform playerBody;

    private float mouseSensitivity;
    private float xRotation = 0f;

    void Start()
    {
        // Set default sensitivity
        mouseSensitivity = standingSensitivity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Hide the cursor
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player body around the Y axis
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the X axis (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamping to prevent flipping
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        Debug.Log("Mouse Sensitivity: " + mouseSensitivity);
    }

    // Method to set mouse sensitivity
    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }
}
