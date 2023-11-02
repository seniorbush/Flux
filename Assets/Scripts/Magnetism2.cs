using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism2 : MonoBehaviour
{
    // Define variables
    public Transform player;
    public float magnetismRange = 10f;
    public float moveSpeed = 3f;
    public float heightOffset = -0.05f; // Adjust this value to control the height offset
    private bool isAttached = false;


    void FixedUpdate()
    {
        if (!isAttached)
        {
            // Check distance between magnetic object and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= magnetismRange)
            {
                // Move towards a point above the player with the specified height offset
                Vector3 targetPosition = player.position + heightOffset * Vector3.up;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }

    // Detect collision with the player.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the object as attached and disable its rigidbody (if it has one) to make it stick to the player
            isAttached = true;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

}
