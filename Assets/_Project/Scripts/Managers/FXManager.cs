using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem impactPS;

    public void PlayImpactPS(Vector3 pos, Vector3 dir)
    {
        var newPS = Instantiate(impactPS);
        newPS.transform.position = pos;
        newPS.transform.LookAt(pos - dir);
        newPS.Play();
    }
}
