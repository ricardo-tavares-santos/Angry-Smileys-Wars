using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rwdesenvOnline : MonoBehaviour {

	public Button onlinerw;
	public Button offlinerw;
	
	void Start () {
		onlinerw.gameObject.SetActive(false);
		offlinerw.gameObject.SetActive(false);		
	}
	
	
    void openOnline()
    {
		onlinerw.gameObject.SetActive(true);	
    }
    public void closeOnline()
    {
		onlinerw.gameObject.SetActive(false);		
    }
	
    void openOffline()
    {
		offlinerw.gameObject.SetActive(true);	
    }
    public void closeOffline()
    {
		offlinerw.gameObject.SetActive(false);		
    }	
	
	
	
	public void isInternet() {
	
		openOffline();
	
	}
	
	
}
