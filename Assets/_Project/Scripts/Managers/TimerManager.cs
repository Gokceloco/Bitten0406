using UnityEngine;
using UnityEngine.InputSystem;

public class TimerManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public TimerUI timerUI;

    private bool _isTimerOn;
    private float _remainingTime;

    private void Update()
    {
        if (_isTimerOn)
        {
            _remainingTime -= Time.deltaTime;

            timerUI.SetFillBar(_remainingTime,
                gameDirector.levelManager.ReturnCurrentLevel()
                .levelTime);

            if (_remainingTime <= 0)
            {
                _isTimerOn = false;
                gameDirector.player
                    .ChangeAnimationState(PlayerAnimationState.Die);
                gameDirector.LevelFailed();
            }
        }      
    }

    public void StartTimer(float totalTime)
    {
        _isTimerOn = true;
        _remainingTime = totalTime;
    }
}
