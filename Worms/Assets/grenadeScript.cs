using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour
{
    private Rigidbody grenadeBody;
    public float grenadeThrowForce = 30f;

    private float grenadeBlowRadius = 15f;
    public float grenadeBlowForce = 2500f;
    public float grenadeBlowDamange;
    public float grenadeFuseTime = 3f;

    public LayerMask whatIsBlastable;

    // Start is called before the first frame update
    void Start()
    {
        grenadeBody = GetComponent<Rigidbody>();
        grenadeBody.AddRelativeForce(Vector3.forward * grenadeThrowForce);
        
        StartCoroutine(WaitToExplode());


    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitToExplode()
    {
        yield return new WaitForSeconds(grenadeFuseTime);

        Collider[] grenadeHitCharacters = Physics.OverlapSphere(transform.position, grenadeBlowRadius, whatIsBlastable);
        for (int i = 0; i < grenadeHitCharacters.Length; i++)
        {
            Vector3 explosionDirection = (transform.position - grenadeHitCharacters[i].transform.position);
            grenadeHitCharacters[i].GetComponent<characterMovement>().ExplosionMovingFunction(explosionDirection);
        }

     

    }

 





/*
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, grenadeBlowRadius);
    }




*/

}



