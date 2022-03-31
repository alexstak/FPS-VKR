using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class NavMeshUpdateOnEnable : MonoBehaviour
{

    public NavMeshData m_NavMeshData;
    private NavMeshDataInstance m_NavMeshInstance;

    public void OnEnable()
    {
        print(m_NavMeshData.name);
        m_NavMeshInstance = NavMesh.AddNavMeshData(m_NavMeshData);
    }

    public void OnDisable()
    {
        NavMesh.RemoveNavMeshData(m_NavMeshInstance);
    }
}