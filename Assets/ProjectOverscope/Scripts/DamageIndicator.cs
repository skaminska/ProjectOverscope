using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI damageText;
    [SerializeField]float lifetime = 3f;
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamageText(int damage)
    {
        damageText.text = damage.ToString();
    }
}
