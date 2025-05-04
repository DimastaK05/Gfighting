using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer3 : MonoBehaviour
{
    public int damage = 10; // Количество урона

    public void DealDamage3(GameObject target)
    {
        PlayerManager3 playerHealth = target.GetComponent<PlayerManager3>(); // Получаем компонент Health


        if (playerHealth != null && PlayerManager3.playerHealth >= 0)
        {
            playerHealth.Damage(damage); // Вызываем метод TakeDamage
        }
    }
}
