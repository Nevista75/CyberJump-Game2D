using System.Collections;
using UnityEngine;

public abstract class PlayerRespawn : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Vector3 spawnPosition;
    protected Vector3 cameraSpawnPosition;
    protected bool isDead = false;
    [SerializeField] public SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        spawnPosition = transform.position;

        if (Camera.main != null)
        {
            cameraSpawnPosition = Camera.main.transform.position;
        }
    }

    public virtual void Die()
    {
        if (!isDead)
        {
            StartCoroutine(RespawnDelay());
        }
    }

    protected virtual IEnumerator RespawnDelay()
    {
        isDead = true;

        AudioManager.Instance.Play(AudioManager.SoundType.Hurt);

        rb.linearVelocity = Vector2.zero;

        if (spriteRenderer != null)
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(1f);

        Respawn();

        if (spriteRenderer != null)
        spriteRenderer.enabled = true;

        isDead = false;
    }

    protected abstract void Respawn();
}
