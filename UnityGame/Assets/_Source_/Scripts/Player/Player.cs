using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Wallet _wallet = new Wallet();
    private int HP = 3;

    public event Action<int> CoinChanged;
    public event Action<int> HPChanged;

    private void Start()
    {
        CoinChanged?.Invoke(_wallet.Coins);
        HPChanged?.Invoke(HP);
    }

    public void AddCoin(int value)
    {
        _wallet.AddCoin(value);
        CoinChanged?.Invoke(_wallet.Coins);
    }

    public void AddDamage(int value)
    {
        HP = Mathf.Clamp(HP-value, 0, int.MaxValue);

        HPChanged?.Invoke(HP);

        if (HP == 0)
            ToDie();
    }

    public void ToDie()
    {
        Destroy(gameObject);
    }
}