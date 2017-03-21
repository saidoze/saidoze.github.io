leagueRankingApp.controller('RankingController', function ($scope, $timeout, $ionicSideMenuDelegate, $location, myConfig, $firebaseObject, $ionicLoading, $firebaseArray, $ionicActionSheet) {
    var playersList = $firebaseArray(firebase.database().ref('players'));
    var gamesList = $firebaseArray(firebase.database().ref('games'));
    var gamedatesList = $firebaseArray(firebase.database().ref('gamedates'));

    $scope.players;
    $scope.dbgames;
    $scope.gamedates;

    $ionicLoading.show({ template: '<ion-spinner class="spinner-calm"></ion-spinner>' });


    $scope.saveRankingImage = function() {
        // Show the action sheet
        var hideSheet = $ionicActionSheet.show({
          buttons: [
            { text: 'Export picture' }
          ],
          //destructiveText: 'Delete',
          titleText: 'Export ranking',
          cancelText: 'Cancel',
          cancel: function() {
                // add cancel code..
              },
          buttonClicked: function(index) {
            exportImage();
            return true;
          }
        });

        // For example's sake, hide the sheet after two seconds
        $timeout(function() {
          hideSheet();
        }, 5000);

      };

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

    function exportImage() {
      var c=document.getElementById("myCanvas");
      var ctx=c.getContext("2d");
      ctx.font="16px Arial";

      //titlerow
      ctx.fillText('MP',200,35);
      ctx.fillText('W',275,35);
      ctx.fillText('D',350,35);
      ctx.fillText('L',425,35);
      ctx.fillText('A',500,35);
      ctx.fillText('G',575,35);
      ctx.fillText('P',650,35);
      ctx.beginPath(); ctx.lineWidth=1; ctx.moveTo(5,45); ctx.lineTo(700,45); ctx.stroke();

      var _index = 2; var _lineindex = 0;
      sortedPlayers = _.chain($scope.players).sortBy('name').reverse().sortBy('attendancePoints').sortBy('goalsScored').sortBy('points').reverse().value();
      _.each(sortedPlayers, function(p) {
        ctx.fillText((_index - 1) + ".",10,35 * _index);
        ctx.fillText(p.name,50,35 * _index);
        ctx.fillText(p.matchesPlayed,200,35 * _index);
        ctx.fillText(p.amountWon,275,35 * _index);
        ctx.fillText(p.amountDraw,350,35 * _index);
        ctx.fillText(p.amountLose,425,35 * _index);
        ctx.fillText(p.attendancePoints,500,35 * _index);
        ctx.fillText(p.goalsScored,575,35 * _index);
        ctx.fillText(p.points,650,35 * _index);
        ctx.beginPath(); ctx.lineWidth=0.5; ctx.moveTo(5,82 + (35 * _lineindex)); ctx.lineTo(700,82 + (35 * _lineindex)); ctx.stroke();

        _index++; _lineindex++;
      });

      c.toBlob(function(blob) {
          saveAs(blob, "Ranking.png");
      });
    }

});