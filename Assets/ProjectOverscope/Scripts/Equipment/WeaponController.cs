using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponController : Singleton<WeaponController>
{
    [SerializeField] List<Weapon> equipedWeapons;
    [SerializeField] TextMeshProUGUI weaponInfo;
    [SerializeField] List<TextMeshProUGUI> weapons;
    [SerializeField] Animator animator;
    [SerializeField] Rig weaponTarget;
    [SerializeField] Rig leftHandRig;
    //temporary. Later weapons game objects will be get from weapon SO
    [SerializeField] GameObject weaponPistol, weaponRifle;

    int currentWeapon;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentWeapon = 0;
        ShowEquipedWeapons();
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, 0.3f));
        weaponPistol.SetActive(false);
        weaponRifle.SetActive(false);
        weaponTarget.weight = 0;
        leftHandRig.weight = 0;
    }

    public Type GetCurrentWeaponType()
    {
        return equipedWeapons[currentWeapon].weaponType;
    }
    
    public void ChangeWeapon(int change)
    {
        if (change > 0)
        {
            if (currentWeapon == equipedWeapons.Count-1)
                currentWeapon = 0;
            else
                currentWeapon++;
        }
        else
        {
            if (currentWeapon == 0)
                currentWeapon = equipedWeapons.Count-1;
            else
                currentWeapon--;
        }
        SetWeapon();
    }

    public void ChangeWeapon(Weapon weapon1, Weapon weapon2)
    {
        equipedWeapons[1] = weapon1;
        equipedWeapons[2] = weapon2;

        ShowEquipedWeapons();
    }

    public void ShowEquipedWeapons()
    {
        weapons[0].text = equipedWeapons[1].itemName;
        weapons[1].text = equipedWeapons[2].itemName;
    }

    private IEnumerator ChangeLeftHandRigWeights(int leftHandWeight)
    {
        yield return new WaitForEndOfFrame();
        leftHandRig.weight = leftHandWeight;
    }
    private void SetWeapon()
    {
        if (equipedWeapons[currentWeapon].weaponType == Type.HANDS)
        {
            StartCoroutine(ChangeLeftHandRigWeights(0));
            weaponPistol.SetActive(false);
            weaponRifle.SetActive(false);
        }
        else if (equipedWeapons[currentWeapon].weaponType == Type.PISTOL)
        {
            weaponPistol.SetActive(true);
            if (weaponRifle.activeInHierarchy)
                weaponRifle.SetActive(false);
            StartCoroutine(ChangeLeftHandRigWeights(0));
        }
        else if (equipedWeapons[currentWeapon].weaponType == Type.RIFLE)
        {
            weaponRifle.SetActive(true);
            if (weaponPistol.activeInHierarchy)
                weaponPistol.SetActive(false);
            StartCoroutine(ChangeLeftHandRigWeights(1));
        }
        animator.runtimeAnimatorController = equipedWeapons[currentWeapon].GetWeaponAnimatorController();
        weaponInfo.text = equipedWeapons[currentWeapon].itemName;
        PlayerStats.Instance.SetCurrentDamage(equipedWeapons[currentWeapon].minDamage, equipedWeapons[currentWeapon].maxDamage);
    }

    IEnumerator ChangeWeaponTargetRigWeight(int targetWeight)
    {
        yield return new WaitForEndOfFrame();
        weaponTarget.weight = targetWeight;
    }
    public void SetAnimation(bool aiming)
    {
        animator.SetBool("Aiming", aiming);
        if(aiming)
            StartCoroutine(ChangeWeaponTargetRigWeight(1));
        else
            StartCoroutine(ChangeWeaponTargetRigWeight(0));

        //if (aiming)
        //animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, 0.3f));
        //else
        //animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, 0.3f));

    }
}
