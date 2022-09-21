using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject overheadCamera;

    public characterMovement movementScript;
    private float baseSpeed = 0f;
    private float baseRotationSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = movementScript.playerSpeed;
        baseRotationSpeed = movementScript.turnSmoothTime;

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            ShowFirstPersonView();
        }
        else if (Input.GetButton("OverheadCam"))
        {
            ShowOverheadView();
        }else
        {
            ShowThirdPersonView();
        }

    }

    public void ShowThirdPersonView()
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
        overheadCamera.SetActive(false);
        movementScript.playerSpeed = baseSpeed;
        movementScript.turnSmoothTime = baseRotationSpeed;
    }

    public void ShowFirstPersonView()
    {
        firstPersonCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
        overheadCamera.SetActive(false);
        movementScript.playerSpeed = baseSpeed * 0.5f;
        movementScript.turnSmoothTime = baseRotationSpeed * 20f;
    }

    public void ShowOverheadView()
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(false);
        overheadCamera.SetActive(true);
        movementScript.playerSpeed = 0f;
        movementScript.turnSmoothTime = Mathf.Infinity;
    }


}


