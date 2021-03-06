﻿using UnityEngine;
using System.Collections;
using System;
/**
 * 回调变换器
 */
public class ActionTransformer : Transformer
{
    private Action m_CallBack;

    public static ActionTransformer FadeTo(GameObject target, Action callBack, float time)
    {
        ActionTransformer transformer = new ActionTransformer();
        transformer.m_CallBack = callBack;
        transformer.m_fTransformTime = time;
        transformer.target = target;
        return transformer;
    }

    public ActionTransformer()
    {
        m_Type = eTransformerID.Action;
    }
    public override void runTransform(float currTime)
    {
        if (currTime >= m_fEndTime)
        {
            m_CallBack();
        }
    }
}