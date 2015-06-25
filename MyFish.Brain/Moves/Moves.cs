using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyFish.Brain.Moves
{
    public static class Moves
    {
        private static readonly Dictionary<char, Func<Position, Board, bool, IEnumerable<Move>>> Factory = new Dictionary<char, Func<Position, Board, bool, IEnumerable<Move>>>
        {
            {'r', (position, board, avoidCheck) => new RookMoves(position, board, avoidCheck)},
            {'n', (position, board, avoidCheck) => new KnightMoves(position, board, avoidCheck)},
            {'b', (position, board, avoidCheck) => new BishopMoves(position, board, avoidCheck)},
            {'q', (position, board, avoidCheck) => new QueenMoves(position, board, avoidCheck)},
            {'k', (position, board, avoidCheck) => new KingMoves(position, board, avoidCheck)},
            {'p', (position, board, avoidCheck) => new PawnMoves(position, board, avoidCheck)},
        };

        private static IEnumerable<Move> MovesFor(this Board board, Piece piece, bool avoidCheck)
        {
            return Factory[piece.Type](piece.Position, board, avoidCheck);
            //return board.Memoize(Factory[piece.Type])(piece.Position, board, avoidCheck);
        }

        public static IEnumerable<Move> MovesFor(this Board board, Piece piece)
        {
            return board.MovesFor(piece, true);
        }

        public static IEnumerable<Position> PositionsCoveredBy(this Board board, Piece piece)
        {
            return board.MovesFor(piece, false).Select(x => x.Destination);
        }

        private static IEnumerable<Move> MovesFor(this Board board, Color color, bool avoidCheck)
        {
            var opponentPieces = board.Pieces.Where(x => x.Color == color);

            return opponentPieces.SelectMany(x => board.MovesFor(x, avoidCheck));
        }

        public static IEnumerable<Move> MovesFor(this Board board, Color color)
        {
            return board.MovesFor(color, true);
        }

        public static IEnumerable<Position> PositionsCoveredBy(this Board board, Color color)
        {
            return board.MovesFor(color, false).Select(x => x.Destination).Distinct();
        }
    }
}