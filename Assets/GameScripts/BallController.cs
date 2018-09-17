using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [SerializeField] Vector2 m_power;
    [SerializeField] float m_powerScale;
    Rigidbody2D m_rb2d;

    // previous position 
	Vector2 prevPos;

	// ドラッグでボールを弾く
    public float power_D;
    Rigidbody2D rdPlayerBall;
    Vector2 ballVec;
    Vector2 clickPosDown, clickPosUp;

    //CountText
	GameObject cd;
    //GameController
    GameObject gc;


	private void Start()
	{
		rdPlayerBall = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        //GameControllerを取得
        gc = GameObject.Find("GameController");
        GameController d1 = gc.GetComponent<GameController>();

        //CountDown.csを取得
        cd = GameObject.Find("CountText");
        CountDown cd_flag = cd.GetComponent<CountDown>();

        // 前のframeの座標と現在の座標の差分からVectorを割り出す
        float x = this.transform.position.x - prevPos.x; // x座標の差分
        float y = this.transform.position.y - prevPos.y; // y座標の差分
        Vector2 vec = new Vector2(x, y).normalized; // Magnitude1のvectorを作成

        // 座標の差分からAtan2を使い角度を求める
        float rot = Mathf.Atan2(vec.y, vec.x) * 180 / Mathf.PI;
        if (rot > 180) rot -= 360;
        if (rot < -180) rot += 360;

        Debug.Log("Angle = " + rot);

        //このflameの座標を代入する
        prevPos = this.transform.position;

        // SpaceKeyを押すと進んでる方向に推進力が増す
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_rb2d.AddForce(vec * 3000);
            // rigidbody2D.velocity = direction * speed;
        }

        if (d1.m_isInGame)
        {
            //残りボールカウントが1以上且つballが止まっている時
            if (cd_flag.count > 0 && m_rb2d.IsSleeping())
            {
                //マウスドラッグでballを動かす
                if (Input.GetMouseButtonDown(0))
                {
                    clickPosDown = Input.mousePosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    clickPosUp = Input.mousePosition;
                    //Debug.log ("ClickUp" + clickPosUp); 

                    // ボールを飛ばす方向を計算
                    // マウスポジションは(x,y)が画面上の位置
                    ballVec = (clickPosDown - clickPosUp);
                    ballVec.Normalize();

                    rdPlayerBall.AddForce(ballVec * power_D);
                    // 何故かCountDown.csで"if(count == 0 && bc_rb2d.IsSleeping())"が機能してくれない為ここでcountデクリメント
                    cd_flag.count--;
                }
            }


            // ボールが水平に飛ぶようになってしまったら調整する（強引）
            if (Vector2.Angle(Vector2.right, m_rb2d.velocity) < 20f)
            {
                m_rb2d.AddForce(Vector2.up * (m_rb2d.velocity.y >= 0 ? 1 : -1) * 15);
            }
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		// "Enemy"tagのobjectに触れた時音を鳴らす
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<AudioSource>().Play();
        }
    }

    // enterでゲームをスタートさせた時呼び出しされる
    public void Reset()
    {
        m_rb2d = GetComponent<Rigidbody2D>();   // RigitBodyを取得する
        m_rb2d.velocity = Vector2.zero;     // 一旦速度をリセットする.gameOverから再開した時の為

    }
}