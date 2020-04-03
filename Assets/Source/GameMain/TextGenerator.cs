using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENUM;

public class TextGenerator : MonoBehaviour
{
    public GameObject TextPrefab;
    public TextMesh textmesh;

    public eTextType texttype;

    // 初期化
    void Start()
    {

        switch (texttype)
        {
            case eTextType.AddScore:
                Destroy(gameObject, Define.TEXT_DELETECOUNT);
                break;
            default:
               break;
        }

    }


}
