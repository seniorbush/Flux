using UnityEngine;

public class Magnetism : MonoBehaviour
{
    public float speed = 6f; // The speed at which the object moves towards the player
    private Transform player; // Reference to the player's transform
    private bool isStuck = false; // Flag to indicate if the object is stuck to the player


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Contact").transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // add functionallity so object sticks too player
            // perhaps special collider abover players head for contact point
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        // Move the object towards the player
        transform.position += direction * speed * Time.deltaTime;
    }
}

