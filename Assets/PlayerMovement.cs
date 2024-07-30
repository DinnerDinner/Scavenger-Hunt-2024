
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public MouseLook mouseLook; // Reference to the MouseLook script

    public float speed = 6.0f;
    public float crouchSpeed = 3.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;
    public float crouchHeight = 1.0f;
    public float standHeight = 2.0f;
    public float sprintSpeed = 12.0f;
    public AudioSource footstepAudio;
    public AudioClip[] footstepClips;
    private float stepRate = 0.5f;
    private float nextStep = 0f;

    private Vector3 velocity;
    private CharacterController controller;
    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (mouseLook == null)
        {
            mouseLook = GetComponent<MouseLook>();
        }
    }

    void Update()
    {
        // Check if the player is grounded
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Handle crouching
        if (Input.GetKeyDown(KeyCode.B))
        {
            isCrouching = true;
            controller.height = crouchHeight;
            speed = crouchSpeed;
            mouseLook.SetMouseSensitivity(mouseLook.crouchingSensitivity);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, crouchHeight / 2, playerCamera.transform.localPosition.z);
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            isCrouching = false;
            controller.height = standHeight;
            speed = 6.0f;
            mouseLook.SetMouseSensitivity(mouseLook.standingSensitivity);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, standHeight / 2, playerCamera.transform.localPosition.z);
        }

        // Get input from user
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Move the player
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // Handle sprinting
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            speed = sprintSpeed;
        }
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            speed = 6.0f;
        }

        // Footstep sound
        if (controller.isGrounded && (moveX != 0 || moveZ != 0))
        {
            if (Time.time > nextStep)
            {
                footstepAudio.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
                nextStep = Time.time + stepRate;
            }
        }
    }
}
