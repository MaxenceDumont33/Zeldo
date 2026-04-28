using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private Vector3 inputDirection;
    [SerializeField] private int walkSpeed;
    [SerializeField] private int RunSpeed;
    private bool isWalkingInYAxis = false;
    private bool isWalkingInXAxis = false;
    private Vector2 previousMoveRef;
    private int speed;
    private void Start()
    {
        speed = walkSpeed;
    }
    void Update()
    {
        transform.position += (inputDirection * speed * Time.deltaTime);
    }
    public void SetInputDirectionX(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isWalkingInXAxis = true;
            if (isWalkingInYAxis == true)
            {
                previousMoveRef = inputDirection;
                print("done");
            }
            inputDirection = context.ReadValue<Vector2>();
        }
        if (context.canceled)
        {
            isWalkingInXAxis = false;
            if (isWalkingInYAxis == true && previousMoveRef.x == 0)
            {
                inputDirection = previousMoveRef;
            }
            if (isWalkingInYAxis == false)
            {
                previousMoveRef = Vector2.zero;
                inputDirection = Vector2.zero;
            }
        }
        SetPlayerRotation();
    }
    public void SetInputDirectionY(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isWalkingInYAxis = true;
            if (isWalkingInXAxis == true)
            {
                previousMoveRef = inputDirection;
            }
            inputDirection = context.ReadValue<Vector2>();
        }
        if (context.canceled)
        {
            isWalkingInYAxis = false;
            if (isWalkingInXAxis == true && previousMoveRef.y == 0)
            {
                inputDirection = previousMoveRef;
            }
            if (isWalkingInXAxis == false)
            {
                previousMoveRef = Vector2.zero;
                inputDirection = Vector2.zero;
            }
        }
        SetPlayerRotation();
    }
    public void SetIsRunning(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed = RunSpeed;
        }
        if (context.canceled)
        {
            speed = walkSpeed;
        }
    }
    private void SetPlayerRotation()
    {
        if (inputDirection == new Vector3 (1, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (inputDirection == new Vector3(-1, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (inputDirection == new Vector3(0, 1))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (inputDirection == new Vector3(0, -1))
        {
            transform.rotation = Quaternion.Euler(0,0,180);
        }
    }
}
