var app = angular.module('myFishApp', ['ngRoute']);

app.factory('_', ['$window', function ($window) {

    var _ = $window._;

    delete $window._;

    return _;

}]).run(['_', function (_) { }]);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/board', {
            templateUrl: 'app/board.html',
            controller: 'BoardController',
            controllerAs: 'board',
        }).
        otherwise({
            redirectTo: '/board'
        });
  }]);

app.controller('BoardController', ['$http', '_', function ($http, _) {

    var fileA = 'a'.charCodeAt(0);

    function enumFiles() {
        return _.map(_.range(8), function (x) { return String.fromCharCode(fileA + x); });
    }

    function enumRanks() {
        return _.range(8, 0, -1);
    }

    function Piece(file, rank, piece, moves) {
        var self = this;
        self.type = piece.type;
        self.position = piece.position;
        self.moves = moves[self.position];
        self.canMove = function () {
            return self.moves;
        };
        self.canMoveTo = function (destination) {
            return self.moves && self.moves.indexOf(destination) >= 0;
        };
    }

    function Cell(file, rank, pieces, moves) {
        var self = this;

        self.rank = rank;
        self.file = file;
        self.isBlack = (rank % 2 + file.charCodeAt()) % 2 == 0;
        self.position = file + rank;

        var piece = _.find(pieces, function (x) {
            return x.position === self.position;
        });

        self.piece = piece ? new Piece(file, rank, piece, moves) : null;
    }

    function Row(rank, pieces, moves) {
        var self = this;

        self.rank = rank;
        self.columns = _.map(enumFiles(), function (file) {
            return new Cell(file, rank, pieces, moves);
        });
    }

    function makeRows(pieces, moves) {
        return _.map(enumRanks(), function (rank) {
            return new Row(rank, pieces, moves);
        });
    }

    var self = this;

    self.enumFiles = enumFiles();

    self.selectedPiece = null;

    function updateBoard(data) {
        self.turn = data.turn;
        self.rows = makeRows(data.pieces, data.moves);
    }

    function suggestMove(callback) {
        $http.get("api/suggestMove").success(callback).error(function (data) {
            alert(data.message);
        });
    }

    self.suggestMove = function () {
        suggestMove(function(data) {
            self.suggestedMove = data;
        });
    };

    function executeMove(move) {
        $http.post("api/move", move).success(function (data) {
            updateBoard(data);
            self.autoMove();
        }).error(function (data) {
            alert(data.message);
        });
    }

    self.autoMove = function () {
        if (self.mode === 'computer' || (self.turn === 'black' && self.mode === 'white') || (self.turn === 'white' && self.mode === 'black')) {
            suggestMove(executeMove);
        }
    };

    function movePiece(piece, destination) {
        var move = { piece: { type: piece.type, position: piece.position }, destination: destination };
        executeMove(move);
    }

    function selectPiece(piece) {
        if (self.selectedPiece) {
            self.selectedPiece.selected = false;
        }
        if (piece) {
            piece.selected = true;
        }
        self.selectedPiece = piece;
    }

    self.click = function (cell) {

        if (self.selectedPiece && self.selectedPiece.canMoveTo(cell.position)) {
            movePiece(self.selectedPiece, cell.position);
            selectPiece(null);
        }
        else {
            selectPiece(self.selectedPiece != cell.piece && cell.piece && cell.piece.canMove() ? cell.piece : null);
        }
    };

    self.mode = 'white';

    $http.get("api/init").success(function (data) {
        updateBoard(data);
    }).error(function (data) {
        alert(data.message);
    });
}]);

app.filter('piece', [function () {
    var map = {
        'K': String.fromCharCode(9812),
        'Q': String.fromCharCode(9813),
        'R': String.fromCharCode(9814),
        'B': String.fromCharCode(9815),
        'N': String.fromCharCode(9816),
        'P': String.fromCharCode(9817),
        'k': String.fromCharCode(9818),
        'q': String.fromCharCode(9819),
        'r': String.fromCharCode(9820),
        'b': String.fromCharCode(9821),
        'n': String.fromCharCode(9822),
        'p': String.fromCharCode(9823)
    };
    return function (piece) {
        var key = piece != null ? piece.type : null;
        return map[key] || null;
    };
}]);
