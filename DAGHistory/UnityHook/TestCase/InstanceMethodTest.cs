﻿/*
 * 实例方法 Hook 测试用例
 * note: 静态方法 Hook 参考 PinnedLog.cs
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class A
{
    public int val;

    public int Func(int x)
    {
        Debug.Log("call of A.Func");
        return x + val;
    }
}

public class B
{
    public int FuncReplace(int x)
    {
        object obj = this;
        A a = obj as A;
        Debug.Log("call of B.Func");
        x += 1;
        a.val = 7;

        // 可以调用原方法或者不调用
        if (x < 100)
            return FuncProxy(x);
        else
            return x + 1;
    }

    public int FuncProxy(int x)
    {
        Debug.Log("随便乱写");
        return x;
    }
}

/// <summary>
/// 测试实例方法 Hook
/// </summary>
public class InstanceMethodTest
{

    public string Test()
    {
        UnityEngine.Debug.Log("XLog: [UnityHack] hook test.");
        A aa = new A() { val = 5 };
        UnityEngine.Debug.Log("XLog: [UnityHack] before hook ret." + aa.Func(2).ToString());

        Type typeA = typeof(A);
        Type typeB = typeof(B);
        try
        {
        MethodInfo miAFunc = typeA.GetMethod("Func");
        MethodInfo miBReplace = typeB.GetMethod("FuncReplace");
        MethodInfo miBProxy = typeB.GetMethod("FuncProxy");
        
       MethodHook hooker = new MethodHook(miAFunc, miBReplace, miBProxy);
        hooker.Install();

        // 调用原始A的方法测试
        A a = new A() { val = 5 };
        int ret = a.Func(2);
        UnityEngine.Debug.Log("XLog: [UnityHack] after hook ret."+ret.ToString());

        }

        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        
        }

        return null;
    }
    

}
