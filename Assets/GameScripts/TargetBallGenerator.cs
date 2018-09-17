using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBallGenerator : MonoBehaviour
{
    [SerializeField] Vector2 m_startGeneratePosition;
    public GameObject prefab;
    //GameController
    GameObject gc;



    void Start()
    {
    }

    void Update()
    {
    }

    public void ResetBall()
    {
		// posにprefabの生成初期位置を代入
		// prefabに設定されているgameObjectを生成
		// 初期位置に座標を合わせる

        Vector2 pos = m_startGeneratePosition;
        GameObject go = Instantiate(prefab, this.gameObject.transform);
        go.transform.position = pos;
    }
}