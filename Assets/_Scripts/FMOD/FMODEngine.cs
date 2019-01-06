using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class FMODEngine : MonoBehaviour
{

    FMOD.System system;//a
    FMOD.Studio.System system2;
    List <FMOD.Sound> sonido;//cada sonido tendrá un canal
    List<FMOD.Sound> sonidoJugador; // todos los sonidos irán en el canal 0;
    List<FMOD.Channel> channel;
    FMOD.Channel channelPlayer;
    FMOD.Reverb3D reverb3d;
    public Transform Player;
    FMOD.Studio.EventInstance eventInstance;
    FMOD.Studio.Bank stringsBank;
    FMOD.Studio.Bank masterBank;
    FMOD.Studio.Bank vidaBank;
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

        CreateBank();





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
    public void CreateReverb3d(float min, float max)
    {
        ERRCHECK(system.createReverb3D( out reverb3d));

        FMOD.REVERB_PROPERTIES prop2 = FMOD.PRESET.UNDERWATER();

        ERRCHECK(reverb3d.setProperties( ref prop2));
     
    }

    public void CreateBank()


         
    {
        //float fHealth = 0.0f;
        float fHealth = 0.0f;

        //void* extraDriverData = NULL;
        //Common_Init(&extraDriverData);

        //FMOD::Studio::System* system = NULL;
        //ERRCHECK(FMOD::Studio::System::create(&system));
        ERRCHECK(FMOD.Studio.System.create(out system2));
        //// The example Studio project is authored for 5.1 sound, so set up the system output mode to match
        //FMOD::System* lowLevelSystem = NULL;
        FMOD.System lowLevelSystem;
        //ERRCHECK(system->getLowLevelSystem(&lowLevelSystem));
        ERRCHECK(system2.getLowLevelSystem(out lowLevelSystem));
        //ERRCHECK(lowLevelSystem->setSoftwareFormat(0, FMOD_SPEAKERMODE_5POINT1, 0));
        ERRCHECK(lowLevelSystem.setSoftwareFormat(0, FMOD.SPEAKERMODE._5POINT1, 0));

        //ERRCHECK(system->initialize(1024, FMOD_STUDIO_INIT_NORMAL, FMOD_INIT_NORMAL, extraDriverData));
        System.IntPtr a = System.IntPtr.Zero;
        system2.initialize(1024, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, a);

      
        ERRCHECK(system2.loadBankFile(Application.dataPath + "/Sounds/Master Bank.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out masterBank));

     
        ERRCHECK(system2.loadBankFile(Application.dataPath + "/Sounds/Master Bank.strings.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out stringsBank));
        
        //FMOD.SOUND_TYPE.Ban
        
        system2.loadBankFile(Application.dataPath + "/Sounds/vida.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out vidaBank);
        //FMOD::Studio::EventDescription* loopingDescription = NULL;
        //ERRCHECK(system->getEvent("event:/vida", &loopingDescription));
        FMOD.Studio.EventDescription loopingDescription;
        ERRCHECK(system2.getEvent("event:/vida", out loopingDescription));

        //FMOD::Studio::EventInstance* loopingInstance = NULL;
        //ERRCHECK(loopingDescription->createInstance(&loopingInstance));
        FMOD.Studio.EventInstance loopingInstance;
         ERRCHECK(loopingDescription.createInstance(out loopingInstance));
        //// Get the Single event
        //FMOD::Studio::EventDescription* vidaDescription = NULL;
        //ERRCHECK(system->getEvent("event:/vida", &vidaDescription));
        FMOD.Studio.EventDescription  vidaDescription;
        ERRCHECK(system2.getEvent("event:/vida", out vidaDescription));

        //// One-shot event
        //FMOD::Studio::EventInstance* eventInstance = NULL;
        //ERRCHECK(vidaDescription->createInstance(&eventInstance));

        
        ERRCHECK(vidaDescription.createInstance(out eventInstance));

        //// Start loading sample data and keep it in memory
        //ERRCHECK(vidaDescription->loadSampleData());
        ERRCHECK(vidaDescription.loadSampleData());
        ERRCHECK(eventInstance.start());
        setBank(1.5f);
    }
    public void createGeometry(float directOclusion,float reverbOclusion,FMOD.VECTOR[] _verts)
    {
        FMOD.Geometry geometry;
        ERRCHECK(system.createGeometry(1, 4, out geometry));

        FMOD.VECTOR[] verts = new FMOD.VECTOR[4];


        verts[0].x = _verts[0].x;
        verts[0].y = _verts[0].y;
        verts[0].z = _verts[0].z;

        verts[1].x = _verts[1].x;
        verts[1].y = _verts[1].y;
        verts[1].z = _verts[1].z;

        verts[2].x = _verts[2].x;
        verts[2].y = _verts[2].y;
        verts[2].z = _verts[2].z;

        verts[3].x = _verts[3].x;
        verts[3].y = _verts[3].y;
        verts[3].z = _verts[3].z;
        int polygonIndex = 0;
       ERRCHECK(geometry.addPolygon(directOclusion, reverbOclusion, true, 4, verts, out polygonIndex));


    }
    public void setBank(float value)
    {
        ERRCHECK(eventInstance.setParameterValue("fHealth",value));
    }
    public void setRever3d(float min, float max,float x , float y, float z)
    {
        FMOD.VECTOR pos = new FMOD.VECTOR();
        float minx = .0f;
        float maxx = .0f;
        reverb3d.get3DAttributes(ref pos,ref minx, ref maxx);

        ERRCHECK(reverb3d.set3DAttributes(ref pos, minx, maxx));
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
        system2.update();
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
        system2.release();
        vidaBank.unload();
        stringsBank.unload();
        masterBank.unload();

    }
}
