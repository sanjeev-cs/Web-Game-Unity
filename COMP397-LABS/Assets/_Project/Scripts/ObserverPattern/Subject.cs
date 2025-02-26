using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Platformer397
{
    public abstract class Subject : MonoBehaviour
    {
        // List of Observers
        [SerializeField] private List<IObserver> observers = new List<IObserver>();

        // Add and/or Remover observers from the list
        public void AddObserver(IObserver observer) => observers.Add(observer);
        public void RemoveObserver(IObserver observer) => observers.Remove(observer);

        // Notification method to notify observers
        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.OnNotify();
            }
        }
    }
}
