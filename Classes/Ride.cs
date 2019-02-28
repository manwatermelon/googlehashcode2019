using System;

public class Ride : IComparable<Ride>
{
    public int startX;
    public int startY;
    public int finishX;
    public int finishY;
    public int startTime;
    public int finishTime;
    public int rideLineNumber;
    public bool isRideFinished = false;
    public int rideSteps;

    public Ride(string parameters, int index)
    {
        var a = parameters.Split(" ".ToCharArray());
        this.startX = Int32.Parse(a[0]);
        this.startY = Int32.Parse(a[1]);
        this.finishX = Int32.Parse(a[2]);
        this.finishY = Int32.Parse(a[3]);
        this.startTime = Int32.Parse(a[4]);
        this.finishTime = Int32.Parse(a[5]);
        this.rideLineNumber = index;
        this.rideSteps = Math.Abs(startX - finishX) + Math.Abs(startY - finishY);
    }

    public int CompareTo(Ride other)
    {
        if (this.finishTime > other.startTime) return 1;
        if (this.finishTime == other.startTime) return 0;
        return -1;
    }

    public int DistanceToCar(Car car)
    {
        int returnInt = 0;
        returnInt = Math.Abs(car.x - this.startX) + Math.Abs(this.startY - car.y);
        return returnInt;
    }

    public bool IsGoodForCar(Car car, int currentTime)
    {
        return currentTime + DistanceToCar(car) + rideSteps <= this.finishTime;
    }
}
