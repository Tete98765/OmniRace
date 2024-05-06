using UnityEngine;
using Enums;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontLeftWheel, frontRightWheel;
    [SerializeField] private WheelCollider rearLeftWheel, rearRightWheel;
    public Transform frontLeftWheelVisual;
    public Transform frontRightWheelVisual;
    public Transform rearLeftWheelVisual;
    public Transform rearRightWheelVisual;

    public float CurrentSpeed { get; set; }
    public float VerticalInput { get; private set; }
    public float HorizontalInput { get; private set; }
    public float YawInput { get; private set; }
    public float UpDownInput { get; private set; }

    public float InitialYRotation { get; private set; }

    private bool waterArea = false;
    private bool emerging = false;
    [SerializeField]
    private BoxCollider waterCollider;
    private float waterLevel;

    [SerializeField]
    private CarMode defaultMode = CarMode.GROUND;

    public CarMode CurrentMode { get; private set; }

    private AController controller = new GroundController();

    private void Start()
    {
        waterLevel = waterCollider.gameObject.transform.position.y;
        SwitchMode(defaultMode);
        CurrentSpeed = 0;
    }

    private void Update()
    {
        waterCollider.gameObject.SetActive(!emerging);
        
        if ((waterArea && CurrentMode == CarMode.WATER && transform.position.y <= waterLevel) || emerging) 
        {
            SwitchMode(CarMode.AIR);
            emerging = true;
            if (transform.position.y >= waterLevel) 
            { 
                emerging = false;
                SwitchMode(CarMode.WATER);
                return;
            }
            controller.AscentCar(this, 1);
            ControlCar();
        }

        else if ((!(frontLeftWheel.isGrounded || frontRightWheel.isGrounded || rearLeftWheel.isGrounded || rearRightWheel.isGrounded)) && CurrentMode != CarMode.AIR)
        {
            if (InitialYRotation == 0) { InitialYRotation = transform.rotation.eulerAngles.y; }
            controller.Land(this);
        }
        else if ((waterArea || CurrentMode != CarMode.WATER))
        {
            InitialYRotation = 0;

            ControlCar();
        }
        else
        {
            // Car is in water mode and not in water area - slowly decelerate
            controller.AccelerateCar(this, 0);
        }

        controller.SpinAndSteerVisualWheels(this, HorizontalInput);

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SwitchMode(CarMode.GROUND);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SwitchMode(CarMode.WATER);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SwitchMode(CarMode.AIR);
        }
    }
    void ControlCar()
    {
        VerticalInput = Input.GetAxis("Vertical");
        controller.AccelerateCar(this, VerticalInput);

        HorizontalInput = Input.GetAxis("Horizontal");
        controller.RotateCar(this, HorizontalInput);

        UpDownInput = Input.GetAxis("UpDown");
        controller.AscentCar(this, UpDownInput);
    }
    public void SwitchMode(CarMode newMode)
    {
        CurrentMode = newMode;
        Rigidbody rb = GetComponent<Rigidbody>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();


        switch (CurrentMode)
        {
            case CarMode.GROUND:
                controller = new GroundController();
                boxCollider.enabled = false;
                if (rb != null) rb.useGravity = true;
                rb.mass = 1500;
                rb.drag = 0f;
                rb.angularDrag = 0.05f;
                gameObject.layer = LayerMask.NameToLayer("PlayerGround");
                ChangeLayerForChildren(transform, "PlayerGround");
                break;
            case CarMode.WATER:
                controller = new WaterController();
                boxCollider.enabled = false;
                if (rb != null) rb.useGravity = true;
                rb.mass = 1500;
                rb.drag = 0f;
                rb.angularDrag = 0.05f;
                gameObject.layer = LayerMask.NameToLayer("PlayerWater");
                ChangeLayerForChildren(transform, "PlayerWater");
                break;
            case CarMode.AIR:
                controller = new AirController();
                boxCollider.enabled = true;
                if (rb != null) rb.useGravity = false;
                rb.mass = 400f;
                rb.drag = 1f;
                rb.angularDrag = 1f;
                gameObject.layer = LayerMask.NameToLayer("PlayerAir");
                ChangeLayerForChildren(transform, "PlayerAir");
                break;
            default:
                break;
        }
        Debug.Log("Switched to " + CurrentMode);
    }

    void ChangeLayerForChildren(Transform parent, string LayerName)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.layer = LayerMask.NameToLayer(LayerName);
            ChangeLayerForChildren(child, LayerName);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WaterArea"))
        {
            waterArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WaterArea"))
        {
            waterArea = false;
        }
    }
}
