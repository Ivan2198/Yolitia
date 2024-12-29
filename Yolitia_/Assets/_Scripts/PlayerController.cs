using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    public Transform target; 
    public float rotationSpeed = 100f; 

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private bool isWalking;
    private bool isFacingRight = true;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ceilngCheck;
    [SerializeField] private float playerSize = .1f;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canMove = true;

    private bool isTouchingCeiling = false;
    [SerializeField] private float ceilingCheckDistance = 0.3f;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private UnityEvent OnJump;
    [SerializeField] private UnityEvent OnIdle;
    [SerializeField] private UnityEvent OnWalking;

    [SerializeField] private PlayerAnimator playerAnimator;

    // Better Jump Var
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        gameInput.OnJumpAction += HandleJumpAction;

       
        OnJump.AddListener(playerAnimator.Jump);
        OnIdle.AddListener(playerAnimator.Idle);
        OnWalking.AddListener(playerAnimator.Walk);
    }

    private void HandleJumpAction(object sender, System.EventArgs e)
    {
        if (isGrounded && !isTouchingCeiling)
        {
            Jump();
        }
    }

    void Update()
    {
        // Wall checking 
        WallCheck();

        // Ground Check
        GroudChecking();

        // Ceiling Check
        CheckCeiling();

        ApplyGravity();

        if (isGrounded && rb.velocity.y < 0.1f)
        {
            velocity.y = -2f; 
        }

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump") && !isTouchingCeiling)
        {
            Jump();
        }
        

        HandleHorizontalRotation();

        //Debug.Log("Is facing right: " + isFacingRight);
        //Debug.Log("Can move: " + canMove);
       
    }

    private void ApplyGravity()
    {

        if (!isGrounded)
        {

            velocity.y += gravity * Time.deltaTime;
        }


        if (isTouchingCeiling && velocity.y > 0)
        {
            velocity.y = 0f;
        }


        rb.velocity = new Vector3(rb.velocity.x, velocity.y, rb.velocity.z);


        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    private void HandleHorizontalRotation()
    {

        Vector2 movementInput = gameInput.GetMovementVectorNormalized();

        if (movementInput.x < 0)
        {
            if (canMove)
                transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);

            if (isFacingRight)
                Turn(); 

            isWalking = true;
            OnWalking?.Invoke();
        }
        else if (movementInput.x > 0)
        {
            if (canMove)
                transform.RotateAround(target.position, Vector3.up, -rotationSpeed * Time.deltaTime);

            if (!isFacingRight)
                Turn(); 

            isWalking = true;
            OnWalking?.Invoke();
        }
        else
        {
            isWalking = false;
        }
    }

    private void Turn()
    {
      
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }
    private void Jump()
    {

        #region Perform Jump

        //float force = jumpForce;
        //if (rb.velocity.y < 0)
        //    force -= rb.velocity.y;

        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); 
        //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
        //OnJump?.Invoke();

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.velocity += Vector3.up * jumpForce;
        #endregion
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void GroudChecking()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
    private void CheckCeiling()
    {
        // Ceiling Check
        isTouchingCeiling = Physics.CheckSphere(ceilngCheck.position, ceilingCheckDistance, groundMask);

        if(isTouchingCeiling)
        {
            Debug.Log("Is touching Ceiling");
        }
    }

    private void WallCheck()
    {
        canMove = !Physics.CheckSphere(wallCheck.position, playerSize, groundMask);
    }
    public void OnDeath()
    {
        gravity = 0f;
    }
    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        Gizmos.color = Color.red;
        if(wallCheck != null)
        Gizmos.DrawWireSphere(wallCheck.position, playerSize);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ceilngCheck.position, ceilingCheckDistance);
    }
    #endregion
}