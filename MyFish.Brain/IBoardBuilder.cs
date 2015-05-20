using System.Collections.Generic;

namespace MyFish.Brain
{
    public interface IBoardBuilder
    {
        Board Build(IEnumerable<Piece> pieces, Color turn);
    }
}