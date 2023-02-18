using DVT.Elevator.ConsoleApp.Enums;
using DVT.Elevator.ConsoleApp.Models.Interfaces;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Elevator: IElevator
    {
        public int Id { get; }

        private int _currentFloor;
        public int CurrentFloor 
        { 
            get { return _currentFloor; }
        }

        private int? _destinationFloor;
        public int? DestinationFloor
        {
            get { return _destinationFloor; }
        }

        private int? _pickupFloor;
        public int? PickupFloor
        {
            get { return _pickupFloor; }
        }

        public event EventHandler<EventArgs> OnPickupFloorArrivalEvent;

        public int WeightLimit { get; } = 7;

        private int _passengers;
        public int Passengers 
        {
            get { return _passengers; }
        }

        public Elevator(int id, int startingFloor)
        {
            Id = id;
            _currentFloor = startingFloor;
        }

        public ElevatorState State
        {
            get
            {
                if (PickupFloor != null)
                {
                    return PickupFloor > CurrentFloor ?
                        ElevatorState.Ascending :
                        ElevatorState.Descending;
                }
                else if (DestinationFloor != null)
                {

                    return DestinationFloor > CurrentFloor ?
                        ElevatorState.Ascending :
                        ElevatorState.Descending;
                }

                return ElevatorState.Idle;
            }
        }

        public int OnboardPassengers(int numberOfPassengers)
        {
            var capableOfOnboarding = numberOfPassengers > WeightLimit ? WeightLimit : numberOfPassengers;

            _passengers = capableOfOnboarding;

            return numberOfPassengers > WeightLimit ? numberOfPassengers - WeightLimit : 0;
        }

        public void MoveOneStep()
        {
            if (DestinationFloor == null && PickupFloor == null)
            {
                return;
            }

            if (CurrentFloor == PickupFloor)
            {
                OnPickupFloorArrivalEvent?.Invoke(this, EventArgs.Empty);
                _pickupFloor = null;

                return;
            }

            if (CurrentFloor == DestinationFloor && PickupFloor == null)
            {
                _destinationFloor = null;
                _passengers = 0;
                return;
            }

            if (State == ElevatorState.Ascending)
            {
                _currentFloor++;
            }
            else
            {
                _currentFloor--;
            }
        }

        public override string ToString()
        {
            switch (State)
            {
                case ElevatorState.Ascending:
                case ElevatorState.Descending:
                    if (PickupFloor != null)
                    {
                        if (CurrentFloor == PickupFloor)
                        {
                            return $"E-{Id} is boarding passengers on F-{CurrentFloor} then headed to F-{DestinationFloor} for dropoff.";
                        }

                        return $"E-{Id} is on F-{CurrentFloor}, headed to F-{PickupFloor} for pickup then F-{DestinationFloor} for dropoff.";
                    }

                    if (CurrentFloor == DestinationFloor)
                    {
                        return $"E-{Id} dropping off {_passengers} passengers on F-{CurrentFloor}.";
                    }

                    return $"E-{Id} is on F-{CurrentFloor}, headed to F-{DestinationFloor} to dropoff {_passengers} passengers.";

                case ElevatorState.Idle:
                default:
                    return $"E-{Id} is idle on F-{CurrentFloor}.";
            }
        }

        public void SetFloors(int pickupFloor, int destinationFloor)
        {
            if (pickupFloor == destinationFloor)
            {
                throw new Exception("Pickup and destination floors cannot be the same.");
            }

            _pickupFloor = pickupFloor;
            _destinationFloor = destinationFloor;
        }
    }
}
