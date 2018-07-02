using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{

    private Dictionary<string, KeyCode> keySettings;
    private Dictionary<string, KeyCode> buttonSettings;
    public float mouseSense = .20f;

    // Use this for initialization
    void Start()
    {
        keySettings = new Dictionary<string, KeyCode>();
        keySettings["Left"] = KeyCode.A;
        keySettings["Right"] = KeyCode.D;
        keySettings["Up"] = KeyCode.W;
        keySettings["Down"] = KeyCode.S;
        keySettings["Jump"] = KeyCode.Space;

        buttonSettings = new Dictionary<string, KeyCode>();
        buttonSettings["X"] = KeyCode.Joystick1Button2;
        buttonSettings["Jump"] = KeyCode.Joystick1Button0;

    }

    /*
	// Update is called once per frame
	void Update () {
		
	}
    */

    public bool GetInteract()
    {
        bool interacted = false;

        if (Input.GetMouseButton(0) == true)
        {
            interacted = true;
        }
        else
        {
            if (Input.GetAxis("RightTrigger") > 0.5)
            {
                interacted = true;
            }
        }

        return (interacted);
    }

    public bool GetThrow()
    {
        bool interacted = false;

        if (Input.GetMouseButton(1) == true)
        {
            interacted = true;
        }
        else
        {
            if (Input.GetAxis("LeftTrigger") > 0.5)
            {
                interacted = true;
            }
        }

        return (interacted);
    }

    //checks if left is pressed on dpad or 
    public bool getLeftPressed()
    {
        bool pressed = false;
        //checks the d pad
        float axisX = Input.GetAxis("DPadAxisX");

        if (axisX < 0)
        {
            pressed = true;
        }
        else
        {
            pressed = Input.GetKeyDown(keySettings["Left"]);
        }

        return (pressed);
    }

    //checks if left is pressed on dpad or 
    public bool GetJumpPressed()
    {
        bool pressed = GetButtonPressed("Jump");

        if (pressed != true)
        {
            pressed = GetKeyPressed("Jump");
        }

        return (pressed);
    }

    //checks if left is pressed on dpad or 
    public bool GetLeft()
    {
        bool pressed = false;
        //checks the d pad
        float axisX = Input.GetAxis("DPadAxisX");

        if (axisX < 0)
        {
            pressed = true;
        }
        else
        {
            pressed = GetKey("Left");
        }

        return (pressed);
    }

    //checks if left is pressed on dpad or 
    public bool GetRight()
    {
        bool pressed = false;
        //checks the d pad
        float axisX = Input.GetAxis("DPadAxisX");

        if (axisX > 0)
        {
            pressed = true;
        }
        else
        {
            pressed = GetKey("Right");
        }

        return (pressed);
    }

    //checks if left is pressed on dpad or 
    public bool GetUp()
    {
        bool pressed = false;
        //checks the d pad
        float axisY = Input.GetAxis("DPadAxisY");

        if (axisY > 0)
        {
            pressed = true;
        }
        else
        {
            pressed = GetKey("Up");
        }

        return (pressed);
    }

    //checks if left is pressed on dpad or 
    public bool GetDown()
    {
        bool pressed = false;
        //checks the d pad
        float axisY = Input.GetAxis("DPadAxisY");

        if (axisY < 0)
        {
            pressed = true;
        }
        else
        {
            pressed = GetKey("Down");
        }

        return (pressed);
    }


    //Sees if a key is pressed
    public bool GetKeyPressed(string KeyName)
    {
        if (keySettings.ContainsKey(KeyName) == false)
        {
            Debug.LogError("Key: " + KeyName + " not set");
            return (false);
        }

        return (Input.GetKeyDown(keySettings[KeyName]));
    }

    //Sees if a key is held
    public bool GetKey(string KeyName)
    {
        if (keySettings.ContainsKey(KeyName) == false)
        {
            Debug.LogError("Key: " + KeyName + " not set");
            return (false);
        }

        return (Input.GetKey(keySettings[KeyName]));
    }

    //Sees if a key is released
    public bool GetKeyUp(string KeyName)
    {
        if (keySettings.ContainsKey(KeyName) == false)
        {
            Debug.LogError("Key: " + KeyName + " not set");
            return (false);
        }

        return (Input.GetKeyUp(keySettings[KeyName]));
    }

    //Sees if a Button is pressed
    public bool GetButtonPressed(string ButtonName)
    {
        if (buttonSettings.ContainsKey(ButtonName) == false)
        {
            Debug.LogError("Key: " + ButtonName + " not set");
            return (false);
        }

        return (Input.GetKeyDown(buttonSettings[ButtonName]));
    }

    //Sees if a Button is pressed
    public bool GetButton(string ButtonName)
    {
        if (buttonSettings.ContainsKey(ButtonName) == false)
        {
            Debug.LogError("Button: " + ButtonName + " not set");
            return (false);
        }

        return (Input.GetKey(buttonSettings[ButtonName]));
    }

    //Sees if a Button is pressed
    public bool GetButtonUp(string ButtonName)
    {
        if (buttonSettings.ContainsKey(ButtonName) == false)
        {
            Debug.LogError("Key: " + ButtonName + " not set");
            return (false);
        }

        return (Input.GetKeyUp(buttonSettings[ButtonName]));
    }


    /// Uses returns the angle of any active joystick
    /// returns 720 if no axis was active
    /// 
    /// Returns teh angle or 720 if there is no active joystick
    public float GetAxisAngle()
    {
        float angle = 999;
        float axisX = 0;
        float axisY = 0;
        float distance = 0;

        //checks joysticks in order of left joy stick, right joy stick, and d pad

        axisX = Input.GetAxis("LeftStickAxisX");
        axisY = Input.GetAxis("LeftStickAxisY");
        distance = Mathf.Sqrt((axisX * axisX) + (axisY * axisY));
        if (distance > .2f)//checks if left joy stick is active
        {
            //sets agnle to match the left joystick
            angle = Mathf.Atan2(axisY, axisX);
        }
        else
        {
            /*
            //TODO: Make DPad work like key presses
            axisX = Input.GetAxis("DPadAxisX");
            axisY = -1*Input.GetAxis("DPadAxisY");
            distance = Mathf.Sqrt((axisX * axisX) + (axisY * axisY));
            if (distance > .1f)//checks if D pad is active
            {
                //sets agnle to match the left joystick
                angle = Mathf.Atan2(axisY, axisX);
            }
            else
            {
            */
            axisX = Input.GetAxis("RightStickAxisX");
            axisY = Input.GetAxis("RightStickAxisY");
            distance = Mathf.Sqrt((axisX * axisX) + (axisY * axisY));
            if (distance > .2f)//checks if right joy stick is active
            {
                //sets agnle to match the left joystick
                angle = Mathf.Atan2(axisY, axisX);
            }
            //}
        }

        if (angle != 999)
        {
            angle = (angle * Mathf.Rad2Deg) + 90;
        }

        return (angle);
    }

    /// Uses returns the angle of any active joystick
    /// returns 720 if no axis was active
    /// 
    /// Returns teh angle or 720 if there is no active joystick
    public float GetAxisLeftAngle()
    {
        float angle = 999;
        float axisX = 0;
        float axisY = 0;
        float distance = 0;

        //checks joysticks in order of left joy stick, right joy stick, and d pad

        axisX = Input.GetAxis("LeftStickAxisX");
        axisY = Input.GetAxis("LeftStickAxisY");
        distance = Mathf.Sqrt((axisX * axisX) + (axisY * axisY));
        if (distance > .2f)//checks if left joy stick is active
        {
            //sets agnle to match the left joystick
            angle = Mathf.Atan2(axisY, axisX);
        }

        if (angle != 999)
        {
            angle = (angle * Mathf.Rad2Deg) + 90;
        }

        return (angle);
    }

    public float GetAxisLeftDistance()
    {
        float axisX = 0;
        float axisY = 0;
        float distance = 0;

        //checks joysticks in order of left joy stick, right joy stick, and d pad

        axisX = Input.GetAxis("LeftStickAxisX");
        axisY = Input.GetAxis("LeftStickAxisY");
        distance = Mathf.Sqrt((axisX * axisX) + (axisY * axisY));


        return (distance);
    }

    public Vector2 GetAxis()
    {
        float axisX = 0;
        float axisY = 0;

        //checks joysticks in order of left joy stick, right joy stick, and d pad

        axisX = Input.GetAxis("LeftStickAxisX");
        axisY = Input.GetAxis("LeftStickAxisY");


        return (new Vector2(axisX, axisY));
    }

    public Vector2 GetAxisRight()
    {
        float axisX = 0;
        float axisY = 0;

        //checks joysticks in order of left joy stick, right joy stick, and d pad

        axisX = Input.GetAxis("RightStickAxisX");
        axisY = Input.GetAxis("RightStickAxisY");


        return (new Vector2(axisX, axisY));
    }

    public Vector2 GetMouseMovement()
    {
        //float mouseChangeX = Input.mousePosition.x;
        //float mouseChangeY = Input.mousePosition.y;
        float mouseChangeX = Input.GetAxis("Mouse X") * mouseSense;
        float mouseChangeY = Input.GetAxis("Mouse Y") * mouseSense;

        return (new Vector2(mouseChangeX, mouseChangeY));
    }
}

