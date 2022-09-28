using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour
{
    private Rigidbody grenadeBody;
    private float grenadeThrowForce = 1000f;

    private float grenadeBlowRadius = 15f;
    private float grenadeBlowDamange = 30f;
    private float grenadeFuseTime = 3f;

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

        //checks colliders that  the grenade can hit within the radius of the grenade blow
        Collider[] grenadeHitCharacters = Physics.OverlapSphere(transform.position, grenadeBlowRadius, whatIsBlastable);
        
        //checks every character within the blast radius, sends the damage and movement info the the character controller script
        for (int i = 0; i < grenadeHitCharacters.Length; i++)
        {
            Vector3 explosionDirection = (transform.position - grenadeHitCharacters[i].transform.position);

            float explosionForce = grenadeBlowRadius - explosionDirection.magnitude;
            
           
            //if the damage is negative, just set it to zero
            if(grenadeBlowDamange < 0f)
            {
                grenadeBlowDamange = 0f;
            }

            grenadeHitCharacters[i].GetComponent<characterMovement>().ExplosionMovingFunction(explosionDirection, grenadeBlowDamange, explosionForce);




        }

        Destroy(gameObject);

    }

 





/*
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, grenadeBlowRadius);
    }




*/

}



