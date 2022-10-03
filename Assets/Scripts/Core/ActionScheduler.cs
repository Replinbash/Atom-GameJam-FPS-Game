using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Core
{
    //ActionScheduler: Aksiyon Zamanlay�c�
    public class ActionScheduler : MonoBehaviour
    {
        // currentAction: �uanki eylem
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        // eylemi null hale getiriyoruz.
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}

