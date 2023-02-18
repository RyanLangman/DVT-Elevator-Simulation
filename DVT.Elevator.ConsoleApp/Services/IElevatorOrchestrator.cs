using DVT.Elevator.ConsoleApp.Models.Interfaces;

namespace DVT.Elevator.ConsoleApp.Services
{
    public interface IElevatorOrchestrator
    {
        void SetupFloors(List<IFloor> floors);

        void TimeStep(int pickupFloor, int destinationFloor, bool skipInput = false);

        void ShowElevatorStatuses();
    }
}
