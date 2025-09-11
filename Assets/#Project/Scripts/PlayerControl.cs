using UnityEngine.InputSystem;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class PlayerControl : MonoBehaviour
{
    public InputActionAsset actions;
    public float speed = 1f;

    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] Collider road;
    private InputAction xAxis;
    private InputAction yAxis;
    private Rigidbody rb;
    private bool isJumping = false;
    private float xMove;
    private float yMove;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        xAxis = actions.FindActionMap("CubeActionsMap").FindAction("XAxis");
        yAxis = actions.FindActionMap("CubeActionsMap").FindAction("YAxis");
    }

    void OnEnable()
    {
        actions.FindActionMap("CubeActionsMap").Enable();
    }

    void OnDisable()
    {
        actions.FindActionMap("CubeActionsMap").Disable();
    }


    void Update() //Inputs, UI, Cameras
    {
        xMove = xAxis.ReadValue<float>();
        yMove = yAxis.ReadValue<float>();

        Jump();
    }

    void FixedUpdate() //Physics (Rigidbody)
    {
        Move();
    }

    // private void MoveX()
    // {
    //     float xMove = xAxis.ReadValue<float>();
    //     transform.position += speed * Time.deltaTime * xMove * transform.right;

    // }

    // private void MoveZ()
    // {
    //     transform.position += speed * Time.deltaTime * transform.forward;
    // }

    void Move()
    {
        Vector3 xzMovement = (transform.right * xMove + transform.forward) * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + xzMovement);
    }

    private void Jump()
    {
        if (isJumping == false && yMove > 0)
        {

            isJumping = true;
            Debug.Log($"jumping ? {isJumping} -- yMove : {yMove}");
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("check 1 --- Collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Road"))
        {
            Debug.Log("check 2 --- Collided with: " + collision.gameObject.name);
            isJumping = false;
        }
    }
}
