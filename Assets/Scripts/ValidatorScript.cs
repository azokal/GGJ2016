using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Mypointertest : PointerInputModule{
	
	public override void Process()
	{
	}
	
	public List<RaycastResult> RaycastMouse(){
		
		PointerEventData pointerData = null;
		
		if (!m_PointerData.TryGetValue (-1, out pointerData) ){
			pointerData = new PointerEventData (eventSystem)
			{
				pointerId = -1,
			};
			m_PointerData.Add (-1, pointerData);
			
		}
		
		pointerData.position = GameObject.Find("Validator").transform.position;
		
		eventSystem.RaycastAll(pointerData, m_RaycastResultCache);
		List<RaycastResult> results = m_RaycastResultCache;
		
		Debug.Log( results.Count);
		
		return results;
	}
}

public class ValidatorScript : MonoBehaviour {

	protected Dictionary<int, PointerEventData> m_PointerData = new Dictionary<int, PointerEventData> ();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PointerEventData pointerData = null;
		
		if (!m_PointerData.TryGetValue (-1, out pointerData) ){
			pointerData = new PointerEventData (EventSystem.current)
			{
				pointerId = -1,
			};
			m_PointerData.Add (-1, pointerData);
			
		}
		
		pointerData.position = GameObject.Find("Validator").transform.position;

		var ray = EventSystem.current.RaycastAll (pointerData, m_PointerData);
		var t = Physics.RaycastAll(ray);
		foreach (var e in t) {
			print(e.collider.name);
		}
	}
}
