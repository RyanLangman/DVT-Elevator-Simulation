using DVT.Elevator.ConsoleApp.Enums;

namespace DVT.Elevator.ConsoleApp.Models.Interfaces
{
    public interface IElevator
    {
        int Id { get; }

        int CurrentFloor { get; }

        int? DestinationFloor { get; }

        int? PickupFloor { get; }

        int Passengers { get; }

        event EventHandler<EventArgs> OnPickupFloorArrivalEvent;

        ElevatorState State { get; }

        int OnboardPassengers(int numberOfPassengers);

        void MoveOneStep();

        void SetFloors(int pickupFloor, int destinationFloor);
    }
}
