using UnityEngine;
using System.Collections;
using System;

public class Chant : Ritual
{
	enum ChantNote {LONG, SHORT, PAUSE};
	enum ChantState {TIMEDSTART, START, PLAYING};
	private ChantNote currentNote;
	private ChantState cstate = ChantState.START;
	private int patternIndex=0;
	private bool wait = false;

	public string chantPattern ="-. -.";
	public float startBufferTime = .2f;
	public float chantFrequencyLength = .5f;
	public float leeway= .2f;

	public override bool Check(Transform p_Actor)
	{
		return !PlayerController.Instance.IsJumping();
	}

	public override void Action(Transform p_Actor)
	{
		switch (cstate) {
		case ChantState.TIMEDSTART:
			if (Time.time % 5 == 0) {
				cstate = ChantState.START;
			}
			break;
		case ChantState.START:
			playSound (chantPattern [patternIndex]);
			wait = true;
			StartCoroutine (waitSoundToPlay ());
			cstate = ChantState.PLAYING;
			break;
		case ChantState.PLAYING:
			if (!wait) {
				//Stop Sound;
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
			break;
		case '.':
			currentNote = ChantNote.SHORT;
			break;
		default:
			currentNote = ChantNote.PAUSE;
			break;
		}
	}

	private IEnumerator waitSoundToPlay() {
		switch (currentNote) {
		case ChantNote.LONG:
			yield return new WaitForSeconds (chantFrequencyLength * 2);
			break;
		default:
			yield return new WaitForSeconds (chantFrequencyLength);
			break;
		}
		wait = false;
	}
}
