using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public int speed = 1;
    public int scaleSpeed = 1;

    Quaternion baseRotation;
    bool firstBPress = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!OVRInput.Get(OVRInput.Button.Two))
        {
            firstBPress = true;
        }
        //if the A button is pressed, have the controller change the scale
        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote).y);
            Vector3 scale = transform.localScale;
            scale.z += OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote).y * scaleSpeed * Time.deltaTime;
            scale.x += OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote).y * scaleSpeed * Time.deltaTime;
            scale.y += OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote).y * scaleSpeed * Time.deltaTime;
            transform.localScale = scale;
        } else if (OVRInput.Get(OVRInput.Button.Two)) //otherwise if B is pressed, control rotation
        {
            //the first time the button is pressed, record the rotation of the remote and don't move the object. Then, record the different between original 
            //and new rotation.
            if (firstBPress)
            {
                //first button press, reset base rotation
                baseRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
                
                Quaternion updateRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
                updateRotation.x -= baseRotation.x;
                updateRotation.y -= baseRotation.y;
                updateRotation.z -= baseRotation.z;
                updateRotation.w -= baseRotation.w;

                Quaternion rotation = transform.rotation;
            

                rotation.x += updateRotation.x;
                rotation.y += updateRotation.y;
                rotation.z += updateRotation.z;
                rotation.w += updateRotation.w;
                transform.rotation = rotation;
                firstBPress = false;
            } else
            {
                Debug.Log("before" + transform.rotation);
                Debug.Log("base" + baseRotation);
                Quaternion updateRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
                updateRotation.x -= baseRotation.x;
                updateRotation.y -= baseRotation.y;
                updateRotation.z -= baseRotation.z;
                updateRotation.w -= baseRotation.w;

                Quaternion rotation = transform.rotation;
                rotation.x += updateRotation.x;
                rotation.y += updateRotation.y;
                rotation.z += updateRotation.z;
                rotation.w += updateRotation.w;
                Debug.Log("After" + transform.rotation);
                transform.rotation = rotation;
            }

        } else if (OVRInput.Get(OVRInput.Button.Three)) //otherwise control position
        {
            //Set position to be controlled by left hand movements
            Vector3 position = transform.position;
            position.x += OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTrackedRemote).x * speed * Time.deltaTime;
            position.z += OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTrackedRemote).z * speed * Time.deltaTime;
            transform.position = position;
        } else
        {
            firstBPress = true;
        }
    }
}
