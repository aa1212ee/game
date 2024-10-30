using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class larua_move_botton : MonoBehaviour
{
    [HideInInspector] public LARUA LARUA;


    public Sprite S0,S1;
    public Image i;
    static int hor;
    static int ver;
    static int sp;
    static int ite;


    public void Awake()
    {
        GameObject l;
        l = GameObject.Find("LARUA");
        LARUA = l.GetComponent<LARUA>();
        hor = 0;
        ver = 0;
        sp = 0;
    }

    public void Update()
    {
        if(stage_manager.pause == true || !manager.sumaho )
        {
            i.enabled = false;
        }
        else
        {
            i.enabled = true;
        }


    }

    public void aa()
    {
        manager.start = 1;
        int a = 0 ;
        //プレイヤーに数値を与える。
        while(a == 0)
        {
            LARUA.hor = hor;
            LARUA.ver = ver;
            LARUA.sp = sp;
            LARUA.ite = ite;
            a++;
        }
        Invoke("shori",0.05f);
    }
    public void Botton()
    {
        if (hor == 0)
        {
            hor = 1;
        }
        i.sprite = S1;
        aa();
        return;
    }
    public void Botton2()
    {
        if (hor == 0)
        {
            hor = 2;
        }
        i.sprite = S1;
        aa();
        return;
    }
    public void jump()
    {
        if (ver == 0)
        {
            ver = 1;
        }
        i.sprite = S1;
        aa();
        return;
    }
    public void special()
    {
        if (sp == 0)
        {
            sp = 1;
        }
        i.sprite = S1;
        aa();
        return;
    }
    public void Cancel1() //移動
    {
        hor = 0;
        aa();
        i.sprite = S0;
        return;
    }
    public void Cancel2() //ジャンプ
    {
        ver = 0;
        aa();
        i.sprite = S0;
        return;
    }
    public void Cancel3() //光攻撃
    {
        sp = 0;
        aa();
        i.sprite = S0;
        return;
    }
    public void item() //アイテム攻撃
    {
        ite = 0;
        aa();
        i.sprite = S0;
        return;
    }

    public void pause() //ポーズ
    {
        if (manager.sumaho)
        {
            i.sprite = S0;
            manager.reply = 0;
            stage_manager.P_teishi();
            return;
        }
    }

    void shori()
    {
        //押しっぱなしに変換
        if (hor == 1)
        {
            hor = 3;
            LARUA.hor = hor;
        }
        if (hor == 2)
        {
            hor = 4;
            LARUA.hor = hor;
        }
        if (ver == 1)
        {
            ver = 2;
            LARUA.ver = ver;
        }
        if (sp == 1)
        {
            sp = 2;
            LARUA.sp = sp;
        }
        if (ite == 1)
        {
            ite = 2;
            LARUA.sp = sp;
        }

    }
}
