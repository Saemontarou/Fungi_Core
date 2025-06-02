using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float sprintingSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    
    public float maxSprintTime = 5f;
    private float _currentSprintTime;
    private bool _isSprinting = false;

    public float sprintRecoveryRate = 1f;
    
    public Image sprintBar;
    
    public AudioSource runSound;
    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource sprintSound;
    
    private CharacterController _characterController;
    
    private ThrowStones _throw;
    
    private float _rotationX = 0;
    
    private Vector3 _moveDirection = Vector3.zero;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (sprintBar != null)
            sprintBar.fillAmount = 0;
        _currentSprintTime = maxSprintTime;
        
        _throw = GetComponent<ThrowStones>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleCamera();
        UpdateSprintUI();
        HandleSprinting();
        RecoverStamina();
    }
    
    private void ThrowStone()
    {
        _throw.ThrowStone();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowStone();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSprinting();
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSprinting();
        }
        
        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            Jump();
        }
    }

    private void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float speed = (_isSprinting && _currentSprintTime > 0) ? sprintingSpeed : walkingSpeed;

        Vector3 targetMoveDirection = (forward * inputZ + right * inputX).normalized * speed;

        if (_characterController.isGrounded)
        {
            _moveDirection.x = targetMoveDirection.x;
            _moveDirection.z = targetMoveDirection.z;
            
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
            
            else
            {
                _moveDirection.y -= gravity * Time.deltaTime;
            }
        }
        
        else
        {
            _moveDirection.x = targetMoveDirection.x;
            _moveDirection.z = targetMoveDirection.z;
            _moveDirection.y -= gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);
        
        ManagePlayerSounds(inputX, inputZ);
    }

    private void Jump()
    {
        _moveDirection.y = jumpSpeed;
        jumpSound.Play();
        walkSound.Stop();
    }

    private void HandleCamera()
    {
        _rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);
       
        playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
       
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void HandleSprinting()
    {
        if (_isSprinting)
        {
            _currentSprintTime -= Time.deltaTime;
            if (_currentSprintTime <= 0)
            {
                _currentSprintTime = 0;
                StopSprinting();
            }
        }
    }

    private void StartSprinting()
    {
        if (!_isSprinting && _currentSprintTime > 0)
        {
            _isSprinting = true;
            sprintSound.Play();
            walkSound.Stop();
        }
    }

    private void StopSprinting()
    {
        _isSprinting = false;
        sprintSound.Stop();
    }

    private void UpdateSprintUI()
    {
        if (sprintBar != null)
        {
            float fillAmount = Mathf.Clamp01(_currentSprintTime / maxSprintTime);
            sprintBar.fillAmount = fillAmount;
        }
    }

    private void RecoverStamina()
    {
        if (!_isSprinting && _currentSprintTime < maxSprintTime)
        {
            _currentSprintTime += sprintRecoveryRate * Time.deltaTime;
           
            if (_currentSprintTime > maxSprintTime)
                _currentSprintTime = maxSprintTime;

            UpdateSprintUI();
        }
    }

    private void ManagePlayerSounds(float inputX, float inputZ)
    {
        bool isMovingHorizontallyOrVertically = Mathf.Abs(inputX) > 0 || Mathf.Abs(inputZ) > 0;

        if (_characterController.isGrounded && isMovingHorizontallyOrVertically)
        {
            if (!walkSound.isPlaying)
                walkSound.Play();

            if (_isSprinting && !runSound.isPlaying)
                runSound.Play();
        }
        
        else
        {
            walkSound.Stop();
            runSound.Stop();
        }
    }
}