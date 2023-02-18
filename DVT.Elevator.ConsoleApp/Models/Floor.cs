namespace DVT.Elevator.ConsoleApp.Models
{
    public class Floor
    {
        public int Id { get; }

        private int _waitingPassengers;
        public int WaitingPassengers
        {
            get { return _waitingPassengers; }
        }

        public event EventHandler<int> OnRequestNewPickupEvent;

        public Floor(int id, List<Elevator> elevators)
        {
            Id = id;

            elevators.ForEach(e =>
            {
                e.OnPickupFloorArrivalEvent += HandleElevatorPickupArrival;
            });
        }

        public void SetWaitingPassengers(int numberOfPeople)
        {
            if (numberOfPeople > 14)
            {
                throw new Exception("Too many people!");
            }

            _waitingPassengers = numberOfPeople;
        }

        private void HandleElevatorPickupArrival(object sender, EventArgs e)
        {
            var elevator = sender as Elevator;

            if (elevator.CurrentFloor == Id) 
            {
                var remainingPassengers = elevator.OnboardPassengers(WaitingPassengers);

                _waitingPassengers = remainingPassengers;

                if (WaitingPassengers == 0) return;

                if (!elevator.DestinationFloor.HasValue)
                {
                    throw new Exception("Destination floor must be present.");
                }

                OnRequestNewPickupEvent?.Invoke(this, elevator.DestinationFloor.Value);
            }
        }
    }
}
