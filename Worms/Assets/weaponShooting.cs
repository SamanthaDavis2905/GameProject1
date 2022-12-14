using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class weaponShooting : MonoBehaviour
{
    public GameObject grenadePrefab;
    private bool grenadeSelected = true;
    public bool canShoot = true;
    public CinemachineVirtualCamera firstPersonCamera;
    public Transform shootingPosition;
    public int equipmentUsed = 0;
    public int maxEquipmentUsed = 5;

    public characterSwapping characterSwappingScript;

    // Start is called before the first frame update
    void Start()
    {

       
    }
   
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2") && !characterSwappingScript.isPaused)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if(canShoot)
                {
                    if (grenadeSelected)
                    {
                        Instantiate(grenadePrefab, shootingPosition.position, firstPersonCamera.transform.rotation);
                        equipmentUsed++;
                    }
                  
                }
            }
        }
    
        if(equipmentUsed >= maxEquipmentUsed)
        {
            canShoot = false;
        }
    
    
    
    }
    


}
