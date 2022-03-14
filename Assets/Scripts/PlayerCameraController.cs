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


    ThirdPersonController thirdPersonController;
    StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
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
        }
        else
        {
            combatCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
        }
    }
}
