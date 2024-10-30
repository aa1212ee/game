using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class larua_animator : MonoBehaviour
{
    public LARUA L;

    void damage2()
    {
        L.damage_all2(false);
    }

    public void shot01()
    {
        L.shota_01();
    }
    public void shot02()
    {
        L.shota_02();
    }
    public void sow01()
    {
        L.sow_01();
    }
    public void sow02()
    {
        L.sow_02();
    }
    public void spn() //スピナー
    {
        L.spnn();
    }
    public void axe() //アックス
    {
        L.axee();
    }
    public void sss(int i)
    {
        if (manager.larua_attack == 4 && L.ss == 0)
        {
            L.ss = i;
        }
        else
        {
            
        }
    }
    

    public void r2x(float x) => L.R2.velocity = new Vector2(x, L.R2.velocity.y);
    public void r2y(float x) => L.R2.velocity = new Vector2(L.R2.velocity.x, x);

    public void end()
    {
        L.attack_end();
    }

    #region
    public void Z_smork()
    {
        L.smork();
    }
    #endregion

}
