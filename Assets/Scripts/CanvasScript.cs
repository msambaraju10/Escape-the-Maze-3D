using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlocks the cursor from the middle of the screen
        Cursor.visible = true; // Makes the cursor visible
    }

    public void PressPlay() // Method for play button
    {
        SceneManager.LoadScene("Level 1"); // Load Level 1 scene
    }

    public void PressQuit() // Method for quit Button
    {
        Application.Quit(); // Quits the game
    }

    public void PressReturn() // Method for quit Button
    {
        SceneManager.LoadScene("Menu"); // Load Level 1 scene
    }

}
