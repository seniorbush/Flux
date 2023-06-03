// using UnityEngine;

// public class Movement : MonoBehaviour
// {
//     private Rigidbody rb;
//     private bool isFacingRight = true;
//     private float horizontal;


//     // Variables to adjust jump mechanics.
//     [SerializeField] public float movementSpeed = 8f;
//     [SerializeField] public float jumpForce = 10.5f;
//     [SerializeField] public float fallMultiplier = 6f;
//     [SerializeField] public float jumpVelocityFalloff = 7f;
//     [SerializeField] public float coyoteTime = .2f;
//     [SerializeField] public float coyoteTimeCounter;
//     [SerializeField] public float jumpBufferTime = .3f;
//     [SerializeField] public float jumpBufferCounter;


//     // Variables to adjust wall mechanics.
//     private bool isWallSliding;
//     [SerializeField] private float wallSlidingSpeed = .2f;
//     private bool isWallJumping;


//     // Ground detection.
//     public bool isTouchingGround;
//     public Transform groundCheck;
//     public float groundCheckRadius; //will need adjusted depending on the scale of the character.
//     public LayerMask groundLayer;


//     // Wall detection.
//     public bool isTouchingWall;
//     public bool isTouchingLeftWall;
//     public bool isTouchingRightWall;
//     public bool pushingLeftWall;
//     public bool pushingRightWall;
//     public Transform wallCheckLeft;
//     public Transform wallCheckRight;
//     public float wallCheckRadius = 0.2f; //will need adjusted depending on the scale of the character.
//     public LayerMask wallLayer;


//     //animation states
//     Animator _animator;
//     string _currentState;
//     const string PLAYER_IDLE = "Idle";
//     const string PLAYER_RUN = "Running";
//     const string PLAYER_JUMP = "Jumping";
//     const string PLAYER_FALL = "Falling";
//     const string PLAYER_FLIP = "Change_Direction";

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         _animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         //Movement controller.
//         float x = Input.GetAxis("Horizontal");
//         float y = Input.GetAxis("Vertical");
//         Vector2 dir = new Vector2(x, y);

//         Move(dir);



//         horizontal = Input.GetAxisRaw("Horizontal");

//         isTouchingGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

//         isTouchingLeftWall = Physics.CheckSphere(wallCheckLeft.position, wallCheckRadius, wallLayer);
//         isTouchingRightWall = Physics.CheckSphere(wallCheckRight.position, wallCheckRadius, wallLayer);

//         pushingLeftWall = isTouchingLeftWall && x < 0;
//         pushingRightWall = isTouchingRightWall && x > 0;


//         //Jump controller.

//         if (isTouchingGround)
//         {
//             coyoteTimeCounter = coyoteTime;
//         }
//         else
//         {
//             coyoteTimeCounter -= Time.deltaTime;
//         }

//         if (Input.GetButtonDown("Jump"))
//         {
//             jumpBufferCounter = jumpBufferTime;
//         }
//         else
//         {
//             jumpBufferCounter -= Time.deltaTime;
//         }
//         if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
//         {
//             Jump(Vector3.up); // execute jump
//             coyoteTimeCounter = 0f; //reset coyote time counter

//         }

//         if (rb.velocity.y < jumpVelocityFalloff || rb.velocity.y > 0 && !Input.GetButton("Jump"))
//         {
//             rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
//             jumpBufferCounter = 0f;
//         }

//         WallSlide();
//         WallJump();
//         Flip();

//         if (isWallJumping)
//         {
//             Flip();
//             // Debug.Log("wall jump");
//             rb.velocity = new Vector3(movementSpeed * 1, jumpForce);
//             // Jump(new Vector3(isTouchingLeftWall ? 3f : -3f, 3f));
//         }
//     }


//     void Move(Vector2 dir)
//     {
//         {
//             rb.velocity = (new Vector2(dir.x * movementSpeed, rb.velocity.y));
//             if (rb.velocity.x <= 0f)
//             {
//                 ChangeAnimationState(PLAYER_IDLE);
//             }
//             else if (rb.velocity.x > 0f)
//             {
//                 ChangeAnimationState(PLAYER_RUN);
//             }
//         }
//     }
//     void Jump(Vector3 dir)
//     {
//         rb.velocity = new Vector3(rb.velocity.x, 0);
//         rb.velocity += dir * jumpForce;


//     }

//     void WallSlide()
//     {
//         var sliding = pushingLeftWall || pushingRightWall;

//         if (sliding && !isWallSliding)
//         {
//             transform.SetParent(pushingLeftWall ? wallCheckLeft.transform : wallCheckRight.transform);
//             isWallSliding = true;

//             if (rb.velocity.y < 0) rb.velocity = new Vector3(0, -wallSlidingSpeed);
//         }
//         else if (!sliding && isWallSliding)
//         {
//             isWallSliding = false;
//         }
//     }

//     void WallJump()
//     {
//         if ((isTouchingLeftWall || isTouchingRightWall) && Input.GetButton("Jump"))
//         {
//             isWallJumping = true;
//             Invoke("WallJumpStop", 0.08f);
//         }
//     }

//     void WallJumpStop()
//     {
//         isWallJumping = false;
//     }

//     private void Flip()
//     {
//         if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
//         {
//             isFacingRight = !isFacingRight;
//             Vector3 localScale = transform.localScale;
//             localScale.x *= -1f;
//             transform.localScale = localScale;
//             ChangeAnimationState(PLAYER_FLIP);
//         }
//     }

//     private void ChangeAnimationState(string newState)
//     {
//         {
//             if (newState == _currentState)
//             {
//                 return; //prevents repeating animations
//             }

//             _animator.CrossFade(newState, 0, 0);//2nd parameter smooths transition
//             // _animator.Play(newState);
//             _currentState = newState;
//         }
//     }

//     //checks if specific animation is playing
//     //parameter "0" is the animation layer
//     bool IsAnimatorPlaying(Animator animator, string stateName)
//     {
//         if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
//         animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }
// }
