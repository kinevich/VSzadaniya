using System;
using System.Collections.Generic;

namespace StatePattern
{
    class ConnectedPhoneState : IPhoneState
    {
        public void Call(Phone phone)
        {
            phone.State = new ConnectionPhoneState();

            ShowHelper.ShowValidPhoneActions(new List<Action> { phone.HangUp, phone.StandByOn });
        }

        public void HangUp(Phone phone)
        {
            phone.State = new NormalPhoneState();

            ShowHelper.ShowValidPhoneActions(new List<Action> { phone.Call });
        }

        public void PickUp(Phone phone)
        {

        }

        public void StandByOff(Phone phone)
        {

        }

        public void StandByOn(Phone phone)
        {

        }
    }
}

