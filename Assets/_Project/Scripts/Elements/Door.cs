using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;

    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            OpenDoor();
        }
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            CloseDoor();
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
