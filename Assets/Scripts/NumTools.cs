using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumTools 
{
    //所有的num数据

    //1.单位  递进
    static int[] UnitRates =
    {   //千 万 亿 兆  其他
        1000,1000,10000,10000,1000
    };
    //2.每个单位的名称
    static string[] UnitNames =
    {   "","千","万" ,"亿","兆","aa","ab","ac","ad","ae","af","ag","ah","ai","aj","ak","al","am","an","ao","ap","aq","ar","as","at","au","av","aw","ax","ay","az",
"ba","bb","bc","bd","be","bf","bg","bh","bi","bj","bk","bl","bm","bn","bo","bp","bq","br","bs","bt","bu","bv","bw","bx","by","bz",
"ca","cb","cc","cd","ce","cf","cg","ch","ci","cj","ck","cl","cm","cn","co","cp","cq","cr","cs","ct","cu","cv","cw","cx","cy","cz",
"da","db","dc","dd","de","df","dg","dh","di","dj","dk","dl","dm","dn","do","dp","dq","dr","ds","dt","du","dv","dw","dx","dy","dz",
};

    public static void init()
    {
        //读取文件
    }

    /// <summary>
    /// 获取某个单位的名称
    /// </summary>
    /// <param name="unitType">单位下标</param>
    /// <returns></returns>
    public static string GetUnitName(int unitType)
    {
        string name = "";
        if (UnitNames != null && unitType>=0)
        {
            if (unitType > UnitRates.Length-1)
            {
                name = UnitNames[UnitNames.Length - 1];
            }
            else
            {
                name = UnitNames[unitType];
            }
        }
        return name;
    }
    /// <summary>
    /// 获取某个单位的差值倍率
    /// </summary>
    /// <param name="unitType">单位下标</param>
    /// <returns></returns>
    public static int GetUnitRate(int unitType)
    {
        //默认 前后单位相差1000倍
        int rate = 1000;
        if (UnitRates != null && unitType >= 0)
        {
            if (unitType > UnitRates.Length - 1)
            {
                rate = UnitRates[UnitRates.Length - 1];
            }
            else
            {
                rate = UnitRates[unitType];
            }
        }
        return rate;
    }
}
