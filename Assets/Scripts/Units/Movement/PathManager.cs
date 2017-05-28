using UnityEngine;
using System.Collections;
using System;

public class PathManager : MonoBehaviour {

    static PathManager m;

    public Transform[] paths;

	// Use this for initialization
	void Awake() {
        m = this;
	}
	
    /// <summary>
    /// Copies random path's x over paramerters.
    /// 
    /// Note: doesn't check if paths are not null.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>Parameter with x set on path</returns>
    internal static Vector3 ChosePath(Vector3 position) {
        if (m && m.paths.Length > 0) {
            position.x = m.paths[UnityEngine.Random.Range(0, m.paths.Length)]
                .transform.position.x;
        }
        return position;
    }
}
