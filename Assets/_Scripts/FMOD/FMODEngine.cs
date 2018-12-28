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


    // Use this for initialization
    void Awake()
    {
        //inicializamos las listas
        sonido = new List<FMOD.Sound>();
        sonidoJugador = new List<FMOD.Sound>();
        channel = new List<FMOD.Channel>();

        FMOD.RESULT result;
        result = FMOD.Factory.System_Create(out system); // Hay que comprobar que inicializa
        //result = system->init(128, FMOD_INIT_NORMAL, 0);
        System.IntPtr a = System.IntPtr.Zero;
        result = system.init(128, FMOD.INITFLAGS.NORMAL, a);
        ERRCHECK(result);
        result = system.set3DSettings(1.0f, 1.0f, 1.0f);
        ERRCHECK(result);

        //creamos el sonido 0 que será del jugador 
        string cadena = Application.dataPath + "/Sounds/Bowhit.wav";
        Debug.Log(cadena);
        CreateSound(cadena);
     


    }

    int CreateSound(string cadena)//creamos sonido y canal devolviendo el numero para que los objetos solo tengan que saber que numero son
    {
        FMOD.Sound aux = new FMOD.Sound();
        sonido.Add(aux);
        int length = sonido.Count;
        //creamos sonido y canal 
        system.createSound(cadena, FMOD.MODE._3D, out aux);
        FMOD.Channel aux2 = new FMOD.Channel();
        channel.Add(aux2);

        return length;
    }

    int CreateSoundPlayer(string cadena)//creamos sonido devolviendo el numero para que los objetos solo tengan que saber que numero son
    {
        FMOD.Sound aux = new FMOD.Sound();
        sonidoJugador.Add(aux);
        int length = sonido.Count;
        //creamos sonido y canal 
        system.createSound(cadena, FMOD.MODE._3D, out aux);
        return length;
    }

    void SetPosition(int number,float x, float y ,float z)
    {
       
        FMOD.VECTOR pos;
        pos.x = x;
        pos.y = y;
        pos.z = z;
        FMOD.VECTOR vel;
        vel.x = 0.0f;
        vel.y = 0.0f;
        vel.z = 0.0F;

        //FSev.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, playerPos)); 
        ERRCHECK(channel[number].set3DAttributes(ref pos, ref vel, ref vel));

    }
    void Play()
    {
        //_system->playSound(
        //sonido, // buffer que se "engancha" a ese canal
        //0, // grupo de canales, 0 sin agrupar (agrupado en el master)
        //false, // arranca sin "pause" (se reproduce directamente)
        //&channel);
        FMOD.ChannelGroup aux;
        system.getMasterChannelGroup(out aux);
      ///  system.playSound(sonido, aux, false, out channel);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Play();
        system.update();
       // Debug.Log("update");

    }

    void ERRCHECK(FMOD.RESULT result)
    {
        if (result != FMOD.RESULT.OK)

        {
            Debug.Log(result.ToString());

        }
    }
}
