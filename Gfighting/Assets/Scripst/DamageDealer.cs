using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10; // ���������� �����

    public void DealDamage(GameObject target)
    {
        PlayerManager playerHealth = target.GetComponent<PlayerManager>(); // �������� ��������� Health
        if (playerHealth != null)
        {
            playerHealth.Damage(damage); // �������� ����� TakeDamage
        }
    }
}