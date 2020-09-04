using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NumType : ICloneable
{
    float value; //整数3~4位，小数3位
    int unit;//单位类型

    List<NumType> others; //剩余部分

    public NumType(float value, int unit)
    {
        this.value = value;
        this.unit = unit;
        others = null;

        
    }

    public NumType()
    {
        this.value = 0.0f;
        this.unit = 0;
        others = null;
    }






    /// <summary>
    /// 增加一个纯数
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    private void AddNumType(float value, int unit)
    {
        if (this.unit == unit)
        {
            this.value += value;
            return;
        }

        //直接加入集合
        if (others == null)
        {
            others = new List<NumType>();
            others.Add(new NumType(value, unit));
            return;
        }

        for (int i = 0; i < others.Count; i++)
        {
            if (others[i].unit == unit)
            {
                others[i].value += value;
                break;
            }
            else if (others[i].unit < unit)
            {
                others.Insert(i, new NumType(value, unit));
                break;
            }
        }
    }


    //判断进位



    public static NumType operator +(NumType num1, NumType num2)
    {
        NumType ret, maxNum, minNum;
        //数值处理
        if (num1.unit >= num2.unit)
        {
            //num1更大
            maxNum = num1;
            minNum = num2;
        }
        else
        {
            //num2更大
            maxNum = num2;
            minNum = num1;
        }

        //1.加入数值
        ret = new NumType(maxNum.value, maxNum.unit);
        
        //2.循环加入
        int maxIndex = maxNum.others != null ? maxNum.others.Count -1 : -1;
        int minIndex = minNum.others != null ? minNum.others.Count  -1: -1;

        NumType newNum;
        while (maxIndex >= 0 && minIndex >= 0)
        {
            if (maxNum.others[maxIndex].unit > minNum.others[minIndex].unit)
            {
                newNum = new NumType(maxNum.others[maxIndex].value , maxNum.others[maxIndex].unit);
                maxIndex--;
            }
            else if (maxNum.others[maxIndex].unit < minNum.others[minIndex].unit)
            {
                newNum = new NumType(minNum.others[minIndex].value, minNum.others[maxIndex].unit);
                minIndex--;
            }
            else
            {
                newNum = new NumType(maxNum.others[maxIndex].value + minNum.others[minIndex].value, maxNum.others[maxIndex].unit);
                maxIndex--;
                minIndex--;
            }
            ret.others.Add(newNum);
        }
        //还剩余
        if (maxIndex >= 0)
        {
            for (int i = maxIndex; i >=0; i--)
            {
                newNum = new NumType(maxNum.others[maxIndex].value + minNum.others[minIndex].value, maxNum.others[maxIndex].unit);
                ret.others.Add(newNum);
            }
        }
        //还剩余
        else if (minIndex >= 0)
        {
            for (int i = minIndex; i >= 0; i--)
            {
                newNum = new NumType(minNum.others[minIndex].value, minNum.others[maxIndex].unit);
                ret.others.Add(newNum);
            }
        }

        //最后 把 min. 当前值加入到里面
        ret.AddNumType(minNum.value, minNum.unit);

        float curValue;
        int curUnit;
        float syValue;
        int syUnit;
        //再判断是否需要升位从最小的往上升
        for (int i = ret.others.Count -1; i < 0 ; i--)
        {
            curValue = ret.others[i].value;
            curUnit = ret.others[i].unit;
            //后面看需要换成 读取数组
            while (curValue > 1000)
            {
                syValue = curValue % 1000;
                syUnit = curUnit;

                //进位
                curValue /= 1000;
                curUnit++;

                if (ret.others[i].value > 0.00001)//小于0.0001 直接忽略
                {
                    //删除本身
                }
               

            }


        }


        int dawei = NumTools.GetUnitRate(ret.unit);
        while (ret.value > dawei)
        {
            ret.value -= dawei;
            ret.unit++;
            dawei = NumTools.GetUnitRate(ret.unit);
        };


        return ret;
    }

    public static NumType operator *(NumType num1, float beilv)
    {
        NumType ret = num1;


        ret.others.Sort();
        return ret;
    }

    public static bool operator >(NumType num1, NumType num2)
    {
        return true;
    }
    public static bool operator <(NumType num1, NumType num2)
    {
        return true;
    }

    public static bool operator ==(NumType num1, NumType num2)
    {
        return true;
    }
    public static bool operator !=(NumType num1, NumType num2)
    {
        return true;
    }

    /// <summary>
    /// 复制一个数值对象
    /// </summary>
    /// <returns></returns>
    public object Clone()
    {
        NumType ret = new NumType(value, unit);

        if (others != null && others.Count > 0)
        {
            ret.others = new List<NumType>();
            foreach (var item in others)
            {
                others.Add(item.Clone() as NumType);
            }
        }
        return ret;
    }


    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return base.Equals(obj);
    }
    // override object.GetHashCode
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

//配置？
public enum UnitType
{
    None = 0, // 初始单位
    K =1,            //千
    W = 2,          //万
    Y = 3,          //亿
    //兆

}