using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoRambo : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    public float Velocidad;
    public float FuerzaSalto;
    private bool Suelo;
    private float LastShoot;
    public GameObject BulletPrefab;
    private int Health = 10;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);


        Animator.SetBool("correr", Horizontal != 0.0f);
        
        //Detectar el suelo
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Suelo = true;
        }
        else Suelo = false;

        if (Input.GetKeyDown(KeyCode.W) && Suelo)
        {
            Saltar();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Disparar();
            LastShoot = Time.time;
        }


    }

    public void Hit()
    {
        Health -= 1;
        if (Health == 0) Destroy(gameObject);
    }

    private void Disparar()
    {
        Vector3 direccion;
        if (transform.localScale.x == 1.0f) direccion = Vector3.right;
        else direccion = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direccion * 0.2f, Quaternion.identity);
        bullet.GetComponent<ScriptBala>().SetDireccion(direccion);
    }

    private void Saltar()
    {
        Rigidbody2D.AddForce(Vector2.up * FuerzaSalto);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }
}
