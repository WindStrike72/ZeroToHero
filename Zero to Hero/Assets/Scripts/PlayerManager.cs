using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float angleY = 0;//shows the directs the player is facing on the x and y axis currently
    public float angleX = 0;//shows how far upwards the player is looking currently
    private ControlManager controlManager;
    private Rigidbody rigidbody;

    private bool upsideDown = false;

    public float StickTurnSensitivity = 5;
    public float speed = 10;

    public Camera playerCamera; 

    // Use this for initialization
    void Start () {
        controlManager = GetComponent<ControlManager>();
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        UpdateAngles();

        SetCamera();

        CheckJump();

        Move();

    }

    private void Move()
    {
        float xMove = 0;
        float zMove = 0;
        float axisAngle = controlManager.GetAxisLeftAngle();

        if (axisAngle != 999)
        {
            //uses joy stick controls
            if(upsideDown == true)
            {
                axisAngle = Mathf.Atan2(Mathf.Sin(Mathf.Deg2Rad * axisAngle), Mathf.Cos(Mathf.Deg2Rad * axisAngle)*-1)*Mathf.Rad2Deg;
            }
            axisAngle = axisAngle + angleY;
            float distance = controlManager.GetAxisLeftDistance(); 
            xMove = Mathf.Sin(Mathf.Deg2Rad * axisAngle) * speed * distance;
            zMove = Mathf.Cos(Mathf.Deg2Rad * axisAngle) * speed * distance;
        }
        else
        {
            //uses keybaord controls

            //sets the starting change
            if (controlManager.GetUp() == true)
            {
                zMove = 1;
            }
            else
            {
                if (controlManager.GetDown() == true)
                {
                    zMove = -1;
                }
            }

            if (controlManager.GetLeft() == true)
            {
                xMove = -1;
            }
            else
            {
                if (controlManager.GetRight() == true)
                {
                    xMove = 1;
                }
            }

            if (upsideDown == true)
            {
                zMove = zMove * -1;
            }
            //checks to see if the play is moving
            if (xMove != 0 || zMove != 0)
            {
                //adjsuts for the angle the player is facing
                float newAngle = Mathf.Deg2Rad * angleY + Mathf.Atan2(xMove, zMove);
                xMove = Mathf.Sin(newAngle) * speed;
                zMove = Mathf.Cos(newAngle) * speed;

            }
            else
            {
                xMove = 0;
                zMove = 0;
            }
        }

       
        rigidbody.velocity = new Vector3(xMove, rigidbody.velocity.y, zMove);
        
        
    }

    //checsk if the player jumped. if they can jump. then jumps if they can
    public void CheckJump()
    {
        if(controlManager.GetJumpPressed())
        {
            if(IsGrounded())
            {
                if(upsideDown == true)
                {
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, -5, rigidbody.velocity.z);
                }
                else
                {
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, 5, rigidbody.velocity.z);
                }
            }

        }
    }

    private bool IsGrounded() 
    {
        if (upsideDown == false)
        {
            return Physics.Raycast(transform.position, -Vector3.up, 0.95f);//half hieght is .85 then plus .1 for the raycast  
        }
        else
        {
            return Physics.Raycast(transform.position, Vector3.up, 0.95f);//half hieght is .85 then plus .1 for the raycast  
        }

    }
    private void UpdateAngles()
    {
        Vector2 rightStick= controlManager.GetAxisRight();
        
        Vector2 mouseChange = controlManager.GetMouseMovement();
        angleX = angleX + mouseChange.y +(rightStick.y * StickTurnSensitivity);

        if (angleX > 180)
        {
            angleX = angleX - 360;
        }
        else
        {
            if (angleX < -180)
            {
                angleX = angleX + 360;
            }
        }

        if(angleX < -90 || angleX > 90)
        {
            //upside down
            upsideDown = true;
            Physics.gravity = new Vector3(0, 9.8f, 0);

            angleY = angleY - mouseChange.x - (rightStick.x * StickTurnSensitivity);
        } 
        else
        {
            //right side up
            upsideDown = false;
            Physics.gravity = new Vector3(0, -9.8f, 0);


            angleY = angleY + mouseChange.x + (rightStick.x * StickTurnSensitivity);
        }
        
    }

    private void SetCamera()
    {
        playerCamera.transform.rotation = Quaternion.Euler(angleX, angleY, 0);

    }

    
}
