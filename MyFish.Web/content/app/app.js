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

    function Piece(piece) {
        
    }

    function Cell(file, rank, pieces) {
        var self = this;

        self.file = file;
        self.piece = pieces[file.charCodeAt(0) - fileA];
        self.isBlack =  (rank % 2 + file.charCodeAt()) % 2 == 0;
    }

    function Rank(rank, pieces) {
        var self = this;

        self.number = rank;
        self.files = _.map(enumFiles(), function(file) {
            return new Cell(file, rank, pieces);
        });
    }


    var self = this;

    self.enumFiles = enumFiles();

    self.pieces = null;

    self.selectedPiece = null;

    self.selectPiece = function(piece) {

        if (self.selectedPiece) {
            self.selectedPiece.selected = false;
        }
        if (piece) {
            piece.selected = true;
        }
        self.selectedPiece = piece;
    };

    $http.get("api/init").success(function (data) {
        self.pieces = data.pieces;

        self.ranks = _.map(enumRanks(), function(rank) {
            return new Rank(rank, data.pieces[rank - 1]);
        });
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
        return map[key] || key;
    };
}]);
