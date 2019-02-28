using System;
using System.Collections.Generic;

public class Car
{
    public int x;
    public int y;
    public int index;
    public List<int> ridesArr;
    public bool isAvailable;
    public int availableTime;

    public Car(int index)
    {
        this.x = 0;
        this.y = 0;
        this.index = index;
        this.isAvailable = true;
        this.ridesArr = new List<int>();
    }

    public void AddRideToList(Ride ride, int time)
    {
        this.ridesArr.Add(ride.rideLineNumber);
        ride.isRideFinished = true;

        // steps to ride start
        int stepsToStart = this.DistanceToRide(ride);
        int startTime = Math.Max(ride.startTime, stepsToStart);

        availableTime = startTime + ride.rideSteps;

        this.x = ride.finishX;
        this.y = ride.finishY;
    }

    public bool IsAvailableAtTime(int time)
    {
        return time >= availableTime;
    }

    public int DistanceToRide(Ride ride)
    {
        return Math.Abs(ride.startX - this.x) + Math.Abs(ride.startY - this.y);
    }
}
