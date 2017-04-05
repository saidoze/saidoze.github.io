leagueRankingApp.controller('CloseGamedateController', function ($stateParams, $scope, $ionicSideMenuDelegate, $location, myConfig, $firebaseObject, $ionicLoading, $firebaseArray, $state, $ionicHistory) {
  console.log($stateParams);
  //get all players
  //show them in a list with checkboxes -> default selected are the players that player in this date

  //save button sets gamedate as closed
  //and adds selected players to the gamedate -> ranking will add 2 points to these players

  var gamedatesList = $firebaseArray(firebase.database().ref('gamedates'));
  var playersList = $firebaseArray(firebase.database().ref('players'));
  var gamesList = $firebaseArray(firebase.database().ref('games'));

  $scope.gamedateId = $stateParams['gamedateId'];
  $scope.players = [];
  $scope.gamedates = [];
  $scope.games = [];

  $ionicLoading.show({ template: '<ion-spinner class="spinner-calm"></ion-spinner>' });

  $scope.closeGamedate = function() {
    //update gamedatesList
    var _gamedate = _.where($scope.gamedates, {$id: $scope.gamedateId});
    if(_gamedate.length > 0) {
      var _playersPresent = _.where($scope.players, {checked: true});
      _gamedate[0].playerIdsPresent = _.pluck(_playersPresent, '$id');
      _gamedate[0].isClosed = true;
      gamedatesList.$save(_gamedate[0]).then(function(ref) {
        $ionicHistory.nextViewOptions({
            disableBack: true
        });
        $state.go('ranking');
      });
    }
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
  gamedatesList.$loaded()
    .then(function (data) {
      console.log("loaded gamedatesList:", data);
      $scope.gamedates = data;
    })
    .catch(function (error) {
      console.error("Error:", error);
    });
  gamesList.$loaded()
    .then(function (data) {
      console.log("loaded gamesList:", data);
      $scope.dbgames = data;
      setCheckedPlayers();
    })
    .catch(function (error) {
      console.error("Error:", error);
    });

    function setCheckedPlayers() {
      var _games = _.where($scope.dbgames, {gamedateId: $scope.gamedateId});
      _.each($scope.players, function(p) {
        //if playerid found in game -> check him in the list
        if(_.findWhere(_games, {team1Player1Id: p.$id}) || _.findWhere(_games, {team1Player2Id: p.$id}) || _.findWhere(_games, {team2Player1Id: p.$id}) || _.findWhere(_games, {team2Player2Id: p.$id}))
          p.checked = true;
      });
    }
});