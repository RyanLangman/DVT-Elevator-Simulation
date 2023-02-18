using DVT.Elevator.ConsoleApp.Services;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Building
    {
        private readonly int _elevatorCount = 4;
        private readonly int _topFloor = 10;
        private readonly int _bottomFloor = 1;

        public List<Floor> Floors { get; set; } = new List<Floor>();

        public List<Elevator> Elevators { get; set; } = new List<Elevator>();

        private readonly IElevatorOrchestrator elevatorOrchestrator;

        public bool Exists { get; set; } = true;

        public Building()
        {
            for (var i = 1; i <= _elevatorCount; i++)
            {
                Elevators.Add(new Elevator(i, _bottomFloor));
            }

            for (var i = _bottomFloor; i <= _topFloor; i++)
            {
                Floors.Add(new Floor(i, Elevators));
            }

            elevatorOrchestrator = new ElevatorOrchestrator(Elevators, Floors);
        }

        public void Exist()
        {
            elevatorOrchestrator.ShowElevatorStatuses();

            Console.WriteLine("Skip instructions this step? (y/n)");

            if (Console.ReadLine() != "y")
            {
                var timeStepInstructions = GetTimeStepInstructions();

                var floor = Floors.First(x => x.Id == timeStepInstructions.PickupFloor);
                floor.SetWaitingPassengers(timeStepInstructions.NumberOfPeople);
                elevatorOrchestrator.TimeStep(timeStepInstructions.PickupFloor, timeStepInstructions.DestinationFloor, false);
            }
            else
            {
                Console.WriteLine();
                elevatorOrchestrator.TimeStep(0, 0, true);
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
