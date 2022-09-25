using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour
{
    private Rigidbody grenadeBody;
    public float throwForce;

    public float blowRadius;
    public float blowForce;
    public float blowDamange;

    public LayerMask whatIsBlastable;
    
    // Start is called before the first frame update
    void Start()
    {
        grenadeBody = GetComponent<Rigidbody>();
        grenadeBody.AddRelativeForce(Vector3.forward * throwForce);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

