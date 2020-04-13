using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZWater : MonoBehaviour
{
    [SerializeField] CustomRenderTexture customRt;
    [SerializeField] Vector2 _resolution = new Vector2(512, 512);

    Vector2 _curInputPosition;
    float _curInputSize;
    float _minInputSize;
    bool _didHit;
    bool _inputPush;
    float _hitTime;
    float _clearHitTime;

    Vector2 _curDripPosition;
    float _curDripSize;
    bool _didDrip;


    MeshCollider _meshCollider;
    static readonly int InputSizeID = Shader.PropertyToID("_inputSize");
    static readonly int MinInputSizeID = Shader.PropertyToID("_minInputSize");
    static readonly int GotInputID = Shader.PropertyToID("_gotInput");
    static readonly int InputPushID = Shader.PropertyToID("_inputPush");
    static readonly int InputXID = Shader.PropertyToID("_inputX");
    static readonly int InputYID = Shader.PropertyToID("_inputY");

    static readonly int DripSizeID = Shader.PropertyToID("_dripSize");
    static readonly int GotDripID = Shader.PropertyToID("_gotDrip");
    static readonly int DripInputXID = Shader.PropertyToID("_dripInputX");
    static readonly int DripInputYID = Shader.PropertyToID("_dripInputY");
    public MeshCollider meshCollider => _meshCollider != null ? _meshCollider : _meshCollider = GetComponent<MeshCollider>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateMaterialValues(); // 不断刷新shader的参数
    }


    public void SetInputPosition(Vector2 texCoordPosition, float inputSize, float minInputSize, bool inputPush, float clearHitTime = -1)
    {
        _curInputPosition = texCoordPosition;
        _curInputSize = inputSize;
        _minInputSize = minInputSize;
        _didHit = true;
        _inputPush = inputPush;
        _hitTime = Time.time;
        _clearHitTime = clearHitTime;
    }

    public void ClearInput()
    {
        _didHit = false;
    }

    void UpdateMaterialValues()
    {
        customRt.material.SetFloat(InputXID, _curInputPosition.x);
        customRt.material.SetFloat(InputYID, _curInputPosition.y);
        customRt.material.SetFloat(InputSizeID, _curInputSize);
        customRt.material.SetFloat(MinInputSizeID, _minInputSize);
        customRt.material.SetFloat(GotInputID, _didHit ? 1 : 0);
        customRt.material.SetFloat(InputPushID, _inputPush ? 1 : 0);
        customRt.material.SetFloat(DripInputXID, _curDripPosition.x);
        customRt.material.SetFloat(DripInputYID, _curDripPosition.y);
        customRt.material.SetFloat(DripSizeID, _curDripSize);
        customRt.material.SetFloat(GotDripID, _didDrip ? 1 : 0);
    }
}
