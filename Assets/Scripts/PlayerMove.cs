﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading.Tasks;

using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public CharacterController charController; // Publicly define our character controller

    public bool canJump; // Enable this in the inspector if you want the player to jump
    

    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public Transform groundCheck; // The transform of our "Ground Check" object. Used to determine if we are on the ground
    public float groundDistance = 0.05f;
    public LayerMask groundMask; // Set a field for our "Ground" layer
    float speed; // Update-able moving speed

    Vector3 velocity;
    public bool grounded; // Defines is the player is touching the ground

    private void Update() // Called once per frame
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Checks if our player is touching an object with a "Ground" layer and sets the boolean accordingly

        if (grounded && velocity.y < 0) // If the player is grounded
        {
            velocity.y = -2f; // Set the downward velocity to 2
        }

        if (Input.GetKey(KeyCode.LeftShift)) // If the left shift button is being pressed
        {
            speed = runSpeed; // Set our speed to runSpeed
        }
        else
        {
            speed = walkSpeed;// Set our speed to walkSpeed
        }

        float x = Input.GetAxis("Horizontal"); // Define an X variable and set it to Unity's "Horizontal" axis
        float z = Input.GetAxis("Vertical"); // Define a Y variable and set it to Unity's "Vertical" axis

        Vector3 move = transform.right * x + transform.forward * z; // Define our move vector

        charController.Move(move * speed * Time.deltaTime); // Move our character controller based on our set inputs

        if (Input.GetButtonDown("Jump") && grounded && canJump) // Unity's default input for "Jump" is the space key
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Set our jumping gravity
        }

        velocity.y += gravity * Time.deltaTime; // Set our regular gravity

        charController.Move(velocity * Time.deltaTime); // Apply our gravity to our character controller
    }

    private void OnTriggerEnter(Collider collisionCube) // Will be called when the player enters the End Cube trigger box
    {
        if (collisionCube.tag == "End Cube") {
            Scene currentScene = SceneManager.GetActiveScene(); // Declare a variable for our current scene

            if (currentScene.name != "Level 2") // If we are not on Level 2 (Change this to your last level when you make more levels)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next level in the build sequence
                //await Task.Delay(5);
                //TextMesh textObject = GameObject.Find("Level text").GetComponent<TextMesh>();
                //textObject.text = "test";
                
            }
            else // If we are on Level 2/your last level
            {
                SceneManager.LoadScene("Menu"); // Load Menu scene
            }
        }

        if (collisionCube.tag == "Magic Cube") {

            charController.enabled = false;
            transform.position = new Vector3(9, 0.5f, 0);
            charController.enabled = true;
        }
        
    }
}