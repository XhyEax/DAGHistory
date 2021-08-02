﻿/*
 * 方法参数是私有类型的Hook测试用例
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PrivateTestA
{
    private class InnerClass
    {
        public int x;
    }

    private enum InnerEnum : short
    {
        E0 = 0,
        E1 = 1,
        E2 = 2,
    }

    private int _val = 0;

    public void FuncTest()
    {
        InnerClass innerClass = new InnerClass() { x = 2 };
        InnerFuncTest(innerClass, InnerEnum.E1);
    }
    private void InnerFuncTest(InnerClass innerClass, InnerEnum innerEnum)
    {
        Debug.LogFormat("InnerTypeTest:innerClass.x:{0}, innerEnum:{1}, val:{2}", innerClass.x, innerEnum.ToString(), _val);
    }
}

public class PrivateTestB
{
    /// <summary>
    /// 替换函数，参数类型只要"兼容"就可以，兼容的定义是引用类型可以使用任意引用类型替代，值类型保证 size 一致就可以
    /// </summary>
    /// <param name="a"></param>
    /// <param name="innerClass"></param>
    /// <param name="innerEnum"></param>
    public void FuncReplace(object innerClass, short innerEnum)
    {
        Debug.Log("PrivateTestB.FuncReplace called");
        innerEnum += 1;
        Proxy(innerClass, innerEnum);
    }

    public  void Proxy(object innerClass, short innerEnum)
    {
        Debug.Log("something");
    }
}

public class PrivateTypeArgMethodTest
{
    public void Test()
    {
        Type typeA = typeof(PrivateTestA);
        Type typeB = typeof(PrivateTestB);

        MethodInfo miAPrivateFunc = typeA.GetMethod("InnerFuncTest", BindingFlags.Instance | BindingFlags.NonPublic);
        MethodInfo miBReplace = typeB.GetMethod("FuncReplace");
        MethodInfo miBProxy = typeB.GetMethod("Proxy");

        MethodHook hooker = new MethodHook(miAPrivateFunc, miBReplace, miBProxy);
        hooker.Install();

        PrivateTestA privateTestA = new PrivateTestA();
        privateTestA.FuncTest();
    }
}
