leagueRankingApp.controller('AddGameController', function ($scope, $ionicSideMenuDelegate, $location, myConfig, $firebaseObject, $ionicLoading, $firebaseArray, $state) {
  var gamedatesList = $firebaseArray(firebase.database().ref('gamedates'));
  var playersList = $firebaseArray(firebase.database().ref('players'));
  var gamesList = $firebaseArray(firebase.database().ref('games'));

  $scope.players = [];
  $scope.player;
  $scope.gamedates = [];
  $scope.gamedate = {};
  $scope.select = {};
  $scope.possibleScores = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11];
  $scope.dbgames = [];
  $scope.games = [{}, {}, {}];
  $scope.gamesSaved = 0;

  $ionicLoading.show({ template: '<ion-spinner class="spinner-calm"></ion-spinner>' });

  $scope.saveGame = function() {
    $ionicLoading.show({ template: '<ion-spinner class="spinner-calm"></ion-spinner>' });
    //add game
    //gamedateid, player id, team scores
    gamesList.$add($scope.games[0]).then(function(res) {$scope.gamesSaved++; console.log("added game 1");});
    gamesList.$add($scope.games[1]).then(function(res) {$scope.gamesSaved++; console.log("added game 2");});
    gamesList.$add($scope.games[2]).then(function(res) {$scope.gamesSaved++; console.log("added game 3");});

    console.log($scope.games);
  }

  $scope.$watch("gamesSaved",
        function handleFooChange( newValue, oldValue ) {
          if(newValue == 3)
            $state.go($state.current, {}, {reload: true});
            //$window.location.reload();
        }
  );

  playersList.$loaded()
    .then(function (data) {
      console.log("loaded playerList:", data);
      $ionicLoading.hide();
      $scope.players = data;
    })
    .catch(function (error) {
      console.error("Error:", error);
    });
  gamedatesList.$loaded()
    .then(function (data) {
      console.log("loaded gamedatesList:", data);
      $scope.gamedates = data;
      $scope.select.dateSelected = String($scope.gamedates[0].dateval);
    })
    .catch(function (error) {
      console.error("Error:", error);
    });
  gamesList.$loaded()
    .then(function (data) {
      console.log("loaded gamesList:", data);
      $scope.dbgames = data;
    })
    .catch(function (error) {
      console.error("Error:", error);
    });

    $scope.$watch("players",
        function handleFooChange( newValue, oldValue ) {
            console.log( "players:", newValue );
            if(_.where(newValue, {checked: true}).length == 4 && _.where(_.toArray($scope.gamedates), {dateval: parseInt($scope.select.dateSelected)}).length > 0) {
              var _gamedateId = _.where(_.toArray($scope.gamedates), {dateval: parseInt($scope.select.dateSelected)})[0].$id;
              var _players = _.where(newValue, {checked: true});
              //create games..maybe a better way?
              $scope.games[0].gamedateId = _gamedateId;
              $scope.games[0].team1Player1Id = _players[0].$id;
              $scope.games[0].team1Player2Id = _players[1].$id;
              $scope.games[0].team2Player1Id = _players[2].$id;
              $scope.games[0].team2Player2Id = _players[3].$id;
              $scope.games[0].team1Score = '0';
              $scope.games[0].team2Score = '0';
              
              $scope.games[1].gamedateId = _gamedateId;
              $scope.games[1].team1Player1Id = _players[0].$id;
              $scope.games[1].team1Player2Id = _players[2].$id;
              $scope.games[1].team2Player1Id = _players[1].$id;
              $scope.games[1].team2Player2Id = _players[3].$id;
              $scope.games[1].team1Score = '0';
              $scope.games[1].team2Score = '0';
              
              $scope.games[2].gamedateId = _gamedateId;
              $scope.games[2].team1Player1Id = _players[0].$id;
              $scope.games[2].team1Player2Id = _players[3].$id;
              $scope.games[2].team2Player1Id = _players[1].$id;
              $scope.games[2].team2Player2Id = _players[2].$id;
              $scope.games[2].team1Score = '0';
              $scope.games[2].team2Score = '0';
            }
        }
    , true);

});