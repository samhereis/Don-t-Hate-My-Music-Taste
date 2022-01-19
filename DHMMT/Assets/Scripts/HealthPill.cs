using Helpers;
using UnityEngine;

public class HealthPill : MonoBehaviour
{
    // Health loot after an enemy dies

    private Vector3 _velocity;

    public PlayerHealthData Target;

    public float PlusToHealth = 40;

    [SerializeField] private float _speed = 0.5f;

    private void OnEnable()
    {
        Target = PlayerHealthData.instance;
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider _triggerEnteredObject_)
    {
        if(_triggerEnteredObject_.GetComponent<PlayerHealthData>())
        {
            if(Target.MaxHealth >= Target.Health + PlusToHealth)
            {
                Target.Health = Target.MaxHealth;
            }
            else
            {
                Target.Health += PlusToHealth;
            }
            Destroy(gameObject);
        }
    }
}
