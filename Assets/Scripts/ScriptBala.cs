using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBala : MonoBehaviour
{

    public float Velocidad;
    public AudioClip Sonido;

    private Rigidbody2D Rigidbody2D;
    private Vector3 Direccion;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sonido);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direccion * Velocidad;
    }

    public void SetDireccion(Vector3 direccion)
    {
        Direccion = direccion;
    }
    public void DestruirBala()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ScriptEnemigo grunt = other.GetComponent<ScriptEnemigo>();
        MovimientoRambo john = other.GetComponent<MovimientoRambo>();
        if (grunt != null)
        {
            grunt.Hit();
        }
        if (john != null)
        {
            john.Hit();
        }
        DestruirBala();
    }

}
