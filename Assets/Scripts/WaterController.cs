using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class WaterController : AController
{
    private float acceleration = 10f;
    private float deceleration = 30f;
    private float brakeForce = 25f;
    private float maxSpeed = 60f;
    private float landingDeceleration = 10f;
    private float rotationSpeed = 15f;
    private float maxSteeringAngle = 30f;

    public override void AccelerateCar(PlayerCarController car, float input)
    {
        // Calculate acceleration
        if (input != 0)
        {
            // Accelerate or brake
            car.CurrentSpeed = Mathf.MoveTowards(car.CurrentSpeed, (input) * maxSpeed, ((car.CurrentSpeed > 0 ^ input > 0) ? brakeForce : acceleration) * Time.deltaTime);
        }
        else
        {
            // Decelerate
            car.CurrentSpeed = Mathf.MoveTowards(car.CurrentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Move the car
        Vector3 movement = car.CurrentSpeed * Time.deltaTime * car.transform.forward;
        car.transform.Translate(movement, Space.World);
    }

    public override void RotateCar(PlayerCarController car, float input)
    {
        float rotation = input * rotationSpeed * Time.deltaTime * ((car.CurrentSpeed != 0) ? 10 : 0);
        car.transform.Rotate(Vector3.up * rotation);
    }

    public override void AscentCar(PlayerCarController car, float input) { }

    public override void SpinAndSteerVisualWheels(PlayerCarController car, float input)
    {
        // Keep wheels turned
        car.frontLeftWheelVisual.localRotation = Quaternion.Euler(0, 0, 90f);

        car.frontRightWheelVisual.localRotation = Quaternion.Euler(0, 0, 90f);

        car.rearLeftWheelVisual.localRotation = Quaternion.Euler(0, 0, 90f);

        car.rearRightWheelVisual.localRotation = Quaternion.Euler(0, 0, 90f);
    }

    public override void Land(PlayerCarController car)
    {
        // Decelerate the car when landing
        car.CurrentSpeed = Mathf.MoveTowards(car.CurrentSpeed, 10f, landingDeceleration * Time.deltaTime);

        // Move the car
        Vector3 movement = car.transform.forward * car.CurrentSpeed * Time.deltaTime;
        car.transform.Translate(movement, Space.World);

        // Calculate the target rotation during landing
        float landingRotation = 0.5f * rotationSpeed * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0f, car.InitialYRotation, 0f) * Quaternion.Euler(0f, landingRotation, 0f);

        // Interpolate between the current rotation and the target rotation
        car.transform.rotation = Quaternion.Slerp(car.transform.rotation, targetRotation, landingRotation);
    }
}
