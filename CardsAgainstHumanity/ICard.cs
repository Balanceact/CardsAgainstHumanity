namespace CardsAgainstHumanity
{
    internal interface ICard
    {
        public string Contents { get; set; }
        public bool IsInDeck { get; set; }
    }
}