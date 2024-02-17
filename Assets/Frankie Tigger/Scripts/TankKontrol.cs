using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TankKontrol : MonoBehaviour
{
    [SerializeField]
    private float ileriHiz = 5f; // Tankýn ileri gidiþ hýzý
    [SerializeField]
    private float geriHiz = 5f; // Tankýn geri gidiþ hýzý

    private bool ileriGidiyor = false; // Tankýn ileri gitme durumu
    private bool geriGidiyor = false; // Tankýn geri gitme durumu

    void Start()
    {
        // Tanký ileri hareket ettir
        IleryeGit();
    }

    void FixedUpdate()
    {
        if (ileriGidiyor)
        {
            transform.Translate(Vector3.forward * ileriHiz * Time.fixedDeltaTime);
        }
        else if (geriGidiyor)
        {
            transform.Translate(-Vector3.forward * geriHiz * Time.fixedDeltaTime);
        }
    }

    // Tanký ileri hareket ettiren metod
    void IleryeGit()
    {
        ileriGidiyor = true;
        geriGidiyor = false;
        Invoke("GeriyeGit", 5f); // 5 saniye sonra geriye gitmeyi baþlat
    }

    // Tanký geri hareket ettiren metod
    void GeriyeGit()
    {
        ileriGidiyor = false;
        geriGidiyor = true;
        Invoke("IleryeGit", 5f); // 5 saniye sonra tekrar ileri gitmeyi baþlat
    }
}