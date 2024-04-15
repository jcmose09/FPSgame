using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText; 

    private Gun gun;

    void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        if (gun == null)
        {
            Debug.LogError("Gun script not found!");
        }
    }

    void Update()
    {
        if (gun != null && gun.ReadyToFire())
        {
            int remainingShots = 7 - gun.GetShotsFired();
            ammoText.text = "Ammo: " + remainingShots;
        }
    }
}
