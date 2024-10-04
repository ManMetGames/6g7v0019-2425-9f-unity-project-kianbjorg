using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody rigidBody;
    public float forceMin = 5f;
    public float forceMax = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 dir = new Vector3(contact.normal.x+Random.Range(-15, 15),contact.normal.y+Random.Range(-15, 15),contact.normal.z+Random.Range(-15, 15));
        float force = Random.Range(forceMin,forceMax);
        rigidBody.AddForce(dir * force);
    }
}
