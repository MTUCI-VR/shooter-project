using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Knife : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("cols");
	}
	private void OnCollisionExit(Collision collision)
	{
		Debug.Log("cole");
	}
	
}
