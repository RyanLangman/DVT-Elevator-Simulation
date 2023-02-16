namespace DVT.Elevator.ConsoleApp.Services
{
    public interface IElevatorOrchestrator
    {
        Models.Elevator GetElevatorStatus(int id);

        void Pickup(int floorId, int destinationFloor);
    }
}
