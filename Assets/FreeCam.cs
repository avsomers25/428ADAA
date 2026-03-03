using UnityEngine;

/// <summary>
/// A simple free camera to be added to a Unity game object.
/// Modified: Movement and rotation are disabled while Ctrl is held.
/// </summary>
public class FreeCam : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float fastMovementSpeed = 100f;
    public float freeLookSensitivity = 3f;
    public float zoomSensitivity = 10f;
    public float fastZoomSensitivity = 50f;

    private bool looking = false;

    void Start(){ 
        StartLooking();
    }

    void Update()
    {
        // 1. Check if Control is being held (Left or Right)
        bool ctrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        // 2. If Ctrl is pressed, stop looking (if we were) and exit the method
        if (ctrlPressed)
        {
            if (looking) StopLooking();
            return; 
        }
        else {
            StartLooking();
        }

        var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var currentSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

        // --- Translation Movement ---
        if (Input.GetKey(KeyCode.A))
            transform.position += -transform.right * currentSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * currentSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * currentSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position += -transform.forward * currentSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
            transform.position += transform.up * currentSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
            transform.position += -transform.up * currentSpeed * Time.deltaTime;


        // --- Rotation (Free Look) ---
        if (looking)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        // --- Zoom ---
        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            var currentZoomSens = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
            transform.position += transform.forward * axis * currentZoomSens;
        }

        // --- Mouse Toggle ---
        // if (Input.GetKeyDown(KeyCode.Mouse1))
        // {
        //     StartLooking();
        // }
        // else if (Input.GetKeyUp(KeyCode.Mouse1))
        // {
        //     StopLooking();
        // }
    }

    void OnDisable()
    {
        StopLooking();
    }

    public void StartLooking()
    {
        looking = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StopLooking()
    {
        looking = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}