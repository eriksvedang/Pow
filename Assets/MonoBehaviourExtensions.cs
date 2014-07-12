using UnityEngine;
using System.Collections;

public static class MonoBehaviourExtensions {

	public static Maybe<T> MaybeGetComponent<T>(this MonoBehaviour monoBehaviour) where T : Component {
		var component = monoBehaviour.GetComponent<T> ();
		if (component == null) {
				return Maybe.Nothing<T> ();
		} else {
				return Maybe.Just (component);
		}
	}

}
