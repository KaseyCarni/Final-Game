using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KACA_Hazard : MonoBehaviour
{

    public Transform target;

    public int moveSpeed;

    public int rotationSpeed;

    private Transform myTransform;


    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        target = go.transform;
    }
    

    void Update()
    {
        Vector3 dir = target.position - myTransform.position;
        dir.z = 0.0f;
        if (dir != Vector3.zero)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
        }

    }

}