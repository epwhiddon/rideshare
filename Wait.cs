using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using UnityEngine;

class Wait
{
    private PriorityQueue<Event> eventQueue = new PriorityQueue<Event>();
    private double simulationTime = 0;

    private class Event
    {
        public double Time { get; set; }
        public Action Action { get; set; }
    }

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




    public void StartWait()
    {
        ScheduleEvent(0, MainMenu);
        RunSimulation();
    }

    private void MainMenu()
    {
        Console.WriteLine($"Simulation Time: {simulationTime}");

        Console.WriteLine("Wait Main Menu:");
        Console.WriteLine("1. End Game");
        Console.WriteLine("2. Take a Break");
        Console.WriteLine("3. Add Fuel");

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
        Console.Write("Do you want to end the game? (yes/no): ");
        string endGameChoice = Console.ReadLine().ToLower();
        if (endGameChoice == "yes")
        {
            ReportFinalMetrics();
            Environment.Exit(0);
        }
        else
        {
            ScheduleEvent(simulationTime, MainMenu);
        }
    }

    private void OnTakeBreak()
    {
        Random random = new Random();
        int breakTime = random.Next(5, 30); // Random break time between 5 to 30 minutes

        Console.WriteLine($"Taking a break for {breakTime} minutes...");

        ScheduleEvent(simulationTime + breakTime, () => EndBreak());
    }

    private void EndBreak()
    {
        Console.WriteLine("Break time ended.");

        Console.Write("End break time? (yes/no): ");
        string endBreakChoice = Console.ReadLine().ToLower();

        if (endBreakChoice == "no")
        {
            ScheduleEvent(simulationTime, () => OnTakeBreak());
        }
        else
        {
            ScheduleEvent(simulationTime, MainMenu);
        }
    }

    private void OnAddFuel()
    {
        Random random = new Random();
        int fuelTime = random.Next(10, 60); // Random fueling time between 10 to 60 minutes

        Console.WriteLine($"Adding fuel for {fuelTime} minutes...");

        ScheduleEvent(simulationTime + fuelTime, () => EndFuel());
    }

    private void EndFuel()
    {
        Console.WriteLine("Fueling time ended.");

        Console.Write("End fuel time? (yes/no): ");
        string endFuelChoice = Console.ReadLine().ToLower();

        if (endFuelChoice == "no")
        {
            ScheduleEvent(simulationTime, () => OnAddFuel());
        }
        else
        {
            ScheduleEvent(simulationTime, MainMenu);
        }
    }


    private int GetChoice(int min, int max)
    {
        int choice;
        do
        {
            Console.Write("Enter your choice: ");
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
