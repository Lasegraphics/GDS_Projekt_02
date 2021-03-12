using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    [SerializeField] float movmentSpeed;
    [SerializeField] float minimumXValue;
    [SerializeField] float maximumXValue;

    void Update()
    {

        if (Input.GetMouseButton(2))
        {
            Debug.Log(2);
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
        /*  if (Input.GetAxis("Mouse ScrollWheel")>0f)
          {
              if (transform.position.y < maximumXValue)
              {
                  var newPos = Vector3.up * movmentSpeed * Time.deltaTime;
                  transform.Translate(newPos, Space.World);
              }
          }
          if (Input.GetAxis("Mouse ScrollWheel") < 0f)
          {
              if (transform.position.y > minimumXValue)
              {
                  var newPos = Vector3.down * movmentSpeed * Time.deltaTime;
                  transform.Translate(newPos, Space.World);
              }

          }
        */
    }
}
