using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public GameObject missile;
    bool rightMissleFired = true;
    public GameObject enemyShip;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2.0f, 14.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(transform.position.x < -75)
        {
            transform.position = new Vector3(-75, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 75)
        {
            transform.position = new Vector3(75, transform.position.y, transform.position.z);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, -1, 1);
            transform.localEulerAngles = new Vector3(0, 0, timer * 60);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, -1, 1);
            transform.localEulerAngles = new Vector3(0, 0, timer * 60);
        }
        else
        {
            if (timer > 0.01)
                timer -= Time.deltaTime;
            else if (timer < -0.01)
                timer += Time.deltaTime;
            else
                timer = 0;
            transform.localEulerAngles = new Vector3(0, 0, timer * 60);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 vec;
            if (rightMissleFired)
                vec = new Vector3(transform.position.x + 5, transform.position.y - 5, transform.position.z);
            else
                vec = new Vector3(transform.position.x - 5, transform.position.y - 5, transform.position.z);
            rightMissleFired = !rightMissleFired;

            Instantiate(missile, vec, transform.rotation);
        }
    }

    void SpawnEnemy()
    {
        var x = Random.Range(-25, 70);
        Instantiate(enemyShip, new Vector3(x, 0, 100), Quaternion.Euler(0, 180, 0));
        Instantiate(enemyShip, new Vector3(x, 0, 130), Quaternion.Euler(0, 180, 0));
        Instantiate(enemyShip, new Vector3(x, 0, 160), Quaternion.Euler(0, 180, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
