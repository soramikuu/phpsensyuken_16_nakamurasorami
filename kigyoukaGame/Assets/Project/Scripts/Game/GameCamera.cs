using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject target; //カメラの対象とするオブジェクト（unityエディタから指定できるように宣言）
    public Vector3 targetOffset;
    public Vector3 offset;
    public float focusSpeed = 1f;


    void Start()
    {
        
    }

    void Update()
    {
        if (target != null)
        {
            if (target.GetComponent<Player>() != null)
            {
                //Playerコンポネントを変数playerに代入
                Player player = target.GetComponent<Player>();
                transform.position = Vector3.Lerp(transform.position, player.transform.position + targetOffset, Time.deltaTime * focusSpeed);
            }
        }
        //カメラをターゲット+offsetの位置に置く(update内に書くことで常に追随)
        transform.position = target.transform.position + offset;

        //クリアしたとき
    //    if (currentDungeon.JustCleared)
    //    {
    //        //DungeonコンポーネントからTresureプロパティに格納されたTresureオブジェクトを呼び出しFocusOn　未実装
    //        FocusOn(currentDungeon.Tresure.gameObject);
    //    }
    }

    private void FocusOn(GameObject target)
    {

    }
}
