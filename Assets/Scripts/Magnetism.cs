using UnityEngine;

public class Magnetism : MonoBehaviour
{
    public GameObject magneticField;
    public GameObject player;


    void Start()
    {
        magneticField = GameObject.FindGameObjectWithTag("Magnetic Field");
        magneticField = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //set condition if specific object has contacted collidor

        //object should seek out player
        //increment carry count(weight) +1
    }
}
