using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseController : MonoBehaviour
{

    [SerializeField] bool _useFixedUpdate;
    [SerializeField] protected float _inputSize = 20;
    [SerializeField] protected float _minInputSize = 5;
    [SerializeField] protected bool _inputPush = false;

    [SerializeField] ZWater _zWater;

    protected virtual void Update()
    {
        if (!_useFixedUpdate)
            DoUpdate();
    }
    void FixedUpdate()
    {
        if (_useFixedUpdate)
            DoUpdate();
    }


    protected void DoUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (_zWater.meshCollider.Raycast(ray, out hit, Mathf.Infinity))
            {
                _zWater.SetInputPosition(hit.textureCoord, _inputSize, _minInputSize, _inputPush);
            }
            else
            {
                _zWater.ClearInput();
            }
        }
        else
        {
            _zWater.ClearInput();
        }
    }
}
