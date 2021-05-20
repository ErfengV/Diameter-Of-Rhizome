using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventCenter : MonoBehaviour
{
    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();



    public static void onListenerAdding(EventType eventType, Delegate callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
        }
    }//添加监听的重复部分
    //无参数
    public static void AddListener(EventType eventType,CallBack callBack)
    {
        onListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack) m_EventTable[eventType] + callBack;
    }
    //有一个参数
    public static void AddListener<T>(EventType eventType, CallBack<T> callBack)//1
    {
        onListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T,X>(EventType eventType, CallBack<T,X> callBack)//2
    {
        onListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T,X>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T, X,Y>(EventType eventType, CallBack<T, X,Y> callBack)//3
    {
        onListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X,Y>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T, X, Y,Z>(EventType eventType, CallBack<T, X, Y,Z> callBack)//4
    {
        onListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y,Z>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T, X, Y, Z,U>(EventType eventType, CallBack<T, X, Y, Z,U> callBack)//5个参数
    {
        onListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z,U>)m_EventTable[eventType] + callBack;
    }


    public static void onListenerRemoving(EventType eventType, Delegate callBack)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("移除监听错误，事件{0}没有对应的委托", eventType));

            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除监听错误，尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
            }

        }
        else
        {
            throw new Exception(string.Format("移除监听错误，没有事件码{0}", eventType));
        }
    }//移除监听的重复部分
    public static void onListrnerRemoved(EventType eventType)
    {
        if (m_EventTable[eventType] == null)
        {
            m_EventTable.Remove(eventType);
        }
    }
    public static void RemoveListener(EventType eventType,CallBack callBack)
    {
        onListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;
        onListrnerRemoved(eventType);
    }
    //有一个参数
    public static void RemoveListener<T>(EventType eventType, CallBack<T> callBack)//1
    {
        onListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] - callBack;
        onListrnerRemoved(eventType);
    }
    public static void RemoveListener<T,X>(EventType eventType, CallBack<T,X> callBack)//2
    {
        onListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T,X>)m_EventTable[eventType] - callBack;
        onListrnerRemoved(eventType);
    }
    public static void RemoveListener<T, X,Y>(EventType eventType, CallBack<T, X,Y> callBack)//3
    {
        onListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X,Y>)m_EventTable[eventType] - callBack;
        onListrnerRemoved(eventType);
    }
    public static void RemoveListener<T, X, Y,Z>(EventType eventType, CallBack<T, X, Y,Z> callBack)//4
    {
        onListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y,Z>)m_EventTable[eventType] - callBack;
        onListrnerRemoved(eventType);
    }
    public static void RemoveListener<T, X, Y, Z,U>(EventType eventType, CallBack<T, X, Y, Z,U> callBack)//5
    {
        onListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z,U>)m_EventTable[eventType] - callBack;
        onListrnerRemoved(eventType);
    }


    //无参数
    public static void Broadcast(EventType eventType)//广播
    {
        Delegate d;
        if(m_EventTable.TryGetValue(eventType,out d))
        {
            CallBack callBack = d as CallBack;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception(string.Format("广播事件错误，事件{0}对应委托具有不同的类型", eventType));
            }
        }
        
    }
    public static void Broadcast<T>(EventType eventType,T arg)//1
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = d as CallBack<T>;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误，事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T,X>(EventType eventType, T arg1,X arg2)//1
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T,X> callBack = d as CallBack<T,X>;
            if (callBack != null)
            {
                callBack(arg1,arg2);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误，事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T, X,Y>(EventType eventType, T arg1, X arg2,Y arg3)//2
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X,Y> callBack = d as CallBack<T, X,Y>;
            if (callBack != null)
            {
                callBack(arg1, arg2,arg3);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误，事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T, X, Y,Z>(EventType eventType, T arg1, X arg2, Y arg3,Z arg4)//4
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y,Z> callBack = d as CallBack<T, X, Y,Z>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3,arg4);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误，事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T, X, Y, Z,U>(EventType eventType, T arg1, X arg2, Y arg3, Z arg4, U arg5)//5
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y, Z,U> callBack = d as CallBack<T, X, Y, Z,U>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4,arg5);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误，事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
}
