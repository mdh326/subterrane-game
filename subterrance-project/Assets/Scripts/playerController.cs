using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public float movementSpeed = 5f;
    public float mouseSensitivity = 10f;
    public float upDownRange = 60f;
    float vertRotation = 0f;
    //public float jumpVelocity = 7f;
    float upVelocity = 0f;

    public float rayLength;
    public bool drawLine;
    public RawImage interactIcon;
    private GameObject hitObject;

    Camera myCamera;
    CharacterController myCharacterController;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        myCharacterController = GetComponent<CharacterController>();
        myCamera = GetComponentInChildren<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        //Rotation
        float horizRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, horizRotation, 0f);

        vertRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        vertRotation = Mathf.Clamp(vertRotation, -upDownRange, upDownRange);
        myCamera.transform.localRotation = Quaternion.Euler(vertRotation, 0f, 0f);

        //Movement
        Vector3 movementVec = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * movementSpeed;
        //Debug.Log(movementVec);
        
        //Jumping and gravity
        upVelocity += Physics.gravity.y * Time.deltaTime;
        //if (Input.GetButtonDown("Jump") && myCharacterController.isGrounded)
        //{
        //    upVelocity = jumpVelocity;
        //}

        movementVec = new Vector3(movementVec.x, upVelocity, movementVec.z);
        myCharacterController.Move(transform.rotation * movementVec * Time.deltaTime);


        InteractRaycast();
        if (Input.GetMouseButtonDown(0) && hitObject.tag == "breakable")
        {
            hitObject.GetComponent<breakableScript>().breakReg();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void InteractRaycast()
    {
        Vector3 viewPos = myCamera.transform.position;
        Vector3 viewDir = myCamera.transform.forward;

         Ray flashlightRay = new Ray(viewPos, viewDir); //defining the ray
        RaycastHit rayHit;

        if (drawLine) {
            Vector3 flashlightRayEndpoint = viewDir * rayLength; //geting endpoint o draw line with
            Debug.DrawLine(viewPos, flashlightRayEndpoint);   //drawing line
        }

        bool hitFound = Physics.Raycast(flashlightRay, out rayHit, rayLength); //checking for hit
        interactIcon.enabled = false;
        if (hitFound) {
            hitObject = rayHit.transform.gameObject;
            if (hitObject.tag == "breakable") { //checking if hit is an interactable object

                interactIcon.enabled = true;

                //Debug.Log(hitObject.name);
            }
        } else {
            hitObject = null;
        }
    }
}
