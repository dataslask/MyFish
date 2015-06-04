using System;
using System.Collections.Generic;

namespace MyFish.Brain.Moves
{
    public static class Moves
    {
        private static readonly Dictionary<char, Func<Position, Board, bool, IEnumerable<Move>>> Factory = new Dictionary<char, Func<Position, Board, bool, IEnumerable<Move>>>
        {
            {'r', (position, board, attacksOnly) => new RookMoves(position, board)},
            {'n', (position, board, attacksOnly) => new KnightMoves(position, board)},
            {'b', (position, board, attacksOnly) => new BishopMoves(position, board)},
            {'q', (position, board, attacksOnly) => new QueenMoves(position, board)},
            {'k', (position, board, attacksOnly) => new KingMoves(position, board, !attacksOnly)},
            {'p', (position, board, attacksOnly) => new PawnMoves(position, board, attacksOnly)},
        };

        public static IEnumerable<Move> For(Piece piece, Board board, bool attacksOnly)
        {
            if (!Factory.ContainsKey(piece.Type))
            {
                throw new ArgumentOutOfRangeException("piece", string.Format("Don't know how to find moves for {0}", piece));
            }
            return Factory[piece.Type](piece.Position, board, attacksOnly);
        }
    }
}