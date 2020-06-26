using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵を動かすロジックを作る
public class PatrollingLogic : MonoBehaviour
{
    //unityエディタから操作できる値
    //方向（前、右、後ろ、左）の配列
    public Vector3[] directions;
    //方向転換にかける時間
    public float timeToChange = 1f;
    //動く速度
    public float movementSpeed;

    //unityエディタから操作できない値
    //開始したら方向ポインタを0に設定し、再移動させるため、タイマもセット
    //方向ポインタ
    private int directionPointer;
    //方向タイマ
    private float directionTimer;


    /*
     * アクションポインタの数字は時計回り
     * 0   1   2   3  
     * 前、右、後ろ、左
     * 4になると割り当てがないので3->0へリセットする（後ほど）
     */

    // Start is called before the first frame update
    void Start()
    {
        //上からスタート
        directionPointer = 0;
        directionTimer = timeToChange;
    }

    // Update is called once per frame
    void Update()
    {
        //以下方向転換ロジック
     　　//1フレームごとに時間減らす
        directionTimer -= Time.deltaTime;
        //タイマーが0になったら（一秒）
        if (directionTimer <= 0f)
        {
            //タイマー回復し
            directionTimer = timeToChange;
            //向きを変える
            directionPointer++;

            //前、右、後ろ、左でループするよう条件指定
            //（方向ポインタが左までいったら前から再スタート）
            if (directionPointer >= directions.Length)
            {
                directionPointer = 0;
            }
        }

        //x,z平面上でオブジェクトを動かす
        //移動 = 方向 * 速度
        GetComponent<Rigidbody>().velocity = new Vector3(
            //移動方向をdirectionPointerの値に設定
            directions[directionPointer].x * movementSpeed,
            GetComponent<Rigidbody>().velocity.y, //y(高さ)は変化させない
            directions[directionPointer].z * movementSpeed

        );
    }
}
