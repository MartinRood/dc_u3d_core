﻿using UnityEngine;
using System.Collections;
/**
 * 空变换器，占据变换时间而不做任何变换
 */
public class NullTransformer : Transformer
{
    public static NullTransformer holdTime(GameObject target, float time)
    {
        NullTransformer result = new NullTransformer();
        result.m_fTransformTime = time;
        result.target = target;
        return result;
    }

    public NullTransformer()
    {
        m_Type = eTransformerID.Null;
    }
    public override void runTransform(float currTime)
    {

    }
}