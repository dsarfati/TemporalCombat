using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }

    public static void SetMaterial(this GameObject go, Material mat)
    {
        var sprites = go.GetComponentsInChildren<SpriteRenderer>();
        var meshes = go.GetComponentsInChildren<Anima2D.SpriteMeshInstance>();

        foreach (var sprite in sprites)
        {
            sprite.material = mat;
        }

        foreach (var mesh in meshes)
        {
            mesh.sharedMaterial = mat;
            mesh.color = mat.color;
        }
    }


    /*
    //Depth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        foreach(Transform child in aParent)
        {
            if(child.name == aName )
                return child;
            var result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }
    */
}
