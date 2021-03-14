using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemigo : MonoBehaviour
{

    public Transform John;
    public GameObject BulletPrefab;

    private int Vida = 3;
    private float LastShoot;

    // Update is called once per frame
    void Update()
    {
        if (John == null) return;

        Vector3 direccion = John.position - transform.position;
        if (direccion.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distancia = Mathf.Abs(John.position.x - transform.position.x);

        if (distancia < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direccion = new Vector3(transform.localScale.x, 0.0f, 0.0f);
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direccion * 0.1f, Quaternion.identity);
        bullet.GetComponent<ScriptBala>().SetDireccion(direccion);
    }

    public void Hit()
    {
        Vida -= 1;
        if (Vida == 0) Destroy(gameObject);
    }
}