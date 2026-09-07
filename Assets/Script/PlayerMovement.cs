using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f;

    private Vector2 arahGerak;

    public int Skor = 0;

    public GameManager gameManager;

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;
    }


    // Mengambil Coin
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject); 
            Skor += 1;
            Debug.Log("Skor: " + Skor);

            gameManager.AmbilKoin(); // Memanggil fungsi AmbilKoin() dari GameManager
        }
    }
    
}
