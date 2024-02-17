using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopter: MonoBehaviour
{

    public Transform[] propellers; // Pervanelerin Transform bile�enleri
    public float rotationSpeed = 100f;
    public float forwardSpeed = 10f;

    void Update()
    {
        // Pervaneleri d�nd�rme
        foreach (Transform propeller in propellers)
        {
            propeller.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        // Helikopterin ileri do�ru hareketi (d�nya koordinat sistemi i�inde)
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

}


