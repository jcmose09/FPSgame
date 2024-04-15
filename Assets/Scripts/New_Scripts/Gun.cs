using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool readyToFire = true;
    [SerializeField] private AudioClip gunShot;
    private AudioSource audioSource;
    private int shotsFired = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Bang()
    {
        if (readyToFire)
        {
            StartCoroutine(ShootAnim());
        }
    }

    // Accessor for checking gun's state
    public bool ReadyToFire()
    {
        return readyToFire;
    }

    public int GetShotsFired()
    {
        return shotsFired;
    }

    private IEnumerator ShootAnim()
    {
        if (audioSource != null)
        {
            audioSource.clip = gunShot;
            audioSource.Play();
        }

        readyToFire = false;
        shotsFired++;

        // Rotate in small increments every 1/30th of a second
        for (int x = 0; x < 4; x++)
        {
            transform.Rotate(-12f, 0, 0);
            yield return new WaitForSeconds(0.033f);
        }
        for (int i = 0; i < 4; i++)
        {
            transform.Rotate(12f, 0, 0);
            yield return new WaitForSeconds(0.033f);
        }

        // Check if shotsFired reaches 7
        if (shotsFired >= 7)
        {
            readyToFire = false;
            shotsFired = 0; // Reset shotsFired counter
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R)); // Wait until R key is pressed
            readyToFire = true; // Set readyToFire to true after R key is pressed
        }
        else
        {
            readyToFire = true;
        }
    }
}

