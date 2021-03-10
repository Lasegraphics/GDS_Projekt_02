using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    [SerializeField] float movmentSpeed;
    [SerializeField] float minimumXValue;
    [SerializeField] float maximumXValue;
    KeyCode code;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey("w"))
        {
            if (transform.position.y < maximumXValue)
            {
                var newPos = Vector3.up * movmentSpeed * Time.deltaTime;
                transform.Translate(newPos, Space.World);
            }
        }
        if (Input.GetKey("s"))
        {
            if (transform.position.y > minimumXValue)
            {
                var newPos = Vector3.down * movmentSpeed * Time.deltaTime;
                transform.Translate(newPos, Space.World);
            }

        }
        if (Input.GetKey("a"))
        {
            if (transform.position.x > minimumXValue)
            {
                var newPos = Vector3.left * movmentSpeed * Time.deltaTime;
                transform.Translate(newPos, Space.World);
            }
        }      
        if (Input.GetKey("d"))
        {
            if (transform.position.x < maximumXValue)
            {
                var newPos = Vector3.right * movmentSpeed * Time.deltaTime;
                transform.Translate(newPos, Space.World);
            }
        }
    }
}
