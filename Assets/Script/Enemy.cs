using UnityEngine;
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int HP = 100;
    // [SerializeField] private Transform player;
    protected Transform player;

    [Header("pengaturan State Machine")]

    [SerializeField] private float jarakDeteksi = 6f;    // masuk chase
    [SerializeField] private float jarakSerang = 1.2f;  //masuk attack
    [SerializeField] private float jedaSerang = 1f;     //detik antara serangan

    // state sekarang -- mulai dari IDLE
    private StateZombie state = StateZombie.IDLE;
    private float waktuserangterakhir;

    public float MS = 2f;

    protected virtual void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {

        // PeriksaTransisi();

        // switch (state)
        // {
        //     case StateZombie.IDLE: perilakuidle(); break;
        //     case StateZombie.PATROL: perilakuPatrol(); break;
        //     case StateZombie.CHASE: perilakuChase(); break;
        //     case StateZombie.ATTACK: perilakuAttack(); break;
                
        // }
    }
    

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            MS * Time.deltaTime
        );
    }

    public virtual void serang()
    {
        Debug.Log("Enemy menyerang!");
    }
    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakSerang)
        {
            state = StateZombie.ATTACK; // sangat dekat --> serang
        }
        else if (jarak <= jarakDeteksi) 
        {
            state = StateZombie.CHASE;  // terlihat --> kejar
        }
        else
        {
            state = StateZombie.PATROL; // jauh --> keliling
        }
    }
    void perilakuidle()
    {
        Debug.Log("Zombie diam menunggu");
    }
    void perilakuPatrol()
    {
        Debug.Log("Zombie sedang patrol");
    }
    void perilakuChase()
    {
        Kejar();
        Debug.Log("Zombie sedang mengejar player");
    }
    void perilakuAttack()
    {
        Debug.Log("Zombie sedang menyerang player");
    }

    public void kenaDamage(int jumlah)
    {
        HP -= jumlah;
        Debug.Log($"{gameObject.name} kena damage {jumlah}, HP sisa: {HP}");

        if (HP <= 0)
        {
            Mati();
        }
    }
    private void Mati()
    {
        Debug.Log($"{gameObject.name} mati!");
        Destroy(gameObject);
    }

}

