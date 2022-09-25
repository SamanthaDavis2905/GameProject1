using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraControl : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject overheadCamera;

    public characterMovement movementScript;
    public float firstPersonSensitivity;
    public int firstPersonVerticalInvert = -1;
    public int firstPersonHorizontalInvert = 1;

    // Start is called before the first frame update
    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            ShowFirstPersonView();
        }
        else if(Input.GetButton("OverheadCam"))
        {
            ShowOverheadView();
        }else
        {
            ShowThirdPersonView();
        }

    }

    //changing camera angles
    public void ShowThirdPersonView()
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
        overheadCamera.SetActive(false);
        
        //initiates character movement
        movementScript.canMove = true;
    }

    
    public void ShowFirstPersonView()
    {
        firstPersonCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
        overheadCamera.SetActive(false);
        
        //stops character movement
        movementScript.canMove = false;

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        //limits horizontal and vertical first person aim up to 90 degrees up and down
        firstPersonCamera.transform.Rotate(new Vector3(0f, horizontal * firstPersonHorizontalInvert * firstPersonSensitivity * Time.deltaTime, 0f), Space.World);
        firstPersonCamera.transform.eulerAngles = new Vector3(firstPersonCamera.transform.eulerAngles.x + firstPersonSensitivity * vertical * Time.deltaTime, firstPersonCamera.transform.eulerAngles.y, firstPersonCamera.transform.eulerAngles.z);
    }
   

    public void ShowOverheadView()
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(false);
        overheadCamera.SetActive(true);
        
        //also stops character movement
        movementScript.canMove = false;
    }



}


