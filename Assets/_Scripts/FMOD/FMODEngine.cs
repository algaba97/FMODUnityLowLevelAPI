using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FMODEngine : MonoBehaviour
{

    FMOD.System system;
    FMOD.Sound sonido;
    FMOD.Channel channel;

    // Use this for initialization
    void Awake()
    {
        FMOD.RESULT result;
        result = FMOD.Factory.System_Create(out system); // Hay que comprobar que inicializa
        //result = system->init(128, FMOD_INIT_NORMAL, 0);
        System.IntPtr a = System.IntPtr.Zero;
        result = system.init(128, FMOD.INITFLAGS.NORMAL, a);
        ERRCHECK(result);
        result = system.set3DSettings(1.0f, 1.0f, 1.0f);
        ERRCHECK(result);


        string cadena = Application.dataPath + "/Sounds/Bowhit.wav";
        Debug.Log(cadena);
        CreateSound(cadena);
     


    }

    void CreateSound(string cadena)
    {
        //_system->createSound(
        //ruta, // path al archivo de sonido
        //FMOD_DEFAULT, // valores (por defecto en este caso: sin loop, 2D)
        //0, // informacion adicional (nada en este caso)
        //&sonido);
        system.createSound(cadena, FMOD.MODE._3D, out sonido);


    }
    void SetPosition()
    {
       
        FMOD.VECTOR pos;
        pos.x = -10.0f;
        pos.y = 0.0f;
        pos.z = 0.0f;
        FMOD.VECTOR vel;
        vel.x = 0.0f;
        vel.y = 0.0f;
        vel.z = 0.0F;

        //FSev.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, playerPos)); 
        ERRCHECK(channel.set3DAttributes(ref pos, ref vel, ref vel));

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
        system.playSound(sonido, aux, false, out channel);
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        Play();
        system.update();
        Debug.Log("update");

    }

    void ERRCHECK(FMOD.RESULT result)
    {
        if (result != FMOD.RESULT.OK)

        {
            Debug.Log(result.ToString());

        }
    }
}
