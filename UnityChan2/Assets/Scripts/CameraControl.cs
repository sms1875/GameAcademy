using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //GameObject parent;
    public GameObject target;

    Vector3 defPosition;
    Quaternion defRotation;
    float defZoom;
    void Start()
    {
        //parent = transform.parent.gameObject;
        //defPosition = transform.position;
        //defRotation = parent.transform.rotation;
        defPosition = target.transform.position;
        defRotation = target.transform.rotation;
        defZoom = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //transform.Translate(Input.GetAxis("Mouse X") / 10, Input.GetAxis("Mouse Y") / 10, 0);
            target.transform.Translate(-Input.GetAxis("Mouse X") / 10, Input.GetAxis("Mouse Y") / 10, 0);
        }
        if (Input.GetMouseButton(1))
        {
            //parent.transform.Rotate(-Input.GetAxis("Mouse Y") * 10, Input.GetAxis("Mouse X") * 10, 0);
            target.transform.Rotate(-Input.GetAxis("Mouse Y") * 10, -Input.GetAxis("Mouse X") * 10, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            Camera.main.fieldOfView += (20 * Input.GetAxis("Mouse ScrollWheel"));

            if (Camera.main.fieldOfView < 10)
            {
                Camera.main.fieldOfView = 10;
            }
            else if (Camera.main.fieldOfView > 100)
            {
                Camera.main.fieldOfView = 100;
            }
        }
        if (Input.GetMouseButton(2))
        {
            //transform.position = defPosition;
            //parent.transform.rotation = defRotation;
            target.transform.position = defPosition;
            target.transform.rotation = defRotation;
            Camera.main.fieldOfView = defZoom;
        }
    }
}
