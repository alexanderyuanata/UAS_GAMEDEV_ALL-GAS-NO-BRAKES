using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControls : MonoBehaviour
{
    public Item_Weapons weapon_data;
    public Animator anim;
    public CharacterController characterController;

    public float damage = 10f;
    public float range = 50f;

    public Camera fpsCam;


    private void Update()
    {
        anim.SetBool("moving", (characterController.velocity.magnitude > 0f));
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

}
