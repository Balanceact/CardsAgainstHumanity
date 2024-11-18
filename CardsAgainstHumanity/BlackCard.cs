using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAgainstHumanity
{
    internal class BlackCard : ICard
    {
        private string _contents;
        private bool _isInDeck;
        private int _pick;

        string ICard.Contents { get => _contents; set => _contents = value; }
        bool ICard.IsInDeck { get { return _isInDeck; } set { _isInDeck = value; } }
        public int Pick { get { return _pick; } }


        public BlackCard(bool isInDeck, int pick, string contents)
        {
            _pick = pick;
            _isInDeck = isInDeck;
            _contents = contents;
        }

    }
}
