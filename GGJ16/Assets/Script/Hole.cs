﻿using UnityEngine;
using System.Collections;
using System;

public class Hole : MonoBehaviour
{

	private bool m_isDying;
	private Vector3 m_playerPosition;
	public AudioClip fallSound;

	void Start()
	{
		
		//GetComponent<BoxCollider>().size = new Vector3( transform.localScale.x*0.05f + 0.75f, 1f, transform.localScale.z*0.05f + 0.75f);
	}

    void OnTriggerEnter(Collider other)
    {
		//Debug.Log("hole death");
        if (other.tag == "Player")
        {
			if (!m_isDying)
				StartCoroutine(StartHoleDeath());
        } 
		if (other.attachedRigidbody.gameObject.name.Contains("Civie"))
		{
			StartCoroutine(StartCivFall(other.attachedRigidbody.gameObject.GetComponent<Civilian>()));
			//other.attachedRigidbody.gameObject.SetActive(false);
		} 
    }


	public IEnumerator StartHoleDeath()
	{
		m_isDying = true;
		PlayerController.Instance.m_MoveIsBlocked = true;
		m_playerPosition =  PlayerController.Instance.transform.position + (transform.position - PlayerController.Instance.transform.position).normalized * 0.5f;
		yield return null;
		SoundManager.instance.PlaySingle(fallSound);
		float time = 0.0f;
		while(time < 1.0f)
		{
			time += Time.deltaTime;
			PlayerController.Instance.transform.localScale = Vector3.one * (1.0f - time);
			//PlayerController.Instance.transform.position = m_playerPosition;
			yield return null;
		}

		//reset
		Application.LoadLevel(Application.loadedLevel);
		//yield return new WaitForSeconds(0.1f);
		m_isDying = false;
	}

	public IEnumerator StartCivFall(Civilian civ)
	{
		//PlayerController.Instance.m_MoveIsBlocked = true;
		//m_playerPosition =  PlayerController.Instance.transform.position + (transform.position - PlayerController.Instance.transform.position).normalized * 0.5f;
		//yield return null;
		civ.Stop();
		SoundManager.instance.PlaySingle(fallSound);
		float time = 0.0f;
		while(time < 1.0f)
		{
			time += Time.deltaTime;
			civ.transform.localScale = Vector3.one * (1.0f - time);
			//PlayerController.Instance.transform.position = m_playerPosition;
			yield return null;
		}
		civ.gameObject.SetActive (false);

	}
}
