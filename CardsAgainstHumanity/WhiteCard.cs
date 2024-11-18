using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAgainstHumanity
{
    internal class WhiteCard : ICard
    {
        private string _contents;
        private bool _isInDeck;

        string ICard.Contents { get { return _contents; } set { _contents = value; } }
        bool ICard.IsInDeck { get { return _isInDeck; } set { _isInDeck = value; } }

        public WhiteCard(bool isInDeck, string contents)
        {
            _contents = contents;
            _isInDeck = isInDeck;
        }

    }
}
