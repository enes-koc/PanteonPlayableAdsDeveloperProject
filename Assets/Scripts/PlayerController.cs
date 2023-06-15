using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject startPos;
    [SerializeField] GameObject platformModel;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] FloatingJoystick joystick;
    float horizontalInput;
    float verticalInput;
    Vector3 movementVec;
    Animator playerAnimator;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {

        GetInput();
        Move();
        Rotation();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = startPos.transform.position;
        }

        if (transform.parent == null)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + platformModel.transform.rotation.eulerAngles.z * rotationSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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
            if (onPlatform)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementVec);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(0, rb.rotation.y, 0);
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementVec);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            // Quaternion targetRotation = Quaternion.LookRotation(movementVec);
            // rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    bool onPlatform = false;
    GameObject platform;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            platform = other.gameObject;
            onPlatform = true;
        }
        else
        {
            onPlatform = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.RotateAround(other.transform.position, Vector3.forward, 0.5f);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
