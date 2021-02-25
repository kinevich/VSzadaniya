using System;
using System.Collections.Generic;

namespace StatePattern
{
    class ConnectionPhoneState : IPhoneState
    {
        public void Call(Phone phone)
        {
            
        }

        public void HangUp(Phone phone)
        {
            phone.State = new NormalPhoneState();

            ShowHelper.ShowValidPhoneActions(new List<Action> { phone.Call });           
        }

        public void PickUp(Phone phone)
        {
            phone.State = new ConnectedPhoneState();

            ShowHelper.ShowValidPhoneActions(new List<Action> { phone.HangUp, phone.Call });
        }

        public void StandByOff(Phone phone)
        {
            
        }

        public void StandByOn(Phone phone)
        {
            phone.State = new InStandByPhoneState();

            ShowHelper.ShowValidPhoneActions(new List<Action> { phone.HangUp, phone.StandByOff });
        }
    }
}

