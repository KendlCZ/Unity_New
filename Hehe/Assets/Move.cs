using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody Player;
    public Animator objectwithAnim;
    bool running;
    public float movementSpeed = 50f;
    void Start()
    {
        Player.GetComponent<Rigidbody>();
        objectwithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)
        && Input.GetKey(KeyCode.LeftShift)
        && !objectwithAnim.GetBool("Aim")
        && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect"))
        {
            Player.AddRelativeForce(new Vector3(0, 0, (movementSpeed + 5) * Time.deltaTime)); // Tento øádek jsme upravili
            objectwithAnim.SetBool("Run", true);
            running = true;
        }


        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !objectwithAnim.GetBool("Aim") && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect"))
        {
            Player.AddRelativeForce(new Vector3(0, 0, 7));
            objectwithAnim.SetBool("Run", true);
            running = true;
        }
        else
        {
            objectwithAnim.SetBool("Run", false);
            running = false;
        }
        if (!running)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Player.AddRelativeForce(new Vector3(movementSpeed, 0, 0));
                CheckAndSetWalkingAnimation();
            }

            if (Input.GetKey(KeyCode.A))
            {
                Player.AddRelativeForce(new Vector3(-movementSpeed, 0, 0));
                CheckAndSetWalkingAnimation();
            }

            if (Input.GetKey(KeyCode.W))
            {
                Player.AddRelativeForce(new Vector3(0, 0, movementSpeed));
                CheckAndSetWalkingAnimation();
            }

            if (Input.GetKey(KeyCode.S))
            {
                Player.AddRelativeForce(new Vector3(0, 0, -movementSpeed));
                CheckAndSetWalkingAnimation();
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                objectwithAnim.SetBool("Walk", false);
            }

            if (Input.GetMouseButtonDown(0) && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect") && !objectwithAnim.GetBool("Holster"))
            {
                objectwithAnim.SetBool("Run", false);
                objectwithAnim.SetBool("Shoot", true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                objectwithAnim.SetBool("Shoot", false);

            }

            if (objectwithAnim.GetBool("Shoot"))
            {
                objectwithAnim.SetBool("Walk", false);
            }

            if (Input.GetMouseButton(1) && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload Ammo Left") && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload Out Of Ammo"))
            {
                objectwithAnim.SetBool("Aim", true);
            }
            else
            {
                objectwithAnim.SetBool("Aim", false);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                objectwithAnim.SetTrigger("Inspect");

            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                objectwithAnim.SetBool("Holster", !objectwithAnim.GetBool("Holster"));
            }
        }
    }

    void CheckAndSetWalkingAnimation()
    {
        if (!objectwithAnim.GetBool("Shoot"))
        {
            objectwithAnim.SetBool("Walk", true);
        }
    }
}
