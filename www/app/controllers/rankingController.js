leagueRankingApp.controller('RankingController', function ($scope, $ionicSideMenuDelegate, $location, myConfig, $firebaseObject, $ionicLoading, $firebaseArray) {
    var playersList = $firebaseArray(firebase.database().ref('players'));
    var gamesList = $firebaseArray(firebase.database().ref('games'));
    var gamedatesList = $firebaseArray(firebase.database().ref('gamedates'));

    $scope.players;
    $scope.dbgames;
    $scope.gamedates;

    $ionicLoading.show({ template: '<ion-spinner class="spinner-calm"></ion-spinner>' });


    $scope.saveRankingImage = function() {
      html2canvas(document.body, {
            onrendered: function(canvas) {
                theCanvas = canvas;
                canvas.toBlob(function(blob) {
                    console.log(blob);
                    var url = window.URL.createObjectURL(blob);

                    var a = document.createElement("a");
                    document.body.appendChild(a);
                    a.style = "display: none";
                    a.href = url;
                    a.download = "q.png";
                    a.click();
                    window.URL.revokeObjectURL(url);
                    //saveAs(blob, "Dashboard.png"); 
                });
            }
        });
    }

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
  gamedatesList.$loaded()
    .then(function (data) {
      console.log("loaded gamedatesList:", data);
      $scope.gamedates = data;
    })
    .catch(function (error) {
      console.error("Error:", error);
    });

    function createRanking() {
      _.each($scope.players, function(g) {g.points = 0; g.matchesPlayed = 0; g.attendancePoints = 0; g.goalsScored = 0; g.amountWon = 0; g.amountDraw = 0; g.amountLose = 0;});

      _.each($scope.dbgames, function(g) {
        //console.log(g);
        var _team1Player1 = _.findWhere($scope.players, {$id: g.team1Player1Id});
        var _team1Player2 = _.findWhere($scope.players, {$id: g.team1Player2Id});
        var _team2Player1 = _.findWhere($scope.players, {$id: g.team2Player1Id});
        var _team2Player2 = _.findWhere($scope.players, {$id: g.team2Player2Id});
        
        _team1Player1.matchesPlayed++;
        _team1Player2.matchesPlayed++;
        _team2Player1.matchesPlayed++;
        _team2Player2.matchesPlayed++;

        if(g.team1Score == 11) {
          _team1Player1.points += 3;
          _team1Player2.points += 3;
          _team1Player1.amountWon += 1;
          _team1Player2.amountWon += 1;
          _team2Player1.amountLose += 1;
          _team2Player2.amountLose += 1;
        } else if (g.team1Score == 10) {
          _team1Player1.points += 1;
          _team1Player2.points += 1;
          _team2Player1.points += 1;
          _team2Player2.points += 1;
          _team1Player1.amountDraw += 1;
          _team1Player2.amountDraw += 1;
          _team2Player1.amountDraw += 1;
          _team2Player2.amountDraw += 1;
        } else {
          _team2Player1.points += 3;
          _team2Player2.points += 3;
          _team2Player1.amountWon += 1;
          _team2Player2.amountWon += 1;
          _team1Player1.amountLose += 1;
          _team1Player2.amountLose += 1;
        }
        
        _team1Player1.goalsScored += g.team1Score;
        _team1Player2.goalsScored += g.team1Score;
        _team2Player1.goalsScored += g.team2Score;
        _team2Player2.goalsScored += g.team2Score;
      });

      _.each($scope.gamedates, function(g) {
        if(g.playerIdsPresent) {
          _.each($scope.players, function(p) {
            //for each gamedate, for each player, check if he was attended
            if(g.playerIdsPresent.indexOf(p.$id) > -1) {
              p.attendancePoints += 2;
              p.points += 2;
            }
          });
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