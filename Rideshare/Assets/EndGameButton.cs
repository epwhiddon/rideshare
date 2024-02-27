using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndGameButton : MonoBehaviour
{
    public Wait waitEvent; // Reference to the Wait script

    // Start is called before the first frame update
    void Start()
    {
        if (waitEvent == null)
        {
            Debug.LogError("Wait.cs reference not set in EndGameButton.cs!");
        }

        // Find the Wait script attached to the same GameObject
        //waitEvent = GetComponent<WaitEvent>();
    }

    // Method to handle button click event
    public void OnEndGameButtonClick()
    {
        // Check if the waitEvent reference is valid
        if (waitEvent != null)
        {
            // Call the OnEndGame method in the Wait.cs script
            waitEvent.OnEndGame();
        }
        else
        {
            Debug.LogError("Wait.cs reference is null in EndGameButton.cs!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndGame()
    {
        // Calls the method in the Wait script to handle the "End Game" action
        waitEvent.OnEndGame();
    }
}
