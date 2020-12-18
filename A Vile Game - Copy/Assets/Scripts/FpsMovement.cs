using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

//WASD-style movement control
public class FpsMovement : MonoBehaviour
{
    [SerializeField] private Camera headCam;
    //character movement speed and gravity
    public float speed = 6.0f;
    public float gravity = -9.8f;
    //how fast the mouse can move
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    //limits for camera movement
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float rotationVert = 0;
    public float lives = 3;
    private CharacterController charController;

    public Camera HeadCam { get => headCam; set => headCam = value; }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {

            lives = lives - 1;
        }
    }
    void Start()
    {
        charController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;//lock the cursor to middle of screen
    }

    void Update()
    {
        MoveCharacter();
        RotateCharacter();
        RotateCamera();
        
    }
    private void MoveCharacter()
    {
        //changes in direction
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        //clamp movment speed
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        //checks to see if player is sprinting and sets appropriate speed
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movement.y = gravity;
            movement *= 2* Time.deltaTime;
            movement = transform.TransformDirection(movement);

        }
        else//otherwise move the regular speed
        {
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);

        }
        //moves player
        charController.Move(movement);
    }

    private void RotateCharacter()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
    }

    private void RotateCamera()
    {
        rotationVert -= Input.GetAxis("Mouse Y") * sensitivityVert;
        rotationVert = Mathf.Clamp(rotationVert, minimumVert, maximumVert);

        HeadCam.transform.localEulerAngles = new Vector3(
            rotationVert, HeadCam.transform.localEulerAngles.y, 0
        );
    }
}
