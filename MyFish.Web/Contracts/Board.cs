using System.Collections.Generic;

namespace MyFish.Web.Contracts
{
    public class Board
    {
        public string Turn { get; set; }
        public Piece[] Pieces { get; set; }
        public Dictionary<string, string[]> Moves { get; set; }
    }
}