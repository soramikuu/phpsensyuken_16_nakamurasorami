using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    //以下メンバ変数
    //トータル敵数
    private int enemyCount;
    //残存敵数
    private int currentEnemyCount;
    //敵の情報を入れる配列
    private Enemy[] enemies;
    //宝物
    private Tresure tresure;
    //クリア真偽
    private bool isClear;
    //クリア処理
    private bool justCleared;

    //以下メンバ変数に外部からアクセスできる関数生成（getプロパティを使う）
    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }
    }
    public int CurrentEnemyCount
    {
        get
        {
            return currentEnemyCount;
        }
    }

    public bool JustCleared
    {
        get
        {
            //初期値はfalseにして返す
            bool returnValue = justCleared;
            justCleared = false;
            return returnValue;
        }
    }

    public Tresure Tresure
    {
        get
        {
            return tresure;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponentsInChildren<Enemy>();
        tresure = GetComponentInChildren<Tresure>();

        tresure.gameObject.SetActive(false);

        enemyCount = enemies.Length;
        //Debug.Log(enemyCount);
    }

// Update is called once per frame
void Update()
    {
        currentEnemyCount = 0;
        foreach(Enemy enemy in enemies)
        {
            if(enemy != null) {
                currentEnemyCount++;
            }
        }
        Debug.Log(currentEnemyCount + "/" + enemyCount);

        if (isClear == false)
        {
            if (currentEnemyCount == 0)
            {
                isClear = true;
                tresure.gameObject.SetActive(true);

                justCleared = true;
            }
        } 
    }
}
