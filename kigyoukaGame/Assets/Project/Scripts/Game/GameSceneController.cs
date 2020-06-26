using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    //[("xx")] = unityエディタの見出し
    //public .. = unityエディタからいじれるメンバ変数

    [Header("Game")]
    public Player player; //Playerクラス（自作）型の変数player
    public GameCamera gameCamera;

    [Header("UI")]
    public Text hpText;
    public Text bombText;
    public Text arrowText;
    public Text tresureText;
    public GameObject dungeonPanel;
    public Text dungeonInfoText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレーヤ情報
        if (player != null)
        {
            hpText.text = "HP: " + player.hp;
            //bombText.text = "Bomb: " + player.bombAmount;
            //arrowText.text = "Arrow: " + player.arrowAmount;
            tresureText.text = "tresure: " + player.tresureAmount;

            //ダンジョン情報
            //現在地（プレーヤクラスのCurrentDungeonプロパティを呼び出し代入）
            Dungeon currentDungeon = player.CurrentDungeon;
            //dungeonPanelに現在地情報をセット
            dungeonPanel.SetActive(currentDungeon != null);
            if (currentDungeon != null)
            {
                //Dungeonコンポーネントのプロパティ(総敵数、残存敵数)を呼び出し変数に代入
                dungeonInfoText.text = "Enemies: " + currentDungeon.CurrentEnemyCount + "/" + currentDungeon.EnemyCount;


            }
        }
        else
        {
            //プレーヤ破壊されたらHP:0と表示
            hpText.text = "HP: 0";
        }
    }

    
}
