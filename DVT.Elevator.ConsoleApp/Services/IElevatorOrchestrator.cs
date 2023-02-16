namespace DVT.Elevator.ConsoleApp.Services
{
    public interface IElevatorOrchestrator
    {
        void ShowElevatorStatuses();

        void TimeStep(int callToFloor, int destinationFloor, bool skipInput);
    }
}
