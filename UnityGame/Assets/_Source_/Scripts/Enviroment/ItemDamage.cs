using UnityEngine;

public class ItemDamage : MonoBehaviour
{
    [SerializeField] private int _value;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.AddDamage(_value);
        }
    }
}