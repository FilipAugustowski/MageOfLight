using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Since the mesh is initially given with the flesh materials, the game will start and automatically set the character to ghost materials,
 * depending on the level, some ghost parts will be turned on while other parts of the skeleton are turned off, this is governed by the scene tracker */

public class GhostPart : MonoBehaviour
{
    Material[] fleshMaterials;
    public Material[] ghostMaterials; /* Keep ghost materials in the same order as flesh materials in the editor */
    Renderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
        fleshMaterials = meshRenderer.materials;
        meshRenderer.enabled = false;

    }


    public void SwapToGhost()
    {
        meshRenderer.enabled = true;
        if (ghostMaterials.Length > 0)
        {
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                meshRenderer.materials[i] = ghostMaterials[i];
            }
        }
        meshRenderer.sharedMaterials = ghostMaterials;
    }

    public void SwapToFlesh()
    {
        meshRenderer.enabled = true;

        if (fleshMaterials.Length > 0)
        {
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                meshRenderer.materials[i] = fleshMaterials[i];
            }
        }
        meshRenderer.sharedMaterials = fleshMaterials;
    }
}
