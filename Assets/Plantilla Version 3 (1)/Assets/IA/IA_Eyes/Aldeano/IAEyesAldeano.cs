using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEyesAldeano : IAEyeBase
{

    private void Start()
    {
        LoadComponent();
    }

    private void Update()
    {
        UpdateScan();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }


    public override void UpdateScan()
    {
        base.UpdateScan();


    }

    private void OnValidate()
    {
        mainDataView.CreateMesh();
        
        RadioActionDataView.CreateMesh();
    }
    private void OnDrawGizmos()
    {
        mainDataView.OnDrawGizmos();
        
        RadioActionDataView.OnDrawGizmos();
    }
}