using System;

namespace assignment1
{
    class Program
    {
        const int boardLength = 8;
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Start();
        }
        void Start()
        {
            ChessPiece[,] chessboard = new ChessPiece[boardLength, boardLength];
            InitChessboard(chessboard);
            DisplayChessboard(chessboard);
            PlayChess(chessboard);
        }
        void InitChessboard(ChessPiece[,] chessboard)
        {
            for (int r = 0; r < chessboard.GetLength(0); r++)
            {
                for (int c = 0; c < chessboard.GetLength(1); c++)
                {
                    chessboard[r, c] = null;
                }
            }
            PutChessPieces(chessboard);
        }
        void DisplayChessboard(ChessPiece[,] chessboard)
        {
            for (int r = 0; r < chessboard.GetLength(0); r++)
            {
                int rowNumber = chessboard.GetLength(0) - r;
                Console.Write($"{rowNumber} ");
                for (int c = 0; c < chessboard.GetLength(1); c++)
                {
                    if ((r + c) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    DisplayChessPiece(chessboard[r, c]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }
        ChessPieceType[] order =
            { ChessPieceType.Rook,
            ChessPieceType.Knight,
            ChessPieceType.Bishop,
            ChessPieceType.Queen,
            ChessPieceType.King,
            ChessPieceType.Bishop,
            ChessPieceType.Knight,
            ChessPieceType.Rook};

        void PutChessPieces(ChessPiece[,] chessboard)
        {
            for (int r = 0; r < chessboard.GetLength(0); r++)
            {
                for (int c = 0; c < chessboard.GetLength(1); c++)
                {
                    ChessPiece piece = new ChessPiece();
                    switch (r)
                    {
                        case 1:
                            piece.color = ChessPieceColor.Black;
                            piece.type = ChessPieceType.Pawn;
                            break;
                        case 6:
                            piece.color = ChessPieceColor.White;
                            piece.type = ChessPieceType.Pawn;
                            break;
                        case 0:
                            piece.color = ChessPieceColor.Black;
                            piece.type = order[c];
                            break;
                        case 7:
                            piece.color = ChessPieceColor.White;
                            piece.type = order[c];
                            break;
                        default:
                            piece = null;
                            break;
                    }
                    chessboard[r, c] = piece;
                }
            }
        }
        void DisplayChessPiece(ChessPiece chessPiece)
        {
            if (chessPiece == null)
            {
                Console.Write("   ");
            }
            else
            {
                if (chessPiece.color.ToString() == "Black")
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                switch (chessPiece.type.ToString())
                {
                    case "Rook":
                        Console.Write(" r ");
                        break;
                    case "Knight":
                        Console.Write(" k ");
                        break;
                    case "Bishop":
                        Console.Write(" b ");
                        break;
                    case "Queen":
                        Console.Write(" Q ");
                        break;
                    case "King":
                        Console.Write(" K ");
                        break;
                    default:
                        Console.Write(" p ");
                        break;
                }
            }
        }
        void PlayChess(ChessPiece[,] chessboard)
        {
            string input = ReadString("Enter move (e.g. a2 a3): ");
            while (input != "stop")
            {
                try
                {
                    string[] positions = input.Split(' ');
                    Position from = new Position();
                    from = from.String2Position(positions[0]);
                    Position to = new Position();
                    to = to.String2Position(positions[0]);
                    if (from != null && to != null)
                    {
                        Console.WriteLine($"move from {positions[0]} to {positions[1]}\n");
                        DoMove(chessboard, from, to);
                        DisplayChessboard(chessboard);
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{e.Message}\n");
                    Console.ResetColor();
                }
                input = ReadString("Enter move (e.g. a2 a3): ");
            }
        }
        void DoMove(ChessPiece[,] chessboard, Position from, Position to)
        {
            CheckMove(chessboard, from, to);
            chessboard[to.row, to.column] = chessboard[from.row, from.column];
            chessboard[from.row, from.column] = null;
        }
        void CheckMove(ChessPiece[,] chessboard, Position from, Position to)
        {
            int hor = Math.Abs(to.column - from.column);
            int ver = Math.Abs(to.row - from.row);
            Console.WriteLine($"{to}{from}");
            if (chessboard[from.row, from.column] == null)
            {
                throw new Exception("No chess piece at from-position");
            }
            else if (chessboard[to.row, to.column] != null && chessboard[to.row, to.column].color == chessboard[from.row, from.column].color)
            {
                throw new Exception("Can not take a chess piece of same color");
            }
            else if (hor == 0 && ver == 0)
            {
                throw new Exception("No movement");
            }
            switch (chessboard[from.row, from.column].type)
            {
                case ChessPieceType.Pawn:
                    if (hor != 0 && ver != 1)
                    {
                        throw new Exception("Invalid move for chess piece Pawn");
                    }
                    break;
                case ChessPieceType.Rook:
                    if (hor * ver != 0)
                    {
                        throw new Exception("Invalid move for chess piece Rook");
                    }    
                    break;
                case ChessPieceType.Knight:
                    if (hor * ver != 2)
                    {
                        throw new Exception("Invalid move for chess piece Knight");
                    }
                    break;
                case ChessPieceType.Bishop:
                    if (hor != ver)
                    {
                        throw new Exception("Invalid move for chess piece Bishop");
                    }
                    break;
                case ChessPieceType.King:
                    if (hor != 1 || ver != 1)
                    {
                        throw new Exception("Invalid move for chess piece King");
                    }
                    break;
                case ChessPieceType.Queen:
                    if (hor * ver != 0 || hor != ver)
                    {
                        throw new Exception("Invalid move for chess piece Queen");
                    }
                    break;
            }
        }



        string ReadString(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }
    }
}
