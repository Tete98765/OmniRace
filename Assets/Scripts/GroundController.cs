using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : AController
{
    private float acceleration = 10f;
    private float deceleration = 10f;
    private float brakeForce = 25f;
    private float maxSpeed = 70f;
    private float landingDeceleration = 10f;
    private float rotationSpeed = 5f;
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
        float rotationAngle = car.CurrentSpeed * Time.deltaTime * 100;
        float steeringAngle = input * maxSteeringAngle;


        // Spin and steer the wheels
        car.frontLeftWheelVisual.localRotation = Quaternion.Euler(car.frontLeftWheelVisual.localRotation.eulerAngles.x % 72 + rotationAngle,
            steeringAngle,
            180f);

        car.frontRightWheelVisual.localRotation = Quaternion.Euler(car.frontRightWheelVisual.localRotation.eulerAngles.x % 72 + rotationAngle,
            steeringAngle,
            0f);

        car.rearLeftWheelVisual.localRotation = Quaternion.Euler(car.rearLeftWheelVisual.localRotation.eulerAngles.x % 72 + rotationAngle,
            0f,
            180f);

        car.rearRightWheelVisual.localRotation = Quaternion.Euler(car.rearRightWheelVisual.localRotation.eulerAngles.x % 72 + rotationAngle,
            0f,
            0f);
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
