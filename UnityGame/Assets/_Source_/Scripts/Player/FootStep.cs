using UnityEngine;

public class FootStep : MonoBehaviour
{
    [SerializeField] private PlayerMoveble _playerMoveble;
    [SerializeField] private ParticleSystem _stepParticle;

    private bool _isActive;

    private void Update()
    {
        if (_isActive == _playerMoveble.IsWalk)
            return;

        _isActive = _playerMoveble.IsWalk;
        ChangeSteps();
    }

    private void ChangeSteps()
    {
        if(_isActive)
            _stepParticle.Play();
        else 
            _stepParticle.Stop();
    }
}