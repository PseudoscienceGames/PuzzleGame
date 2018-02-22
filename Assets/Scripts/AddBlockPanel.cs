using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlockPanel : MonoBehaviour
{
	bool hidden = true;

	public void Show()
	{
		if(hidden)
		{
			StartCoroutine(ShowButtons());
			hidden = false;
		}
	}

	public void Hide()
	{
		if(!hidden)
		{
			StartCoroutine(HideButtons());
			hidden = true;
		}
	}

	IEnumerator ShowButtons()
	{
		float t = 0;
		while (t <= 1)
		{
			t += Time.deltaTime * 4f;
			GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(0, 138.48f, 1 - t), GetComponent<RectTransform>().anchoredPosition.y);
			yield return null;
		}
		yield return null;
	}
	IEnumerator HideButtons()
	{
		float t = 0;
		while (t <= 1)
		{
			t += Time.deltaTime * 4f;
			GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(0, 138.48f, t), GetComponent<RectTransform>().anchoredPosition.y);
			yield return null;
		}
		yield return null;
	}
}
