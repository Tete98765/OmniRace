using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera backCamera;
    public KeyCode switchKey = KeyCode.Tab;  // Key to switch cameras

    void Start()
    {
        // Ensure at least one camera is enabled
        if (mainCamera)
        {
            mainCamera.enabled = true;
        }
        else
        {
            Debug.LogError("No camera assigned as mainCamera!");
        }
    }

    void Update()
    {
        // Check if the switch key is held down
        if (Input.GetKey(KeyCode.F))
        {
            mainCamera.enabled = false;
            backCamera.enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            backCamera.enabled = false;
        }
    }
}
