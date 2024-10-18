using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    float osa_x;
    float osa_y;
    public GameObject player;
    float mousesensitivity = 6;
    public GameObject ak;
    public GameObject pistol;
    TMP_Text ammoText;
    Animator objectWithAnim;

    void Start()
    {
        ammoText = GameObject.FindGameObjectWithTag("ammo").GetComponent<TMP_Text>();
        objectWithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        osa_x -= Input.GetAxis("Mouse Y") * mousesensitivity;
        osa_y += Input.GetAxis("Mouse X") * mousesensitivity;
        osa_x = Mathf.Clamp(osa_x, 335, 420);
        transform.localEulerAngles = new Vector3(osa_x, osa_y, 0);

        if (Input.GetAxis("Mouse ScrollWheel") != 0 && objectWithAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            ChangeWeapon();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            objectWithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();

        }


    }

    void ChangeWeapon()
    {
        if (ak.activeSelf)
        {
            pistol.SetActive(true);
            ak.SetActive(false);
            ammoText.text = pistol.GetComponent<Shoot>().ammo.ToString() + "/ 30";
        }
        else
        {
            pistol.SetActive(false);
            ak.SetActive(true);
            ammoText.text = ak.GetComponent<Shoot>().ammo.ToString() + "/ 30";
        }

        objectWithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();
    }

    
}
