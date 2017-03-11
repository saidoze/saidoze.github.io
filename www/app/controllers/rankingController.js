leagueRankingApp.controller('RankingController', function ($scope, $ionicSideMenuDelegate, $location, myConfig, $firebaseObject, $ionicLoading, $firebaseArray) {
    var playersList = $firebaseArray(firebase.database().ref('players'));
    var gamesList = $firebaseArray(firebase.database().ref('games'));

    $scope.players;
    $scope.dbgames;

    $ionicLoading.show({ template: '<ion-spinner class="spinner-calm"></ion-spinner>' });

    playersList.$loaded()
    .then(function (data) {
      console.log("loaded playerList:", data);
      $ionicLoading.hide();
      $scope.players = data;
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

    function createRanking() {
      _.each($scope.players, function(g) {g.points = 0; g.matchesPlayed = 0;});

      _.each($scope.dbgames, function(g) {
        console.log(g);
        console.log(g.team1Player1Id);
        var _team1Player1 = _.findWhere($scope.players, {$id: g.team1Player1Id});
        var _team1Player2 = _.findWhere($scope.players, {$id: g.team1Player2Id});
        var _team2Player1 = _.findWhere($scope.players, {$id: g.team2Player1Id});
        var _team2Player2 = _.findWhere($scope.players, {$id: g.team2Player2Id});
        console.log(_team1Player1);
        _team1Player1.matchesPlayed++;
        _team1Player2.matchesPlayed++;
        _team2Player1.matchesPlayed++;
        _team2Player2.matchesPlayed++;

        if(g.team1Score == 11) {
          _team1Player1.points += 3;
          _team1Player2.points += 3;
        } else if (g.team1Score == 10) {
          _team1Player1.points += 1;
          _team1Player2.points += 1;
          _team2Player1.points += 1;
          _team2Player2.points += 1;
        } else {
          _team2Player1.points += 3;
          _team2Player2.points += 3;
        }


      });

    }

    $scope.$watchGroup(['players', 'dbgames'], function(newValues, oldValues, scope) {
      if(newValues[0] && newValues[1]) {
        if(newValues[0].length > 0 && newValues[1].length > 0) {
          console.log("newValues:", newValues);
          createRanking();
        }
      }
    });


});