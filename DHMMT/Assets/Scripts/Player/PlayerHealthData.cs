using UnityEngine;

public class PlayerHealthData : MonoBehaviour,  IHealthData
{
    public static PlayerHealthData instance;

    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    [SerializeField] bool _isAlive = true;

    public float Health { get => _health; set { _health = value; HealthBar.instance.SetValue(_health); } }
    [SerializeField] float _health;

    public float MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
    [SerializeField] float _MaxHealth;

    private void Awake()
    {
        instance = this;

        InvokeRepeating(nameof(SetMaxHealth), 1, 0.5f);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Destroy(gameObject);
        };
    }
    void SetMaxHealth()
    {
        if (HealthBar.instance == null || HealthBar.instance.slider == null)
        {
            return;
        }

        HealthBar.instance.slider.maxValue = MaxHealth;
        CancelInvoke(nameof(SetMaxHealth));
    }
}
