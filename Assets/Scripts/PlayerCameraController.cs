using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera combatCamera;
    [SerializeField] float normalSensitivity;
    [SerializeField] float combatSensitivity;
    [SerializeField] LayerMask combatMask = new LayerMask();
    [SerializeField] GameObject crosshair;


    ThirdPersonController thirdPersonController;
    StarterAssetsInputs starterAssetsInputs;
    bool weaponEquiped;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        weaponEquiped = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, combatMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim)
        {
            combatCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(combatSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            crosshair.SetActive(true);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            
            if(!weaponEquiped)
            {
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
                
            }


            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        }
        else
        {
            combatCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            combatCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            crosshair.SetActive(false);
        }

        if (starterAssetsInputs.atack)
        {
            if(!weaponEquiped)
                animator.SetTrigger("atack");
            //audio.PlayOneShot(shoot, 1f);
            
            //Vector3 aimDirection = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            //Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            starterAssetsInputs.atack = false;
        }
    }
}
