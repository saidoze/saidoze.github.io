leagueRankingApp.controller('AddGameController', function ($scope, $ionicSideMenuDelegate, $location, myConfig, $firebaseObject, $ionicLoading, $firebaseArray) {
  var gamedatesList = $firebaseArray(firebase.database().ref('gamedates'));
  var playersList = $firebaseArray(firebase.database().ref('players'));

  $scope.players = [];
  $scope.player;
  $scope.gamedates = [];
  $scope.gamedate = {};
  $scope.select = {};

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
  gamedatesList.$loaded()
    .then(function (data) {
      console.log("loaded gamedatesList:", data);
      $scope.gamedates = data;
      $scope.select.dateSelected = String($scope.gamedates[0].dateval);
    })
    .catch(function (error) {
      console.error("Error:", error);
    });

    $scope.$watch(
                    "select.dateSelected",
                    function handleFooChange( newValue, oldValue ) {
                        console.log( "gamedate:", newValue );
                    }
                , true);

});