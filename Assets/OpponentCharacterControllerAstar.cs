using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OpponentCharacterControllerAstar : MonoBehaviour
{
    Vector3 rotatingPlatformDestination;
    AIPath aiPath;
    Rigidbody rb;
    bool onRotatingPlatform = false;
    Animator playerAnimator;
    GameManager gameManager;

    public Transform raceStartPosition { get; set; }
    public Transform raceEndPosition { get; set; }
    private void Start()
    {
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();
        RaceManager.Instance?.AddRacer(this.gameObject);
        aiPath.destination = raceEndPosition.position;
        aiPath.canMove = false;
    }

    private void Update()
    {
        if (gameManager.State == GameState.Racing)
        {
            if (aiPath.canMove == false)
            {
                aiPath.canMove = true;
            }
            Move();
        }
        else
        {
            aiPath.enabled=false;
        }
    }

    void Move()
    {

        if (onRotatingPlatform)
        {
            float distance = Vector3.Distance(transform.position, rotatingPlatformDestination);
            Vector3 direction = (rotatingPlatformDestination - transform.position).normalized;
            AlignToSurface();
            transform.Translate(direction * 7 * Time.deltaTime);
            playerAnimator.SetFloat("playerSpeed", rb.velocity.magnitude);
        }
        else
        {
            aiPath.enabled = true;
            transform.parent = null;
            playerAnimator.SetFloat("playerSpeed", aiPath.velocity.magnitude);
        }
    }

    public void GoSpawnPoint()
    {
        transform.position = raceStartPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            rotatingPlatformDestination = other.GetComponent<RotatingPlatformController>().rotatingPlatformDestination;
            transform.parent = other.gameObject.transform;
            onRotatingPlatform = true;
            aiPath.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.parent = null;
            onRotatingPlatform = false;
            aiPath.enabled = true;
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

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 50);
        }
    }

}
