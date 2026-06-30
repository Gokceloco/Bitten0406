using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;

    private bool _isOpen;
    private bool _isPlayerInRange;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame 
            && _isPlayerInRange 
            && GetComponentInParent<Level>().ReturnEnemyCount() == 0)
        {
            if (_isOpen)
            {
                CloseDoor();
                _isOpen = false;
            }
            else
            {
                OpenDoor();
                _isOpen = true;
            }
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame
                && _isPlayerInRange)
        {
            GetComponentInParent<LevelManager>().gameDirector.uiManager.messageUI.Show("CLEAR LEVEL TO OPEN!", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }

    void OpenDoor()
    {
        doorRight.DOKill();
        doorLeft.DOKill();
        doorRight.DOLocalMoveZ(-2.9f, .2f);
        doorLeft.DOLocalMoveZ(1.5f, .2f);
    }
    void CloseDoor()
    {
        doorRight.DOKill();
        doorLeft.DOKill();
        doorRight.DOLocalMoveZ(-1.43f, .2f);
        doorLeft.DOLocalMoveZ(0, .2f);
    }
}
