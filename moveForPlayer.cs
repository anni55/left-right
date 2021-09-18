using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Notifications.Android;

public class moveForPlayer : MonoBehaviour
{
    public Rigidbody rb;


    public GameObject goStrateButten;
    public GameObject goTurneButten;
    public GameObject truejumpButten;
    public GameObject falsejumpButten;


    public GameObject control;
    public GameObject gameOver;
    public GameObject GameComp;


    public GameObject ObjectMusic;
    private AudioSource AudioFile;
    

    public GameObject VibOnButten;
    public GameObject VibOffButten;
    public GameObject souOnButten;
    public GameObject souOffButten;
    public GameObject NotifiOnButten;
    public GameObject notifiOffButten;

    public GameObject spick1Obj;
    public GameObject spick2Obj;


    public float speed = 600;
    private float speedX;
    private float speedZ;
    private int addTime = 0;
    private int count = 0;
    public int Gforce;


    private int souAloud;
    private int forvib;
    private int NotificationsAloude;
	  private int save;



////////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
	  	rb = GetComponent<Rigidbody>();
      goTurneButten.SetActive(false);
      goStrateButten.SetActive(true);

      NotificationsAloude = PlayerPrefs.GetInt("NotificationsAloude");
      souAloud = PlayerPrefs.GetInt("souAloud");
      forvib = PlayerPrefs.GetInt("forvib");
      save = PlayerPrefs.GetInt("save");     


      if (souAloud == 0)
      {
        ObjectMusic = GameObject.FindWithTag("audio manager");
        AudioFile = ObjectMusic.GetComponent<AudioSource>();
        souOnButten.SetActive(true);
        souOffButten.SetActive(false);
      }
      if (souAloud == 1)
      {
        ObjectMusic = GameObject.FindWithTag("audio manager");
        AudioFile = ObjectMusic.GetComponent<AudioSource>();
        souOnButten.SetActive(false);
        souOffButten.SetActive(true);
      }
      
      if (forvib == 1)
      {
        VibOnButten.SetActive(true);
        VibOffButten.SetActive(false);
      }
      if (forvib == 0)
      {
        VibOffButten.SetActive(true);
        VibOnButten.SetActive(false);
      }

      
      notifications();
    }
    void Update()
    {
      rb.velocity = new Vector3(speedX, Gforce, speedZ)*Time.fixedDeltaTime;
      //Debug.Log(souAloud);
      

      if(NotificationsAloude == 0)
      {
        NotifiOnButten.SetActive(true);
        notifiOffButten.SetActive(false);
        
        Debug.Log("Notifications is OFF");
      }
      if(NotificationsAloude == 1)
      {
        NotifiOnButten.SetActive(false);
        notifiOffButten.SetActive(true);

        Debug.Log("Notifications is ON");
      }

      if (forvib == 1)
      {
        VibOnButten.SetActive(false);
        VibOffButten.SetActive(true);
        Debug.Log("Vibration is OFF");
      }
      if (forvib == 0)
      {
        VibOnButten.SetActive(true);
        VibOffButten.SetActive(false);
        Debug.Log("Vibration is ON");
      }
      
      if (souAloud == 1)
      {
        Debug.Log("Sound is ON");
        souOffButten.SetActive(true);
        souOnButten.SetActive(false);
        AudioFile.volume = 0.6f;
      }
      if (souAloud == 0)
      {
        Debug.Log("sound is OFF");
        souOnButten.SetActive(true);
        souOffButten.SetActive(false);
        AudioFile.volume = 0;
      }
    }
    
    void FixedUpdate()
    {

      count = count + addTime;


      if (count == 20)
      {
        Gforce = -400;
      }
      if (count == 40)
      {
        truejumpButten.SetActive(true);
        falsejumpButten.SetActive(false);
        speed = 600;
        addTime = 0;
        count = 0;
      }

    }



    public void GoTurn()
    {
      speedX = -600;
      speedZ = 0;

      goStrateButten.SetActive(true);
      goTurneButten.SetActive(false);
    }
    public void GoStrate()
    {
      speedZ = 600;
      speedX = 0;
      
      goStrateButten.SetActive(false);
      goTurneButten.SetActive(true);
    }
    public void Jump()
    {
      speed = 100;
      Gforce = 500;
      addTime = 1;
      truejumpButten.SetActive(false);
      falsejumpButten.SetActive(true);
    }

    
    ///////////////////////////////////////////////////////////////////////////////////////////////////


    public void souOn()
    {
      souAloud = 1;
      PlayerPrefs.SetInt("souAloud",souAloud);

      souOnButten.SetActive(false);
      souOffButten.SetActive(true);

      AudioFile.volume = 0.6f;
    }
    public void souOff()
    {
      souAloud = 0;
      PlayerPrefs.SetInt("souAloud",souAloud);

      souOffButten.SetActive(false);
      souOnButten.SetActive(true);

      AudioFile.volume = 0;
    }

    public void vibOn()
    {
      VibOnButten.SetActive(false);
      VibOffButten.SetActive(true);

      forvib = 1;
      PlayerPrefs.SetInt("forvib",forvib);
    }
    public void vibOff()
    {
      VibOnButten.SetActive(true);
      VibOffButten.SetActive(false);

      forvib = 0;
      PlayerPrefs.SetInt("forvib",forvib);
    }
    
    public void NotificationsOn()
    {
      NotificationsAloude = 1;
      PlayerPrefs.SetInt("NotificationsAloude",NotificationsAloude);
      //Debug.Log("Notification is on");
    }
    public void NotificationsOff()
    {
      NotificationsAloude = 0;
      PlayerPrefs.SetInt("NotificationsAloude",NotificationsAloude);
      //Debug.Log("Notification is NOT on");
    }

///////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void notifications()
    {
        if ( NotificationsAloude == 1)
        {
          var channel = new AndroidNotificationChannel()
          {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
          };
          AndroidNotificationCenter.RegisterNotificationChannel(channel);


          var notification = new AndroidNotification();
          notification.Title = "Hey! come on";
          notification.Text = "let's playe Endless";
          notification.FireTime = System.DateTime.Now.AddHours(3);

          AndroidNotificationCenter.SendNotification(notification, "channel_id");
          var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

          if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
          {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
          }
        }
        else
        {
          Update();
        }
    }

///////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnTriggerEnter(Collider other)
	  {

	  	if(other.transform.tag == "end")
	  	{
        gameOver.SetActive(true);
        control.SetActive(false);
        
        if(forvib == 0)
        {
          Handheld.Vibrate();
        }
      }
      
	  	if(other.transform.tag == "spick1")
	  	{
        spick1Obj.SetActive(true);
      }
	  	if(other.transform.tag == "spick2")
	  	{
        spick2Obj.SetActive(true);
      }

	  	if(other.transform.tag == "spick")
	  	{
        gameOver.SetActive(true);
      }
      
	  	if(other.transform.tag == "finesh")
	  	{
        speedX = speedX/2;
        speedZ = speedZ/2;
        
	      save = save + 1;
    	  PlayerPrefs.SetInt("save",save);
        
        if(forvib == 0)
        {
          Handheld.Vibrate();
        }
        
        GameComp.SetActive(true);
        control.SetActive(false);
		  }
    }
}