using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody carRb;
    public WheelCollider frontLeft, frontRight, backLeft, backRight;
    public float motorTorque = 1500f; // Puissance moteur
    public float maxSteerAngle = 30f; // Angle max des roues avant
    public float brakeForce = 3000f; // Force de freinage

    InputSystem_Actions actionMap;
    Vector2 movementValue;

    private void Start()
    {
        actionMap = new InputSystem_Actions();
        actionMap.Player.Move.started += PressMovementStartProcess;
        actionMap.Player.Move.canceled += PressMovementCancelProcess;
        actionMap.Enable();
    }

    private void PressMovementStartProcess(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        movementValue = value;
        print("ddddfd");
    }

    private void PressMovementCancelProcess(InputAction.CallbackContext context) {
    
        movementValue = Vector2.zero;
    }

    private void Update()
    {
        //carRb.AddForce(new Vector3(movementValue.x * speed * Time.deltaTime,0, movementValue.y * speed * Time.deltaTime));

        backLeft.motorTorque = movementValue.y * motorTorque;
        backRight.motorTorque = movementValue.y * motorTorque;

        // Direction sur les roues avant
        frontLeft.steerAngle = movementValue.x * maxSteerAngle;
        frontRight.steerAngle = movementValue.x * maxSteerAngle;
    }
}
