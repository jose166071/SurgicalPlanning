using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class my_rotate : MonoBehaviour
{
    public InputActionProperty leftjoystick;
    public InputActionProperty rightjoystick;
    Vector2 joystick;
    Vector2 joystick2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        joystick = leftjoystick.action.ReadValue<Vector2>();
        joystick2 = rightjoystick.action.ReadValue<Vector2>();
        //Debug.Log(joystick);
        gameObject.transform.Rotate(-joystick.y, -joystick.x, joystick2.x);
        //if (rotacionx == true)
        //{
        //    new_rotation = new Vector3(rotation.x - joystick.y, rotation.y - joystick.x, rotation.z + joystick2.x);

        //}
        //else if (rotacionx == false)
        //{
        //    new_rotation = new Vector3(rotation.x + joystick.y, rotation.y - joystick.x, rotation.z + joystick2.x);

        //}
        //if ((rotation.x >= 269 && rotation.x <= 270) || (rotation.x > 270 && rotation.x <= 271))
        //{
        //    if (joystick.y <= 0)
        //    {
        //        rotacionx = true;
        //    }
        //    else
        //    {
        //        rotacionx = false;
        //    }
        //}
        //else if ((rotation.x >=89 && rotation.x<=90.0)||(rotation.x > 90 && rotation.x <= 91))
        //{
        //    if (joystick.y <= 0)
        //    {
        //        rotacionx = false;
        //    }
        //    else
        //    {
        //        rotacionx = true;
        //    }
        //}
        //else if (new_rotation.y== 0)
        //{
        //    new_rotation.y = 360;
        //}
        //else if(new_rotation.z == 0)
        //{
        //    new_rotation.z = 360;
        //}
        //gameObject.transform.rotation = Quaternion.Euler(new_rotation);

        //Debug.Log(rotacionx);
        //Debug.Log(rotation.x);
        //Debug.Log(new_rotation);
    }
}
