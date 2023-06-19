using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    MenuManager menuManager;
    int failAttempt = 0;
    public Transform raceStartPosition { get; set; }
    public event Action OnFinishRace;
    Rigidbody rb;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        menuManager = MenuManager.Instance;
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
        else if (gameManager.State != GameState.Painting)
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

    public void GoSpawnPoint()
    {
        rb.velocity = Vector3.zero;
        transform.position = raceStartPosition.position;
        failAttempt++;
        menuManager.SetFailAttempt(failAttempt);
    }

    IEnumerator MoveToPaintingPosition(Vector3 paintingPosition)
    {
        float moveTime = 3;
        transform.DOMove(paintingPosition, moveTime);
        yield return new WaitForSeconds(moveTime);
        playerAnimator.SetFloat("playerSpeed", 0);
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
            StartCoroutine(MoveToPaintingPosition(other.gameObject.transform.Find("PlayerFinishPosition").transform.position));
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
