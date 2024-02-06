using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


public class SimeExecProcess : MonoBehaviour ///Initializations
{
    //Initialize global variables
   public int numOfRiders = 10;
   public Riders[] Rider;
   int TotalFuel; 
  

   // Start is called before the first frame update
    void Start()
    {
        InitializeRider();
        PerformTasks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeRider()
    {
        Rider = GenerateRiders(numOfRiders);
    }

  private Riders[] GenerateRiders(int numOfRiders) 
    {
       Riders[] riders = new Riders[numOfRiders];
        System.Random rand = new System.Random();

        for(int i = 0; i < numOfRiders; i++) 
        {
            double tip = 5 + rand.NextDouble() * (30 - 5); // Generate random tip amount between $5 and $30 in c#
            int rating = 1 + UnityEngine.Random.Range(1,6); // Generate random rating between 1 and 5 (inclusive)

            Riders rider = new Riders // Create a new rider and assign attributes 
            {
                rider_ID = i + 1, 
                TipAmount = tip,
                Rating = rating
            };

            riders[i] = rider;

            //Spawn on Map

        }
       
       return riders;
    }

    public bool RiderDecisionTask1() //Task1: Is rider requesting ride? 
    {

       foreach (Riders rider in Rider) //Check queue for rider 
       {
            if(rider != null) //If rider is in the queue 
            {
                return true;
            }
       }

       return false; //no rider found 
    }
  
  public bool RiderDecisionTask2() //Task2: Rider accept or reject ride?
  {

    float randomValue = UnityEngine.Random.value; //Generate random probability 

    return (randomValue >= 0.5);
  }

  private void PerformTasks()
  {
    bool taskResult1 = RiderDecisionTask1();
    bool taskResult2 = RiderDecisionTask2();
    CheckRiderDecisionTasks(taskResult1, taskResult2);
  }

  public void CheckRiderDecisionTasks(bool task1Result, bool task2Result)
  {

    if(task1Result == true && task2Result == true)
    {
        //Schedule Pickup
        PickupRider();
    }
    else
    {
        //Schedule Waiting...
    }
  }

  public void FuelSpent()
  {
    //Get current fuel amount
    //Calucalte fuel spent based on milage 
  }

  public void DriverCurrentLocation()
  {
        ///return driver current location 
  }

  public void PickupLocation()
  {
        ///return rider pickup location
        
  }

  public void DestinationLocation()
  {
        ///return rider destination location
  }

  public void CalculateShortestPath()
  {
        
        ///calculate shortest path based on a and b locations
  }

 public void CheckPickupStatus()
 {
        ///while distance travelled is less than destination 
 }
  public void PickupRider()
  {
        ///drive to rider 
  }

}

[Serializable]
public class Riders
{
   [SerializeField] public int rider_ID;
    public double TipAmount;
    public int Rating;

}

public class Driver
{
     public double Earnings;
     public int AvgRating;
     public int FuelSpent; 
}