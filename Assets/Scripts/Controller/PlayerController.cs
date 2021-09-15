using UnityEngine;

namespace Controller
{
    /// <summary>
    /// This class handles the movement of the player with given input from the input manager
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Setting")]

        [Tooltip("The speed at which the player moves")]
        public float moveSpeed = 2f;
        [Tooltip("The speed at which the player rotates to look left and right (calculated in degrees)")]
        public float lockSpeed = 60f;
        [Tooltip("The power with which the player jumps")]
        public float jumpPower = 8f;
        [Tooltip("The strength of gravity")]
        public float gravity = 9.81f;
        [Header("Required Reference")]
        [Tooltip("The player shooter script that fires projectiles")]
        public Shooter playerShooter;

        private CharacterController _characterController;
        private InputManager _inputManager;
        Vector3 moveDirection;
        void Start()
        {
            SetUpCharacterController();
            SetUpInputManager();
            
        }
    
        void Update()
        {
            ProcessMovement();
        }

        private void SetUpCharacterController()
        {
            _characterController = GetComponent<CharacterController>();
            if (_characterController == null)
            {
                Debug.LogError("WTF???!!!");
            }
        }
        void SetUpInputManager()
        {
            _inputManager = InputManager.instance; 
        }
        
        
       
        void ProcessMovement()
        {
            float leftRightInput = _inputManager.horizontalMoveAxis;
            float forwardBackInput = _inputManager.verticalMoveAxis;
            bool jumpPressed = _inputManager.jumpPressed;
          

            if (_characterController.isGrounded)
            {
                moveDirection = new Vector3(leftRightInput, 0, forwardBackInput);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= moveSpeed;

                if (jumpPressed)
                {
                    moveDirection.y = jumpPower;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;
            _characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}
