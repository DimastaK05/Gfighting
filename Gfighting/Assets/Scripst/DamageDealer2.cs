using UnityEngine;

public class DamageDealer2 : MonoBehaviour
{
    public int damage = 10; // Количество урона

    public void DealDamage2(GameObject target)
    {
        PlayerManager2 playerHealth = target.GetComponent<PlayerManager2>(); // Получаем компонент Health


        if (playerHealth != null && PlayerManager2.playerHealth >= 0)
        {
            playerHealth.Damage(damage); // Вызываем метод TakeDamage
        }
    }
}