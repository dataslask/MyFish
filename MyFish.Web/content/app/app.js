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
    
    function Piece(file, rank, piece) {
        var self = this;
        self.file = file;
        self.rank = rank;
        self.type = piece.type;
        self.color = piece.color;
    }

    function Cell(file, rank, pieces) {
        var self = this;

        self.file = file;
        self.rank = rank;

        var piece = _.find(pieces, function(x) {
            return x.position.file === file && x.position.rank === rank;
        });
        self.piece = piece;
        self.isBlack =  (rank % 2 + file.charCodeAt()) % 2 == 0;
    }

    function Row(rank, pieces) {
        var self = this;

        self.rank = rank;
        self.columns = _.map(enumFiles(), function(file) {
            return new Cell(file, rank, pieces);
        });
    }

    function makeRows(data) {
        return _.map(enumRanks(), function(rank) {
            return new Row(rank, data.pieces);
        });
    }

    var self = this;

    self.enumFiles = enumFiles();

    self.selectedPiece = null;

    function movePiece(piece, destination) {
        var moveCommand = { piece: piece, destination: destination };
        $http.post("api/move", moveCommand).success(function(data, status) {
            self.rows = makeRows(data);
        }).error(function(data) {
            alert(data.message);
        });
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

    self.click = function(cell) {

        if (self.selectedPiece && (!cell.piece || cell.piece.color !== self.selectedPiece.color)) {
            movePiece(self.selectedPiece, cell);
            selectPiece(null);
        }
        else {
            selectPiece(self.selectedPiece != cell.piece ? cell.piece : null);
        }
    };

    $http.get("api/init").success(function (data) {
        self.rows = makeRows(data);
    });
}]);

app.filter('piece', [function () {
    var map = {
        'white-king': String.fromCharCode(9812),
        'white-queen': String.fromCharCode(9813),
        'white-rook': String.fromCharCode(9814),
        'white-bishop': String.fromCharCode(9815),
        'white-knight': String.fromCharCode(9816),
        'white-pawn': String.fromCharCode(9817),
        'black-king': String.fromCharCode(9818),
        'black-queen': String.fromCharCode(9819),
        'black-rook': String.fromCharCode(9820),
        'black-bishop': String.fromCharCode(9821),
        'black-knight': String.fromCharCode(9822),
        'black-pawn': String.fromCharCode(9823)
    };
    return function (piece) {
        var key = piece != null ? piece.color + '-' + piece.type : null;
        return map[key] || null;
    };
}]);
