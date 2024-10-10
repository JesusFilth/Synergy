using TMPro;
using UnityEngine;

public class HPView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.HPChanged += UpdateData;
    }

    private void OnDisable()
    {
        _player.HPChanged -= UpdateData;
    }

    private void UpdateData(int value)
    {
        _text.text = value.ToString();
    }
}