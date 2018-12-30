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
    public Transform Player;

    // Use this for initialization
    void Awake()
    {
        //inicializamos las listas
        sonido = new List<FMOD.Sound>();
        sonidoJugador = new List<FMOD.Sound>();
        channel = new List<FMOD.Channel>();

        FMOD.RESULT result;
        result = FMOD.Factory.System_Create(out system); // Hay que comprobar que inicializa
        ERRCHECK(result);
        //result = system->init(128, FMOD_INIT_NORMAL, 0);
        System.IntPtr a = System.IntPtr.Zero;
        Debug.Log("Aqui");
        result = system.init(128, FMOD.INITFLAGS.NORMAL, a);
        ERRCHECK(result);
        Debug.Log("Aqui");
        result = system.set3DSettings(1.0f, 1.0f, 1.0f);
        ERRCHECK(result);

        //creamos el canal 0 para el sonido del jugador
       



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
        sonidoJugador.Add(aux);
        int length = sonido.Count;
        //creamos sonido y canal 
        if (!loop) system.createSound(Cadena, FMOD.MODE._3D, out aux);
        else system.createSound(Cadena, FMOD.MODE._3D | FMOD.MODE.LOOP_NORMAL, out aux);

        return length-1;
    }

    public void SetPosition(int number,float x, float y ,float z)
    {
        FMOD.Channel aux2 = channel[number];

        FMOD.VECTOR pos;

        FMOD.VECTOR vel;
        aux2.get3DAttributes(out pos, out vel, out vel);
        pos.x = x;
        pos.y = y;
        pos.z = z;
      

        Debug.Log(number);
        Debug.Log(channel.Count);
        Debug.Log(channel[number]);

        //FSev.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, playerPos)); 
        ERRCHECK(aux2.set3DAttributes(ref pos, ref vel, ref vel));

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

    // Update is called once per frame
    void LateUpdate()
    {

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
