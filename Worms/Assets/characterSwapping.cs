using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class characterSwapping : MonoBehaviour  
{
    public GameObject character_1;
    public GameObject character_2;
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;
    public GameObject camera_1_Location;
    public GameObject camera_2_Location;
    public GameObject swapTeamConfirmation;
    public GameObject team_1_WinText;
    public GameObject team_2_WinText;
    public GameObject pauseMenu;
    public TextMeshProUGUI distanceTraveled;
    public TextMeshProUGUI grenadesUsed;

    private bool isTeam_1_Turn = true;
    private bool isTeam_2_Turn = false;

    private bool isDoingConfirmation = false;
    public bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        swapTeamConfirmation.SetActive(false);
        team_1_WinText.SetActive(false);
        team_2_WinText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //pull up confirmation text as long as it's not up already and wait for what the player does next
        if (Input.GetButtonDown("SwapTeam") && !isDoingConfirmation && !character_1.GetComponent<characterMovement>().isDead && !character_2.GetComponent<characterMovement>().isDead && !isPaused)
        {
            isDoingConfirmation = true;
            swapTeamConfirmation.SetActive(true);
            StartCoroutine(SwitchTeam());
        }

        if (character_2.GetComponent<characterMovement>().isDead)
        {
            team_1_WinText.SetActive(true);
        }

        if (character_1.GetComponent<characterMovement>().isDead)
        {
            team_2_WinText.SetActive(true);
        }

        if (Input.GetButtonDown("Cancel") && !isPaused && !isDoingConfirmation)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            StartCoroutine(PauseMenu());
        }

        //Rounds out the distance traveled that turn, shows it and also shows your max distance you're allowed to travel
        if (isTeam_1_Turn)
        {
            distanceTraveled.SetText("Distance traveled: " + (Mathf.Round(character_1.GetComponent<characterMovement>().playerTotalDistanceTraveled)).ToString() + " out of " + (character_1.GetComponent<characterMovement>().allowedDistancePerTurn).ToString());
            grenadesUsed.SetText("Grenades used: " + (character_1.GetComponent<weaponShooting>().equipmentUsed) + " out of " + (character_1.GetComponent<weaponShooting>().maxEquipmentUsed));
        }
        
        if (isTeam_2_Turn)
        {
            distanceTraveled.SetText("Distance traveled: " + (Mathf.Round(character_2.GetComponent<characterMovement>().playerTotalDistanceTraveled)).ToString() + " out of " + (character_2.GetComponent<characterMovement>().allowedDistancePerTurn).ToString());
            grenadesUsed.SetText("Grenades used: " + (character_2.GetComponent<weaponShooting>().equipmentUsed) + " out of " + (character_2.GetComponent<weaponShooting>().maxEquipmentUsed));
        }


    }



    //Wait a wee bit, then wait until the player hits swap team or cancel, and then swap team or reset the confirmation thingy
    private IEnumerator SwitchTeam()
    {

        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(()=> Input.GetButtonDown("SwapTeam") || Input.GetButtonDown("Cancel"));

        if (Input.GetButtonDown("SwapTeam"))
        {
            //swaps from team 1 to team 2
            if (!isTeam_2_Turn)
            {
                swapTeamConfirmation.SetActive(false);
                thirdPersonCamera.m_Follow = character_2.transform;
                thirdPersonCamera.m_LookAt = character_2.transform;
                firstPersonCamera.transform.parent = character_2.transform;
                firstPersonCamera.transform.position = camera_2_Location.transform.position;

                character_2.GetComponent<characterMovement>().playerTotalDistanceTraveled = 0f;
                               
                character_2.GetComponent<characterMovement>().isThisTeamsTurn = true;
                character_1.GetComponent<characterMovement>().isThisTeamsTurn = false;
                isTeam_1_Turn = false;
                isTeam_2_Turn = true;
                character_1.GetComponent<weaponShooting>().enabled = false;
                character_2.GetComponent<weaponShooting>().enabled = true;
                character_2.GetComponent<weaponShooting>().canShoot = true;
                character_2.GetComponent<weaponShooting>().equipmentUsed = 0;
            }
            //swaps from team 2 to team 1
            else if (!isTeam_1_Turn)
            {
                swapTeamConfirmation.SetActive(false);
                thirdPersonCamera.m_Follow = character_1.transform;
                thirdPersonCamera.m_LookAt = character_1.transform;
                firstPersonCamera.transform.parent = character_1.transform;
                firstPersonCamera.transform.position = camera_1_Location.transform.position;
                character_1.GetComponent<characterMovement>().playerTotalDistanceTraveled = 0f;
                character_1.GetComponent<characterMovement>().isThisTeamsTurn = true;
                character_2.GetComponent<characterMovement>().isThisTeamsTurn = false;
                isTeam_1_Turn = true;
                isTeam_2_Turn = false;
                character_2.GetComponent<weaponShooting>().enabled = false;
                character_1.GetComponent<weaponShooting>().enabled = true;
                character_1.GetComponent<weaponShooting>().canShoot = true;
                character_1.GetComponent<weaponShooting>().equipmentUsed = 0;
            }
        }
        else
        {
            swapTeamConfirmation.SetActive(false);
        }


        isDoingConfirmation = false;
            
    }

    private IEnumerator PauseMenu()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetButtonDown("Cancel"));
        pauseMenu.SetActive(false);
        isPaused = false;

    }




}//END CLASS
