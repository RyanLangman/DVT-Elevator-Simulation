namespace DVT.Elevator.ConsoleApp.Models.Interfaces
{
    public interface IFloor
    {
        int Id { get; }

        int WaitingPassengers { get; }

        event EventHandler<int> OnRequestNewPickupEvent;

        void SetWaitingPassengers(int waitingPassengers);
    }
}
