using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AController
{

    public abstract void AccelerateCar(PlayerCarController car, float input);

    public abstract void RotateCar(PlayerCarController car, float input);

    public abstract void AscentCar(PlayerCarController car, float input);

    public abstract void SpinAndSteerVisualWheels(PlayerCarController car, float input);

    public abstract void Land(PlayerCarController car);

    public virtual void Emerge(PlayerCarController car) { return; }
}
