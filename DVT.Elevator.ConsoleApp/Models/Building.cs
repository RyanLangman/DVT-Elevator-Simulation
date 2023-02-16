using DVT.Elevator.ConsoleApp.Services;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Building
    {
        private readonly int _elevatorCount = 4;
        private readonly int _floorCount = 10;

        public List<Floor> Floors { get; set; } = new List<Floor>();

        public List<Elevator> Elevators { get; set; } = new List<Elevator>();

        private readonly IElevatorOrchestrator elevatorOrchestrator;

        public bool Exists { get; set; } = true;

        public Building()
        {
            for (var i = 1; i <= _elevatorCount; i++)
            {
                // Elevators will start at floor 1
                Elevators.Add(new Elevator(i));
            }

            for (var i = 1; i <= _floorCount; i++)
            {
                Floors.Add(new Floor(i, Elevators));
            }

            elevatorOrchestrator = new ElevatorOrchestrator(Elevators, Floors);
        }

        public void Exist()
        {
            // TODO: Validate input
            elevatorOrchestrator.ShowElevatorStatuses();

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

                var floor = Floors.First(x => x.Id == callToFloor);
                floor.WaitingPassengers = passengerCount;
                elevatorOrchestrator.TimeStep(callToFloor, destinationFloor, false);
            }
            else
            {
                Console.WriteLine();
                elevatorOrchestrator.TimeStep(0, 0, true);
            }
            // BONUS: If we have time, implement a queue for elevators

            // TODO: Take as many passengers as the weight limit allows, any outstanding passengers
            // will wait on the floor and will automatically call the next, closest available elevator
        }
    }
}
