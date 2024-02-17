using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopter: MonoBehaviour
{

    public Transform[] propellers; // Pervanelerin Transform bileþenleri
    public float rotationSpeed = 100f;
    public float forwardSpeed = 10f;

    void Update()
    {
        // Pervaneleri döndürme
        foreach (Transform propeller in propellers)
        {
            propeller.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        // Helikopterin ileri doðru hareketi (dünya koordinat sistemi içinde)
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

}


