using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal {
    public class Character : MonoBehaviour
    {
        private static Character Instance;

        public static Character myInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = FindObjectOfType<Character>();
                }
                return Instance;
            }
        }

        private InputManager _inputManager;
        private CharacterController _characterController;
        [HideInInspector]
        public GameObject _mainCamera;
        private Animator _animator;
        [HideInInspector]
        public Transform myTransform;
        public Vector2 InputMove;
        public bool InputSprint;

        [Header("Character Stats")]
        [SerializeField]
        public PlayerStat _stamina;
        private float initStamina = 100;

        public bool _hasAnimator;

        [Header("Action States")]
        [SerializeField] public bool _isWalking;
        [SerializeField] public bool _isSprinting;
        [SerializeField] public bool _isSprintJump;

        [Header("Player Movetion")]
        public float WalkSpeed;
        public float SprintSpeed;
        public float SpeedChange = 10.0f;
        public float RotationSmoothTime = 0.12f;

        [HideInInspector]
        public float triggerSpeed;
        public float FallingTimeout = 0.15f;
        public float JumpHeight = 1.2f;
        public float JumpSprintHeight = 2f;
        public float JumpingTimeout = 0.50f;

        [Header("Charater GroundCheck and Gravity")]
        public float Gravity = -15.0f;
        public float groundedOffset = -0.14f;
        public LayerMask groundLayers;
        public bool isGrounded = true;
        public float GroundedRadius = 0.28f;

        [HideInInspector]
        public float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        //TimeOut
        private float _fallingTimeoutDelta;
        private float _jumpingTimeoutDelta;


        private float _animationBlendMove;
        private float _motionSpeed = 1f;
        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _inputManager = GetComponent<InputManager>();
            _characterController = GetComponent<CharacterController>();

            _stamina.Initialize(initStamina, initStamina);

            _jumpingTimeoutDelta = JumpingTimeout;
            _fallingTimeoutDelta = FallingTimeout;
        }
        protected virtual void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);
        }
        private void FixedUpdate()
        {
            _hasAnimator = TryGetComponent(out _animator);
            HanlderGravity();
            HanlderJumpAndSprintJump();
            /*HandlerMovement();*/
            HanlderGroundedCheck();
           /* HandleRotation();*/
        }

        #region Jump And Gravity

        private void HanlderJumpAndSprintJump()
        {
            if (isGrounded)
            {
                _fallingTimeoutDelta = FallingTimeout;

                if (_hasAnimator)
                {
                    _animator.SetBool("Jump", false);
                    _animator.SetBool("SprintJump", false);
                    _animator.SetBool("FreeFall", false);
                }
                if (_verticalVelocity < 0.0f) _verticalVelocity = -2f;

                if (_inputManager.jump && _jumpingTimeoutDelta <= 0.0f && !_isSprinting)
                {
                    if (_hasAnimator)
                    {
                        _animator.SetTrigger("Jump");
                    }
                    _inputManager.sprint = false;
                }

                if (_inputManager.jump && _jumpingTimeoutDelta <= 0.0f && _isSprinting)
                {
                    if (_hasAnimator)
                        _isSprintJump = true;
                        _animator.SetBool("SprintJump", true);
                        _stamina.myCurrentValue += 0.5f;
                }
                else
                {
                    _isSprintJump = false;
                }


                if (_jumpingTimeoutDelta >= 0.0f) _jumpingTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                _jumpingTimeoutDelta = JumpingTimeout;

                if (_fallingTimeoutDelta >= 0.0f)
                {
                    _fallingTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    if (_hasAnimator)
                    {
                        _animator.SetBool("FreeFall", true);
                    }
                }
                _inputManager.jump = false;
            }
        }
        public void addJumpForce()
        {
            _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }
        public void addSprintJumpForce()
        {
            _verticalVelocity = Mathf.Sqrt(JumpSprintHeight * -2f * Gravity);
        }
        #endregion

        #region  Gravity

        private void HanlderGravity()
        {
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        #endregion


        #region Grounded Check

        private void HanlderGroundedCheck()
        {


            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
                transform.position.z);
            isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, groundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character

            if (_hasAnimator)
            {
                _animator.SetBool("Grounded", isGrounded);
            } 
        }
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),
                GroundedRadius);
        }
        #endregion
    }
}




#region Copy
/*public class Character : MonoBehaviour
{
    private InputManager inputManager;

    protected CharacterController characterController;
    protected Animator animator;
    private Vector3 carrentDirection;
    private Transform cameraObject;


    [Header("Player")]
    [SerializeField] protected float speed;
    [SerializeField] protected float sprintSpeed;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] public float speedChanger = 10.0f;
    [SerializeField] private int level;
    [SerializeField] private int maxStamina;

    [Header("State")]
    [SerializeField] private PlayerStat health;
    protected int myHealthInit;


    private float targetSpeed;
    private float lerpSpeed = 3;

    //animator
    private int animIDspeed;
    private float animationBlend;

    public int myLevel { get => level; set => level = value; }
    public InputManager InputManager { get => inputManager; set => inputManager = value; }

    // Health Stat
    public PlayerStat myHealth { get => health; set => health = value; }
    public int MyHealthInit { get => myHealthInit; set => myHealthInit = value; }

    protected virtual void Start()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraObject = Camera.main.transform;
        animIDspeed = Animator.StringToHash("Speed");

    }
    private void FixedUpdate()
    {
        HandleMavement();
        HandleRotation();
    }

    private void HandleMavement()
    {
        targetSpeed = inputManager.Sprint ? sprintSpeed : speed;

        if (inputManager.movementInput == Vector2.zero)
        {
            targetSpeed = 0f;
        }

        carrentDirection = cameraObject.forward * inputManager.verticalInput;
        carrentDirection = carrentDirection + cameraObject.right * inputManager.horizontalInput;
        carrentDirection.y = 0;

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChanger);
        speedChanger += Time.deltaTime * lerpSpeed;

        characterController.Move(carrentDirection.normalized * Time.deltaTime * targetSpeed);

        animator.SetFloat(animIDspeed, animationBlend);

        if (animationBlend <= 2 && animationBlend != 0.1)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (animationBlend > 2)
        {
            animator.SetBool("isRunning", true);

        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void HandleRotation()
    {
        Vector3 rotationDirection = Vector3.zero;

        rotationDirection = cameraObject.forward * inputManager.verticalInput;
        rotationDirection = rotationDirection + cameraObject.right * inputManager.horizontalInput;
        rotationDirection.y = 0;

        if (rotationDirection == Vector3.zero)
        {
            rotationDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection.normalized);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        transform.rotation = playerRotation;
    }
}
*/

#endregion 
