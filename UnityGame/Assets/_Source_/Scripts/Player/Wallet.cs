using UnityEngine;

public class Wallet
{
    public int Coins { get; private set; }

    public void AddCoin(int value)
    {
        Coins = Mathf.Clamp(Coins + value, 0, int.MaxValue);
    }
}
