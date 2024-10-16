using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody rigidBody;
    public float forceMin = 5f;
    public float forceMax = 10f;

    public int health = 1;
    public float scale = 1.5f;
    public PlayerMaker maker;

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

        if ((maker.startGame) && (collision.gameObject.GetComponent<Player>())) {
            if (collision.gameObject.transform.localScale.x < transform.localScale.x) {
                collision.gameObject.GetComponent<Player>().health--;
                transform.localScale = new Vector3(transform.localScale.x*scale,transform.localScale.y*scale,transform.localScale.z*scale);
            }
        }
    }

    void Update() {
        if (health <= 0) Destroy(gameObject);

        if ((transform.position.x > 100) || (transform.position.x < -100) || (transform.position.y > 100) || (transform.position.y < -100) || (transform.position.z > 100) || (transform.position.z < -100)) Destroy(gameObject);
    }
}
