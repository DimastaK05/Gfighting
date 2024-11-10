using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10; // Количество урона

    public void DealDamage(GameObject target)
    {
        PlayerManager playerHealth = target.GetComponent<PlayerManager>(); // Получаем компонент Health
        if (playerHealth != null)
        {
            playerHealth.Damage(damage); // Вызываем метод TakeDamage
        }
    }
}