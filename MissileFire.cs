using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFire : MonoBehaviour
{
    Rigidbody misBody;
    float backwardForceTimer;
    ParticleSystem fire;
    public GameObject explosion;
    bool isPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        misBody = GetComponent<Rigidbody>();
        this.misBody.useGravity = false;
        backwardForceTimer = 1f;
        fire = GetComponent<ParticleSystem>();
        fire.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= 100)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if(backwardForceTimer > 0f)
        {
            misBody.AddForce(Vector3.back * 10, ForceMode.Force);
            backwardForceTimer -= Time.fixedDeltaTime;
            Debug.Log(backwardForceTimer);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            misBody.AddForce(Vector3.forward * 10, ForceMode.Impulse);
            fire.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") && !isPlayed)
        {
            isPlayed = true;
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
