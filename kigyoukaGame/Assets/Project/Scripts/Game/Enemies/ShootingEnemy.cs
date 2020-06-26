using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy //Enemyクラスを継承
{
    //unityエディタから操作できる変数（実数等）
    //ShootingEnemyの中のモデル
    public GameObject model;
    //回転するまでの時間
    public float timeToRotate = 2f;
    public float rotationSpeed = 6f;
    //弾丸プレハブの生成
    public GameObject bulletPrefab;
    public float timeToShoot = 1f;

    //固定変数
    //オブジェクトの回転
    private Quaternion objectRotation;
    //回転する角度（向き）
    private int objectAngle;
    //回転タイマー
    private float rotationTimer;
    //シューティングタイマー
    private float shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        //デフォルト値の設定
        rotationTimer = timeToRotate;
        shootingTimer = timeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the enemy's angle
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0f)
        {
            //時間リセット
            rotationTimer = timeToRotate;
            //向き変える
            objectAngle += 90;
        }

        //回転
        //unityエディタ上では、回転量はオイラー角が使われているが、内部では四元数が使われている。
        //親オブジェクトの向きを継承するローカル座標系で向きを回転させる（　<=> 親オブジェクトの影響を受けないワールド座標系で回転させるにはrotation）
        //Lerpの引数は回転開始値、回転終了値、回転速度 Time.deltaTimeを掛けることで毎フレームごとの動きになる（スムーズな回転に）
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, objectAngle, 0), Time.deltaTime * rotationSpeed);

        //弾丸シューティング
        shootingTimer -= Time.deltaTime;
        //タイマが0秒以下になったら
        if (shootingTimer <= 0f)
        {
            //タイマリロード
            shootingTimer = timeToShoot;
            //弾丸生成
            GameObject bulletObject = Instantiate(bulletPrefab);
            //弾丸位置 = shootingenemy の前方
            bulletObject.transform.position = transform.position + model.transform.forward;

            //弾丸が進む方向
            bulletObject.transform.forward = model.transform.forward;

        }
    }
}
