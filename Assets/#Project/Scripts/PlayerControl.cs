using UnityEngine.InputSystem;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class PlayerControl : MonoBehaviour
{
    public InputActionAsset actions;
    public float startingSpeed = 1f;

    [SerializeField] float jumpSpeed = 5f;
    // [SerializeField] float jumpHeight = 2f;
    [SerializeField] Collider road;
    private InputAction xAxis;
    private InputAction yAxis;
    private Rigidbody rb;
    private Vector3 startingPosition;
    private bool isJumping = false;
    private float xMove;
    private float yMove;
    private float finishLine;


    void OnEnable()
    {
        actions.FindActionMap("CubeActionsMap").Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("CubeActionsMap").Disable();
    }
    void Update() // Inputs, UI, Cameras
    {
        xMove = xAxis.ReadValue<float>();
        yMove = yAxis.ReadValue<float>();

        if (rb.position.z <= finishLine) // Keeps moving unless reached the finish line.
        {
            Move();

            if (isJumping == false && yMove > 0)
            {
                Jump();
            }
        }

        Debug.Log($"UPDATE DATA -- {isJumping}");
        
    }
    // void FixedUpdate() // Physics (Rigidbody)
    // {
    // }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Reset();
        }
        if (collision.gameObject.CompareTag("Road"))
        {
            isJumping = false;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road") && isJumping == false)
        {
            Reset(); // Reset position when player falls from the platform 
        }
    }


    public void Initialize(Vector3 startingPosition, float finishLine)
    {
        xAxis = actions.FindActionMap("CubeActionsMap").FindAction("XAxis");
        yAxis = actions.FindActionMap("CubeActionsMap").FindAction("YAxis");

        this.startingPosition = startingPosition;
        this.finishLine = finishLine;

        isJumping = false;
        
        SetPosition(startingPosition);
    }
    public void SetPosition(Vector3 position)
    {
        rb = GetComponent<Rigidbody>();
        rb.MovePosition(position);
    }
    public void Reset()
    {
        SetPosition(startingPosition);
        isJumping = false;
    }

    private void Move()
    {
        Vector3 xzMovement = (transform.right * xMove + transform.forward) * startingSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + xzMovement);
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        isJumping = true;
        Debug.Log($"JUMP BUTTON PRESSED. DATA : isJumping : {isJumping} -- yMove : {yMove}");

    }

}