using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera combatCamera;
    [SerializeField] float normalSensitivity;
    [SerializeField] float combatSensitivity;
    [SerializeField] LayerMask combatMask = new LayerMask();
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject hitedPoint;
    [SerializeField] GameObject damageText; 

    ThirdPersonController thirdPersonController;
    StarterAssetsInputs starterAssetsInputs;

    Animator animator;

    WeaponController weaponController;
    //bool weaponEquiped;

    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        weaponController = GetComponent<WeaponController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Vector3 hitTransform = Vector3.zero;
        GameObject hitedObj = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, combatMask))
        {
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.point;
            hitedObj = raycastHit.transform.gameObject;
        }

        if(starterAssetsInputs.changeWeapon == new Vector2(0, 120))
        {
            weaponController.ChangeWeapon(1);
        }
        if (starterAssetsInputs.changeWeapon == new Vector2(0, -120))
        {
            weaponController.ChangeWeapon(-1);
        }

        if (starterAssetsInputs.aim)
        {
            weaponController.SetAnimation(true);

            combatCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(combatSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            crosshair.SetActive(true);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);


        }
        else
        {
            weaponController.SetAnimation(false);
            
            combatCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            crosshair.SetActive(false);


        }

        if (starterAssetsInputs.atack)
        {
            starterAssetsInputs.atack = false;
            switch (weaponController.GetCurrentWeaponType())
            {
                
                case Type.HANDS:
                    break;
                // Each weaponType should have own anims and behaviours
                default:
                    Shoot(hitedObj, hitTransform);
                    break;
            }            
        }

        if (starterAssetsInputs.interact) 
        {
            starterAssetsInputs.interact = false;
            Collider[] objects = Physics.OverlapSphere(transform.position, 2f);
            foreach(var obj in objects){
                if (obj.GetComponent<IInteractible>() != null)
                {
                    obj.GetComponent<IInteractible>().Interact();
                }
            }

        }
    }

    private void Shoot(GameObject hitedObj, Vector3 hitTransform)
    {
        if (hitedObj != null)
        {
            
            if (hitedObj.GetComponent<NPCStats>() != null)
            {
                hitedObj.GetComponent<NPCStats>().GetHit(PlayerStats.Instance.GetCurrentDamage());
                DamageIndicator damageInfo = Instantiate(damageText, hitTransform + new Vector3(0,1,0), Quaternion.identity).GetComponent<DamageIndicator>();
                damageInfo.SetDamageText(PlayerStats.Instance.GetCurrentDamage());
            }
                
        }
    }
}
