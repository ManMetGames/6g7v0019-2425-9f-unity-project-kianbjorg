using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaker : MonoBehaviour
{

    public float cooldown;
    public float cooldownAverage = 30;
    public GameObject prefab;

    public bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(cooldownAverage-15,cooldownAverage+15);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0) cooldown--; else {
            cooldown = Random.Range(cooldownAverage-15,cooldownAverage+15);
            GameObject sphere = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            sphere.
        }
    }
}
