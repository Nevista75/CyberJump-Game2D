using UnityEngine;

public class UfoTrigger : MonoBehaviour
{
    [Tooltip("Tag yang dipasang pada semua GameObject UFO Anda")]
    public string ufoTag = "UFO"; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player menyentuh trigger! Mencari semua UFO dengan tag: " + ufoTag);
            
            // Mencari SEMUA object di scene yang memiliki tag tersebut
            GameObject[] semuaUfo = GameObject.FindGameObjectsWithTag(ufoTag);
            
            foreach (GameObject ufoObj in semuaUfo)
            {
                UfoMovement movement = ufoObj.GetComponent<UfoMovement>();
                if (movement != null)
                {
                    movement.Activate();
                }
            }
            
            Debug.Log(semuaUfo.Length + " UFO telah diaktifkan secara bersamaan!");
        }
    }
}