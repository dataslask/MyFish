using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFish.Brain
{
    public class Fen
    {
        //http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation

        public const string InitialBoard = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        public static Board Init(string fen = InitialBoard)
        {
            var fields = GetFields(fen);

            var pieces = GetPieces(fields[0]);
            var turn = GetTurn(fields[1]);

            return Board.GetBuilder().Build(pieces, turn);
        }

        private static Color GetTurn(string turn)
        {
            if (turn != "w" && turn != "b")
            {
                throw new ArgumentException(string.Format("Invalid turn: {0}", turn));
            }
            return turn == "w" ? Color.White : Color.Black;
        }

        private static IEnumerable<Piece> GetPieces(string placements)
        {
            var rankNumber = 1;

            foreach (var rank in GetRanks(placements))
            {
                var file = 'a';

                foreach (var piece in rank)
                {
                    if (IsEmpty(piece))
                    {
                        file += GetEmptyPieceCount(piece);
                    }
                    else
                    {
                        yield return Create(piece, file, rankNumber);
                        
                        file++;
                    }
                }
                rankNumber++;
            }
        }

        private static Piece Create(char piece, char file, int rankNumber)
        {
            return PieceFacory.Create(piece, new Position(file, rankNumber));
        }

        private static char GetEmptyPieceCount(char piece)
        {
            return (char)(piece - '0');
        }

        private static bool IsEmpty(char piece)
        {
            return piece <= '9';
        }

        private static IEnumerable<string> GetRanks(string placements)
        {
            var ranks = placements.Split('/').Reverse().ToArray();

            if (ranks.Length != 8)
            {
                throw new ArgumentException(string.Format("Wrong number of ranks in: {0}", placements));
            }
            return ranks;
        }

        private static string[] GetFields(string fen)
        {
            var fields = fen.Split(' ');

            if (fields.Length != 6)
            {
                throw new ArgumentException(string.Format("Wrong number of fields in: {0}", fen));
            }
            return fields;
        }
    }
}