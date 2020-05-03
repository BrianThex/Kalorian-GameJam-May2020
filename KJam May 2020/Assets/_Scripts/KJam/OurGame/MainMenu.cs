using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

namespace KJam.OurGame
{
	public class MainMenu : MonoBehaviour
	{
		#region Singleton
		public static MainMenu instance;

		private void Awake()
		{
			instance = this;
		}
		#endregion

		#region Button Animation Vars
		public TMP_Text leftAnim = null, rightAnim = null;

		public float btnAnimDistance = 0.1f, btnAnimSpeed = 5.0f;

		private Button activeButton = null;

		private TMP_Text activeText = null, lastActiveTxt = null;
		#endregion

		public TMP_InputField nickNameInputField = null;

		private const string playerPrefsNameKey = "NickName";

		public Button findMatchBtn = null;

		private void Start() => SetLastUser();

		private void SetLastUser()
		{
			// checking if there are any player prefs saved for the current user, if there arent any, then skip this function
			// but if there are then set that to the defalt text in the input field
			if (!PlayerPrefs.HasKey(playerPrefsNameKey))
			{
				return;
			}
			else
			{
				string defaultName = PlayerPrefs.GetString(playerPrefsNameKey);
				nickNameInputField.text = defaultName;
				SetNickName(defaultName);
			}
		}

		public void SetNickName(string name)
		{
			findMatchBtn.interactable = !string.IsNullOrEmpty(name);
		}

		private void SaveNickName()
		{
			string nickName = nickNameInputField.text;

			PhotonNetwork.NickName = nickName;

			PlayerPrefs.SetString(playerPrefsNameKey, nickName);
			PlayerPrefs.Save();
		}

		private void Update()
		{
			SetBtnAnimPos();
			AnimateBtnObjs();

			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				if (nickNameInputField.isFocused && nickNameInputField.text.Length > 0)
				{
					//find match
				}
				else
				{
					switch (activeButton.name)
					{
						case "FindMatchBtn":
							//find match
							break;
						case "HowToPlayBtn":
							//go to howtoplay menu
							break;
						case "OptionsBtn":
							//go to options menu
							break;
					}
				}
			}
		}

		private void SetBtnAnimPos()
		{
			PointerEventData pointerEData = new PointerEventData(EventSystem.current);
			pointerEData.position = Input.mousePosition;

			List<RaycastResult> rayResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEData, rayResults);

			for (int i = 0; i < rayResults.Count; i++)
			{
				activeText = rayResults[i].gameObject.GetComponent<TMP_Text>();
				activeButton = rayResults[i].gameObject.GetComponent<Button>();

				if (activeButton == null)
				{
					rayResults.RemoveAt(i);
					i--;
				}
				else
				{
					if (lastActiveTxt != null)
					{
						if (lastActiveTxt != activeText)
						{
							lastActiveTxt.color = Color.white;
						}
					}
					lastActiveTxt = activeText;

					leftAnim.transform.position = new Vector2(leftAnim.transform.position.x, rayResults[i].gameObject.transform.position.y); //,leftAnim.transform.position.z);
					rightAnim.transform.position = new Vector2(rightAnim.transform.position.x, rayResults[i].gameObject.transform.position.y); //,rightAnim.transform.position.z);

					activeText.color = activeButton.colors.highlightedColor;
				}
			}
		}

		private void AnimateBtnObjs()
		{
			Vector2 lAnimatedPos = leftAnim.transform.position;
			Vector2 rAnimatedPos = rightAnim.transform.position;

			lAnimatedPos.x -= btnAnimDistance * Mathf.Sin(Time.time * btnAnimSpeed);
			rAnimatedPos.x += btnAnimDistance * Mathf.Sin(Time.time * btnAnimSpeed);

			leftAnim.transform.position = lAnimatedPos;
			rightAnim.transform.position = rAnimatedPos;
		}
	}
}

