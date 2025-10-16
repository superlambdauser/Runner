using UnityEngine.InputSystem;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class PlayerControl : MonoBehaviour
{
    [SerializeField] InputActionAsset actions;
    [SerializeField] Collider road;
    [SerializeField] float startingSpeed;
    [SerializeField] float speedIncrease;
    [SerializeField] float jumpSpeed;
    [SerializeField] float sweetSpotForReset = 0.5f;
    // [SerializeField] float jumpHeight = 2f;
    private InputAction xAxis;
    private Rigidbody rb;
    private Vector3 startingPosition;
    private bool isJumping = false;
    private float speed;
    private float xMove;
    private float finishLine;


    public void Initialize(Vector3 startingPosition, float finishLine, float startingSpeed, float speedIncrease, float jumpSpeed)
    {
        xAxis = actions.FindActionMap("CubeActionsMap").FindAction("XAxis");

        this.startingPosition = startingPosition;
        this.finishLine = finishLine;
        this.startingSpeed = startingSpeed;
        this.speedIncrease = speedIncrease;
        this.jumpSpeed = jumpSpeed;

        speed = startingSpeed;
        isJumping = false;
        
        SetPosition(startingPosition);
    }


    void OnEnable()
    {
        actions.FindActionMap("CubeActionsMap").Enable();
        actions.FindActionMap("CubeActionsMap").FindAction("YAxis").performed += Jump; // This detects when the button of the action map is pressed and links it to the function to execute (here : Jump)
    }

    void OnDisable()
    {
        actions.FindActionMap("CubeActionsMap").Disable();
        actions.FindActionMap("CubeActionsMap").FindAction("YAxis").performed -= Jump;
    }

    public void Process() // Inputs, UI, Cameras
    {   
        if (rb.position.z <= finishLine && rb.position.y > startingPosition.y-sweetSpotForReset) // Keeps moving unless reached the finish line.
        {
            Move();

            speed += speedIncrease * Time.deltaTime;
        }
        else
        {
            Reset();
            // Ideally "You Win ! Retry ?"
        }   
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Reset();
        }
        if (collision.collider.CompareTag("Road"))
        {
            isJumping = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (isJumping == false)
        {
            if (collision.gameObject.CompareTag("Road"))
            {
                Reset(); // Reset position when player falls from the platform 
            }
        }
    }

    private void Reset()
    {
        speed = startingSpeed;
        isJumping = false;

        SetPosition(startingPosition);
    }

    private void SetPosition(Vector3 position)
    {
        rb = GetComponent<Rigidbody>();
        rb.MovePosition(position);
    }

    private void Move()
    {
        xMove = xAxis.ReadValue<float>();

        Vector3 xzMovement = (transform.right * xMove + transform.forward) * speed * Time.deltaTime; // Moves on the x axis from xMove units and keeps going forward at a certain speed
        rb.MovePosition(rb.position + xzMovement);
    }

    private void Jump(InputAction.CallbackContext callbackContext)
    {
        if (isJumping == false)
        {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse); // Impulse gives Newtons/sec. and calculates the mass of the body   =/=   Acceleration that gives Newtons solo and don't takes mass in consideration 
        isJumping = true;
        Debug.Log($"JUMP BUTTON PRESSED. DATA : isJumping : {isJumping}");
        }
    }
}