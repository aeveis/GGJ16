using UnityEngine;
using System.Collections;
using System;

public class Chant : Ritual
{
	enum ChantNote {LONG, SHORT, PAUSE};
	public enum ChantState {WAITTIME, START, PLAYING};
	private ChantNote currentNote;
	public ChantState cstate = ChantState.WAITTIME;
	private int patternIndex=0;
	private bool wait = false;
	private float waitTime = 0;

	public string chantPattern ="-. -.";
	public float startBufferTime = .2f;
	public float chantFrequencyLength = .5f;
	public float leeway= .2f;
	public AudioClip chantSound;

	public override bool Check(Transform p_Actor)
	{
		return !PlayerController.Instance.IsJumping();
	}

	public override void Action(Transform p_Actor)
	{
		Debug.Log (cstate );
		switch (cstate) {
		case ChantState.WAITTIME:
			Debug.Log (cstate);
			Debug.Log (Time.time );
			if (Mathf.RoundToInt(Time.time)%5 == 0) {
				cstate = ChantState.START;
				Debug.Log ("start chant");
			}
			break;
		case ChantState.START:
			playSound (chantPattern [patternIndex]);
			wait = true;
			waitTime = 0;
			cstate = ChantState.PLAYING;
			break;
		case ChantState.PLAYING:
			waitTime += Time.deltaTime;
			switch (currentNote) {
			case ChantNote.LONG:
				if (waitTime >= chantFrequencyLength * 2) {
					wait = false;
				}
				break;
			default:
				if (waitTime >= chantFrequencyLength) {
					wait = false;
				}
				break;
			}

			if (!wait) {
				SoundManager.instance.FadeoutThenStop ();
				patternIndex = (patternIndex+1)%chantPattern.Length;
				cstate = ChantState.START;
			}

			break;
		default:
			break;
		}
	}

	public override void OnVisionConeEnter()
	{

	}

	public override void OnVisionConeExit()
	{
	}

	private void playSound(char c) {
		switch (c) {
		case '-':
			currentNote = ChantNote.LONG;
			SoundManager.instance.PlaySingle (chantSound);
			break;
		case '.':
			currentNote = ChantNote.SHORT;
			SoundManager.instance.PlaySingle (chantSound);
			break;
		default:
			currentNote = ChantNote.PAUSE;
			break;
		}
	}

}
