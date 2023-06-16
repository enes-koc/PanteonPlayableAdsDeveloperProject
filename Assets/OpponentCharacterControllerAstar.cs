using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OpponentCharacterControllerAstar : MonoBehaviour
{
    [SerializeField] Transform endDestination;
    Vector3 rotatingPlatformDestination;
    private AIPath aiPath;
    
    bool onRotatingPlatform = false;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        aiPath.destination = endDestination.position;
    }

    private void Update()
    {
        Move();
    }

    void Move(){

        if (onRotatingPlatform)
        {
            float distance = Vector3.Distance(transform.position, rotatingPlatformDestination);
            Vector3 direction = (rotatingPlatformDestination - transform.position).normalized;
            AlignToSurface();
            transform.Translate(direction * 7 * Time.deltaTime);

        }
        else
        {
            aiPath.enabled = true;
            transform.parent = null;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            rotatingPlatformDestination=other.GetComponent<RotatingPlatformController>().rotatinPlatformDestination;
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
