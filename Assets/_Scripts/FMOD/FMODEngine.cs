using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class FMODEngine : MonoBehaviour
{

    FMOD.System system;//a
    List <FMOD.Sound> sonido;//cada sonido tendrá un canal
    List<FMOD.Sound> sonidoJugador; // todos los sonidos irán en el canal 0;
    List<FMOD.Channel> channel;
    FMOD.Channel channelPlayer;
    public Transform Player;

    // Use this for initialization
    void Awake()
    {
        //inicializamos las listas
        sonido = new List<FMOD.Sound>();
        sonidoJugador = new List<FMOD.Sound>();
        channel = new List<FMOD.Channel>();
        channelPlayer = new FMOD.Channel();

        FMOD.RESULT result;
        result = FMOD.Factory.System_Create(out system); // Hay que comprobar que inicializa
        ERRCHECK(result);
        //result = system->init(128, FMOD_INIT_NORMAL, 0);
        System.IntPtr a = System.IntPtr.Zero;
        result = system.init(128, FMOD.INITFLAGS.NORMAL, a);
        ERRCHECK(result);
        result = system.set3DSettings(1.0f, 1.0f, 1.0f);
        ERRCHECK(result);

       
        



    }

    public int CreateSound(string cadena,bool loop)//creamos sonido y canal devolviendo el numero para que los objetos solo tengan que saber que numero son
    {
        string Cadena = Application.dataPath + "/Sounds/" + cadena;
        FMOD.Sound aux = new FMOD.Sound();
       
        //creamos sonido y canal 
        if (!loop) system.createSound(Cadena, FMOD.MODE._3D, out aux);
       

        else ERRCHECK(system.createSound(Cadena, FMOD.MODE._3D | FMOD.MODE.LOOP_NORMAL, out aux));
        sonido.Add(aux);
        int length = sonido.Count;
        FMOD.Channel aux2 = new FMOD.Channel();
        channel.Add(aux2);

        return length-1;
    }

   public  int CreateSoundPlayer(string cadena,bool loop)//creamos sonido devolviendo el numero para que los objetos solo tengan que saber que numero son
    {
        string Cadena = Application.dataPath + "/Sounds/" + cadena;
        FMOD.Sound aux = new FMOD.Sound();
        
        //creamos sonido y canal 
        if (!loop) system.createSound(Cadena, FMOD.MODE._2D, out aux);
        else system.createSound(Cadena, FMOD.MODE._2D | FMOD.MODE.LOOP_NORMAL, out aux);

        sonidoJugador.Add(aux);
        int length = sonido.Count;

        return length-1;
    }

    public void SetPosition(int number,float x, float y ,float z)
    {
        FMOD.VECTOR pos;

        FMOD.VECTOR vel;
        if (number != -1)
        {
            FMOD.Channel aux2 = channel[number];


           
            aux2.get3DAttributes(out pos, out vel, out vel);
            pos.x = x;
            pos.y = y;
            pos.z = z;


            Debug.Log(number);
            Debug.Log(channel.Count);
            Debug.Log(channel[number]);

            //FSev.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, playerPos)); 
            ERRCHECK(channel[number].set3DAttributes(ref pos, ref vel, ref vel));
        }
        else
        {
            ERRCHECK(channelPlayer.get3DAttributes(out pos, out vel, out vel));
            pos.x = x;
            pos.y = y;
            pos.z = z;
            ERRCHECK(channelPlayer.set3DAttributes(ref pos, ref vel, ref vel));

        }

    }


   public  void Play(int numero)
    {
        //_system->playSound(
        //sonido, // buffer que se "engancha" a ese canal
        //0, // grupo de canales, 0 sin agrupar (agrupado en el master)
        //false, // arranca sin "pause" (se reproduce directamente)
        //&channel);
        FMOD.ChannelGroup aux;
        system.getMasterChannelGroup(out aux);
        FMOD.Channel aux2 = channel[numero];
        system.playSound(sonido[numero], aux, false, out aux2);
        channel[numero] = aux2;
    }
    public void PlayPlayer(int numero)
    {
        Debug.Log("PlayPlayer");
      
        FMOD.ChannelGroup aux;
        system.getMasterChannelGroup(out aux);
        system.playSound(sonidoJugador[numero], aux, false, out channelPlayer);
        
    }



    // Update is called once per frame
    void LateUpdate()
    {
       // SetPosition(0, Player.position.x, Player.position.y, Player.position.z);
        moveListener(Player.position.x, Player.position.y, Player.position.z);
        system.update();
       // Debug.Log("update");

    }

    void ERRCHECK(FMOD.RESULT result)
    {
        if (result != FMOD.RESULT.OK)

        {
            Debug.Log("Cierra ya abre Unity, comprueba las rutas");
            Debug.Log(result.ToString());

        }
    }
    void moveListener(float x,float y ,float z)
    {
        FMOD.VECTOR pos, vel, up, forward;
        system.get3DListenerAttributes(0, out pos, out vel, out up, out forward);
        pos.x = x;
        pos.y = y;
        pos.z = z;
        system.set3DListenerAttributes(0, ref pos, ref vel, ref up, ref forward);
    }
    void OnApplicationQuit()
    {
        system.release();
        sonido.Clear();//cada sonido tendrá un canal
        sonidoJugador.Clear(); // todos los sonidos irán en el canal 0;
        channel.Clear();
    }
}
