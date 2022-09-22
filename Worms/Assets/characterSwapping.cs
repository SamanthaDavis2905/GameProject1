using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class characterSwapping : MonoBehaviour  
{
    public GameObject character_1;
    public GameObject character_2;
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;
    public GameObject camera_1_Location;
    public GameObject camera_2_Location;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //swap character1 to character2
        if (Input.GetButtonDown("SwapCharacter") && character_1.GetComponent<characterMovement>().isTurnOver)
        {
            thirdPersonCamera.m_Follow = character_2.transform;
            thirdPersonCamera.m_LookAt = character_2.transform;
            firstPersonCamera.transform.position = camera_2_Location.transform.position; 
            character_2.GetComponent<characterMovement>().isTurnOver = false;
        }

        if (Input.GetButtonDown("SwapCharacter") && character_2.GetComponent<characterMovement>().isTurnOver)
        {
            thirdPersonCamera.m_Follow = character_1.transform;
            thirdPersonCamera.m_LookAt = character_1.transform;
            character_1.GetComponent<characterMovement>().isTurnOver = false;
        }
    }







}//END CLASS
