using DVT.Elevator.ConsoleApp.Enums;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Elevator
    {
        public int Id { get; }

        public int _currentFloor;
        public int CurrentFloor 
        { 
            get { return _currentFloor; }
        }

        public int? _destinationFloor;
        public int? DestinationFloor
        {
            get { return _destinationFloor; }
        }

        public int? _pickupFloor;
        public int? PickupFloor
        {
            get { return _pickupFloor; }
        }

        public event EventHandler<EventArgs> OnPickupFloorArrivalEvent;

        public int WeightLimit { get; } = 7;

        private int Passengers { get; set; }

        public Elevator(int id)
        {
            Id = id;
            _currentFloor = 1;
        }

        public ElevatorDirection Direction
        {
            get
            {
                if (PickupFloor != null)
                {
                    return PickupFloor > CurrentFloor ?
                        ElevatorDirection.Ascending :
                        ElevatorDirection.Descending;
                }
                else if (DestinationFloor != null)
                {

                    return DestinationFloor > CurrentFloor ?
                        ElevatorDirection.Ascending :
                        ElevatorDirection.Descending;
                }

                return ElevatorDirection.Idle;
            }
        }

        public int OnboardPassengers(int numberOfPassengers)
        {
            var capableOfOnboarding = numberOfPassengers > WeightLimit ? WeightLimit : numberOfPassengers;

            Passengers = capableOfOnboarding;

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
                Passengers = 0;
                return;
            }

            if (Direction == ElevatorDirection.Ascending)
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
            switch (Direction)
            {
                case ElevatorDirection.Ascending:
                case ElevatorDirection.Descending:
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
                        return $"E-{Id} dropping off {Passengers} passengers on F-{CurrentFloor}.";
                    }

                    return $"E-{Id} is on F-{CurrentFloor}, headed to F-{DestinationFloor} to dropoff {Passengers} passengers.";

                case ElevatorDirection.Idle:
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
