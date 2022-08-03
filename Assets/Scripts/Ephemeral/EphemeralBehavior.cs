using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EphemeralBehavior : MonoBehaviour
{
    [HideInInspector] public Animator ephemeralController;
    public GameObject assignedWell;

    [Header("Function Options")]
    [SerializeField] public bool canMove;

    [Header("Movement Parameters")]
    public int hDistance;
    public int vDistance;
    public int hSpeed;
    public int vSpeed;

    [Header("Particle Systems")]
    //public ParticleSystem body;
    //public ParticleSystem flame;
    public ParticleSystem explosion;

    private void Awake()
    {
        ephemeralController = gameObject.GetComponent<Animator>();
        ephemeralController.SetBool("CanMove", canMove);
    }

    public void WellChange()
    {
        if (assignedWell != null)
        assignedWell.GetComponent<EphemeralWellBehavior>().DeactivateWell();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bark" && ephemeralController.GetBool("CanMove") == true)
        {
            //0 = none, 1 = right, 2 = left, 3 = up, 4 = down..
            if (collision.GetComponentInParent<BarkInteraction>().lastDirection == 1)
            {
                ephemeralController.SetInteger("Direction", 1);
                ephemeralController.SetTrigger("Move");
            }
            else if (collision.GetComponentInParent<BarkInteraction>().lastDirection == 2)
            {
                ephemeralController.SetInteger("Direction", 2);
                ephemeralController.SetTrigger("Move");
            }
            else if (collision.GetComponentInParent<BarkInteraction>().lastDirection == 3)
            {
                ephemeralController.SetInteger("Direction", 3);
                ephemeralController.SetTrigger("Move");
            }
            else if (collision.GetComponentInParent<BarkInteraction>().lastDirection == 4)
            {
                ephemeralController.SetInteger("Direction", 4);
                ephemeralController.SetTrigger("Move");
            }
        }
        else if(collision.tag == "Totem")
        {
            //body.Stop();
            //flame.Stop();
            //explosion.Play();
            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == 8 && assignedWell != null)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            //explosion.Play();
            assignedWell.GetComponent<EphemeralWellBehavior>().Reset();
        }

        else if (collision.tag == "EphemeralWell" && assignedWell == null)
        {
            assignedWell = collision.gameObject;
        }
    }

}
