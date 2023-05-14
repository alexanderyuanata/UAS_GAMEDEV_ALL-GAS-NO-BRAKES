using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControls : MonoBehaviour
{
    public Item_Weapons weapon_data;
    public Animator anim;
    public CharacterController characterController;

    private void Update()
    {
        Debug.Log(characterController.velocity.magnitude);
        anim.SetBool("moving", (characterController.velocity.magnitude > 0f));
    }

}
