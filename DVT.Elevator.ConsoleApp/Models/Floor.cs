using DVT.Elevator.ConsoleApp.Enums;
using DVT.Elevator.ConsoleApp.Services;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Floor
    {
        public int Id { get; set; }

        public List<Passenger> WaitingPassengers { get; set; } = new List<Passenger>();

        private readonly IElevatorCaller elevatorControlPanel;

        public Floor(int id)
        {
            Id = id;
            elevatorControlPanel = new ElevatorCaller();
        }

        void CallElevator(ElevatorDirection direction)
        {
            elevatorControlPanel.CallElevator(Id, direction);
        }
    }
}
