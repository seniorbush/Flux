using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    private float _playersFeet;

    private float Move;
    [SerializeField] public float MovementSpeed = 8f;
    [SerializeField] public float jumpForce = 10.5f;
    [SerializeField] public float fallMultiplier = 6f;
    [SerializeField] public float jumpVelocityFalloff = 7f;
    [SerializeField] public float coyoteTime = .2f;
    [SerializeField] public float coyoteTimeCounter;
    [SerializeField] public float jumpBufferTime = .3f;
    [SerializeField] public float jumpBufferCounter;


    public bool _isGrounded;
    public bool _isFacingRight;



    public LayerMask groundLayer;
    public LayerMask wallLayer;



    //animation states {change from srting to hash to increase performance}
    Animator _animator;
    string _currentState;
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Running";
    const string PLAYER_JUMP = "Jump_Init";
    const string PLAYER_FALL = "Falling";
    const string PLAYER_TURN = "Change_Direction";


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _playersFeet = GetComponent<CapsuleCollider>().bounds.extents.y;

        _isFacingRight = true;
    }

    void Update()
    {
        //Ground check.
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, _playersFeet + 0.1f, groundLayer))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }


        //Movement controller.
        Move = Input.GetAxis("Horizontal");
        _rb.velocity = (new Vector2(Move * MovementSpeed, _rb.velocity.y));


        if (_isGrounded && _rb.velocity.x == 0f)
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
        else if (_isGrounded && _rb.velocity.x > 0f || _isGrounded && _rb.velocity.x < 0f)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
        else if (!_isGrounded && _rb.velocity.y > 0f || !_isGrounded && _rb.velocity.y < 0f)
        {
            if (!IsAnimatorPlaying(_animator, PLAYER_JUMP))
            {
                ChangeAnimationState(PLAYER_FALL);
            }
        }

        if (_isGrounded && _isFacingRight && _rb.velocity.x < 0f || _isGrounded && !_isFacingRight && _rb.velocity.x > 0f)
        {
            ChangeDirection();
        }



        //Jump controller.
        if (_isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            Jump(Vector3.up); // execute jump
            coyoteTimeCounter = 0f; //reset coyote time counter
        }

        if (_rb.velocity.y < jumpVelocityFalloff || _rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
            jumpBufferCounter = 0f;
        }


    }



    void Jump(Vector3 dir)
    {
        // ChangeAnimationState(PLAYER_JUMP);
        _rb.velocity = new Vector3(_rb.velocity.x, 0);
        _rb.velocity += dir * jumpForce;
    }

    private void ChangeAnimationState(string newState)
    {
        {
            if (newState == _currentState)
            {
                return; //prevents repeating animations
            }

            _animator.CrossFade(newState, 0, 0);//2nd parameter smooths transition
            // _animator.Play(newState);
            _currentState = newState;
        }
    }

    //checks if specific animation is playing
    //parameter "0" is the animation layer
    bool IsAnimatorPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
        animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ChangeDirection()
    {
        ChangeAnimationState(PLAYER_TURN);
        transform.Rotate(0, 180, 0, Space.Self);
        _isFacingRight = !_isFacingRight;
    }
}
