using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace vk.Idle.Num
{
    public class NumGroupType
    {
        //注意这里面的NumType 一定是一个纯数
        //且从0开始 依次数值 递减
        List<NumType> others; //剩余部分

        public NumGroupType()
        {
            others = new List<NumType>();
        }

        public NumGroupType(int value, int unit)
        {
            NumType num = new NumType(value, unit);
            others = new List<NumType>();
            others.Add(num);
        }

        public NumGroupType(float value, int unit)
        {
            FNumType num = new FNumType(value, unit);
            others = new List<NumType>();
            others.Add(num.highNum);
            others.Add(num.lowNum);
        }

        public static NumGroupType operator +(NumGroupType num1, NumType num2)
        {
            NumType num;
            bool isAdded = false;
            for (int i = 0; i < num1.others.Count; i++)
            {
                num = num1.others[i];
                //等级相等
                if (num2.unit == num1.others[i].unit)
                {
                    num.value += num2.value;
                    num1.others[i] = num;

                    //判断进位
                    num1.CarryHandler(i);
                    isAdded = true;
                    break;
                }
                else if (num2.unit > num1.others[i].unit)
                {
                    num1.others.Insert(i, num2);

                    isAdded = true;
                    break;
                }
            }
            if (isAdded == false)
            {
                num1.others.Add(num2);
            }

            return num1;
        }
        public static NumGroupType operator +(NumGroupType num1, FNumType num2)
        {
            num1 = num1 + num2.highNum;
            num1 = num1 + num2.lowNum;
            return num1;
        }

            //升位处理
            private void CarryHandler(int index)
        {
            NumType num;
            float value;
 
            //从index 判断 
            for(int i = index; i >= 0; i--)
            {
                num = others[index];
               if (num.value > 1000)
                {
                  
                }
                else
                {
                    break;
                }
            }
            
        
        }
        //升位处理
        private void CarryHandler()
        {
            //从最后判断所有的
        }
    }

    /// <summary>
    /// 包含小数
    /// </summary>
    public struct FNumType
    {
        public NumType highNum;  //整数部分 3~4位，
        public NumType lowNum;   //小数部分 3位

        public FNumType(float value, int unit)
        {
            highNum = new NumType((int)value, unit);

            lowNum = new NumType((int)((value - (int)value) * 1000), unit -1);
        }
    }

    /// <summary>
    /// 单独
    /// </summary>
    public struct NumType
    {
        public int value;    //整数3~4位，//小数3位
        public int unit;           //单位类型

        public NumType(int value, int unit)
        {
            this.value = value;
            this.unit = unit;
        }
    }
}
