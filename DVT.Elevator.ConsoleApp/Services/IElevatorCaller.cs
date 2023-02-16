using DVT.Elevator.ConsoleApp.Enums;

namespace DVT.Elevator.ConsoleApp.Services
{
    public interface IElevatorCaller
    {
        void CallElevator(int floorNumber, ElevatorDirection direction);
    }
}
