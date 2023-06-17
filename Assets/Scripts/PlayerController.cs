using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] FloatingJoystick joystick;
    float horizontalInput;
    float verticalInput;
    Vector3 movementVec;
    Animator playerAnimator;
    GameManager gameManager;

    public event Action OnFinishRace;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
        RaceManager.Instance?.AddRacer(this.gameObject);
    }

    void Update()
    {
        if (gameManager.State == GameState.Racing)
        {
            GetInput();
        }
    }

    void FixedUpdate()
    {
        if (gameManager.State == GameState.Racing)
        {
            Move();
            Rotation();
            AlignToSurface();
        }
        else
        {
            playerAnimator.SetFloat("playerSpeed", 0);
        }
    }

    void GetInput()
    {
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;
    }

    void Move()
    {
        movementVec = new Vector3(horizontalInput, 0f, verticalInput);
        transform.position += movementVec * moveSpeed * Time.deltaTime;
        playerAnimator.SetFloat("playerSpeed", movementVec.magnitude);
    }

    void Rotation()
    {
        if (horizontalInput != 0 && verticalInput != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementVec);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void AlignToSurface()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            Quaternion currentRotation = transform.rotation;
            currentRotation.x = 0f;
            currentRotation.z = 0f;
            targetRotation.x = 0f;
            targetRotation.z = 0f;

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.gameObject.transform;
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            OnFinishRace?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
