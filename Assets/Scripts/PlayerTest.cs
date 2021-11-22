using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerTest : MonoBehaviour
{
    public float h; // test Axis value
    public float v;
    public bool grabZone = false; // ho messo questo bool per testare solo lo zoom
    public float speed = 5f;

    Rigidbody2D rb;
    Animator anim;  ///TODO: maik quando fai il player lascia la ref ANIMATOR 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grab_Zone") 
            grabZone = true;
        anim.SetBool("GrabZone", true);
        Debug.Log("SOLO ZOOM TEST!!!!!!!!!!!");
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grab_Zone")
            grabZone = false;
        anim.SetBool("GrabZone", false);
    }

    void Movement()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, 0) * speed * Time.deltaTime);
    }

    private void Update()
    {
        Movement();
    }

}
