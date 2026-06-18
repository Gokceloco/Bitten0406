using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource shootAS;

    public void PlayShootAS()
    {
        shootAS.Play();
    }
}
