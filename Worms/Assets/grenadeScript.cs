using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour
{
    private Rigidbody grenadeBody;
    public float grenadeThrowForce = 30f;

    private float grenadeBlowRadius = 15f;
    public float BlowForce;
    public float BlowDamange;
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
       
        if (Physics.CheckSphere(transform.position, grenadeBlowRadius, whatIsBlastable))
        {
            Debug.Log("grenade boom");
        }




    }

/*
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, grenadeBlowRadius);
    }

*/

}



