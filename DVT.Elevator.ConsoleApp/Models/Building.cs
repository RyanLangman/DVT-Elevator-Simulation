using DVT.Elevator.ConsoleApp.Services;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Building
    {
        public List<Floor> Floors { get; set; } = new List<Floor>();

        public List<Elevator> Elevators { get; set; } = new List<Elevator>();

        private readonly IElevatorOrchestrator elevatorOrchestrator;

        public bool Exists { get; set; } = true;

        public Building()
        {
            for (var i = 1; i <= 1; i++)
            {
                // Elevators will start at floor 1
                Elevators.Add(new Elevator(1));
            }

            for (var i = 1; i <= 10; i++)
            {
                Floors.Add(new Floor(i, Elevators));
            }

            elevatorOrchestrator = new ElevatorOrchestrator(Floors, Elevators);
        }

        public void Exist()
        {
            // TODO: Validate input
            Console.WriteLine();
            Console.WriteLine("Skip instructions this step? (y/n)");
            if (Console.ReadLine() != "y")
            {
                Console.WriteLine("Choose floor to call elevator (1-10): ");
                var callToFloor = int.Parse(Console.ReadLine());

                Console.WriteLine("How many passengers? ");
                var passengerCount = int.Parse(Console.ReadLine());

                Console.WriteLine("Choose a destination floor (1-10): ");
                var destinationFloor = int.Parse(Console.ReadLine());
                Console.WriteLine();

                // TODO: Orchestrator will receive inputs and allocate an elevator to that floor
                // Each time step ascends/descends elevators 1 floor
                var floor = Floors.First(x => x.Id == callToFloor);
                floor.WaitingPassengers = passengerCount;
                elevatorOrchestrator.TimeStep(callToFloor, destinationFloor, false);
            }
            else
            {
                Console.WriteLine();
                elevatorOrchestrator.TimeStep(0, 0, true);
            }
            // TODO: While an elevator is in motion, it's inaccessible to a new pickup
            // BONUS: If we have time, implement a queue for elevators

            // TODO: Take as many passengers as the weight limit allows, any outstanding passengers
            // will wait on the floor and will automatically call the next, closest available elevator

            // There will be a constraint here that if all elevators are in motion, then we cannot 
            // successfully call a new elevator, we must wait for one to complete its dropoff.
        }
    }
}
