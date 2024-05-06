using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public float sensitivity = 2f;
    public float distanceFromPlayer = 10f;
    public float initialHeight = 2f;
    public float initialLookDownAngle = 10f;

    void Start()
    {
        // Calculate the initial position of the camera
        Vector3 offset = -player.forward * distanceFromPlayer + Vector3.up * initialHeight;
        Vector3 initialPosition = player.position + offset;

        // Set the initial position of the camera
        transform.position = initialPosition;

        // Look down 10 degrees
        transform.rotation = Quaternion.Euler(initialLookDownAngle, transform.rotation.eulerAngles.y, 0f);

        // Look at the player
        transform.LookAt(player.position);
    }

    void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera around the player based on mouse movement
        transform.RotateAround(player.position, Vector3.up, mouseX * sensitivity);

        // Adjust the pitch (look up and down) based on mouse movement
        transform.Rotate(Vector3.right, -mouseY * sensitivity);
    }

    private void Move()
    {
        // Calculate the new position of the camera after rotation
        Vector3 desiredPosition = player.position - transform.forward * distanceFromPlayer;

        // Ensure the camera cannot go below the player (and the ground)
        if (desiredPosition.y < player.position.y)
        {
            desiredPosition.y = player.position.y;
        }

        // Set the new position of the camera
        transform.position = desiredPosition;

        // Make the camera always look at the player
        transform.LookAt(player.position);
    }
}
