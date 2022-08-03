using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.VFX;

public class ChangeLightColor : MonoBehaviour
{
    public Color color;
    public GameObject totem, Light;
    public AudioSource TotemActive, RockLift;
    public float TimeToWait;

    [Header("Particle Systems")]
    public ParticleSystem explosion;
    public ParticleSystem smoke;    

    [Header("Animators")]
    public Animator totemAnimator;
    //public Animator carattere1Animator;
    //public Animator carattere6Animator;

    [Header("Material")]
    public Material newMaterial;
    //public GameObject carattere1, carattere2;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ephemeral")
        {
            explosion.Play();
            smoke.Stop();
            //Light.SetActive(true);
            Light.GetComponent<Light2D>().intensity = Mathf.PingPong(Time.time, 1);
            totemAnimator.SetTrigger("TotemUp");
            //carattere1Animator.SetTrigger("Carattere1Up");
            //carattere6Animator.SetTrigger("Carattere6Up");
            TotemActive.Play();
            StartCoroutine(WaitRockLift());
            totem.GetComponent<MeshRenderer>().material = newMaterial;
            //carattere2.GetComponent<MeshRenderer>().material = newMaterial;
        }
    }

    IEnumerator WaitRockLift()
    {
        yield return new WaitForSeconds(TimeToWait);
        RockLift.Play();
        StopCoroutine(WaitRockLift());
    }
}
