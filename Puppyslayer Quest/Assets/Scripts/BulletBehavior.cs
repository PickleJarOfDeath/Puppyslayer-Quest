using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;
    public GameObject explosion;
    public bool fuseContact = false;
    private Vector3 hitPosition;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            Debug.Log("fuse triggered");
            fuseContact = true;
            hitPosition = this.transform.position - GetComponent<Rigidbody>().velocity.normalized;
            GameObject newExplosion = Instantiate(explosion, hitPosition, this.transform.rotation) as GameObject;
            fuseContact = false;
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fuseContact == true)
        {
            //GameObject newExplosion = Instantiate(explosion, hitPosition, this.transform.rotation) as GameObject;
            //fuseContact = false;
            //Destroy(this.gameObject);
        }
    }
}
