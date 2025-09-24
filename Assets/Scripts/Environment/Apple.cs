using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private float rotTimer = 20;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    public float RotTimer => rotTimer;

    private void OnEnable()
    {
        StartCoroutine(CountdownToRot());
    }

    private IEnumerator CountdownToRot() // корутина гниени€ €блок
    {
        while (rotTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            rotTimer--;
        }
        sr.enabled = false;
        rb.gravityScale = 0;
        col.enabled = false;
        particles.Play();
        while (particles.isPlaying)
            yield return null;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        //Debug.Log($"{gameObject.name} was destroyed!");
    }
}
