using TMPro;
using UnityEngine;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.CoinChanged += UpdateData;
    }

    private void OnDisable()
    {
        _player.CoinChanged -= UpdateData;
    }

    private void UpdateData(int value)
    {
        _text.text = value.ToString();
    }
}