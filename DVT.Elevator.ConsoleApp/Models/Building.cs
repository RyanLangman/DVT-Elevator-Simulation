namespace DVT.Elevator.ConsoleApp.Models
{
    public class Building
    {
        public List<Floor> Floors { get; set; } = new List<Floor>();

        public List<Elevator> Elevators { get; set; } = new List<Elevator>(); 

        public Building()
        {
            for (var i = 1; i <= 10; i++)
            {
                Floors.Add(new Floor(i));

                if (i <= 5)
                {
                    Elevators.Add(new Elevator(i));
                }
            }
        }

        public void Exist()
        {
            // Building continues to exist, elevators are operational and people
            // are interacting with them

            // TODO: Prompt for which floor to call an elevator to, how many people
            // are on the floor, and their destination floor

            // TODO: Orchestrator will receive inputs and allocate an elevator to that floor
            // Each time step ascends/descends elevators 1 floor

            // TODO: While an elevator is in motion, it's inaccessible to a new pickup
            // BONUS: If we have time, implement a queue for elevators

            // TODO: Take as many passengers as the weight limit allows, any outstanding passengers
            // will wait on the floor and will automatically call the next, closest available elevator

            // There will be a constraint here that if all elevators are in motion, then we cannot 
            // successfully call a new elevator, we must wait for one to complete its dropoff.
        }
    }
}
