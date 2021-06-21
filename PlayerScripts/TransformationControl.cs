using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationControl : MonoBehaviour
{
    public SkinnedMeshRenderer[] level1Meshes;
    public SkinnedMeshRenderer[] level2Meshes;
    public SkinnedMeshRenderer[] level3Meshes;
    public SkinnedMeshRenderer[] level4Meshes;

    public GhostPart[] level1GhostParts;
    public GhostPart[] level2GhostParts;
    public GhostPart[] level3GhostParts;
    public GhostPart[] level4GhostParts;


    public bool hideLevel1Mesh;
    public bool hideLevel2Mesh;
    public bool hideLevel3Mesh;
    public bool hideLevel4Mesh;

    //// Update is called once per frame
    //void Update()
    //{
    //    if(hideLevel1Mesh)
    //    {
    //        ActivateLevelTransformation(1);
    //    }
    //    if(hideLevel2Mesh)
    //    {
    //        ActivateLevelTransformation(2);
    //    }
    //    if (hideLevel3Mesh)
    //    {
    //        ActivateLevelTransformation(3);
    //    }
    //    if (hideLevel4Mesh)
    //    {
    //        ActivateLevelTransformation(4);
    //    }
    //}

    public void ActivateLevelTransformation(int _level)
    {
        if(_level == 1)
        {
            foreach(GhostPart part in level1GhostParts)
            {
                part.SwapToGhost();
            }    
            
            foreach(SkinnedMeshRenderer mesh in level1Meshes)
            {
                mesh.enabled = false;
            }
        }
        if (_level == 2)
        {
            foreach (GhostPart part in level2GhostParts)
            {
                part.SwapToGhost();
            }

            foreach (SkinnedMeshRenderer mesh in level2Meshes)
            {
                mesh.enabled = false;
            }

        }
        if (_level == 3)
        {
            foreach (GhostPart part in level3GhostParts)
            {
                part.SwapToGhost();
            }

            foreach (SkinnedMeshRenderer mesh in level3Meshes)
            {
                mesh.enabled = false;
            }

        }
        if (_level == 4)
        {
            foreach (GhostPart part in level4GhostParts)
            {
                part.SwapToFlesh();
            }

            foreach (SkinnedMeshRenderer mesh in level4Meshes)
            {
                mesh.enabled = false;
            }

        }
    }
}
