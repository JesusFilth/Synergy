using UnityEngine;

public class Barrel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            player.ToDie();
    }
}
