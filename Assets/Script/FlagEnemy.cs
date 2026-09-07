using UnityEngine;

public class FlagEnemy : Enemy
{
    public bool flag = true;

    public override void serang()
    {
        Debug.Log("FlagEnemy menyerang!");
    }
}
