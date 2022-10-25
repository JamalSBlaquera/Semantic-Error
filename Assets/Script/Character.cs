using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal {
    public class Character : InspactorManager
    {
       
        protected virtual void Awake()
        {
            SetRagdollPart();
        }

        protected virtual void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _inputManager = GetComponent<InputManager>();
            _characterController = GetComponent<CharacterController>();
            _characterController.slopeLimit = 45.0f;

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
            HanlderGroundedCheck();
        }

        #region Jump And Gravity
        public void addJumpForce()
        {
            _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }
        public void addDoableJumpForce()
        {
            _verticalVelocity = Mathf.Sqrt(DoableJumpHeight * -2f * Gravity);
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
            for (float i = 0; i <= 2; i++)
            {
                Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z + i / 5 - .18f);
                isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
            }
            
            if (_hasAnimator)
                _animator.SetBool("Grounded", isGrounded);
                
        }
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;
            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            /*Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),
                GroundedRadius);*/

            for (float i = 0; i <= 2; i++)
            {
                Vector3 rayPosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z + i/5 - .18f);
                Vector3 direction = transform.TransformDirection(Vector3.down) * 1f;
                Gizmos.DrawRay(rayPosition, direction);
            }

            /*Vector3 rayPositionForward = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 directionForward = transform.TransformDirection(Vector3.forward) * 1f;
            Gizmos.DrawRay(rayPositionForward, directionForward);*/
        }
        #endregion

        public void animationBlendMove(float speed)
        {
            _animationBlendMovement = Mathf.Lerp(_animationBlendMovement, speed, Time.deltaTime * SpeedChange);
            if (_animationBlendMovement >= 2.1f)
            {
                _isSprinting = true;
                _isWalking = false;
            }
            else if (_animationBlendMovement > 0.1f)
            {
                _isWalking = true;
                _isSprinting = false;
            }
            else
            {
                _isWalking = false;
                _isSprinting = false;
                _animationBlendMovement = 0f;
            }
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (RagdollParts.Contains(collider)) return;

            Character control = collider.transform.root.GetComponent<Character>();

            if (control == null) return;

            if (collider.gameObject == control.gameObject) return;

            if (!CollideParts.Contains(collider))
            {
                CollideParts.Add(collider);
            }
        }
        private void OnTriggerExit(Collider collider)
        {
            if (CollideParts.Contains(collider))
            {
                CollideParts.Remove(collider);
            }
        }

        private void SetRagdollPart()
        {
            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
            foreach(Collider col in colliders)
            {
                if (col.gameObject != this.gameObject)
                {
                    col.isTrigger = true;
                    RagdollParts.Remove(this.gameObject.GetComponent<Collider>());
                    RagdollParts.Add(col);
                }
            }
        } 
        public void TurnOnRagdoll()
        {
            Debug.Log(true);
            Gravity = 0f;
            this.gameObject.GetComponent<Collider>().enabled = false;
            _animator.enabled = false;
            _animator.avatar = null;
            foreach (Collider col in RagdollParts)
            {
                col.isTrigger = false;
                col.attachedRigidbody.velocity = Vector3.zero;
            }
        }
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
