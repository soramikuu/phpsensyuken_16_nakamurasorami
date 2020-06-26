using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float swingingSpeed = 2f; //剣振るスピード
    public float cooldownSpeed = 2f; //戻るスピード
    public float attackDuration = 0.5f;

    public float cooldownDuration = 0.5f; //再び剣振れるまでの時間

    private Quaternion targetRotation;
    private float cooldownTimer;
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = Quaternion.Euler(0, 0, 0); //物体の三次元の回転0
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            //剣の回転
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * swingingSpeed); //引数はaとbの間をtで回転する。
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * cooldownSpeed);
        }
        cooldownTimer -= Time.deltaTime;

    }

    public void Attack()
    {
        if (cooldownTimer > 0)
        {
            return;
        }

        targetRotation = Quaternion.Euler(90, 0, 0);

        cooldownTimer = cooldownDuration;

        StartCoroutine(CooldownWait());
    }

    //IEnumeratorは反復処理をサポートする型 待機時間を書く
    private IEnumerator CooldownWait()
    {
        isAttacking = true;

        //呼び出し元に値を返しつつ繰り返し処理を継続する 待機時間の処理
        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;

        //物体の三次元回転
        targetRotation = Quaternion.Euler(0, 0, 0);
    }
}
