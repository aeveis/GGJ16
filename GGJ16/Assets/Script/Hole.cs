using UnityEngine;
using System.Collections;
using System;

public class Hole : MonoBehaviour
{

	private bool m_isDying;
	private Vector3 m_playerPosition;
	public AudioClip fallSound;

    void OnTriggerEnter(Collider other)
    {
		Debug.Log("hole death");
        if (other.tag == "Player")
        {
			if (!m_isDying)
				StartCoroutine(StartHoleDeath());
        }
    }

	void LateUpdate()
	{
		if (m_isDying)
			PlayerController.Instance.transform.position = m_playerPosition;
	}

	public IEnumerator StartHoleDeath()
	{
		m_isDying = true;
		m_playerPosition =  PlayerController.Instance.transform.position + (transform.position - PlayerController.Instance.transform.position).normalized * 0.5f;
		yield return null;
		SoundManager.instance.PlaySingle(fallSound);
		float time = 0.0f;
		while(time < 1.0f)
		{
			time += Time.deltaTime;
			PlayerController.Instance.transform.localScale = Vector3.one * (1.0f - time);

			yield return null;
		}

		//reset
		Application.LoadLevel(Application.loadedLevel);
		//yield return new WaitForSeconds(0.1f);
		m_isDying = false;
	}
}
