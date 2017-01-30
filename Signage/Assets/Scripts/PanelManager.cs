using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

	// Book
	public BookData[] books;
	// initialized?
	private bool flagInit = false;
	// Generate Count
	private int genCount = 0;
	private int genLimit;
	// Canvas
	private GameObject canvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas");
		StartCoroutine(GetData());
	}
	
	// Update is called once per frame
	void Update () {
		if (flagInit)
		{
			StartCoroutine(timeCounter());
		}

	}

	// Get JSON and Init data
	private IEnumerator GetData()
	{
		// Get GoogleID from O Bookstore site.
		Debug.Log("Start Connection...");
		string url = "http://192.168.10.8/manage/DigiSign.php";
		WWWForm wwwForm = new WWWForm();
		wwwForm.AddField("reqword", "DigiSignReq");
		WWW result = new WWW(url, wwwForm);
		yield return result;
		Debug.Log("result = " + result.text);
		
		if(result.error == null)
		{
			JSONObject jsonGet = new JSONObject(result.text);
			books = new BookData[jsonGet.Count];
			genLimit = jsonGet.Count;

			for (int i = 0; i < jsonGet.Count; ++i)
			{
				JSONObject jsonCol = jsonGet[i];
				JSONObject jsonTitle = jsonCol.GetField("Title");
				JSONObject jsonAuthor = jsonCol.GetField("Author");
				JSONObject jsonDate = jsonCol.GetField("Date");
				JSONObject jsonDescri = jsonCol.GetField("Description");
				JSONObject jsonGoogleID = jsonCol.GetField("GoogleID");
				books[i] = new BookData(jsonTitle.str, jsonAuthor.str, jsonDate.str, jsonDescri.str, jsonGoogleID.str);
			}
		}

		flagInit = true;
	}

	// Generate a Panel per 7 seconds
	IEnumerator timeCounter()
	{
		flagInit = false;
		while (true)
		{
			if (genCount >= genLimit) genCount = 0;
			generatePanel();
			yield return new WaitForSeconds(7);
			genCount++;
			Debug.Log("gencount +1");
		}
	}

	// Panel Generate and set Data
	void generatePanel()
	{
		GameObject gengo = (GameObject)Instantiate(Resources.Load("Prefabs/Panel"));
		gengo.transform.SetParent(canvas.transform, false);

		GameObject gengoChild = gengo.transform.FindChild("BookDetail").gameObject;
		gengoChild.GetComponent<Text>().text = books[genCount].description;
		gengoChild = gengo.transform.FindChild("BookTitle").gameObject;
		gengoChild.GetComponent<Text>().text = books[genCount].title;
		gengoChild = gengo.transform.FindChild("BookDate").gameObject;
		gengoChild.GetComponent<Text>().text = books[genCount].date;
		gengoChild = gengo.transform.FindChild("BookAuthor").gameObject;
		gengoChild.GetComponent<Text>().text = books[genCount].author;
		gengoChild = gengo.transform.FindChild("BookImage").gameObject;
		StartCoroutine(downloadImage(gengoChild));
	}
	
	// Download and set Panel Image
	IEnumerator downloadImage(GameObject gengoChild)
	{
		WWW image = new WWW(books[genCount].img);
		yield return image;
		gengoChild.GetComponent<RawImage>().texture = image.textureNonReadable;
	}
}
