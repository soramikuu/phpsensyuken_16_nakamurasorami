using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //クラス内でhp変数定義
    public int hp = 1;

    //hpは敵の種類によって変わるため仮の関数にする
    public virtual void Hit()
    {
        hp--;
        if (hp <= 0) {
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter (Collider otherCollider)
    {
        if (otherCollider.GetComponent<Sword>() != null){
            if (otherCollider.GetComponent<Sword>().isAttacking)
            {
                Hit();
            }
        }
        else if (otherCollider.GetComponent<Arrow>() != null)
        {
            Hit();
            //矢も同時に破壊
            Destroy(otherCollider.gameObject);
        }
    }
}
