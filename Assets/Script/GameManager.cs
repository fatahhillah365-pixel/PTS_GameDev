using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int totalKoin;

    private int koinTerkumpul = 0;


    void Start()
    {
        // Menghitung jumlah Coin yang ada di scene
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;

        Debug.Log("Total Coin : " + totalKoin);
    }


    public void AmbilKoin()
    {
        // Menambah jumlah koin yang diambil
        koinTerkumpul++;

        // Jika semua koin sudah diambil
        if (koinTerkumpul == totalKoin)
        {
            Menang();
        }
    }


    void Menang()
    {
        Debug.Log("KAMU MENANG!");
    }
}