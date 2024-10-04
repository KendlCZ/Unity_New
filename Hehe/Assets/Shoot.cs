using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    RaycastHit hitInfo;
    Animator objectwithAnim;
    public float dostrel;
    public float sila;
    public float poskozeni;
    public GameObject efekttrefy;
    Image crosshair;
    ParticleSystem efektVystrelu;
    AudioSource audioSource;
    int ammo = 30;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        objectwithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Image>();
        efektVystrelu = GameObject.FindGameObjectWithTag("EffectShoot").GetComponent<ParticleSystem>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !objectwithAnim.GetBool("Run") && !objectwithAnim.GetBool("Holster") && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect") && ammo > 0)
        {
            InvokeRepeating("ShootBullet", 0, 0.25f);
        }

        if (objectwithAnim.GetBool("Aim"))
        {
            crosshair.enabled = false;
        }
        else
        {
            crosshair.enabled = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("ShootBullet");
        }

        if (efektVystrelu.isPlaying && efektVystrelu.time >= 0.15f)
        {
            efektVystrelu.Stop();
        }

        if (Input.GetKey(KeyCode.R))
        {
            objectwithAnim.SetTrigger("Reload");
            ammo = 30;
            text.text = ammo + " / 30";
        }
    }

    void ShootBullet()
    {
        if (ammo > 0)
        {
            efektVystrelu.Stop();
            efektVystrelu.Play();
            audioSource.Stop();
            audioSource.Play();

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, dostrel))
            {
                GameObject trefa = Instantiate(efekttrefy, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(trefa, 1);

                if (hitInfo.transform.GetComponent<Rigidbody>())
                {
                    hitInfo.transform.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * sila);
                }

                if (hitInfo.transform.GetComponent<EnemyHealth>())
                {
                    hitInfo.transform.GetComponent<EnemyHealth>().TakeDamage(poskozeni);
                }
            }
            ammo--;
            text.text = ammo + " / 30";
        }
    }
}
