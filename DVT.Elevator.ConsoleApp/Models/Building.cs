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
            elevatorOrchestrator.ShowElevatorStatuses();

            Console.WriteLine();
            Console.WriteLine("Skip instructions this step? (y/n)");
            if (Console.ReadLine() != "y")
            {
                var timeStepInstructions = GetTimeStepInstructions();

                var floor = Floors.First(x => x.Id == timeStepInstructions.PickupFloor);
                floor.WaitingPassengers = timeStepInstructions.NumberOfPeople;
                elevatorOrchestrator.TimeStep(timeStepInstructions.PickupFloor, timeStepInstructions.DestinationFloor, false);
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

        private TimeStepInstructions GetTimeStepInstructions()
        {
            var timeStepInstructions = new TimeStepInstructions(_floorCount, 1);

            var validFloorChosen = false;
            while (!validFloorChosen)
            {
                Console.WriteLine($"Choose floor to call elevator (1-{_floorCount}):");
                var userInput = Console.ReadLine();

                validFloorChosen = timeStepInstructions.SetPickupFloorNumber(userInput);
            }

            var validDestinationChosen = false;
            while (!validDestinationChosen)
            {
                Console.WriteLine($"Choose a destination floor (1-{_floorCount}):");
                var userInput = Console.ReadLine();

                validDestinationChosen = timeStepInstructions.SetDestinationFloorNumber(userInput);
            }

            var validNumberOfPeople = false;
            while (!validNumberOfPeople)
            {
                Console.WriteLine("How many people need a lift?");
                var userInput = Console.ReadLine();

                validNumberOfPeople = timeStepInstructions.SetAwaitingPassengers(userInput);
            }

            return timeStepInstructions;
        }
    }
}
