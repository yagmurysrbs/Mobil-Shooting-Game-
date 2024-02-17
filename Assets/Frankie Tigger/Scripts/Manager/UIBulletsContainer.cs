using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBulletsContainer : MonoBehaviour
{
    public static UIBulletsContainer instance; 

    [Header(" Elements ")]
    [SerializeField] private Transform bulletsParent;


    [Header(" Settings ")]
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    private int bulletsShot;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        PlayerShooter.onShot += OnShotCallback;

        PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;
        PlayerMovement.onExitedWarzone += ExitedWarzoneCallback;
    }


    private void OnDestroy()
    {
        PlayerShooter.onShot -= OnShotCallback;

        PlayerMovement.onEnteredWarzone -= EnteredWarzoneCallback;
        PlayerMovement.onExitedWarzone -= ExitedWarzoneCallback;
    }
    // Start is called before the first frame update
    void Start()
    {

        bulletsParent.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnteredWarzoneCallback()
    {
        bulletsParent.gameObject.SetActive(true);
    }

    private void ExitedWarzoneCallback()
    {
        bulletsParent.gameObject.SetActive(false);

        Reload();
    }


    private void Reload()
    {
        bulletsShot = 0;

        for (int i = 0; i < bulletsParent.childCount; i++)
            bulletsParent.GetChild(i).GetComponent<Image>().color = activeColor;
    }

    private void OnShotCallback()
    {
        bulletsShot++;

       

        bulletsShot = Mathf.Min(bulletsShot, bulletsParent.childCount);

        bulletsParent.GetChild(bulletsShot - 1).GetComponent<Image>().color = inactiveColor;
    }

    public bool CanShoot()
    {
        return bulletsShot < bulletsParent.childCount;
    }
}
