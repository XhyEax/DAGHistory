using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DAGHistory
{
    class MyHook
    {
        public static MethodHook NewHook(Type tTarget, Type tProxy, string methodName)
        {
            MethodInfo miTarget = tTarget.GetMethod(methodName);
            if (miTarget == null)
            {
                //MyLog.Log("public method ["+ methodName + "] not found, try to GetMethod with BindingFlags");
                miTarget = tTarget.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            }

            if (miTarget == null)
            {
                MyLog.Log("Error: " + methodName + " not found");
                return null;
            }

            MethodInfo miReplace = tProxy.GetMethod(methodName + "Replace");
            MethodInfo miProxy = tProxy.GetMethod(methodName + "Proxy");

            return new MethodHook(miTarget, miReplace, miProxy);
        }

        public static MethodHook MethodInstall(MethodInfo miTarget, MethodInfo miReplace, MethodInfo miProxy)
        {
            if (miTarget == null || miReplace == null || miProxy == null)
            {
                MyLog.Log("Error: MethodInfo is null");
                return null;
            }

            try
            {
                MethodHook methodHook = new MethodHook(miTarget, miReplace, miProxy);
                methodHook.Install();
                return methodHook;
            }
            catch (Exception e)
            {
                MyLog.LogError("MethodInstall", e);
            }
            return null;
        }

        public static void InstallWithTypeLength(Type tTarget, Type tProxy, string methodName, int typeLength)
        {
            if (tTarget == null || tProxy == null)
            {
                MyLog.Log("Error: Type is null");
            }

            foreach (MethodInfo mi in tTarget.GetMethods())
            {
                if (mi.Name.Equals(methodName) && mi.GetParameters().Length == typeLength)
                {
                    MethodInfo miReplace = tProxy.GetMethod(methodName + "Replace");
                    MethodInfo miProxy = tProxy.GetMethod(methodName + "Proxy");
                    MethodInstall(mi, miReplace, miProxy);
                    break;
                }
            }

            foreach (MethodInfo mi in tTarget.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (mi.Name.Equals(methodName) && mi.GetParameters().Length == typeLength)
                {
                    MethodInfo miReplace = tProxy.GetMethod(methodName + "Replace");
                    MethodInfo miProxy = tProxy.GetMethod(methodName + "Proxy");
                    MethodInstall(mi, miReplace, miProxy);
                    break;
                }
            }

        }

        public static MethodHook Install(Type tTarget, Type tProxy, string methodName)
        {
            if (tTarget == null || tProxy == null)
            {
                MyLog.Log("Error: Type is null");
            }
            MethodInfo miTarget = tTarget.GetMethod(methodName);
            if (miTarget == null)
            {
                miTarget = tTarget.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            }

            if (miTarget == null)
            {
                MyLog.Log("Error: " + methodName + " not found");
                return null;
            }

            MethodInfo miReplace = tProxy.GetMethod(methodName + "Replace");
            MethodInfo miProxy = tProxy.GetMethod(methodName + "Proxy");
            return MethodInstall(miTarget, miReplace, miProxy);
        }

    }
}
