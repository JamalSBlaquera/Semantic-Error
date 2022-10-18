using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    public class InspactorManager : MonoBehaviour
    {
        [HideInInspector]
        public InputManager _inputManager;
        [HideInInspector]
        public CharacterController _characterController;
        [HideInInspector]
        public GameObject _mainCamera;
        [HideInInspector]
        public Animator _animator;
        [HideInInspector]
        public Transform myTransform;

        //list
        public List<Collider> RagdollParts = new List<Collider>();
        public List<Collider> CollideParts = new List<Collider>();


        public bool _hasAnimator;

        [Header("Action States")]
        [SerializeField] public bool InputAttack;
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
        [HideInInspector]
        public float _terminalVelocity = 53.0f;

        //TimeOut
        [HideInInspector]
        public float _fallingTimeoutDelta;
        [HideInInspector]
        public float _jumpingTimeoutDelta;


        private float _animationBlendMove;

        public bool IsAttackin;

        // animator blend
        public float _animationBlendMovement;
        private float _rotationVelocity;
        private float _targetRotation;
    }

}
