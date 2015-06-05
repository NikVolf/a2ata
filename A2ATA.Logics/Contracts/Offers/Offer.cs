namespace A2ATA.Logics.Contracts.Offers
{
    public class Offer
    {
        public string Id { get; set; }

        public string SourceActorId { get; set; }

        public Clause[] SourcePartyClauses { get; set; }

        public Party[] Parties { get; set; }

    }
}
