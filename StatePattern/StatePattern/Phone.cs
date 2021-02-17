namespace StatePattern
{
    class Phone
    {
        public IPhoneState State { get; set; }

        public Phone(IPhoneState state) => State = state;

        public void Call() => State.Call(this);

        public void HangUp() => State.HangUp(this);

        public void PickUp() => State.PickUp(this);

        public void StandByOff() => State.StandByOff(this);

        public void StandByOn() => State.StandByOn(this);
    }
}

