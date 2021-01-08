using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    bool pathTwo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 15);
        if(transform.position.z <= 30 && !pathTwo)
        {
            transform.localEulerAngles = new Vector3(0, -45, 0);
            pathTwo = true;
        }
        else if (UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).y == -45 && transform.position.z >= 80)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if(transform.position.z <= -10)
        {
            Destroy(gameObject);
        }
    }
}
