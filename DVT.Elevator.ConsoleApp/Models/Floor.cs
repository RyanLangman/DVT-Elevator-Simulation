namespace DVT.Elevator.ConsoleApp.Models
{
    public class Floor
    {
        public int Id { get; }

        public int WaitingPassengers { get; set; }

        public event EventHandler<int> OnRequestNewPickupEvent;

        public Floor(int id, List<Elevator> elevators)
        {
            Id = id;

            elevators.ForEach(e =>
            {
                e.OnPickupFloorArrivalEvent += HandleElevatorPickupArrival;
            });
        }

        private void HandleElevatorPickupArrival(object sender, EventArgs e)
        {
            var elevator = sender as Elevator;

            if (elevator.CurrentFloor == Id) 
            {
                var remainingPassengers = elevator.OnboardPassengers(WaitingPassengers);

                WaitingPassengers = remainingPassengers;

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
