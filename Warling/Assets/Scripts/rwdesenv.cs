using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;
using UnityEngine.UI;
//???
using EasyMobile;

public class rwdesenv : MonoBehaviour {
	
	//??? ca-app-pub-2697339358784861/5512114771
    void Awake()
    {
		if (!GameServices.IsInitialized()) {
			GameServices.ManagedInit();
		} 		
		
        if (!RuntimeManager.IsInitialized()) {
            RuntimeManager.Init();
		}	
    }	

	
	public void abrirLeaderboard() {
		if (GameServices.IsInitialized()) {
			GameServices.ShowLeaderboardUI();
		} else {
			GameServices.Init();
			GameServices.ShowLeaderboardUI();
		} 
	}		
	
	

	public void voltarHome() {
		Advertising.LoadInterstitialAd();
		bool isReady = Advertising.IsInterstitialAdReady(); 
		if (isReady)
		{
			Advertising.ShowInterstitialAd(); 
		}		
		SceneManager.LoadScene("Scene1");
    }

	public Button tutbasic;
	public Button btnPick0rw;
	public Button btnPick1rw;
	public Button btnPick2rw;
	public Button btnPick3rw;
	public Button btnPick4rw;
	public Button btnPick5rw;
	public Button btnPick6rw;
	public Button btnPick7rw;
	public Button btnPick8rw;
	public Button btnPick9rw;
	public Button btnPick10rw;
	public Button btnPick11rw;
	public Button btnPick12rw;
	public Button btnPick13rw;
	public Button btnPick14rw;
	public Button btnPick15rw;
	public Button btnPick16rw;
	public Button btnPick17rw;

	void Start() {
		tutbasic.gameObject.SetActive(false);
		btnPick0rw.gameObject.SetActive(false);
		btnPick1rw.gameObject.SetActive(false);
		btnPick2rw.gameObject.SetActive(false);
		btnPick3rw.gameObject.SetActive(false);
		btnPick4rw.gameObject.SetActive(false);
		btnPick5rw.gameObject.SetActive(false);
		btnPick6rw.gameObject.SetActive(false);
		btnPick7rw.gameObject.SetActive(false);
		btnPick8rw.gameObject.SetActive(false);
		btnPick9rw.gameObject.SetActive(false);
		btnPick10rw.gameObject.SetActive(false);
		btnPick11rw.gameObject.SetActive(false);
		btnPick12rw.gameObject.SetActive(false);
		btnPick13rw.gameObject.SetActive(false);
		btnPick14rw.gameObject.SetActive(false);
		btnPick15rw.gameObject.SetActive(false);
		btnPick16rw.gameObject.SetActive(false);
		btnPick17rw.gameObject.SetActive(false);
	}

    public void openTutbasic()
    {
		tutbasic.gameObject.SetActive(true);	
    }
    public void closeTutbasic()
    {
		tutbasic.gameObject.SetActive(false);		
    }	
	
    public void open0()
    {
		btnPick0rw.gameObject.SetActive(true);	
    }
    public void close0()
    {
		btnPick0rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open1()
    {
		btnPick1rw.gameObject.SetActive(true);	
    }
    public void close1()
    {
		btnPick1rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open2()
    {
		btnPick2rw.gameObject.SetActive(true);	
    }
    public void close2()
    {
		btnPick2rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open3()
    {
		btnPick3rw.gameObject.SetActive(true);	
    }
    public void close3()
    {
		btnPick3rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open4()
    {
		btnPick4rw.gameObject.SetActive(true);	
    }
    public void close4()
    {
		btnPick4rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open5()
    {
		btnPick5rw.gameObject.SetActive(true);	
    }
    public void close5()
    {
		btnPick5rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open6()
    {
		btnPick6rw.gameObject.SetActive(true);	
    }
    public void close6()
    {
		btnPick6rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open7()
    {
		btnPick7rw.gameObject.SetActive(true);	
    }
    public void close7()
    {
		btnPick7rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open8()
    {
		btnPick8rw.gameObject.SetActive(true);	
    }
    public void close8()
    {
		btnPick8rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open9()
    {
		btnPick9rw.gameObject.SetActive(true);	
    }
    public void close9()
    {
		btnPick9rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open10()
    {
		btnPick10rw.gameObject.SetActive(true);	
    }
    public void close10()
    {
		btnPick10rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open11()
    {
		btnPick11rw.gameObject.SetActive(true);	
    }
    public void close11()
    {
		btnPick11rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open12()
    {
		btnPick12rw.gameObject.SetActive(true);	
    }
    public void close12()
    {
		btnPick12rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open13()
    {
		btnPick13rw.gameObject.SetActive(true);	
    }
    public void close13()
    {
		btnPick13rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open14()
    {
		btnPick14rw.gameObject.SetActive(true);	
    }
    public void close14()
    {
		btnPick14rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open15()
    {
		btnPick15rw.gameObject.SetActive(true);	
    }
    public void close15()
    {
		btnPick15rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open16()
    {
		btnPick16rw.gameObject.SetActive(true);	
    }
    public void close16()
    {
		btnPick16rw.gameObject.SetActive(false);		
    }
//-------------	
    public void open17()
    {
		btnPick17rw.gameObject.SetActive(true);	
    }
    public void close17()
    {
		btnPick17rw.gameObject.SetActive(false);		
    }



    public void OpenStoreBlack()
    {
		Application.OpenURL("https://play.google.com/store/apps/dev?id=8661706558284396298");		
    }
    public void OpenStoreWhite()
    {
		Application.OpenURL("https://play.google.com/store/apps/dev?id=7003053749192026050");		
    }
	
    public void LikeFacebookPage()
    {
	    Application.OpenURL("https://fb.me/rwdesenvgames");
    }	


/*    public void Rate()
    {
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.RWdesenv.SimpleMagic");		
    }	*/
	
	
	string subject = "Angry Smileys Wars";
	string body = "Angry Smileys Wars\n\nhttps://play.google.com/store/apps/details?id=com.RWdesenv.AngrySmileysWars";	 
	public void shareText(){
		//execute the below lines if being run on a Android device
		#if UNITY_ANDROID
			  //Refernece of AndroidJavaClass class for intent
			  AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			  //Refernece of AndroidJavaObject class for intent
			  AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			  //call setAction method of the Intent object created
			  intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			  //set the type of sharing that is happening
			  intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			  //add data to be passed to the other activity i.e., the data to be sent
			  intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
			  intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
			  //get the current activity
			  AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			  AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			  //start the activity by sending the intent data
			  currentActivity.Call ("startActivity", intentObject);
		#endif
	}	

		
	
}
