using UnityEngine;
using System.Collections;

public class BallShooting : MonoBehaviour 
{
    public GameObject Ball;
    public float Force = 50.0f;
    public Vector3 Torque = new Vector3(100, 0, 0);

    private Gun _gun;
    private int shotsfired = 0;

    private bool cursorLocked = false;

	void Start() 
    {
        LockCursor();

        _gun = GameObject.Find("Gun").GetComponent<Gun>();
    }

    void Update()
    {
        if (_gun.ReadyToFire())
        {
            if (Input.GetMouseButtonDown(0))
            {
                _gun.Bang();

                if (shotsfired < 7) // Check if less than 7 shots fired
                {
                    SpawnBall();
                    shotsfired++;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            shotsfired = 0;
        }

        if (Input.GetKeyDown(KeyCode.C))
            LockCursor();
    }

    void LockCursor()
    {
        if (!cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorLocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = false;
        }
    }

    void SpawnBall()
    {
        GameObject ball = Instantiate(Ball, transform.position, Quaternion.identity);
        Rigidbody ball_rb = ball.GetComponent<Rigidbody>();
        ball.name = "ball";
        ball.transform.position = transform.TransformPoint(2 * Vector3.forward);
        ball_rb.AddForce(transform.TransformDirection(new Vector3(0, 0, Force)));
        ball_rb.AddTorque(Torque);
    }
}