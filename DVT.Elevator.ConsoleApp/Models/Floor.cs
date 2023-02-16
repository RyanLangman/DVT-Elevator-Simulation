namespace DVT.Elevator.ConsoleApp.Models
{
    public class Floor
    {
        public int Id { get; set; }

        public int WaitingPassengers { get; set; }

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
                Console.WriteLine($"{WaitingPassengers - remainingPassengers} passengers boarded on E-{elevator.Id}.");

                // TODO: If there are any passengers remaining, we need to call another elevator to take them.
                Console.WriteLine($"{remainingPassengers} passengers waiting on F-{Id}.");

                WaitingPassengers = remainingPassengers;
            }
        }

        // TODO: Event handler when elevator arriving on this for to disembark passengers?
    }
}
