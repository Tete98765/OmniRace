using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirController : AController
{
    private float acceleration = 10f;
    private float deceleration = 10f;
    private float brakeForce = 10f;
    private float maxSpeed = 50f;
    private float maxVerticalSpeed = 10f;
    private float rotationSpeed = 2f;
    private float responsiveness = 10f;
    private float maxAngle = 8.0f;

    public override void AccelerateCar(PlayerCarController car, float input)
    {
        if (input != 0)
        {
            car.CurrentSpeed = Mathf.MoveTowards(car.CurrentSpeed, input * maxSpeed, (Mathf.Sign(car.CurrentSpeed) != Mathf.Sign(input) ? brakeForce : acceleration) * Time.deltaTime);
        }
        else
        {
            car.CurrentSpeed = Mathf.MoveTowards(car.CurrentSpeed, 0f, deceleration * Time.deltaTime);
        }

        Vector3 movement = car.CurrentSpeed * Time.deltaTime * car.transform.forward;
        car.transform.Translate(movement, Space.World);

        Vector3 horizontalRotation = new Vector3(0, car.transform.eulerAngles.y, 0);
        car.transform.eulerAngles = horizontalRotation;
    }

    public override void RotateCar(PlayerCarController car, float input)
    {
        float rotation = input * rotationSpeed * Time.deltaTime * ((car.CurrentSpeed != 0) ? 10 : 0);
        car.transform.Rotate(Vector3.up * rotation);

        float currentRoll = car.transform.eulerAngles.z;
        if (currentRoll > 180f) currentRoll -= 360f;

        float newRoll = currentRoll - (input * responsiveness);
        newRoll = Mathf.Clamp(newRoll, -maxAngle, maxAngle);

        Vector3 newRotation = new Vector3(car.transform.eulerAngles.x, car.transform.eulerAngles.y, newRoll);
        car.transform.eulerAngles = newRotation;
    }

    public override void AscentCar(PlayerCarController car, float input)
    {
        Vector3 verticalMovement = Vector3.up * input * maxVerticalSpeed * Time.deltaTime;
        car.transform.Translate(verticalMovement, Space.World);

        float currentPitch = car.transform.eulerAngles.x;
        if (currentPitch > 180f) currentPitch -= 360f;

        float newPitch = currentPitch - (input * responsiveness);
        newPitch = Mathf.Clamp(newPitch, -maxAngle, maxAngle); 

        Vector3 newRotation = new Vector3(newPitch, car.transform.eulerAngles.y, car.transform.eulerAngles.z);
        car.transform.eulerAngles = newRotation;
    }

    public override void SpinAndSteerVisualWheels(PlayerCarController car, float input)
    {
        Quaternion leftWheelRotation = Quaternion.Euler(0f, 0f, -270f);
        Quaternion rightWheelRotation = Quaternion.Euler(0f, 0f, 90f);

        car.frontLeftWheelVisual.localRotation = leftWheelRotation;
        car.frontRightWheelVisual.localRotation = rightWheelRotation;
        car.rearLeftWheelVisual.localRotation = leftWheelRotation;
        car.rearRightWheelVisual.localRotation = rightWheelRotation;
    }

    public override void Land(PlayerCarController car)
    {
    }

}
