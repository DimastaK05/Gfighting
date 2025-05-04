using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer3 : MonoBehaviour
{
    public int damage = 10; // ���������� �����

    public void DealDamage3(GameObject target)
    {
        PlayerManager3 playerHealth = target.GetComponent<PlayerManager3>(); // �������� ��������� Health


        if (playerHealth != null && PlayerManager3.playerHealth >= 0)
        {
            playerHealth.Damage(damage); // �������� ����� TakeDamage
        }
    }
}
