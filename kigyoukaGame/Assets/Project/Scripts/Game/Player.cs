using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //メンバ変数

    //エディタから操作できる変数
    //プレイヤ外観
    [Header("Visuals")]
    public GameObject model;
    public float rotatingSpeed = 2f; //publicの後に変数宣言することでunityエディタから変数いじれる

    //動き
    [Header("Movement")]
    public float movingVelocity;
    //public float junmingForce;
    public float jumpingVelocity;
    public float knockbackForce;

    //装備
    [Header("Equipment")]
    public int hp = 5;
    public Sword sword;
    public GameObject bombPrefab;
    public int bombAmount = 15;
    public float throwingSpeed;
    public Bow bow;
    public int tresureAmount = 0;

    //エディタから操作できない変数
    private Rigidbody playerRigidbody;
    private bool canJump;
  　//プレーヤの向き
    private Quaternion targetModelRotation;
    private float knockbackTimer;
    //テレポート
    private bool justTeleported;
    //ダンジョン
    private Dungeon currentDungeon;

    //プロパティ
    public bool JustTeleported
    {
        get
        {
            bool returnValue = justTeleported;
            justTeleported = false;
            return returnValue;
        }
    }

    public Dungeon CurrentDungeon
    {
        get{
            return currentDungeon;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //プレーヤの剛体設定
        playerRigidbody = GetComponent<Rigidbody>();
        //プレーヤの向き
        targetModelRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Raycat to identify if the player can jump. 床に接触したときのみジャンプできる（空中ジャンプはできない）
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f)){ //out hitでRayが当たったオブジェクトの情報取得
            //ジャンプOK
            canJump = true;
        };

        //プレーヤの回転
        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, Time.deltaTime * rotatingSpeed);

        
        if (knockbackTimer > 0)
        {
            //一秒間はノックバック処理続ける（後方にふっとぶ）
            knockbackTimer -= Time.deltaTime;
        }
        else
        {
            ProcessInput();
        }

        Debug.Log(currentDungeon);

    }

    void ProcessInput()
    {
        //プレーヤの速度　デフォルトは0
        playerRigidbody.velocity = new Vector3(
                0,
                playerRigidbody.velocity.y,
                0
            );

        // Move in XZ plane. 以下平面の動き

        if (Input.GetKey("right"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(
                -movingVelocity,
                playerRigidbody.velocity.y,
                playerRigidbody.velocity.z
            );
            //transform.position += Vector3.right * 5f * Time.deltaTime;
            //transform.position = new Vector3(
            //    transform.position.x + 5f * Time.deltaTime,
            //    transform.position.y,
            //    transform.position.z
            //);

            //右に向く
            targetModelRotation = Quaternion.Euler(0, 270, 0);
            //model.transform.localEulerAngles = new Vector3(0, 270, 0); //EulerAngles = 姿勢

        }
        if (Input.GetKey("left"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(
                movingVelocity,
                playerRigidbody.velocity.y,
                playerRigidbody.velocity.z
            );
            //transform.position += Vector3.left * 5f * Time.deltaTime;

            //左向く
            targetModelRotation = Quaternion.Euler(0, 90, 0);
            //model.transform.localEulerAngles = new Vector3(0, 90, 0);


        }
        if (Input.GetKey("up"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(
                playerRigidbody.velocity.x,
                playerRigidbody.velocity.y,
                -movingVelocity
                
            );
            //transform.position += Vector3.forward * 5f * Time.deltaTime;

            //後ろ向く
            targetModelRotation = Quaternion.Euler(0, 180, 0);
            //model.transform.localEulerAngles = new Vector3(0, 180, 0);

        }
        if (Input.GetKey("down"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(
                playerRigidbody.velocity.x,
                playerRigidbody.velocity.y,
                movingVelocity
            );
            //transform.position += Vector3.back * 5f * Time.deltaTime;

            //前向く
            targetModelRotation = Quaternion.Euler(0, 0, 0);
            //model.transform.localEulerAngles = new Vector3(0, 0, 0); 


        }
        // ジャンプ
        if (canJump == true && Input.GetKeyDown("space"))
        {
            canJump = false; //何度も空中でジャンプはできない
            //GetComponent<Rigidbody>().AddForce(0, junmingForce, 0);
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                jumpingVelocity,
                playerRigidbody.velocity.z

            );
        }

        // 装備
        // 剣を振り下ろす
        if (Input.GetKeyDown("z"))
        {
            sword.Attack();
        }
        //矢を放つ
        if (Input.GetKeyDown("x"))
        {
            bow.Attack();
        }

        //爆弾投げる
        if (Input.GetKeyDown("c"))
        {
            ThrowBomb();
        }

    }

    private void ThrowBomb()
    {
        if (bombAmount <= 0)
        {
            return;
        }

        //プレハブから爆弾生成
        GameObject bombObject = Instantiate(bombPrefab);
        //プレーヤの前に生成
        bombObject.transform.position = transform.position + model.transform.forward;

        //投げる方向は斜め上
        Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;

        bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection *  throwingSpeed);

        bombAmount--;
    }

    //トリガー付きの物体に衝突したときに発生させるイベント
    void OnTriggerEnter (Collider otherCollider)
    {
        //Debug.Log(otherCollider.name);
        //ぶつかった物体にEnemyBulletスクリプトがアタッチされていたとき
        if (otherCollider.GetComponent<EnemyBullet>() != null)
        {
            //プレーヤにダメージ
            Hit((transform.position - otherCollider.transform.position).normalized);
            //Destroy(otherCollider.gameObject);
        }
        else if (otherCollider.GetComponent<Tresure>() != null){
            tresureAmount++;
            Destroy(otherCollider.gameObject);
            if (tresureAmount >= 1)
            {     
                 SceneManager.LoadScene("Clear");
            }
        }
    }

    //プレーヤの所在地
    void OnTriggerStay(Collider otherCollider)
    {
        //もしダンジョンにいたら
        if (otherCollider.GetComponent<Dungeon>() != null)
        {
            //現在地 = ダンジョンとする
            currentDungeon = otherCollider.GetComponent<Dungeon>();
            Debug.Log(currentDungeon);
            
        }
    }

    //ダンジョンから出た場合
    void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Dungeon>() != null)
        {
            Dungeon exitDungeon = otherCollider.GetComponent<Dungeon>();
            if (exitDungeon == currentDungeon)
            {
                currentDungeon = null;

            }

        }
    }

    //敵に衝突した時に発生させるイベント
    void OnCollisionEnter(Collision collision)
    {
        //Enemyオブジェクトに当たったら
        if (collision.gameObject.GetComponent<Enemy>()){
            //Hit関数呼び出す その際にヒットポイントを当たった対象物の地点へ近づける
            Hit((transform.position - collision.transform.position).normalized);
        }
    }

    //Hit関数
    private void Hit(Vector3 direction)
    {
        //衝突した時とぶ方向
        Vector3 knockbackDirection = (direction + Vector3.up).normalized;
        //とばす
        playerRigidbody.AddForce(knockbackDirection * knockbackForce);

        knockbackTimer = 1f;

        //HPを減らす
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Menu");
        }
    }

    public void Teleport(Vector3 target)
    {
        transform.position = target;
        justTeleported = true;
    }
}


/*
 * 
 * メモ
 * 60 frames per second
 * 0.166s
 * Move 0.1 to the right per frame
 * 6 units in the horizontal axis
 * 
 * 20 frames per second
 * 0.400s
 * Moves 0.1 to the right per frame
 * 2 units in the horizontal axis
 * 
 */
