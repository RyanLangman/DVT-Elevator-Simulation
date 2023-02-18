using DVT.Elevator.ConsoleApp.Models.Interfaces;
using DVT.Elevator.ConsoleApp.Services;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Building
    {
        private readonly int _topFloor = 10;
        private readonly int _bottomFloor = 1;

        public List<IFloor> Floors { get; set; } = new List<IFloor>();
        public List<IElevator> Elevators { get; set; } = new List<IElevator>();

        private readonly IElevatorOrchestrator ElevatorOrchestrator;

        public bool Exists { get; set; } = true;

        public Building(List<IElevator> elevators, IElevatorOrchestrator elevatorOrchestrator)
        {
            Elevators = elevators;
            ElevatorOrchestrator = elevatorOrchestrator;

            // The building needs its floors constructed
            for (var i = 1; i <= _topFloor; i++)
            {
                Floors.Add(new Floor(i, Elevators));
            }

            // Since the floors have now been built, we inform the orchestrator of this 
            // construction so its aware of what floors exist
            elevatorOrchestrator.SetupFloors(Floors);
        }

        public void Exist()
        {
            ElevatorOrchestrator.ShowElevatorStatuses();

            Console.WriteLine("Press enter to continue or enter 's' to skip step, 'q' to quit.");

            var command = Console.ReadLine();

            if (command != null && command.ToLower() == "q")
            {
                Exists = false;
                return;
            }

            if (command == null || string.IsNullOrWhiteSpace(command))
            {
                var timeStepInstructions = GetTimeStepInstructions();

                var floor = Floors.First(x => x.Id == timeStepInstructions.PickupFloor);
                floor.SetWaitingPassengers(timeStepInstructions.NumberOfPeople);
                ElevatorOrchestrator.TimeStep(timeStepInstructions.PickupFloor, timeStepInstructions.DestinationFloor, false);
            }
            else
            {
                ElevatorOrchestrator.TimeStep(0, 0, true);
            }
        }

        private TimeStepInstructions GetTimeStepInstructions()
        {
            var timeStepInstructions = new TimeStepInstructions(_topFloor, _bottomFloor);

            var validFloorChosen = false;
            while (!validFloorChosen)
            {
                Console.WriteLine($"Choose floor to call elevator ({_bottomFloor}-{_topFloor}):");
                var userInput = Console.ReadLine();

                validFloorChosen = timeStepInstructions.SetPickupFloorNumber(userInput);
            }

            var validDestinationChosen = false;
            while (!validDestinationChosen)
            {
                Console.WriteLine($"Choose a destination floor ({_bottomFloor}-{_topFloor}):");
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
