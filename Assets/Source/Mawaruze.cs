using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mawaruze : MonoBehaviour
{
    public float RotateSpeed = 100.0f;
    public Vector3 centerPosition = new Vector3( 0, 0, 1.5f );

    void Start()
    {

    }

    void Update()
    {
        // ‰ñ‚·‚º
        this.gameObject.transform.RotateAround( centerPosition, new Vector3( 0, 1, 0 ), Time.deltaTime * RotateSpeed );
    }
}
