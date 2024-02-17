using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TankKontrol : MonoBehaviour
{
    [SerializeField]
    private float ileriHiz = 5f; // Tank�n ileri gidi� h�z�
    [SerializeField]
    private float geriHiz = 5f; // Tank�n geri gidi� h�z�

    private bool ileriGidiyor = false; // Tank�n ileri gitme durumu
    private bool geriGidiyor = false; // Tank�n geri gitme durumu

    void Start()
    {
        // Tank� ileri hareket ettir
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

    // Tank� ileri hareket ettiren metod
    void IleryeGit()
    {
        ileriGidiyor = true;
        geriGidiyor = false;
        Invoke("GeriyeGit", 5f); // 5 saniye sonra geriye gitmeyi ba�lat
    }

    // Tank� geri hareket ettiren metod
    void GeriyeGit()
    {
        ileriGidiyor = false;
        geriGidiyor = true;
        Invoke("IleryeGit", 5f); // 5 saniye sonra tekrar ileri gitmeyi ba�lat
    }
}