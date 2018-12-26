using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    //events are stored here
    private IDictionary<string, UnityEvent> eventDictionary;

    //name of subscribed objects are stored as strings
    private IDictionary<string, List<string>> subscribedObjects;


    private static EventManager eventManager;
    private static bool ApplicationIsQuitting = false;

    private string findName;

    public IDictionary<string, UnityEvent> EventDictionary
    {
        get
        {
            return eventDictionary;
        }
    }

    public IDictionary<string, List<string>> SubscribedObjects
    {
        get
        {
            return subscribedObjects;
        }
    }

    //event manager singleton
    public static EventManager instance
    {
        get
        {
            //prevent the creation of an instance after the object has been destroyed
            if (ApplicationIsQuitting)
                return null;

            if (eventManager == null)
            {
                //try to find a eventManager in the hierarchy
                eventManager = GameObject.FindObjectOfType<EventManager>();

                if (eventManager == null)
                {
                    GameObject container = new GameObject("EventManager");
                    eventManager = container.AddComponent<EventManager>();
                    eventManager.initialize();
                }
            }
            eventManager.initialize();
            return eventManager;
        }
    }


    private void initialize()
    {
        //create an event dictionary if none exists
        if (eventDictionary == null)
            eventDictionary = new Dictionary<String, UnityEvent>();

        //create the subscriber list if none exists
        if (subscribedObjects == null)
            subscribedObjects = new Dictionary<string, List<string>>();
    }

    private void OnDestroy()
    {
        ApplicationIsQuitting = true;
    }

    //starts listening for events
    public void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);

            string name = listener.Target.ToString();
            instance.subscribedObjects[eventName].Add(name);

        }
        else
        {

            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);

            //create a new entry in the dictionary
            instance.subscribedObjects.Add(eventName, new List<string>());
            //enter the name into the dictionary 
            string name = listener.Target.ToString();
            instance.subscribedObjects[eventName].Add(name);

        }
    }

    //stops listening for events
    public void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null)
            return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);

            //remove the entry of this subscriber from the subscriberlist
            List<string> name;
            if (instance.subscribedObjects.TryGetValue(eventName, out name))
            {
                //convert the target info to a string
                findName = listener.Target.ToString();
                //remove the subscriber from the list by finding the target infor with a predicate
                instance.subscribedObjects[eventName].Remove(name.Find(isName));
            }

        }
    }

    //triggers an event
    public void TriggerEvent(string eventName, bool debug = false)
    {
        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
        else
            Debug.LogWarning("failed to trigger event: " + eventName + ". the event does not exist");
    }

    //predicate for findin names of subscribers in the subscriberlist
    private bool isName(string name)
    {
        return (name == findName);
    }
}
