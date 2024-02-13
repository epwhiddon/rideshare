using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using UnityEngine;

public class Wait : MonoBehaviour
{
    private PriorityQueue<Event> eventQueue = new PriorityQueue<Event>();
    private double simulationTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartWait();
    }

    // Update is called once per frame
    void Update()
    {

    }

      public void StartWait()
    {
        ScheduleEvent(0, WaitMenu);
        RunSimulation();
    }

    private class Event
    {
        public double Time { get; set; }
        public Action Action { get; set; }
    }

    //Unused Code?
    private class PriorityQueue<T>
    {
        private List<T> elements = new List<T>();

        public int Count => elements.Count;

        public void Enqueue(T item, double priority)
        {
            elements.Add(item);
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty.");

            T minPriorityItem = elements.First();
            elements.RemoveAt(0);
            return minPriorityItem;
        }
    }

    private void ScheduleEvent(double time, Action action)
    {
        eventQueue.Enqueue(new Event { Time = time, Action = action }, time);
    }

    private void RunSimulation()
    {
        while (eventQueue.Count > 0)
        {
            Event currentEvent = eventQueue.Dequeue();
            simulationTime = currentEvent.Time;
            currentEvent.Action.Invoke();
        }
    }


    private void WaitMenu()
    {
        Debug.Log($"Simulation Time: {simulationTime}");

        Debug.Log("Wait Main Menu:");
        Debug.Log("1. End Game");
        Debug.Log("2. Take a Break");
        Debug.Log("3. Add Fuel");

        int choice = GetChoice(1, 3);

        switch (choice)
        {
            case 1:
                ScheduleEvent(simulationTime, () => OnEndGame());
                break;
            case 2:
                ScheduleEvent(simulationTime, () => OnTakeBreak());
                break;
            case 3:
                ScheduleEvent(simulationTime, () => OnAddFuel());
                break;
        }
    }

    private void OnEndGame()
    {
        Debug.Log("Do you want to end the game? (yes/no): ");
        string endGameChoice = Console.ReadLine().ToLower();    //need to bug
        if (endGameChoice == "yes")
        {
            //ReportFinalMetrics();
            Environment.Exit(0);
        }
        else
        {
            ScheduleEvent(simulationTime, WaitMenu);
        }
    }

    private void OnTakeBreak()
    {
        System.Random rand = new System.Random();
        int breakTime = rand.Next(5, 30); // Random break time between 5 to 30 minutes

        Debug.Log($"Taking a break for {breakTime} minutes...");

        ScheduleEvent(simulationTime + breakTime, () => EndBreak());
    }

    private void EndBreak()
    {
        Debug.Log("Break time ended.");

        Debug.Log("End break time? (yes/no): ");
        string endBreakChoice = Console.ReadLine().ToLower();   //need to bug

        if (endBreakChoice == "no")
        {
            ScheduleEvent(simulationTime, () => OnTakeBreak());
        }
        else
        {
            ScheduleEvent(simulationTime, WaitMenu);
        }
    }

    private void OnAddFuel()
    {
        System.Random rand = new System.Random();
        int fuelTime = rand.Next(10, 60);           // Random fueling time between 10 to 60 minutes

        Debug.Log($"Adding fuel for {fuelTime} minutes...");

        ScheduleEvent(simulationTime + fuelTime, () => EndFuel());
    }

    private void EndFuel()
    {
        Debug.Log("Fueling time ended.");

        Debug.Log("End fuel time? (yes/no): ");
        string endFuelChoice = Console.ReadLine().ToLower();    //need to bug

        if (endFuelChoice == "no")
        {
            ScheduleEvent(simulationTime, () => OnAddFuel());
        }
        else
        {
            ScheduleEvent(simulationTime, WaitMenu);
        }
    }


    private int GetChoice(int min, int max)
    {
        int choice;
        do
        {
            Debug.Log("Enter your choice: ");
        } while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max);
        return choice;
    }
}

class Program
{
    static void Main()
    {
        Wait simulation = new Wait();
        simulation.StartWait();
    }
}
