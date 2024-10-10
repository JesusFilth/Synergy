using UnityEngine;

public class BonusTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerAnimation player))
            player.ToBonus();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerAnimation player))
            player.ToBonus();
    }
}
