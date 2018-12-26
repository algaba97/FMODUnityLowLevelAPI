using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SCR_Visual_BezThrowCurve : MonoBehaviour
{

    public Transform CurveOriginTran;
    public Transform CurveHeightTran;
    public Transform CurveEndTran;

    private Vector3 CurveOrigin;
    private Vector3 CurveHeight;
    private Vector3 CurveEnd;

    public LineRenderer Renderer;
    public int vertexCount = 12;

    void Update()
    {
        CurveOrigin = CurveOriginTran.position;
        CurveHeight = CurveHeightTran.position;
        CurveEnd = CurveEndTran.position;

        List<Vector3> pointlist = new List<Vector3>();
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            Vector3 TangentVertex1 = Vector3.Lerp(CurveOrigin, CurveHeight, ratio);
            Vector3 TangentVertex2 = Vector3.Lerp(CurveHeight, CurveEnd, ratio);
            Vector3 bezierPoint = Vector3.Lerp(TangentVertex1, TangentVertex2, ratio);

            pointlist.Add(bezierPoint);
        }
        Renderer.positionCount = pointlist.Count;
        Renderer.SetPositions(pointlist.ToArray());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(CurveOrigin, CurveHeight);
        Gizmos.DrawLine(CurveHeight, CurveEnd);
    }
}
